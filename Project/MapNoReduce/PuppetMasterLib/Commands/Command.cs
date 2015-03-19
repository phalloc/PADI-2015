using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce.Commands
{
    abstract class Command
    {
        protected string line;

        public Command(string line)
        {
            this.line = line;
        }

        public abstract bool Parse(string line);
        public abstract bool Execute();

    }
}
