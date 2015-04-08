using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Remoting;


namespace PADIMapNoReduce.Commands
{
    public class SubmitJobCmd : Command
    {

        public static string COMMAND = "SUBMIT";


        string entryUrl = "";
        string inputFile = "";
        string outputFile = "";
        int numSplits;
        string classMapper;
        string dllPath;

        public SubmitJobCmd(PuppetMaster pm) : base(pm) { }

        protected override bool ParseAux()
        {
            string[] args = line.Split(' ');

            Logger.LogWarn(line);
            if (args.Length == 7)
            {
                entryUrl = args[1];
                inputFile = args[2];
                outputFile = args[3];
                numSplits = Convert.ToInt32(args[4]);
                classMapper = args[5];
                dllPath = args[6];

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
            SubmitJob(entryUrl.Trim(), inputFile.Trim(), outputFile.Trim(), numSplits, classMapper.Trim(), dllPath.Trim());
        }

        public override Command CreateCopy()
        {
            return new SubmitJobCmd(puppetMaster);
        }

        public void SubmitJob(string entryUrl, string inputFile, string outputFile, int numSplits, string classMapper, string dllPath)
        {


            string commandResult = "[SUBMIT] EntryUrl: " + entryUrl + "\r\n" +
                            "          inputFile: " + inputFile + "\r\n" +
                            "          outputFile: " + outputFile + "\r\n" +
                            "          outputFile: " + outputFile + "\r\n" +
                            "          numSplits: " + numSplits + "\r\n" +
                            "          classMapper: " + classMapper.GetType().Name + "\r\n" +
                            "          dllPath" + dllPath;
            Logger.LogInfo(commandResult);


            string arguments = entryUrl + " " + inputFile + " " + outputFile + " " + numSplits + " " + classMapper + " " + dllPath;
            ProcessUtil.ExecuteNewProcess(PropertiesPM.clientExeLocation, arguments);

        }
    }
}
