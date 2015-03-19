using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    class SubmitJobCmd : Command
    {

        string entryUrl;
        string inputFile;
        string outputFile;
        int numSplits;
        IMap map;

        public SubmitJobCmd(string line) : base(line) { }

        public override bool Parse(string line)
        {
            string[] args = line.Split(' ');
            if (args.Length == 6)
            {
                entryUrl = args[1];
                inputFile = args[2];
                outputFile = args[3];
                numSplits = Convert.ToInt32(args[4]);

                //COMMON = DLL where the className is at
                map = (IMap)Activator.CreateInstance(Type.GetType(args[5]));

                return true;
            }

            return false;
        }

        public override bool Execute()
        {
            if (!Parse(line)) { return false; }
            return SubmitJob(entryUrl, inputFile, outputFile, numSplits, map);
        }

        public bool SubmitJob(string entryUrl, string inputFile, string outputFile, int numSplits, IMap mapper)
        {

            
            System.Diagnostics.Debug.WriteLine("HERE -> " + mapper.Map("this sentence is now uppercase"));

            return true;
        }
    }
}
