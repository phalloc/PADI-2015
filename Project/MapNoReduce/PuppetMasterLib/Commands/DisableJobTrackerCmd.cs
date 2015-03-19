using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    class DisableJobTrackerCmd : Command
    {
        string workerId;

        public DisableJobTrackerCmd(string line) : base(line) { }

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
            return DisableJobTracker(workerId);
        }


        public bool DisableJobTracker(string workerId)
        {
            System.Diagnostics.Debug.WriteLine("DisableJobTracker");
            return true;
        }
    }
}
