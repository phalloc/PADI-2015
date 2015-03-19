using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    class StatusCmd : Command
    {

        public StatusCmd(string line) : base(line) { }

        public override bool Parse(string line)
        {
            return true;
        }

        public override bool Execute()
        {
            if (!Parse(line)) { return false; }
            return RefreshStatus();
        }


        public bool RefreshStatus()
        {
            System.Diagnostics.Debug.WriteLine("RefreshStatus");
            return true;
        }

    }
}
