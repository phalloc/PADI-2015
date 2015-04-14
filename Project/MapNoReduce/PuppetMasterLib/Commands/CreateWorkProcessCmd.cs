using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Threading;

namespace PADIMapNoReduce.Commands
{
    public class CreateWorkerCmd : Command
    {
        public static string COMMAND = "WORKER";

        string id = "";
        string puppetMasterUrl = "";
        string serviceUrl = "";
        string entryUrl = "";

        public CreateWorkerCmd(PuppetMaster pm) : base(pm) { }

        protected override bool ParseAux()
        {
            
            string[] args = line.Split(' ');

            if (args.Length == 4)
            {
                id = args[1];
                puppetMasterUrl = args[2];
                serviceUrl = args[3];
                entryUrl = "";

                return true;
            }
            else if (args.Length == 5)
            {
                id = args[1];
                puppetMasterUrl = args[2];
                serviceUrl = args[3];
                entryUrl = args[4];

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
            CreateWorker(id.Trim(), puppetMasterUrl.Trim(), serviceUrl.Trim(), entryUrl.Trim());
        }

        public override Command CreateCopy()
        {
            return new CreateWorkerCmd(puppetMaster);
        }

        public void CreateWorker(string id, string puppetMasterUrl, string serviceUrl, string entryUrl)
        {
            Logger.LogInfo("[WORKER] " + id + " " + puppetMaster + " " + serviceUrl + " " + entryUrl);
            if (puppetMasterUrl != "") { 
                Logger.LogInfo("CONTACTING PUPPET MASTER");
                try
                {
                    IPuppetMaster pm = (IPuppetMaster)Activator.GetObject(typeof(IPuppetMaster), puppetMasterUrl);
                    pm.CreateWorker(id, puppetMasterUrl, serviceUrl, entryUrl);
                }
                catch (Exception ex)
                {
                    if (ex is RemotingException || ex is SocketException)
                    {
                        Logger.LogErr("Puppet Master @ " + puppetMasterUrl + " is down. --> " + ex.Message);
                    }
                }
            }
            else
            {
                IPuppetMaster p = new PM();
                p.CreateWorker(id, puppetMasterUrl, serviceUrl, entryUrl);
            }

            Logger.LogInfo("Waiting 1 second for node start");
            Thread.Sleep(1000);
        }

    }
}
