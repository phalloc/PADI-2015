
using System;
using System.Collections.Generic;
namespace PADIMapNoReduce
{
    public interface IWorker
    {
       void ReceiveWork(string clientURL, long fileSize, long splits, string mapperName, byte[] mapperCode);
       bool FetchWorker(string clientURL, string jobTrackerURL, string mapperName, byte[] mapperCode, long fileSize, long totalSplits, long remainingSplits, string backURL, bool fromSlowSplit);
       void BackUpdate(string nextNextURL);
       void FrontUpdate(string nextNextURL);
       bool IsAlive();
       List<string> AddWorker(string entryURL, bool firstContact);
       String DownNodeFrontNotify(string backURL);
       void DownNodeBackNotify(string nextNextURL);
       void Register(string entryURL);
       void deadCheck(string backURL);
       void UpdateCurrentJobTracker(string jobtracker);
       bool IsWorking();

        //jobtracker specific methods
       void RegisterWorker(string workerId, string workerUrl);
       void LogStartedSplit(string workerId, long fileSize, long totalSplits, long remainingSplits);
       void LogFinishedSplit(string workerId, long totalSplits, long remainingSplits);
       bool PingJT();
       string SetUpAsSecondaryServer(string clientUrl, string primaryJTurl, long fileSize, long numSplits);
       bool IsPrimary();
       bool CanContinueProcessSplit(string workerId, long splitId);



        //special commands
       void FreezeWorker();
       void UnfreezeWorker();
       void FreezeJobTracker();
       void UnfreezeJobTracker();
       void Slow(int seconds);
       IDictionary<string, string> Status();
    }
}
