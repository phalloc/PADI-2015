using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    class UnfreezeWorkerCmd : Command
    {
        string workerId;

        public UnfreezeWorkerCmd(string line) : base(line) { }

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


        public bool UnfreezeWorker(string workerId)
        {
            commandResult = "[UNFREEZING] " + workerId;

            return true;
        }
    }
}
