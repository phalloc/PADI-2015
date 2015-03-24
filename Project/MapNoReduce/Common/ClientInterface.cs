using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    interface ClientInterface
    {
        public void requestSplit();
        public void assignJob();
    }
}
