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

            string commandResult = "[REFRESHING]";
            Logger.LogInfo(commandResult);


            try
            {
                Logger.LogInfo("[REFRESHING]");
                //IWorker w = puppetMaster.GetRemoteWorker(workerId);
                //w.FreezeWorker();
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.Message);
            }



        }

    }
}
