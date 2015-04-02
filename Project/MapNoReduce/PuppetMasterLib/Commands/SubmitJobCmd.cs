using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    class SubmitJobCmd : Command
    {

        string entryUrl;
        string inputFile;
        string outputFile;
        int numSplits;
        IMapper map;

        public SubmitJobCmd(string line) : base(line) { }

        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');
            if (args.Length == 7)
            {
                entryUrl = args[1];
                inputFile = args[2];
                outputFile = args[3];
                numSplits = Convert.ToInt32(args[4]);

                map = (IMapper)Activator.CreateInstance(Type.GetType(args[5] + "," + args[6]));

                return true;
            }

            return false;
        }

        protected override bool ExecuteAux()
        {
            return SubmitJob(entryUrl, inputFile, outputFile, numSplits, map);
        }

        public bool SubmitJob(string entryUrl, string inputFile, string outputFile, int numSplits, IMapper mapper)
        {

            commandResult = "[SUBMIT] EntryUrl: " + entryUrl + "\r\n" +
                            "          inputFile: " + inputFile + "\r\n" +
                            "          outputFile: " + outputFile + "\r\n" +
                            "          outputFile: " + outputFile + "\r\n" +
                            "          numSplits: " + numSplits + "\r\n" +
                            "          mapper: " + mapper.GetType().Name + "\r\n" +
                            "          Ola ---> MAPPER ---> " + mapper.MapDummy("Ola");
            
            return true;
        }
    }
}
