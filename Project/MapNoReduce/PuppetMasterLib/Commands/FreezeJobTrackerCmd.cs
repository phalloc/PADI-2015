using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class FreezeJobTrackerCmd : Command
    {

        public static string COMMAND = "FREEZEC";
        

        string workerId;


        public FreezeJobTrackerCmd(PuppetMaster pm) : base(pm) { }

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

        protected override void ExecuteAux()
        {
            FreezeJobTracker(workerId);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public void FreezeJobTracker(string workerId)
        {         
            try
            {
                IWorker w = puppetMaster.GetRemoteWorker(workerId);
                Logger.LogInfo("[FREEZE JT] " + workerId + " Job tracker");
                w.FreezeJobTracker();
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.GetType().FullName);
                Logger.LogErr(ex.Message);
            }

        }
    }
}
