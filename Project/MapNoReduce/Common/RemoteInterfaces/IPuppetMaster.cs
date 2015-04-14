using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public interface IPuppetMaster
    {
        void CreateWorker(string id, string puppetMasterUrl, string serviceUrl, string entryUrl);
    }
}
