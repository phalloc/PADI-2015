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

        public void Wait(int sec)
        {

            string commandResult = "[WAIT] " + sec + " seconds";
            Logger.LogInfo(commandResult);
            
            Thread.Sleep(1000 * sec);
            Logger.LogInfo("[WAIT] FINISHED");
        }

    }
}
