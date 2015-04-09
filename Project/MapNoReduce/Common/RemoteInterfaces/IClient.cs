using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    //FIXME
    // bifurcar em dois
    public interface IClient
    {

        string getWorkSplit(long beginSplit, long endSplit);
        void returnWorkSplit(IList<KeyValuePair<string, string>> Map, int splitId);

    }
}
