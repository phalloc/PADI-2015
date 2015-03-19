using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    class FreezeWorkerCmd : Command
    {
        string workerId;

        public FreezeWorkerCmd(string line) : base(line) { }

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
            return FreezeWorker(workerId);
        }


        public bool FreezeWorker(string workerId)
        {
            commandResult = "[FREEZING] " + workerId;

            return true;
        }
    }
}
