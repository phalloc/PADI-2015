
namespace PADIMapNoReduce
{
    public interface IWorker
    {
       void ReceiveWork(string clientURL, int splits);
       bool IsAlive();
       bool AddWorker(string entryURL);

    }
}
