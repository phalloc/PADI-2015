using System;
using System.Collections;
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

        private string nextURL;
        private string nextNextURL;

        private IClient client = null;

        TcpChannel myChannel;

        public Node(){
            //required for remoting
        }

        public Node(string entryURL)
        {
            BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
            IDictionary props = new Hashtable();
                props["port"] = 0;
                props["timeout"] = 1000; // in milliseconds

            myChannel = new TcpChannel(props, null, provider);
            var channelData = (ChannelDataStore)myChannel.ChannelData;

            ChannelServices.RegisterChannel(myChannel, true);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Node),
                serviceName,
                WellKnownObjectMode.Singleton);

            Console.WriteLine("New node created with URL: tcp://localhost:" + new System.Uri(channelData.ChannelUris[0]).Port + "/" + serviceName);
          
            //check if other nodes exist
            if (entryURL != null)
            {
                Console.WriteLine("Attaching myself to existing network at: " + entryURL);
                //IWorker worker = (IWorker)Activator.GetObject(typeof(IWorker), entryURL);
                //worker.AddWorker(myURL);
            }

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
                Console.WriteLine("Received: " + clientUrl + " with " + splits + " splits");
                client = (IClient)Activator.GetObject(typeof(IClient), clientUrl);
                client.getWorkSplit();
            }
            catch (RemotingException e)
            {
                Console.WriteLine("Remoting Exception: " + e.Message);
            }
        }

        public bool IsAlive()
        {
            return true;
        }

        public bool AddWorker(string newURL)
        {
            return true;
        }

        //args: entryURL
        static void Main(string[] args)
        {
            Node node = new Node(args[0]);
            Console.ReadLine();
        }
    }
}
