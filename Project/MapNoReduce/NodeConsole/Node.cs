using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace PADIMapNoReduce
{
    public class Node : MarshalByRefObject, IWorker
    {

        public delegate bool RemoteAsyncDelegate(bool reply);
        private static string serviceName = "IWorker";

        private long id;
        private string puppetMasterURL;
        private string myURL;
        private static string clientURL;

        private string nextURL = null;
        private string nextNextURL = null;

        private IClient client = null;

        public Node(string port)
        {  
            myURL = "tcp://localhost:" + port + "/" + serviceName;
        }

        public void Register(string entryURL){
            //check if other nodes exist
            if (entryURL != null)
            {
                Logger.LogInfo("Attaching myself to existing network at: " + entryURL);
                IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), entryURL);
                if (worker != null)
                {
                    Logger.LogInfo("not null");
                }
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
            else Logger.LogInfo("I am the first node of my network...\n #foreveralone \n #whyUDoThis \n");

        }

        public long getId()
        {
            return id;
        }

        public void setId(long id)
        {
            this.id = id;
        }

        public void ReceiveWork(string clientUrl, int splits)
        {
            try
            {
                Logger.LogInfo("Received: " + clientUrl + " with " + splits + " splits");
                client = (IClient)Activator.GetObject(typeof(IClient), clientUrl);
                client.getWorkSplit();
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

        //args: entryURL
        static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                Node node = new Node(args[0]);
                TcpChannel myChannel = new TcpChannel(int.Parse(args[0]));
                ChannelServices.RegisterChannel(myChannel, true);
                RemotingServices.Marshal(node, serviceName, typeof(IWorker));
                Logger.LogInfo("Registered with url: " + node.myURL);
                Logger.LogInfo("Registering on network with entry point: " + args[1]);
                node.Register(args[1]);
              
            }else if(args.Length == 1){
                Node node = new Node(args[0]);
                TcpChannel myChannel = new TcpChannel(int.Parse(args[0]));
                ChannelServices.RegisterChannel(myChannel, true);
                RemotingServices.Marshal(node, serviceName, typeof(IWorker));
                Logger.LogInfo("Registered with url: " + node.myURL);
            }
            else
            {
                Logger.LogErr("Error: Please provide entry port as first argument");

            }
            Console.ReadLine();
        }
    }
}
