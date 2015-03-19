using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    class SleepCmd : Command
    {

        int sec;

        public SleepCmd(string line) : base(line) { }

        public override bool Parse(string line)
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                sec = Convert.ToInt32(args[1]);

                return true;
            }

            return false;

        }

        public override bool Execute()
        {
            if (!Parse(line)) { return false; }
            return Sleep(sec);
        }


        public bool Sleep(int sec){
            System.Diagnostics.Debug.WriteLine("RefreshStatus");
            return true;
        }

    }
}
