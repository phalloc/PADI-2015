using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce.Commands
{
    public abstract class Command
    {
        protected string line;
       
        public void Parse(string line) {
            this.line = line;
            if(!ParseAux()){
                this.line = null;
                throw new Exception("Couldn't parse command: " + line);
            }
        }
        public bool Execute()
        {
            if (line == null){
                throw new Exception("Run parse first");
            }

            bool result = ExecuteAux();
            this.line = null;
            return result;
        }

        protected abstract bool ExecuteAux();
        protected abstract bool ParseAux();
        public abstract string getCommandName();

        
        
    }
}
