using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Remoting;


namespace PADIMapNoReduce.Commands
{
    public class UnfreezeWorkerCmd : Command
    {

        public static string COMMAND = "UNFREEZEW";


        string workerId = "";

        public UnfreezeWorkerCmd(PuppetMaster pm) : base(pm) { }


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

        protected override void ExecuteAux()
        {
            UnfreezeWorker(workerId.Trim());
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public override Command CreateCopy()
        {
            return new UnfreezeWorkerCmd(puppetMaster);
        }

        public void UnfreezeWorker(string workerId)
        {
                Logger.LogInfo("[UNFREEZEW] " + workerId + " (W)");
           try
            {
                IWorker w = NetworkManager.GetRemoteWorker(workerId);
                w.UnfreezeWorker();
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
