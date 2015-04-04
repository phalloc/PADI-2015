using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class StatusCmd : Command
    {
        public static string COMMAND = "STATUS";
        
        
        protected override bool ParseAux()
        {
            return true;
        }

        protected override bool ExecuteAux()
        {
            return RefreshStatus();
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public bool RefreshStatus()
        {

            string commandResult = "[REFRESHING]";
            Logger.LogInfo(commandResult);


            return true;
        }

    }
}
