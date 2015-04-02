using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class Node : MarshalByRefObject, IWorker
    {

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
            myChannel = new TcpChannel(0);
            var channelData = (ChannelDataStore)myChannel.ChannelData;
            ChannelServices.RegisterChannel(myChannel, true);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Node),
                serviceName,
                WellKnownObjectMode.Singleton);

            Console.WriteLine("New node created with URL: tcp://localhost:" + new System.Uri(channelData.ChannelUris[0]).Port + "/" + serviceName);          
        }

        public long getId()
        {
            return id;
        }

        public void setId(long id)
        {
            this.id = id;
        }

        public void receiveWork(string clientUrl, int splits)
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

        //args: entryURL
        static void Main(string[] args)
        {
            Node node = new Node(args[0]);
            Console.ReadLine();
        }
    }
}
