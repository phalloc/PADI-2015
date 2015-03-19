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

        protected override bool ParseAux()
        {
            return true;
        }

        protected override bool ExecuteAux()
        {
            return RefreshStatus();
        }


        public bool RefreshStatus()
        {
            System.Diagnostics.Debug.WriteLine("RefreshStatus");

            commandResult = "[REFRESHING]";

            return true;
        }

    }
}
