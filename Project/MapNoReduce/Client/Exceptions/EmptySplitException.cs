using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    class EmptySplitException : Exception
    {


        public override string ToString()
        {
            return "Interval provided doesn't return a single line";
        }
    }
}
