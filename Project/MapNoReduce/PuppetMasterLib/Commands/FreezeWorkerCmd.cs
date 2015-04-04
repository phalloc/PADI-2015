using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class FreezeWorkerCmd : Command
    {

        public static string COMMAND = "FREEZEW";
        
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

        protected override void ExecuteAux()
        {
             FreezeWorker(workerId);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }


        public void FreezeWorker(string workerId)
        {
            string commandResult = "[FREEZING] " + workerId;
            Logger.LogInfo(commandResult);

        }
    }
}
