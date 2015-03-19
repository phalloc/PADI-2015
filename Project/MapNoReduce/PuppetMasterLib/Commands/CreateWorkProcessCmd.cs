using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    class CreateWorkProcessCmd : Command
    {
        string id ;
        string puppetMasterUrl;
        string serviceUrl;
        string entryUrl;

        public CreateWorkProcessCmd(string line) : base(line) { } 

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

        protected override bool ExecuteAux()
        {
            return CreateWorkProcess(id, puppetMasterUrl, serviceUrl, entryUrl);
        }

        public bool CreateWorkProcess(string id, string puppetMasterUrl, string serviceUrl, string entryUrl)
        {

            commandResult = "[CREATE] id: " + id + "\r\n" +  
                            "          puppetMasterUrl: " + puppetMasterUrl  + "\r\n" + 
                            "          service Url: " + serviceUrl + "\r\n" + 
                            "          entryUrl " + entryUrl;

            return true;
        }

    }
}
