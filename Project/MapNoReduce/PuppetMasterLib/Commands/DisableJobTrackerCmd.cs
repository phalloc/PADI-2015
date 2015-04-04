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
            return DisableJobTracker(workerId);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public bool DisableJobTracker(string workerId)
        {

            string commandResult = "[DISABLING] " + workerId + " Job traceker";
            Logger.LogInfo(commandResult);


            return true;
        }
    }
}
