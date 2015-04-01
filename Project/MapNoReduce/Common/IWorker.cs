using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public interface IWorker
    {
       void receiveWork(string entryUrl, int splits);

    }
}
