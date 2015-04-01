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

        TcpChannel channel;

        public Node (string path){
            channel = new TcpChannel(8086);
            ChannelServices.RegisterChannel(channel,true);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Node),
				"IWorker",
				WellKnownObjectMode.Singleton);
        }

        private long id;

        public long getId()
        {
            return id;
        }

        public void setId(long id)
        {
            this.id = id;
        }

        public void receiveWork(string entryUrl, int splits){
            //do something
        }
    }
}
