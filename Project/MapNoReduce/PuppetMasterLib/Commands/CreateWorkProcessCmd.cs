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

        public override bool Parse(string line)
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

        public override bool Execute()
        {
            if (!Parse(line)) { return false;  }
            return CreateWorkProcess(id, puppetMasterUrl, serviceUrl, entryUrl);
        }

        public bool CreateWorkProcess(string id, string puppetMasterUrl, string serviceUrl, string entryUrl)
        {
            System.Diagnostics.Debug.WriteLine("CreateWorkProcess");
            return true;
        }

    }
}
