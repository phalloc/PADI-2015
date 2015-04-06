using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Remoting;


namespace PADIMapNoReduce.Commands
{
    public class StatusIndividualCmd : Command
    {
        public static string COMMAND = "REFRESH";

        string workerId = "";
        
        public StatusIndividualCmd(PuppetMaster pm) : base(pm) { }


        protected override bool ParseAux()
        {
            string[] args = this.line.Split(' ');
            if (args.Length != 2)
            {
                return false;
            }

            workerId = args[1];


            return true;
        }

        protected override void ExecuteAux()
        {
            RefreshStatus(workerId, false);
        }

        public override string getCommandName()
        {
            return COMMAND;
        }

        public override Command CreateCopy()
        {
            return new StatusIndividualCmd(puppetMaster);
        }

        private static void RefreshStatusAuxiliary(string workerId){
                IWorker w = NetworkManager.GetActiveRemoteWorkers()[workerId];
                IDictionary<string, string> result = w.Status();
                NodeRepresentation nodeRep = new NodeRepresentation(result);
                NetworkManager.UpdateNodeInformation(workerId, nodeRep);
        }

        public static void RefreshStatus(string workerId, bool fromBuldRefresh)
        {
            Logger.LogInfo("[REFRESH] " + workerId);

            if (fromBuldRefresh){
                RefreshStatusAuxiliary(workerId);
                return;
            }
           
            try
            {
                RefreshStatusAuxiliary(workerId);
            }
            catch (Exception ex)
            {
                if (ex is RemotingException || ex is SocketException)
                {
                    Logger.LogErr("[" + workerId + " is down]: " + ex.Message);
                    NetworkManager.SetWorkerAsDown(workerId);
                }
            }

            Logger.Refresh();
            
        }

    }
}
