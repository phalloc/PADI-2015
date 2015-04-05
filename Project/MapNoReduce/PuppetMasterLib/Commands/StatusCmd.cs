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


            try
            {
                List<string> listBecameInactiveWorkers = new List<string>();
                Logger.LogInfo("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                Logger.LogInfo("@@@@@@@@@@@ ACTIVE WORKERS @@@@@@@@@@@@@@");
                foreach(KeyValuePair<string, IWorker> entry in puppetMaster.GetActiveRemoteWorkers()){
                    string id = entry.Key;
                    IWorker w = entry.Value;

                    try
                    {
                        IDictionary<string, string> result = w.Status();

                        Logger.LogInfo("----------- " + id + " -----------");
                        foreach (KeyValuePair<string, string> data in result)
                        {
                            string key = data.Key;
                            string value = data.Value;

                            Logger.LogInfo(key + " = " + value);
                        }
                    }
                    catch (SocketException ex)
                    {
                        Logger.LogErr("[" + id + " is down]: " + ex.Message);
                        listBecameInactiveWorkers.Add(id);
                    }
                }

                foreach (string id in listBecameInactiveWorkers){
                    puppetMaster.SetWorkerAsDown(id);
                }


                Logger.LogInfo("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

                Logger.LogInfo("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                Logger.LogInfo("@@@@@@@@@@@@ DOWN WORKERS @@@@@@@@@@@@@@@");
                foreach (string id in puppetMaster.GetDownWorkers())
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
