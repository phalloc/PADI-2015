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
        private static string entryURL;

        private string nextURL;
        private string nextNextURL;

        TcpChannel channel;

        public Node(){
            //required for remoting
        }

        public Node(string entryURL)
        {
            channel = new TcpChannel(0);
            var channelData = (ChannelDataStore)channel.ChannelData;
            ChannelServices.RegisterChannel(channel, true);
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

        public void receiveWork(string entryUrl, int splits)
        {
            Console.WriteLine("Received: " + entryUrl + " with " + splits + " splits");
        }

        //args: entryURL
        static void Main(string[] args)
        {
            Node node = new Node(args[0]);
            Console.ReadLine();
        }
    }
}
