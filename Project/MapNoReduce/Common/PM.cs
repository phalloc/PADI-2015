using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class PM : IPuppetMaster
    {
        public void CreateWorker()
        {
            Logger.LogInfo("RECEIVED REQUEST TO CREATE WORKER");
        }
    }
}
