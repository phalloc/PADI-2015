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

        protected override bool ExecuteAux()
        {
            return UnfreezeWorker(workerId);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public bool UnfreezeWorker(string workerId)
        {
            string commandResult = "[UNFREEZING] " + workerId;
            Logger.LogInfo(commandResult);
            return true;
        }
    }
}
