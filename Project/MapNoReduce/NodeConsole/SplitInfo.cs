using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PADIMapNoReduce
{
    public class SplitInfo
    {
        Stopwatch stopWatch = new Stopwatch();
        long elapsedMiliSeconds = long.MaxValue;
        bool finished = false;
        public long splitId;
        public long fileSize;
        public long totalSplits;
        public long remainingSplits;
        public string assignedWorker;

        public SplitInfo(string assignedWorker, long splitId, long fileSize, long totalSplits, long remainingSplits)
        {
            this.assignedWorker = assignedWorker;
            this.splitId = splitId;
            this.fileSize = fileSize;
            this.totalSplits = totalSplits;
            this.remainingSplits = remainingSplits;
            stopWatch.Start();
        }

        public void EndedSplit()
        {
            stopWatch.Stop();
            elapsedMiliSeconds = SplitTime();
            finished = true;
        }

        public bool DidFinished()
        {
            return finished;
        }

        public long SplitTime()
        {
            return stopWatch.ElapsedMilliseconds;
        }
    }
}
