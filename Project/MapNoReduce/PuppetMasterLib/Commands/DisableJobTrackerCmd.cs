using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class DisableJobTrackerCmd : Command
    {

        public static string COMMAND = "FREEZEC";
        

        string workerId;


        public DisableJobTrackerCmd(PuppetMaster pm) : base(pm) { }

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
            DisableJobTracker(workerId);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public void DisableJobTracker(string workerId)
        {         
            try
            {
                Logger.LogInfo("[DISABLING] " + workerId + " Job tracker");
                IWorker w = puppetMaster.GetRemoteWorker(workerId);
                w.FreezeJobTracker();
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.Message);
            }

        }
    }
}
