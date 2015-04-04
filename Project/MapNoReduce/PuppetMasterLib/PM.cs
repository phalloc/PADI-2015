using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class PM : MarshalByRefObject, IPuppetMaster
    {
        public void CreateWorker(string id, string serviceUrl, string entryUrl)
        {
            Logger.LogInfo("RECEIVED REQUEST TO CREATE WORKER");
            Logger.LogInfo("id: " + id);
            Logger.LogInfo("serviceUrl: " + serviceUrl);
            Logger.LogInfo("entryUrl: " + entryUrl);
        }
    }
}
