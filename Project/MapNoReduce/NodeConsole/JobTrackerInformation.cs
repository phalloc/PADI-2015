using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class JobTrackerInformation
    {
        Node currentSecondaryJT = null;
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

        public JobTrackerInformation(Node jtnode, long numSplits)
        {
            this.jtNode = jtnode;
            this.numSplits = numSplits;
        }

        public void AlertChangeOfJobTracker(string newJobTrackerUrl)
        {
            Logger.LogInfo("Updating current JobTracking across the network");
            foreach (KeyValuePair<string, IWorker> keyValue in activeNodes)
            {
                string key = keyValue.Key;
                IWorker worker = keyValue.Value;
                worker.UpdateCurrentJobTracker(newJobTrackerUrl);
            }
            //TODO when detected current jobtracker done
        }

        public void RegisterWorker(string workerId, IWorker worker)
        {
            if (!activeNodes.ContainsKey(workerId)) {
                Logger.LogInfo("Registering new worker " + workerId);
                activeNodes.Add(workerId, worker);
            }
        }

        public void UnregisterWorker(string url)
        {
            activeNodes.Remove(url);
        }

        public void LogFinishedSplit(string workerId, long totalSplits, long remainingSplits)
        {
            lock (LockLogFinished)
            {
                long splitId = remainingSplits;
                SplitInfo splitInfo = splitInfos[splitId];
                if (splitInfo.DidFinished())
                {
                    Logger.LogInfo("[" + workerId + " SLOW FINISHED " + splitId + "]");
                    return;
                }

                splitInfos[splitId].EndedSplit();

                if (averageSplitTime == long.MaxValue)
                {
                    averageSplitTime = splitInfos[splitId].SplitTime() + 500;
                }


                numSplits--;

                Logger.LogInfo("[" + workerId + " ENDED " + splitId + "] - " + splitInfos[splitId].SplitTime() + " ms. " + numSplits + " splits remaining");
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
                    Logger.LogWarn("[" + workerId + " STARTED " + splitId + "] " + " was already registered with another split.");
                    workersSplits.Remove(workerId);
                }
                
                workersSplits.Add(workerId, splitId);
                

                if (splitInfos.ContainsKey(splitId))
                {
                    Logger.LogWarn("[SPLIT START " + workerId + "]" + " is processing a slow split " + splitId);
                    splitInfos.Remove(splitId);
                }

                Logger.LogInfo(fileSize + " @ " + totalSplits + " @ " + remainingSplits);
                splitInfos.Add(remainingSplits, new SplitInfo(splitId, fileSize, totalSplits, remainingSplits));
            }
        }

        public SplitInfo FindSlowSplit()
        {
            foreach (KeyValuePair<string, long> keyValue in workersSplits)
            {
                string key = keyValue.Key;
                SplitInfo splitInfo = splitInfos[keyValue.Value];

                long waitTime = splitInfo.splitId == 0 ? 2 * averageSplitTime : averageSplitTime;

                //Logger.LogInfo(splitInfo.splitId + " taking " + splitInfo.SplitTime() + " waitTime is " + waitTime);
                if (!splitInfo.DidFinished() && splitInfo.SplitTime() > waitTime)
                {
                    Logger.LogWarn("[SPLIT " + splitInfo.splitId + " SLOW] taking " + splitInfo.SplitTime() + " vs " + waitTime);
                    return splitInfo;
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
                return true;
            }

            return false;
        }

    }
}
