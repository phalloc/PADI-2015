using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PADIMapNoReduce.Commands
{
    public class StatusCmd : Command
    {
        public static string COMMAND = "STATUS";

        public StatusCmd(PuppetMaster pm) : base(pm) { }


        protected override bool ParseAux()
        {
            return true;
        }

        protected override void ExecuteAux()
        {
            RefreshStatus();
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public void RefreshStatus()
        {

            Logger.LogInfo("[REFRESHING]");
            if (NetworkManager.GetActiveRemoteWorkers().Count == 0)
            {
                Logger.LogWarn("No workers registered");
                return;
            }

            try
            {
                List<string> listBecameInactiveWorkers = new List<string>();
                Logger.LogInfo("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                Logger.LogInfo("@@@@@@@@@@@ ACTIVE WORKERS @@@@@@@@@@@@@@");
                foreach (KeyValuePair<string, IWorker> entry in NetworkManager.GetActiveRemoteWorkers())
                {
                    string id = entry.Key;
                    IWorker w = entry.Value;

                    try
                    {
                        IDictionary<string, string> result = w.Status();
                        NodeRepresentation nodeRep = NodeRepresentation.ConvertFromNodeStatus(result);
                        NetworkManager.UpdateNodeInformation(nodeRep.id, nodeRep);

                        Logger.LogInfo("\r\n" + nodeRep.Print());
                    }
                    catch (SocketException ex)
                    {
                        Logger.LogErr("[" + id + " is down]: " + ex.Message);
                        listBecameInactiveWorkers.Add(id);
                    }
                }

                foreach (string id in listBecameInactiveWorkers){
                    NetworkManager.SetWorkerAsDown(id);
                }


                Logger.LogInfo("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

                Logger.LogInfo("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                Logger.LogInfo("@@@@@@@@@@@@ DOWN WORKERS @@@@@@@@@@@@@@@");
                foreach (string id in NetworkManager.GetDownWorkers())
                {
                    Logger.LogInfo(id);
                }
                Logger.LogInfo("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.Message);
            }
        }

    }
}
