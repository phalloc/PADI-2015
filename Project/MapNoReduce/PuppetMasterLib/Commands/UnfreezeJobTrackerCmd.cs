using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PADIMapNoReduce.Commands
{
    public class UnfreezeJobTrackerCmd : Command
    {

        public static string COMMAND = "UNFREEZEC";
        
        string workerId;

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
             UnfreezeJobTracker(workerId);
        }


        public void UnfreezeJobTracker(string workerId)
        {
            try
            {
                IWorker w = NetworkManager.GetRemoteWorker(workerId);
                Logger.LogInfo("[UNFREEZE JT] " + workerId + " Job tracker");
                w.UnfreezeJobTracker();
            }
            catch (SocketException ex)
            {
                Logger.LogErr("[" + workerId + " is down]: " + ex.Message);
                NetworkManager.SetWorkerAsDown(workerId);
            }
        }
    }
}
