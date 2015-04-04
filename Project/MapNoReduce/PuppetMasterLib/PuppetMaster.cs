using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Collections;

namespace PADIMapNoReduce
{
    public class PuppetMaster
    {
        private CommandsManager cm = new CommandsManager();
        private IDictionary<string, string> knownWorkers = new Dictionary<string, string>();
        private string serviceUrl;
        private string serviceName = "PM";
        private int port;

       

        private TcpChannel serviceChannel;

        public void LoadConfigurationFile(IDictionary<string, string> dic)
        {
            this.knownWorkers = dic;
            Logger.LogInfo("LOADED CONFIGURATION FILE");
        }

        public void InitializeService()
        {
            
            int portAvailable = NetworkUtil.GetFirstAvailablePort(20001, 29999);
            if (portAvailable < 0)
            {
                throw new Exception("No port in range is available");
            }

            this.port = portAvailable;

            serviceChannel = new TcpChannel(portAvailable);

            ChannelServices.RegisterChannel(serviceChannel, true);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(PM),
                this.serviceName,
                WellKnownObjectMode.Singleton);
        
            Logger.LogInfo("Started PuppetMaster service @ tcp://localhost:" + this.port + "/" + this.serviceName);
        }

        public void LoadFile(string file)
        {
            cm.LoadFile(file);
        }

        public void ExecuteScript()
        {
            cm.ExecuteScript();
        }

        public void ExecuteCommand(string line)
        {
            cm.ExecuteCommand(line);
        }
        
    }
}
