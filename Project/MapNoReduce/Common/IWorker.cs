
using System;
using System.Collections.Generic;
namespace PADIMapNoReduce
{
    public interface IWorker
    {
       void ReceiveWork(string clientURL, int splits);
       bool IsAlive();
       List<string> AddWorker(string entryURL, bool firstContact);
    }
}
