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
        private CommandsManager cm;
        private IDictionary<string, string> knownWorkers = new Dictionary<string, string>();
        private string serviceUrl;
        private string serviceName = "PM";
        private int port;

        private string workerExeLocation = null;
        private string clientExeLocation = null;

        private TcpChannel serviceChannel;
        private PM remoteObject;


        public PuppetMaster(){
            cm = new CommandsManager(this);
        }

        public void SetWorkerExeLocation(string exeLocation)
        {
            Logger.LogInfo("setting up worker exe Location");
            this.workerExeLocation = exeLocation;
            remoteObject.SetworkerExecutableDirectoy(exeLocation);
        }

        public void SetClientExeLocation(string exeLocation)
        {
            Logger.LogInfo("setting up client exe location");
            this.clientExeLocation = exeLocation;
        }

        public void LoadConfigurationFile(IDictionary<string, string> dic)
        {
            this.knownWorkers = dic;
            Logger.LogInfo("Finished loading configuration file");
        }

        public void InitializeService()
        {
            
            int portAvailable = NetworkUtil.GetFirstAvailablePort(20001, 29999);
            if (portAvailable < 0)
            {
                throw new Exception("No port in range is available");
            }

            this.port = portAvailable;

            remoteObject = new PM();
            TcpChannel myChannel = new TcpChannel(this.port);
            ChannelServices.RegisterChannel(myChannel, true);
            RemotingServices.Marshal(remoteObject, serviceName, typeof(PM));

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
