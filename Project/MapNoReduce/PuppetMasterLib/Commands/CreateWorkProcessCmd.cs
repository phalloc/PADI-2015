using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public class CreateWorkProcessCmd : Command
    {
        public static string COMMAND = "WORKER";
        


        string id ;
        string puppetMasterUrl;
        string serviceUrl;
        string entryUrl;


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
            CreateWorkProcess(id, puppetMasterUrl, serviceUrl, entryUrl);
        }


        public void CreateWorkProcess(string id, string puppetMasterUrl, string serviceUrl, string entryUrl)
        {

            string commandResult = "[CREATE] id: " + id + "\r\n" +  
                            "          puppetMasterUrl: " + puppetMasterUrl  + "\r\n" + 
                            "          service Url: " + serviceUrl + "\r\n" + 
                            "          entryUrl " + entryUrl;


            Logger.LogInfo(commandResult);
        }

    }
}
