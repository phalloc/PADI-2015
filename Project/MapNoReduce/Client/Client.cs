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
    public class Client
    {
        private IWorker worker = null;
        private string clientURL = "tcp://localhost:8086/IClient";

        private FormRemoteGUI connectedForm = null;

        public Client(FormRemoteGUI form)
        {
            this.connectedForm = form;
        }

        public void submitJob(string filePath, string destPath, string entryUrl, int splits, IMapper mapper)
        {
            try
            {
                TcpChannel channel = new TcpChannel(8086);
                ChannelServices.RegisterChannel(channel, true);

                RemoteClient rmClient = new RemoteClient(filePath, connectedForm);
                RemotingServices.Marshal(rmClient, "IClient" , typeof(RemoteClient));

                worker = (IWorker)Activator.GetObject(typeof(IWorker), entryUrl);
                if (worker == null)
                {
                    //bruno mete na consola que não conseguimos localizar o worker pretendido
                }
                else worker.ReceiveWork(clientURL, splits);
            }
            //catched when node is not responding
            catch (RemotingTimeoutException timeException)
            {
                System.Diagnostics.Debug.WriteLine("time exception: " + timeException.Message);
            }
        }
    }
}
