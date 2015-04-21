using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    partial class Node : MarshalByRefObject, IWorker
    {
        private Object stateLock = new Object();
        private int pendingRequests = 0;

        //just to keep track of splits for STATUS
        List<KeyValuePair<long, long>> processedSplits = new List<KeyValuePair<long, long>>();


        private ServerRole _serverRole = ServerRole.NONE;
        private ServerRole serverRole
        {
            get
            {
                return _serverRole;
            }
            set
            {
                Logger.LogInfo("ROLE: " + value);
                _serverRole = value;
            }
        }

        private ExecutionState _status = ExecutionState.WAITING;
        private ExecutionState status
        {
            get
            {
                return _status;
            }
            set
            {
                Logger.LogInfo("STATUS: " + value);
                _status = status;
            }
        }

        private ServerState _serverState = ServerState.ALIVE;
        private ServerState serverState
        {
            get
            {
                return _serverState;
            }
            set
            {
                Logger.LogInfo("STATE: " + value);
                _serverState = value;

            }
        }

        private void WaitForUnfreeze()
        {
            lock (stateLock)
            {
                pendingRequests++;
                while (serverState != ServerState.ALIVE)
                {
                    Monitor.Wait(stateLock);
                }
            }

            pendingRequests--;
        }

        public void FreezeWorker()
        {
            if (serverState != ServerState.ALIVE)
            {
                return;
            }

            Logger.LogInfo("[FREEZEW] (W)");
            serverState = ServerState.FREEZEW;
        }

        public void UnfreezeWorker()
        {
            Logger.LogInfo("[UNFREEZEW] (W)");
            lock (stateLock)
            {
                if (serverState == ServerState.FREEZEW)
                {
                    Monitor.PulseAll(stateLock);
                }
               
                serverState = ServerState.ALIVE;
            }
        }

        public void FreezeJobTracker()
        {
            if (serverState != ServerState.ALIVE)
            {
                return;
            }

            Logger.LogInfo("[FREEZEC] (JT)");
            serverState = ServerState.FREEZEC;
        }

        public void UnfreezeJobTracker()
        {
            Logger.LogInfo("[UNFREEZEC] (JT)");
            lock (stateLock)
            {
                if (serverState == ServerState.FREEZEC)
                {
                    Monitor.PulseAll(stateLock);
                }

                serverState = ServerState.ALIVE;
            }
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
            result.Add(NodeRepresentation.BACK_URL, this.backURL);


            string splits = "";
            foreach (KeyValuePair<long, long> split in processedSplits)
            {
                string start = split.Key.ToString();
                string end = split.Value.ToString();
                splits += "(" + start + ", " + end + "),  ";
            }
            result.Add(NodeRepresentation.PROCESSED_SPLITS, splits);
            result.Add(NodeRepresentation.PENDING_REQUESTS, this.pendingRequests.ToString());
            result.Add(NodeRepresentation.SERVER_ROLE, this.serverRole.ToString());
            result.Add(NodeRepresentation.SERVER_STATUS, this.status.ToString());
            result.Add(NodeRepresentation.SERVER_STATE, this.serverState.ToString());
            

            return result;
        }
    }
}
