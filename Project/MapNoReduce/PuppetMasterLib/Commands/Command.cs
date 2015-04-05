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

        protected PuppetMaster puppetMaster = null;

        public Command(PuppetMaster pm)
        {
            this.puppetMaster = pm;
        }
       
        public void Parse(string line) {
            this.line = line;
            if(!ParseAux()){
                this.line = null;
                throw new Exception("Couldn't parse command: " + line);
            }
        }

        public void Execute()
        {
            if (line == null){
                throw new Exception("Run parse first");
            }

            ExecuteAux();
            this.line = null;
 
        }

        protected abstract void ExecuteAux();
        protected abstract bool ParseAux();
        public abstract string getCommandName();       
    }
}
