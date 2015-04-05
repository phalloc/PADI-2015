using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public class NetworkManager
    {
        //all nodes including downOnes
        //Key = id
        private static IDictionary<string, NodeRepresentation> knownWorkers = new Dictionary<string, NodeRepresentation>();

        //all nodes including downOne
        //Key = url
        private static IDictionary<string, NodeRepresentation> knownKUrlWorkers = new Dictionary<string, NodeRepresentation>();

        //all down nodes
        private static List<string> downWorkers = new List<string>();

        //all active remote nodes
        private static IDictionary<string, IWorker> activeWorkersObj = new Dictionary<string, IWorker>();

        //all active activeJobTrackers
        //private static IDictionary<string, IWorker> activeJobTrackers = new Dictionary<string, IWorker>();

        public static IWorker GetRemoteWorker(string id)
        {
            if (!activeWorkersObj.ContainsKey(id))
                throw new Exception("Worker id not configured");

            return activeWorkersObj[id];
        }

        public static IDictionary<string, NodeRepresentation> GetKnownWorkers()
        {
            return knownWorkers;
        }

        public static IDictionary<string, NodeRepresentation> GetKnownUrlWorkers()
        {
            return knownKUrlWorkers;
        }

        public static IDictionary<string, IWorker> GetActiveRemoteWorkers()
        {
            return activeWorkersObj;
        }

        public static List<string> GetDownWorkers()
        {
            return downWorkers;
        }

        public static void SetWorkerAsDown(string id)
        {
            if (!knownWorkers.ContainsKey(id) || !activeWorkersObj.ContainsKey(id))
            {
                throw new Exception("Trying to flag worker " + id + " as inactive, yet it is not in the active list");
            }

            Logger.LogWarn("Adding worker " + id + " to the list of down workers.");
            activeWorkersObj.Remove(id);
            downWorkers.Add(id);
        }

        public static void UpdateNodeInformation(string id, NodeRepresentation node)
        {
            if (!knownWorkers.ContainsKey(id))
            {
                throw new Exception("Trying to update worker information where he does not exist!");
            }

            knownWorkers[id] = node;
            knownKUrlWorkers[node.myUrl] = node;
        }

        public static void RegisterNewWorker(string id, string url)
        {
            Logger.LogInfo("Register worker: " + id + " : " + url);

            if (knownWorkers.ContainsKey(id))
                knownWorkers.Remove(id);

            knownWorkers.Add(id, new NodeRepresentation(id, url));

            if (activeWorkersObj.ContainsKey(id))
                activeWorkersObj.Remove(id);

            IWorker w = (IWorker)Activator.GetObject(typeof(IWorker), url);
            activeWorkersObj.Add(id, w);
        }

        public static void Clear()
        {
            knownWorkers.Clear();
            knownKUrlWorkers.Clear();
            activeWorkersObj.Clear();
            downWorkers.Clear();
        }
    }
}
