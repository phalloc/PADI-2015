using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{

    class InvalidAccessException : Exception
    {
        private long _begin;
        private long _end;
        private long _FileSize;

        public string message;

        public InvalidAccessException(long begin, long end, long fileSize)
        {
            _begin = begin;
            _end = end;
            _FileSize = fileSize;

            message = "Interval " + "(" + begin +", " + end + ")" + "provided invalid, interval should be within " + fileSize + " bytes";

        }

        public override string ToString()
        {
            return message;
        }
    }
}
