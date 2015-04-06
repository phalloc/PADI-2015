using System;
using System.Threading;

namespace PADIMapNoReduce.Commands
{
    public class WaitCmd : Command
    {
        public static string COMMAND = "WAIT";
        

        int sec = 0;

        public WaitCmd(PuppetMaster pm) : base(pm) { }

        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                System.Diagnostics.Debug.WriteLine(args[1]);
                sec = Convert.ToInt32(args[1]);

                return true;
            }

            return false;

        }

        protected override void ExecuteAux()
        {
            Wait(sec);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public override Command CreateCopy()
        {
            return new WaitCmd(puppetMaster);
        }

        public void Wait(int sec)
        {

            Logger.LogInfo("[WAIT] Interrupting the commands of the script for " + sec + " seconds");

            Thread.Sleep(1000 * sec);
            
            Logger.LogInfo("[WAIT] Resuming...");
        }

    }
}
