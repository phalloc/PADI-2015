using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    class DelayWorkerCmd : Command
    {

        public DelayWorkerCmd(string line) : base(line) { }

        int sec;
        string workerId;

        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');
            if (args.Length == 3)
            {
                workerId = args[1];
                sec = Convert.ToInt32(args[2]);

                return true;
            }

            return false;

        }

        protected override bool ExecuteAux()
        {
            return DelayWorker(workerId, sec);
        }


        public bool DelayWorker(string workerId, int seconds)
        {
            System.Diagnostics.Debug.WriteLine("RefreshStatus");

            commandResult = "I got this result DelayWorker";

            return true;
        }
    }
}
