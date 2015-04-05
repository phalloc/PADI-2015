using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class CreateWorkerCmd : Command
    {
        public static string COMMAND = "WORKER";
        
        string id ;
        string puppetMasterUrl;
        string serviceUrl;
        string entryUrl;

        public CreateWorkerCmd(PuppetMaster pm) : base(pm) { }

        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');
            if (args.Length == 5)
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
            CreateWorker(id, puppetMasterUrl, serviceUrl, entryUrl);
        }


        public void CreateWorker(string id, string puppetMasterUrl, string serviceUrl, string entryUrl)
        {
            if (puppetMasterUrl != "") { 
                Logger.LogInfo("CONTACTING PUPPET MASTER");
            
                IPuppetMaster pm = (IPuppetMaster)Activator.GetObject(typeof(IPuppetMaster), puppetMasterUrl);
                pm.CreateWorker(id, serviceUrl, entryUrl);
            }
            else
            {
                IPuppetMaster p = new PM();
                p.CreateWorker(id, serviceUrl, entryUrl);
            }
        }

    }
}
