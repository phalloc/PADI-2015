using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                
                foreach(KeyValuePair<string, IWorker> entry in puppetMaster.GetRemoteWorkers()){
                    string id = entry.Key;
                    IWorker w = entry.Value;

                    try
                    {
                        IDictionary<string, string> result = w.Status();

                        Logger.LogInfo("--------- WORKER " + id + " -----------");
                        foreach (KeyValuePair<string, string> data in result)
                        {
                            string key = data.Key;
                            string value = data.Value;

                            Logger.LogInfo(key + " = " + value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogErr(ex.GetType().FullName);
                        Logger.LogErr(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.GetType().FullName);
                Logger.LogErr(ex.Message);
            }



        }

    }
}
