using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PADIMapNoReduce.Commands
{
    public class SubmitJobCmd : Command
    {

        public static string COMMAND = "SUBMIT";
        

        string entryUrl;
        string inputFile;
        string outputFile;
        int numSplits;
        IMapper map;

        public SubmitJobCmd(PuppetMaster pm) : base(pm) { }

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

        public override string getCommandName()
        {
            return COMMAND;
        }

        protected override void ExecuteAux()
        {
            SubmitJob(entryUrl, inputFile, outputFile, numSplits, map);
        }

        public void SubmitJob(string entryUrl, string inputFile, string outputFile, int numSplits, IMapper mapper)
        {

            string commandResult = "[SUBMIT] EntryUrl: " + entryUrl + "\r\n" +
                            "          inputFile: " + inputFile + "\r\n" +
                            "          outputFile: " + outputFile + "\r\n" +
                            "          outputFile: " + outputFile + "\r\n" +
                            "          numSplits: " + numSplits + "\r\n" +
                            "          mapper: " + mapper.GetType().Name + "\r\n" +
                            "          Ola ---> MAPPER ---> " + mapper.MapDummy("Ola");
            Logger.LogInfo(commandResult);
        }
    }
}
