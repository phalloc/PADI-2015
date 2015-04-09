using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace PADIMapNoReduce
{
    public class Node : MarshalByRefObject, IWorker
    {
        public delegate void RemoteAsyncDelegate(string clientURL, string jobTrackerURL, long start, long end, string mapperName, byte[] mapperCode, long splitSize, int remainingSplits);
        private static string serviceName = "W";

        private string id;
        private int channelPort;
        private string myURL;
        private string clientURL;

        private string nextURL = null;
        private string nextNextURL = null;
        private string currentJobTrackerUrl = "<JobTracker Id>";

        private string currentRole = "WORKER";
        private string status = "PENDING";

        private IClient client = null;

        public Node(string id, string serviceURL)
        {
            this.id = id;
            myURL = serviceURL;
            string[] splits = serviceURL.Split(':');
            splits = splits[splits.Length-1].Split('/');
            channelPort = int.Parse(splits[0]);
        }

        public void Register(string entryURL){
            //check if other nodes exist
            if (entryURL != null)
            {
                Logger.LogInfo("Attaching myself to existing network at: " + entryURL);
                IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), entryURL);
                List<String> urls = worker.AddWorker(myURL, true);
                nextURL = urls[1];
                nextNextURL = urls[2];
                if (urls[0].Equals("continue"))
                {
                    Logger.LogInfo("Updating existing node at: " + nextURL);
                    worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                    worker.AddWorker(myURL, false);
                }
                Logger.LogInfo("Successfully registered on the network.");
                Logger.LogInfo("nextURL: " + nextURL);
                Logger.LogInfo("nextNextURL: " + nextNextURL);

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
                    if (type.FullName.EndsWith("." + mapperName))//potencial problema de nomes
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
            //FIXME
            throw (new System.Exception("could not invoke method"));
        }


        public void ReceiveWork(string clientURL, long fileSize, int splits, string mapperName, byte[] mapperCode)
        {
            try
            {
                Logger.LogInfo("Received: " + clientURL + " with " + splits + " splits");
                currentRole = "JOBTRACKER";
                status = "WORKING";
                currentJobTrackerUrl = this.id;
                client = (IClient)Activator.GetObject(typeof(IClient), clientURL);
                this.clientURL = clientURL;

                IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                long splitSize = fileSize / splits;
                long start = 0;
                long end = splitSize;
                
                RemoteAsyncDelegate RemoteDel = new RemoteAsyncDelegate(worker.FetchWorker);
                IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, myURL, start, end, mapperName, mapperCode, splitSize, splits, null, null);
                return;
            }
            catch (RemotingException e)
            {
                Logger.LogErr("Remoting Exception: " + e.Message);
            }
        }

        public void FetchWorker(string clientURL, string jobTrackerURL, long start, long end, string mapperName, byte[] mapperCode, long splitSize, int remainingSplits)
        {
            try
            {
                IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), nextURL);
                RemoteAsyncDelegate RemoteDel = new RemoteAsyncDelegate(worker.FetchWorker);

                if (status.Equals("WORKING"))
                {
                    Logger.LogInfo("Forwarded work from JobTracker: " + jobTrackerURL +" remainingSplits: " + remainingSplits);
                    IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, jobTrackerURL, start, end, mapperName, mapperCode, splitSize, remainingSplits, null, null);
                }
                else
                {
                    Logger.LogInfo("Received Work from JobTracker: " + jobTrackerURL + " remainingSplits: " + remainingSplits);
                    client = (IClient)Activator.GetObject(typeof(IClient), clientURL);
                    this.clientURL = clientURL;
                    status = "WORKING";
                    currentRole = "WORKER";
                    if (remainingSplits != 0)
                    {
                        long nextStart = end + 1;
                        long nextEnd = end + splitSize;
                        IAsyncResult RemAr = RemoteDel.BeginInvoke(clientURL, jobTrackerURL, nextStart, nextEnd, mapperName, mapperCode, splitSize, remainingSplits - 1, null, null);
                    }
                    string line = client.getWorkSplit(start, end);
                    IList<KeyValuePair<string, string>> processedWork = processStringWithMapper(mapperName, mapperCode, line);
                    client.returnWorkSplit(processedWork, remainingSplits);
                }
                return;
            }
            catch (RemotingException e)
            {
                Logger.LogErr("Remoting Exception: " + e.Message);
            }

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
                urls = new List<string> {"done", myURL, myURL};
            }

            //two nodes in the network and firstcontact from new node
            else if (nextURL.Equals(nextNextURL) && firstContact)
            {
                nextNextURL = nextURL;
                nextURL = newURL;
                urls = new List<string> {"continue", nextNextURL, myURL };
            }

            //two nodes in the network but secondcontact from new node
            else if (nextURL.Equals(nextNextURL) && !firstContact)
            {
                nextNextURL = newURL;
            }

            //three or more nodes in the network
            else if(!nextURL.Equals(nextNextURL))
            {
                string tmpNextURL = nextURL;
                string tmpNextNextURL = nextNextURL;
                nextNextURL = nextURL;
                nextURL = newURL;
                urls =  new List<string> {"done", tmpNextURL, tmpNextNextURL };
            }

            Logger.LogInfo("Successfully updated network!");
            Logger.LogInfo("nextUrl: " + nextURL);
            Logger.LogInfo("nextNextUrl: " + nextNextURL);
            return urls;
        }


        public void FreezeWorker()
        {
            Logger.LogInfo("[FREEZEW] (W)");
        }

        public void UnfreezeWorker()
        {
            Logger.LogInfo("[UNFREEZEW] (W)");
        }

        public void FreezeJobTracker()
        {
            Logger.LogInfo("[FREEZEC] (JT)");
        }

        public void UnfreezeJobTracker()
        {
            Logger.LogInfo("[UNFREEZEC] (JT)");
        }

        public void Slow(int seconds)
        {
            Logger.LogInfo("[SLOWW] " + seconds);
        }


        public IDictionary<string, string> Status()
        {
            Logger.LogInfo("[STATUS]");

            IDictionary<string, string> result = new Dictionary<string, string>();

            result.Add(NodeRepresentation.ID, this.id);
            result.Add(NodeRepresentation.SERVICE_URL, this.myURL);
            result.Add(NodeRepresentation.NEXT_URL, this.nextURL);
            result.Add(NodeRepresentation.NEXT_NEXT_URL, this.nextNextURL);
            result.Add(NodeRepresentation.CURRENT_JT, this.currentJobTrackerUrl);


            result.Add("currentRole", this.currentRole);
            result.Add("status", this.status);

            return result;
        }


        //args: id, serviceURL, entryURL
        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                Logger.LogErr("Error: Invalid arguments. Usage: [required: id, serviceURL], [optional: entryURL]");
            }

            try
            {
                Node node = new Node(args[0], args[1]);
                TcpChannel myChannel = new TcpChannel(node.channelPort);
                ChannelServices.RegisterChannel(myChannel, true);
                RemotingServices.Marshal(node, serviceName, typeof(IWorker));
                Logger.LogInfo("Registered with url: " + node.myURL);
                if (args.Length >= 3)
                {
                    Logger.LogInfo("Registering on network with entry point: " + args[2]);
                    node.Register(args[2]);
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
