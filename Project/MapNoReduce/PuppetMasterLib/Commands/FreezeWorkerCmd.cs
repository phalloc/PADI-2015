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

        public override bool Parse(string line)
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                workerId = args[1];

                return true;
            }

            return false;

        }

        public override bool Execute()
        {
            if (!Parse(line)) { return false; }
            return FreezeWorker(workerId);
        }


        public bool FreezeWorker(string workerId)
        {
            System.Diagnostics.Debug.WriteLine("FreezeWorker");
            return true;
        }
    }
}
