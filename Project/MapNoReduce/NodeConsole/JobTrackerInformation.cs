using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class JobTrackerInformation
    {
        IDictionary<string, IWorker> activeNodes = new Dictionary<string, IWorker>();

        //worker->splitId
        IDictionary<string, long> workersSplits = new Dictionary<string, long>();

        //splitId->SplitInfo
        IDictionary<long, SplitInfo> splitInfos = new Dictionary<long, SplitInfo>();

        //average seconds (kindish)
        long averageSplitTime = long.MaxValue;

        //locks for jobtracker functions
        static object LockLogSplitStarted = new Object();
        static object LockLogFinished = new Object();

        public long numSplits = 0;

        Node jtNode;

        public bool CanContinueProcessSplit(string workerId, long splitId)
        {
            if (splitInfos.ContainsKey(splitId)) {
                return splitInfos[splitId].assignedWorker == workerId;
            }

            return false;
        }

        public bool didFinishedCurrentJob()
        {
            return numSplits <= 0;
        }

        public JobTrackerInformation(Node jtnode, long numSplits)
        {
            this.jtNode = jtnode;
            this.numSplits = numSplits;
        }

        public void AlertChangeOfJobTracker(string newJobTrackerUrl)
        {
            //Logger.LogInfo("Updating current JobTracking across the network");
            foreach (KeyValuePair<string, IWorker> keyValue in activeNodes)
            {
                string key = keyValue.Key;
                IWorker worker = keyValue.Value;
                try { 
                   worker.UpdateCurrentJobTracker(newJobTrackerUrl);
                }
                catch (Exception)
                {
                    //only alert the active workers
                }
            }
        }

        public void RegisterWorker(string workerId, IWorker worker)
        {
            if (!activeNodes.ContainsKey(workerId)) {
                //Logger.LogInfo("Registering new worker " + workerId);
                activeNodes.Add(workerId, worker);
            }
        }

        public void LogFinishedSplit(string workerId, long totalSplits, long remainingSplits)
        {
            lock (LockLogFinished)
            {
                long splitId = remainingSplits;
                SplitInfo splitInfo = splitInfos[splitId];
                if (splitInfo.DidFinished())
                {
                    //Logger.LogInfo("[" + workerId + " SLOW FINISHED " + splitId + "]");
                    return;
                }

                splitInfos[splitId].EndedSplit();


                averageSplitTime = splitInfos[splitId].SplitTime() + 500;
                
                numSplits--;

                Logger.LogInfo("[" + workerId + " ENDED " + splitId + "] in " + splitInfos[splitId].SplitTime() + " ms. " + numSplits + " splits remaining");
            }
        }

        public void LogStartedSplit(string workerId, long fileSize, long totalSplits, long remainingSplits)
        {
            lock (LockLogSplitStarted)
            {
                long splitId = remainingSplits;
                Logger.LogInfo("[" + workerId + " STARTED " + splitId + "]");
                if (workersSplits.ContainsKey(workerId))
                {
                    //Logger.LogWarn("[" + workerId + " STARTED " + splitId + "] " + " was already registered with another split.");
                    workersSplits.Remove(workerId);
                }
                
                workersSplits.Add(workerId, splitId);
                

                if (splitInfos.ContainsKey(splitId))
                {
                    Logger.LogWarn("[SPLIT START " + workerId + "]" + " is processing a slow split " + splitId);
                    splitInfos.Remove(splitId);
                }

                splitInfos.Add(splitId, new SplitInfo(workerId, splitId, fileSize, totalSplits, remainingSplits));
            }
        }

        public SplitInfo FindSlowSplit()
        {
            foreach (KeyValuePair<string, long> keyValue in workersSplits)
            {
                string key = keyValue.Key;
                SplitInfo splitInfo = splitInfos[keyValue.Value];


                if (averageSplitTime < long.MaxValue)
                {
                    long waitTime = splitInfo.splitId == 0 ? 3 * averageSplitTime : 2 * averageSplitTime;

                    if (!splitInfo.DidFinished() && splitInfo.SplitTime() > waitTime)
                    {
                        Logger.LogWarn("[SPLIT " + splitInfo.splitId + " SLOW] taking " + splitInfo.SplitTime() + " vs " + waitTime);

                        return splitInfo;
                    }
                }
                
            }
            return null;
        }

        public IWorker GetFirstFreeWorker()
        {
            foreach (KeyValuePair<string, IWorker> keyValue in activeNodes)
            {
                string key = keyValue.Key;
                IWorker worker = keyValue.Value;

                if (!worker.IsWorking())
                {
                    return worker;
                }
            }
            return null;
        }

        public bool DidFinishJob()
        {
            if (numSplits <= 0)
            {
                Logger.LogWarn("[JOB FINISHED] Clearing the job tracker information for the next job");
                workersSplits.Clear();
                splitInfos.Clear();
                jtNode.RevertToNoneState();
                return true;
            }

            return false;
        }

    }
}
