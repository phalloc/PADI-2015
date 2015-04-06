using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Remoting;


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

        int PMStarPort = 20001;
        int PMEndPort = 29999;
        int WStartPort = 30001;
        int WEndPort = 39999;

        protected override bool ParseAux()
        {
            
            string[] args = line.Split(' ');

            //<id> <entry-url>
            if (args.Length == 3)
            {
                id = args[1].Trim(); ;
                serviceUrl = args[2].Trim();

                return true;
            }
            else if (args.Length == 4)
            {
                id = args[1];
                string url = args[2];

                string[] splits = url.Split(':');
                splits = splits[splits.Length - 1].Split('/');
                int channelPort = int.Parse(splits[0]);

 
                //Second argument is a puppet-master url
                if (channelPort >= PMStarPort && channelPort <= PMEndPort)
                {
                    puppetMasterUrl = args[2];
                    serviceUrl = args[3];
                }
                else if (channelPort >= WStartPort && channelPort <= WEndPort)
                {
                    serviceUrl = args[2];
                    entryUrl = args[3];
                }
                else
                {
                    return false;
                }


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
            Logger.LogInfo("----");
            Logger.LogInfo(id);
            Logger.LogInfo(puppetMasterUrl);
            Logger.LogInfo(serviceUrl);
            Logger.LogInfo(entryUrl);

            if (puppetMasterUrl != "") { 
                Logger.LogInfo("CONTACTING PUPPET MASTER");

                try
                {
                    IPuppetMaster pm = (IPuppetMaster)Activator.GetObject(typeof(IPuppetMaster), puppetMasterUrl);
                    pm.CreateWorker(id, serviceUrl, entryUrl);
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
                Logger.LogInfo("Creating locally");
                IPuppetMaster p = new PM();
                p.CreateWorker(id, serviceUrl, entryUrl);
            }
        }

    }
}
