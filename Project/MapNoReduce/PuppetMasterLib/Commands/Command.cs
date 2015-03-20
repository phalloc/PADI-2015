using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public abstract class Command
    {
        protected static string NO_RESULT = "No result";

        protected string line;
        protected string commandResult = NO_RESULT;
        protected bool didParse = false;

        public Command(string line)
        {
            this.line = line;
        }

        public string getResult()
        {
            return commandResult;
        }

        public bool Parse() {
            return didParse ? didParse : (didParse = ParseAux());
        
        }
        public bool Execute()
        {
            return (!didParse && Parse()) ? false : ExecuteAux();
        }

        protected abstract bool ExecuteAux();
        protected abstract bool ParseAux();
        
    }
}
