
using System;
using System.Collections.Generic;
namespace PADIMapNoReduce
{
    public interface IWorker
    {
       void ReceiveWork(string clientURL, long fileSize, int splits, string mapperName, byte[] mapperCode);
       void FetchWorker(string clientURL, string jobTrackerURL, long start, long end, string mapperName, byte[] mapperCode, long splitSize, int remainingSplits);
       bool IsAlive();
       List<string> AddWorker(string entryURL, bool firstContact);

       void FreezeWorker();
       void UnfreezeWorker();
       void FreezeJobTracker();
       void UnfreezeJobTracker();
       void Slow(int seconds);
       IDictionary<string, string> Status();



    }
}
