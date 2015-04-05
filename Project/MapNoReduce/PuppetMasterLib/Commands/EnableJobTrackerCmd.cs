using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class EnableJobTrackerCmd : Command
    {

        public static string COMMAND = "UNFREEZEC";
        
        string workerId;

        public EnableJobTrackerCmd(PuppetMaster pm) : base(pm) { }
        
        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                workerId = args[1];

                return true;
            }

            return false;

        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        protected override void ExecuteAux()
        {
             EnableJobTracker(workerId);
        }


        public void EnableJobTracker(string workerId)
        {
            try
            {
                Logger.LogInfo("[ENABLING] " + workerId + " Job tracker");
                IWorker w = puppetMaster.GetRemoteWorker(workerId);
                w.UnfreezeJobTracker();
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.Message);
            }
        }
    }
}
