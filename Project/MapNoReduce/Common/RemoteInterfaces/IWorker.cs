
using System;
using System.Collections.Generic;
namespace PADIMapNoReduce
{
    public interface IWorker
    {
       void ReceiveWork(string clientURL, long fileSize, long splits, string mapperName, byte[] mapperCode);
       bool FetchWorker(string clientURL, string jobTrackerURL, string mapperName, byte[] mapperCode, long fileSize, long totalSplits, long remainingSplits, string backURL);
       void BackUpdate(string nextNextURL);
       void FrontUpdate(string nextNextURL);
       bool IsAlive();
       List<string> AddWorker(string entryURL, bool firstContact);
       String DownNodeFrontNotify(string backURL);
       void DownNodeBackNotify(string nextNextURL);
       void Register(string entryURL);
       void deadCheck(string backURL);

       void FreezeWorker();
       void UnfreezeWorker();
       void FreezeJobTracker();
       void UnfreezeJobTracker();
       void Slow(int seconds);
       IDictionary<string, string> Status();



    }
}
