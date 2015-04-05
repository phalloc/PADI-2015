using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Collections;
using System.Threading;

namespace PADIMapNoReduce
{
    public class PuppetMaster
    {
        private CommandsManager cm;

        private string serviceUrl;
        private static string SERVICE_NAME = "PM";
        private int port;

        private PM remoteObject;


        public PuppetMaster(){
            cm = new CommandsManager(this);
        }

        public void InstaciateWorkers(IDictionary<string, string> dic)
        {
            Logger.LogWarn("CLEARING NETWORK MANAGER");
            NetworkManager.Clear();

            Logger.LogWarn("LOADING...");
            string previousWorker = "";

            foreach (KeyValuePair<string, string> entry in dic)
            {
                string id = entry.Key;
                string url = entry.Value;
                Logger.LogWarn("Creating " + id);
                remoteObject.CreateWorker(id, url, previousWorker);
                previousWorker = url;
                Thread.Sleep(1000);
            }

            Logger.LogWarn("FINISHED CREATING WORKERS....");
            Logger.LogInfo("---- YOU CAN NOW START ----");
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
            RemotingServices.Marshal(remoteObject, SERVICE_NAME, typeof(IPuppetMaster));

            serviceUrl = "tcp://localhost:" + this.port + "/" + SERVICE_NAME;
            Logger.LogInfo("Started PuppetMaster service @ " + serviceUrl);
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
