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

        void getWorkSplit();
        void returnWorkSplit();

    }
}
