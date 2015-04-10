using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    partial class Node : MarshalByRefObject, IWorker
    {
        public void FreezeWorker()
        {
            Logger.LogInfo("[FREEZEW] (W)");

        }

        public void UnfreezeWorker()
        {
            Logger.LogInfo("[UNFREEZEW] (W)");

        }

        public void FreezeJobTracker()
        {
            Logger.LogInfo("[FREEZEC] (JT)");

        }

        public void UnfreezeJobTracker()
        {
            Logger.LogInfo("[UNFREEZEC] (JT)");

        }

        public void Slow(int seconds)
        {
            Logger.LogInfo("[SLOWW] " + seconds + ". Delaying the worker process before mapping");
            sleep_seconds = seconds;
        }


        public IDictionary<string, string> Status()
        {
            Logger.LogInfo("[STATUS]");

            IDictionary<string, string> result = new Dictionary<string, string>();

            result.Add(NodeRepresentation.ID, this.id);
            result.Add(NodeRepresentation.SERVICE_URL, this.myURL);
            result.Add(NodeRepresentation.NEXT_URL, this.nextURL);
            result.Add(NodeRepresentation.NEXT_NEXT_URL, this.nextNextURL);
            result.Add(NodeRepresentation.CURRENT_JT, this.currentJobTrackerUrl);
            result.Add("startSplit", this.startSplit.ToString());
            result.Add("endSplit", this.endSplit.ToString());


            result.Add("serverRole", this.serverRole.ToString());
            result.Add("serverStatus", this.status.ToString());

            return result;
        }
    }
}
