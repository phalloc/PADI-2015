using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace PADIMapNoReduce
{
    public partial class Node : MarshalByRefObject, IWorker
    {
        private int sleep_seconds = 0;

        public delegate bool FetchWorkerAsyncDel(string clientURL, string jobTrackerURL, string mapperName, byte[] mapperCode, long fileSize, long totalSplits, long remainingSplits, string backURL, bool propagateRemainingSplits);
        public delegate void RecoverDeadNodeAsyncDel(string entryURL);

        private static string serviceName = "W";

        private const int TIMEOUT = 5000;
        bool didRegisted = false;
        private string id;
        private int channelPort;
        private string myURL;
        private string clientURL;
        private string pmUrl;

        private string nextURL = null;
        private string nextNextURL = null;
        private string backURL = null;
        private string currentJobTrackerUrl = "<JobTracker Id>";
        private IClient client = null;
        private TcpChannel myChannel = null;

        private long fileSize;
        private long totalSplits;
        private long remainingSplits;
        private ExecutionState backupStatus;

        IWorker currentJobTracker;

        private Object CurrentJobTrackerLock = new Object();

        public Node(string id, string pmUrl, string serviceURL)
        {
            this.id = id;
            this.pmUrl = pmUrl;
            myURL = serviceURL;
            string[] splits = serviceURL.Split(':');
            splits = splits[splits.Length-1].Split('/');
            channelPort = int.Parse(splits[0]);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Register(string entryURL){
            //check if other nodes exist
            Logger.LogInfo("starting network registration on node: " + entryURL);
            if (entryURL != null)
            {
                IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), entryURL);
                List<String> urls = worker.AddWorker(myURL, true);
                nextURL = urls[1];
                nextNextURL = urls[2];
                backURL = urls[3];
                if (urls[0].Equals("smallUpdate"))
                {
                    Logger.LogInfo("Updating existing node at: " + nextURL);
                    worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                    worker.AddWorker(myURL, false);
                }
                if (urls[0].Equals("bigUpdate"))
                {
                    string wayBackURL = urls[4];
                    Logger.LogInfo("Updating back node at: " + wayBackURL);
                    worker = (IWorker)Activator.GetObject(typeof(IWorker), wayBackURL);
                    worker.BackUpdate(myURL);
                    Logger.LogInfo("Updating next node at: " + nextURL);
                    worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                    worker.FrontUpdate(myURL);
                }
                PrintUpdateNetwork();
          
            }
            else Logger.LogErr("Did not provided entryURL");

        }

        

        public void UpdateJobTracker(string newJobTracker)
        {
            Logger.LogWarn("UPDATING JOBTRACKER");
            this.currentJobTrackerUrl = newJobTracker;
            IWorker currentJobTracker =  (IWorker)Activator.GetObject(typeof(IWorker), newJobTracker);
        }


        public String DownNodeFrontNotify(string backURL)
        {
            this.backURL = backURL;
            PrintUpdateNetwork();
            return nextURL;
        }

        public void DownNodeBackNotify(string nextNextURL)
        {
            this.nextNextURL = nextNextURL;
            PrintUpdateNetwork();
        }


        public void BackUpdate(string nextNextURL)
        {
            this.nextNextURL = nextNextURL;
            PrintUpdateNetwork();
        }

        void PrintUpdateNetwork()
        {
            Logger.LogInfo("Successfully updated network!");
            //Logger.LogInfo("nextUrl: " + nextURL);
            //Logger.LogInfo("nextNextUrl: " + nextNextURL);
            //Logger.LogInfo("backURL: " + backURL);
        }

        public void FrontUpdate(string backURL)
        {
            this.backURL = backURL;
            PrintUpdateNetwork();
        }

        public void SetTcpChannel(TcpChannel newChannel)
        {
            this.myChannel = newChannel;
        }


        public bool IsAlive()
        {
            return true;
        }

        public void nodeDown()
        {
           IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), nextNextURL);
           String tmpURL = worker.DownNodeFrontNotify(myURL);
           nextURL = nextNextURL;
           nextNextURL = tmpURL;
           worker = (IWorker)Activator.GetObject(typeof(IWorker), backURL);
           worker.DownNodeBackNotify(nextURL);
        }

        public void UpdateCurrentJobTracker(string jobtrackerUrl)
        {
            currentJobTrackerUrl = jobtrackerUrl;
            currentJobTracker = (IWorker)Activator.GetObject(typeof(IWorker), jobtrackerUrl);
        }

        public void liveCheck(object ar)
        {
            IAsyncResult iar = (IAsyncResult)ar;
            Thread.Sleep(TIMEOUT);
            Console.Write("LIVE CHECK");
            
            /*
            if (!iar.IsCompleted)
            {
               Logger.LogErr(" ---- NODE DOWN ALERT ---- ");
               nodeDown();
               PrintUpdateNetwork();
               IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
               FetchWorkerAsyncDel RemoteDel = new FetchWorkerAsyncDel(worker.FetchWorker);
                
               if (backupStatus == ExecutionState.WORKING)
               {
                   IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, currentJobTrackerUrl, mapperName, mapperCode, fileSize, totalSplits, remainingSplits, myURL, true, null, null);
               }
               else
               {
                   if (remainingSplits > 1)
                   {
                       IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, currentJobTrackerUrl, mapperName, mapperCode, fileSize, totalSplits, remainingSplits - 1, myURL, true, null, null);

                   }
               }
            }*/
        }

        private void backupWorkData(ExecutionState status, string clientURL, string jobTrackerURL, string mapperName, byte[] mapperCode, long fileSize, long totalSplits, long remainingSplits){
            this.mapperName = mapperName;
            this.mapperCode = mapperCode;
            this.fileSize = fileSize;
            this.totalSplits = totalSplits;
            this.remainingSplits = remainingSplits;
        }

        public bool FetchWorker(string clientURL, string jobTrackerURL, string mapperName, byte[] mapperCode, long fileSize, long totalSplits, long remainingSplits, string backURL, bool propagateRemainingSplits)
        {
            try
            {
                /* wait until if I am unfrozen */
                WaitForUnfreeze();
                /* --------------------------- */

                //dead node revived!!! nooooooooo !
                if (!backURL.Equals(this.backURL))
                {
                    Logger.LogInfo("Receiving work from the Dead. Invoking Node Registration");
                    //do stuff to recover that node back to the network
                    IWorker revivedWorker = (IWorker)Activator.GetObject(typeof(IWorker), backURL);
                    RecoverDeadNodeAsyncDel deadDel = new RecoverDeadNodeAsyncDel(revivedWorker.Register);
                    IAsyncResult deadResponse = deadDel.BeginInvoke(currentJobTrackerUrl, null, null);
                    return true;
                }

                currentJobTrackerUrl = jobTrackerURL;
                if (currentJobTracker == null)
                {
                    currentJobTracker = (IWorker)Activator.GetObject(typeof(IWorker), jobTrackerURL);
                }
                
                backupWorkData(status, clientURL, jobTrackerURL, mapperName, mapperCode, fileSize, totalSplits, remainingSplits);

                IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                FetchWorkerAsyncDel RemoteDel = new FetchWorkerAsyncDel(worker.FetchWorker);
                Thread liveCheck = new Thread(this.liveCheck);

                if (serverRole == ServerRole.JOB_TRACKER)
                {
                    IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, jobTrackerURL, mapperName, mapperCode, fileSize, totalSplits, remainingSplits, myURL, true, null, null);
                    liveCheck.Start(RemAr);
                }
                else
                {
                    //registering as worker
                    Logger.LogInfo("Registering in the jobtracker at " + jobTrackerURL);
                    if (!didRegisted) { 
                        currentJobTracker.RegisterWorker(id, myURL);
                        didRegisted = true;
                    }

                    if (status == ExecutionState.WORKING && propagateRemainingSplits)
                    {
                        //Logger.LogInfo("Forwarded work from JobTracker: " + jobTrackerURL +" remainingSplits: " + remainingSplits);
                        IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, jobTrackerURL, mapperName, mapperCode, fileSize, totalSplits, remainingSplits, myURL, true, null, null);
                        liveCheck.Start(RemAr);
                    }
                    else
                    {
                        Logger.LogInfo("Received Work from JobTracker: " + jobTrackerURL + " remainingSplits: " + remainingSplits);

                        //registering as worker
                        Logger.LogInfo("Warning Jobtracker that I am going to start");
                        currentJobTracker.LogStartedSplit(id, fileSize, totalSplits, remainingSplits);

                        serverRole = ServerRole.WORKER;
                        status = ExecutionState.WORKING;

                        this.clientURL = clientURL;
                        client = (IClient)Activator.GetObject(typeof(IClient), clientURL);

                        if (remainingSplits > 1 && propagateRemainingSplits)
                        {
                            IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, jobTrackerURL, mapperName, mapperCode, fileSize, totalSplits, remainingSplits - 1, myURL, true, null, null);
                            liveCheck.Start(RemAr);
                        }

                        long startSplit = (remainingSplits - 1) * (fileSize / totalSplits);

                        long endSplit;
                        if (remainingSplits == totalSplits)
                        {
                            endSplit = fileSize;
                        }
                        else
                        {
                            endSplit = (remainingSplits - 1 + 1) * (fileSize / totalSplits) - 1;//Making sure it reaches 0
                        }

                        Logger.LogInfo("client.getWorkSplit(" + startSplit + ", " + endSplit + ")");
                        //string mySplit = client.getWorkSplit(startSplit, endSplit);




                        //byte[] mySplit = client.getWorkSplit(startSplit, endSplit);
                        //Logger.LogInfo("client.finishedGetWorkSplit(" + startSplit + ", " + endSplit + ")");

                        SleepIfAskedTo();



                        if (this.mapper == null || this.mapperType == null)
                        {
                            GetMapperObject(mapperName, mapperCode);
                        }

                        fetchItProcessItSendIt(startSplit, endSplit, remainingSplits);

                        currentJobTracker.LogFinishedSplit(id, totalSplits, remainingSplits);
                        Logger.LogInfo("client.finishedProcessingSplit(" + startSplit + ", " + endSplit + ")");


                        processedSplits++;
                        if (remainingSplits == 1)
                        {
                            IWorker wasIDeadWorker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                            wasIDeadWorker.deadCheck(myURL);
                        }

                        status = ExecutionState.WAITING;
                    }
                }
                return true;
            }
            catch (RemotingException e)
            {
                Logger.LogErr("Remoting Exception: " + e.Message);
            }
            return true;

        }


        public void deadCheck(string backURL){
            if(!this.backURL.Equals(backURL)){
                Logger.LogInfo("Receiving work from the Dead. Invoking Node Registration");
                //do stuff to recover that node back to the network
                IWorker revivedWorker = (IWorker)Activator.GetObject(typeof(IWorker), backURL);
                RecoverDeadNodeAsyncDel deadDel = new RecoverDeadNodeAsyncDel(revivedWorker.Register);
                IAsyncResult deadResponse = deadDel.BeginInvoke(currentJobTrackerUrl, null, null);
            }
        }

        public List<string> AddWorker(string newURL, bool firstContact)
        {
            List<string> urls = null;
            //only one node in the network
            if (nextURL == null)
            {
                nextURL = newURL;
                nextNextURL = newURL;
                backURL = newURL;
                urls = new List<string> {"done", myURL, myURL, myURL};
            }

            //two nodes in the network and firstcontact from new node
            else if (nextURL.Equals(nextNextURL) && firstContact)
            {
                nextNextURL = nextURL;
                nextURL = newURL;
                urls = new List<string> {"smallUpdate", nextNextURL, myURL, myURL};
            }

            //two nodes in the network but secondcontact from new node
            else if (nextURL.Equals(nextNextURL) && !firstContact)
            {
                nextNextURL = newURL;
                backURL = newURL;
            }

            //three or more nodes in the network
            else if(!nextURL.Equals(nextNextURL))
            {
                string tmpNextURL = nextURL;
                string tmpNextNextURL = nextNextURL;
                nextNextURL = nextURL;
                nextURL = newURL;
                urls =  new List<string> {"bigUpdate", tmpNextURL, tmpNextNextURL, myURL, backURL };
            }

            PrintUpdateNetwork();

    
            return urls;
        }




        //args: id, serviceURL, entryURL
        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                Logger.LogErr("Error: Invalid arguments. Usage: [required: id, pmUrl, serviceURL], [optional: entryURL]");
            }

            try
            {
                Node node = new Node(args[0], args[1], args[2]);
                BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
                IDictionary props = new Hashtable();
                props["port"] = node.channelPort;
                props["timeout"] = 5000; // in milliseconds
                TcpChannel myChannel = new TcpChannel(props, null, provider);
                node.SetTcpChannel(myChannel);
                ChannelServices.RegisterChannel(myChannel, true);
                RemotingServices.Marshal(node, serviceName, typeof(IWorker));
                Logger.LogInfo("Registered node " + args[0] + " with url: " + node.myURL);
                if (args.Length >= 4)
                {
                    Logger.LogInfo("Registering on network with entry point: " + args[3]);
                    node.Register(args[3]);
                }
            }
            catch (RemotingException re)
            {
                Logger.LogErr("Remoting Exception: " + re.StackTrace);
            }
            Console.ReadLine();
        }
    }
}
