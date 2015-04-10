using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    class EmptySplitException : Exception
    {

        long _pos;

        public EmptySplitException(long pos)
        {
            _pos = pos;
        }

        public override string ToString()
        {
            return "Interval provided doesn't return a single line: stopped at " + _pos;
        }
    }
}
