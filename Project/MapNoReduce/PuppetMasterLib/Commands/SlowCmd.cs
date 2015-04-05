using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PADIMapNoReduce.Commands
{
    public class SleepCmd : Command
    {
        public static string COMMAND = "SLEEPP";


        int sec;
        string workerId;

        public SleepCmd(PuppetMaster pm) : base(pm) { }

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
            Sleep(workerId, sec);
        }


        public void Sleep(string workerId, int seconds)
        {

            try
            {
                IWorker w = NetworkManager.GetRemoteWorker(workerId);
                Logger.LogInfo("[SLOW] " + workerId + " for " + seconds + " seconds.");
                w.Slow(seconds);
            }
            catch (SocketException ex)
            {
                Logger.LogErr("[" + workerId + " is down]: " + ex.Message);
                NetworkManager.SetWorkerAsDown(workerId);
            }
        }
    }
}
