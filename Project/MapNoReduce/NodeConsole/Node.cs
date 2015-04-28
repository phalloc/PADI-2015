using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;

namespace PADIMapNoReduce
{
    public partial class Node : MarshalByRefObject, IWorker
    {
        private int sleep_seconds = 0;

        public delegate void FetchWorkerAsyncDel(string clientURL, string jobTrackerURL, string mapperName, byte[] mapperCode, long fileSize, long totalSplits, long remainingSplits);
        private static string serviceName = "W";

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



        public Node(string id, string pmUrl, string serviceURL)
        {
            this.id = id;
            this.pmUrl = pmUrl;
            myURL = serviceURL;
            string[] splits = serviceURL.Split(':');
            splits = splits[splits.Length-1].Split('/');
            channelPort = int.Parse(splits[0]);
        }

        public void Register(string entryURL){
            //check if other nodes exist
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
                Logger.LogInfo("------------------------------");
                Logger.LogInfo("Successfully registered on the network.");
                Logger.LogInfo("nextURL: " + nextURL);
                Logger.LogInfo("nextNextURL: " + nextNextURL);
                Logger.LogInfo("backURL: " + backURL);
          
            }
            else Logger.LogErr("Did not provided entryURL");

        }

        private IList<KeyValuePair<string, string>> processStringWithMapper(string mapperName, byte[] mapperCode, string split)
        {
            Assembly assembly = Assembly.Load(mapperCode);
            //procurar o metodo
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass == true)
                {
                    if (type.FullName.EndsWith("." + mapperName))
                    {
                        // create an instance of the object
                        object ClassObj = Activator.CreateInstance(type);

                        // Dynamically Invoke the method
                        object[] args = new object[] { split };
                        object resultObject = type.InvokeMember("Map",
                          BindingFlags.Default | BindingFlags.InvokeMethod,
                               null,
                               ClassObj,
                               args);
                        IList<KeyValuePair<string, string>> result = (IList<KeyValuePair<string, string>>)resultObject;
                        Logger.LogInfo("Processed: ");
                        foreach (KeyValuePair<string, string> p in result)
                        {
                            Logger.LogInfo("key: " + p.Key + ", value: " + p.Value);
                        }
                        return result;
                    }
                }
            }
            throw (new System.Exception("could not invoke method"));
        }


        public void ReceiveWork(string clientURL, long fileSize, long splits, string mapperName, byte[] mapperCode)
        {
            try
            {
                /* wait until if I am unfrozen */
                WaitForUnfreeze();
                /* --------------------------- */


                Logger.LogInfo("Received: " + clientURL + " with " + splits + " splits fileSize =" + fileSize);
                
                currentJobTrackerUrl = this.id;
                serverRole = ServerRole.JOB_TRACKER;
                status = ExecutionState.WORKING;
                
                this.clientURL = clientURL;
                client = (IClient)Activator.GetObject(typeof(IClient), clientURL);
                
                IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                if (splits > fileSize)
                    splits = fileSize;
                long splitSize = fileSize / splits;


                FetchWorkerAsyncDel RemoteDel = new FetchWorkerAsyncDel(worker.FetchWorker);
                IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, myURL, mapperName, mapperCode, fileSize, splits, splits, null, null);
                
                return;
            }
            catch (RemotingException e)
            {
                Logger.LogErr("Remoting Exception: " + e.Message);
            }
        }

        public void FetchWorker(string clientURL, string jobTrackerURL, string mapperName, byte[] mapperCode, long fileSize, long totalSplits, long remainingSplits)
        {
            try
            {
                /* wait until if I am unfrozen */
                WaitForUnfreeze();
                /* --------------------------- */

                IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                FetchWorkerAsyncDel RemoteDel = new FetchWorkerAsyncDel(worker.FetchWorker);

                if (status == ExecutionState.WORKING)
                {
                    //Logger.LogInfo("Forwarded work from JobTracker: " + jobTrackerURL +" remainingSplits: " + remainingSplits);
                    IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, jobTrackerURL, mapperName, mapperCode, fileSize, totalSplits, remainingSplits, null, null);
                }
                else
                {
                    Logger.LogInfo("Received Work from JobTracker: " + jobTrackerURL + " remainingSplits: " + remainingSplits);
                    serverRole = ServerRole.WORKER;
                    status = ExecutionState.WORKING;
                    
                    this.clientURL = clientURL;
                    client = (IClient)Activator.GetObject(typeof(IClient), clientURL);
                    
                    if (remainingSplits > 1)
                    {
                        IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, jobTrackerURL, mapperName, mapperCode, fileSize, totalSplits, remainingSplits - 1, null, null);
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
                    string line = client.getWorkSplit(startSplit, endSplit);

                    if (sleep_seconds > 0)
                    {
                        int seconds = sleep_seconds * 1000;
                        Logger.LogInfo("[SLOWW BEFORE INVOKING MAPPER] Sleeping for " + sleep_seconds + " seconds");
                        Thread.Sleep(seconds);
                        Logger.LogInfo("[SLOWW BEFORE INFOKING MAPPER] Woke up!!");
                        sleep_seconds = 0;
                    }

                    Logger.LogInfo("client.finishedGetWorkSplit(" + startSplit + ", " + endSplit + ")");
                    IList<KeyValuePair<string, string>> processedWork = processStringWithMapper(mapperName, mapperCode, line);
                    Logger.LogInfo("client.finishedProcessingSplit(" + startSplit + ", " + endSplit + ")");


                    processedSplits++;
                    client.returnWorkSplit(processedWork, remainingSplits);
                    status = ExecutionState.WAITING;
                }
                return;
            }
            catch (RemotingException e)
            {
                Logger.LogErr("Remoting Exception: " + e.Message);
            }

        }

        public void BackUpdate(string nextNextURL)
        {
            this.nextNextURL = nextNextURL;
            Logger.LogInfo("------------------------------");
            Logger.LogInfo("Successfully updated network!");
            Logger.LogInfo("nextUrl: " + nextURL);
            Logger.LogInfo("nextNextUrl: " + nextNextURL);
            Logger.LogInfo("backURL: " + backURL);
        }

        public void FrontUpdate(string backURL)
        {
            this.backURL = backURL;
            Logger.LogInfo("------------------------------");
            Logger.LogInfo("Successfully updated network!");
            Logger.LogInfo("nextUrl: " + nextURL);
            Logger.LogInfo("nextNextUrl: " + nextNextURL);
            Logger.LogInfo("backURL: " + backURL);
        }


        public bool IsAlive()
        {
            return true;
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

            Logger.LogInfo("------------------------------");
            Logger.LogInfo("Successfully updated network!");
            Logger.LogInfo("nextUrl: " + nextURL);
            Logger.LogInfo("nextNextUrl: " + nextNextURL);
            Logger.LogInfo("backURL: " + backURL);
    
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
                TcpChannel myChannel = new TcpChannel(node.channelPort);
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
