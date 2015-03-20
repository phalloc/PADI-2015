using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    class DisableJobTrackerCmd : Command
    {
        string workerId;

        public DisableJobTrackerCmd(string line) : base(line) { }

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


        public bool DisableJobTracker(string workerId)
        {

            commandResult = "[DISABLING] " + workerId + " Job traceker";

            return true;
        }
    }
}
