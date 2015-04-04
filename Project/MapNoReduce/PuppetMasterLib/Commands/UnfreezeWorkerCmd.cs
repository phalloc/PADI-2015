using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class UnfreezeWorkerCmd : Command
    {

        public static string COMMAND = "UNFREEZEW";
        

        string workerId;

        public UnfreezeWorkerCmd(PuppetMaster pm) : base(pm) { }


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
            UnfreezeWorker(workerId);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public void UnfreezeWorker(string workerId)
        {
            string commandResult = "[UNFREEZING] " + workerId;
            Logger.LogInfo(commandResult);
        }
    }
}
