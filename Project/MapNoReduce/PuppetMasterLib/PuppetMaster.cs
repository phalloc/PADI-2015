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
        private static IDictionary<string, string> knownWorkers = new Dictionary<string, string>();
        private static IDictionary<string, IWorker> knownWorkersObjs = new Dictionary<string, IWorker>();

        private string serviceUrl;
        private static string SERVICE_NAME = "PM";
        private int port;

        private static string workerExeLocation = null;
        private static string clientExeLocation = null;

        private PM remoteObject;


        public PuppetMaster(){
            cm = new CommandsManager(this);
        }

        public static string GetWorkerExeLocation()
        {
            return workerExeLocation;
        }

        public static string getClientExeLocation()
        {
            return clientExeLocation;
        }

        public void SetWorkerExeLocation(string exeLocation)
        {
            PuppetMaster.workerExeLocation = exeLocation;
        }

        public void SetClientExeLocation(string exeLocation)
        {
            PuppetMaster.clientExeLocation = exeLocation;
        }

        public void InstaciateWorkers(IDictionary<string, string> dic)
        {
            Logger.LogWarn("CREATING SET OF WORKERS....");
            string previousWorker = "";

            foreach (KeyValuePair<string, string> entry in dic)
            {
                string id = entry.Key;
                string url = entry.Value;
                remoteObject.CreateWorker(id, url, previousWorker);
                previousWorker = url;
            }

            Logger.LogWarn("FINISHED CREATING WORKERS....");
        }

       public IWorker GetRemoteWorker(string id)
        {
            if (!knownWorkersObjs.ContainsKey(id))
                throw new Exception("Worker id not configured");

            return knownWorkersObjs[id];
        }

       public IDictionary<string, IWorker> GetRemoteWorkers()
       {
           return knownWorkersObjs;
       }

        public static void RegisterNewWorker(string id, string url)
        {
            Logger.LogInfo("Register worker: " + id + " : " + url);

            if(PuppetMaster.knownWorkers.ContainsKey(id))
                PuppetMaster.knownWorkers.Remove(id);

            PuppetMaster.knownWorkers.Add(id, url);


            if (PuppetMaster.knownWorkersObjs.ContainsKey(id))
                PuppetMaster.knownWorkersObjs.Remove(id);

            IWorker w = (IWorker)Activator.GetObject(typeof(IWorker), url);
            PuppetMaster.knownWorkersObjs.Add(id, w);
        }

        public static void UnregisterNewWorker(string id)
        {
            Logger.LogInfo("Unregistered worker: " + id);
            PuppetMaster.knownWorkers.Remove(id);
            PuppetMaster.knownWorkersObjs.Remove(id);
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
