using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Remoting;


namespace PADIMapNoReduce.Commands
{
    public class SleepCmd : Command
    {
        public static string COMMAND = "SLOWW";


        int sec;
        string workerId = "";

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
            Sleep(workerId.Trim(), sec);
        }

        public override Command CreateCopy()
        {
            return new SleepCmd(puppetMaster);
        }

        public void Sleep(string workerId, int seconds)
        {

            Logger.LogInfo("[SLOWW] " + workerId + " " + seconds);
            try
            {
                IWorker w = NetworkManager.GetRemoteWorker(workerId);
                w.Slow(seconds);
            }
            catch (Exception ex)
            {
                if (ex is RemotingException || ex is SocketException)
                {
                    Logger.LogErr("[" + workerId + " is down]: " + ex.Message);
                    NetworkManager.SetWorkerAsDown(workerId);
                    Logger.RefreshNetwork();
                }
            }
        }
    }
}
