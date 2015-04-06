using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PADIMapNoReduce.Commands
{
    public class FreezeJobTrackerCmd : Command
    {

        public static string COMMAND = "FREEZEC";
        string workerId = "";


        public FreezeJobTrackerCmd(PuppetMaster pm) : base(pm) { }

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

        public override Command CreateCopy()
        {
            return new FreezeJobTrackerCmd(puppetMaster);
        }

        protected override void ExecuteAux()
        {
            FreezeJobTracker(workerId.Trim());
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public void FreezeJobTracker(string workerId)
        {         
            try
            {
                IWorker w = NetworkManager.GetRemoteWorker(workerId);
                Logger.LogInfo("[FREEZE JT] " + workerId + " Job tracker");
                w.FreezeJobTracker();
            }
            catch (SocketException ex)
            {
                Logger.LogErr("[" + workerId + " is down]: " + ex.Message);
                NetworkManager.SetWorkerAsDown(workerId);
            }

        }
    }
}
