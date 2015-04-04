using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class DelayWorkerCmd : Command
    {
        public static string COMMAND = "SLEEPP";


        int sec;
        string workerId;

        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');
            if (args.Length == 3)
            {
                workerId = args[1];


                System.Diagnostics.Debug.WriteLine(args[2]);
                
                sec = Convert.ToInt32(args[2]);

                return true;
            }

            return false;

        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        protected override void ExecuteAux()
        {
            DelayWorker(workerId, sec);
        }


        public void DelayWorker(string workerId, int seconds)
        {

            string commandResult = "[DELAY] " + workerId + " for " + seconds + " seconds.";
            Logger.LogInfo(commandResult);
        }
    }
}
