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
        int averageSplitTime = int.MaxValue;
        long averageSplitSize = long.MaxValue;

        //locks for jobtracker functions
        static object LockLogSplitStarted = new Object();
        static object LockLogFinished = new Object();

        private long numSplits = 0;

        Node jtNode;

        public JobTrackerInformation(Node jtnode, long numSplits)
        {
            this.jtNode = jtnode;
            this.numSplits = numSplits;
        }

        public void AlertChangeOfJobTracker()
        {   
            //TODO when detected current jobtracker done
        }

        public void RegisterWorker(string url, IWorker worker)
        {
            if (!activeNodes.ContainsKey(url)) { 
                activeNodes.Add(url, worker);
            }
        }

        public void UnregisterWorker(string url)
        {
            activeNodes.Remove(url);
        }

        public void LogFinishedSplit(long totalSplits, long remainingSplits)
        {
            lock (LockLogFinished)
            {
                long splitId = totalSplits - remainingSplits;
                splitInfos[splitId].EndedSplit();

                if (averageSplitTime == int.MaxValue)
                {
                    averageSplitTime = splitInfos[splitId].elapsedSeconds;
                }

                long splitSize = splitInfos[splitId].splitSize;
                if (splitSize == long.MaxValue || splitSize < averageSplitSize)
                {
                    averageSplitSize = splitSize;
                }
                numSplits--;
                Logger.LogInfo("SplitId " + splitId + " took + " + splitInfos[splitId].elapsedSeconds + " seconds. " + numSplits + " splits remaining");
            }
        }

        public void LogStartedSplit(string workerId, long fileSize, long totalSplits, long remainingSplits)
        {
            lock (LockLogSplitStarted)
            {
                long splitId = totalSplits - remainingSplits;
                if (workersSplits.ContainsKey(workerId))
                {
                    workersSplits.Remove(workerId);
                }
                else
                {
                    workersSplits.Add(workerId, splitId);
                }
                if (splitInfos.ContainsKey(splitId))
                {
                    Logger.LogErr("SOMEONE IS TRYING TO PROCESS THE SAME SPLIT");
                }
                splitInfos.Add(remainingSplits, new SplitInfo(splitId, fileSize, totalSplits, remainingSplits));
            }
        }



        public void NotifyRemainingWorks()
        {
            foreach (KeyValuePair<string, long> keyValue in workersSplits)
            {
                string key = keyValue.Key;
                SplitInfo splitInfo = splitInfos[keyValue.Value];

                int waitTime = splitInfo.splitSize > averageSplitSize ? 2 * averageSplitTime : averageSplitTime;
                if (!splitInfo.DidFinished() && splitInfo.SplitTime() > waitTime)
                {
                    Logger.LogWarn("Slow worker... sending split elsewhere");
                }
            }
        }


        public bool DidFinishedJob()
        {
            if (numSplits < 0)
            {
                Logger.LogWarn("Clearing the job tracker information for the next job");
                workersSplits.Clear();
                splitInfos.Clear();
                return true;
            }

            return false;
        }

    }
}
