using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Remoting;


namespace PADIMapNoReduce.Commands
{
    public class UnfreezeJobTrackerCmd : Command
    {

        public static string COMMAND = "UNFREEZEC";

        string workerId = "";

        public UnfreezeJobTrackerCmd(PuppetMaster pm) : base(pm) { }
        
        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');
            if (args.Length == 2)
            {
                workerId = args[1];

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
            UnfreezeJobTracker(workerId.Trim());
        }

        public override Command CreateCopy()
        {
            return new UnfreezeJobTrackerCmd(puppetMaster);
        }

        public void UnfreezeJobTracker(string workerId)
        {
            Logger.LogInfo("[UNFREEZEC] " + workerId + " (JT)");
            try
            {
                IWorker w = NetworkManager.GetRemoteWorker(workerId);
                w.UnfreezeJobTracker();
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
