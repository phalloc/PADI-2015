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

        //all remote nodes
        private static IDictionary<string, IWorker> workersObj = new Dictionary<string, IWorker>();

        public static IWorker GetRemoteWorker(string id)
        {
            if (!workersObj.ContainsKey(id))
                throw new Exception("Worker id not configured");

            return workersObj[id];
        }

        public static IDictionary<string, NodeRepresentation> GetKnownWorkers()
        {
            return knownWorkers;
        }

        public static IDictionary<string, NodeRepresentation> GetKnownUrlWorkers()
        {
            return knownKUrlWorkers;
        }

        public static IDictionary<string, IWorker> GetRemoteWorkersObj()
        {
            return workersObj;
        }

        public static List<string> GetDownWorkers()
        {
            return downWorkers;
        }

        public static void SetWorkerAsDown(string id)
        {
            if (!knownWorkers.ContainsKey(id) || !workersObj.ContainsKey(id))
            {
                throw new Exception("Trying to flag worker " + id + " as inactive, yet it is not in the active list");
            }

            if (downWorkers.Contains(id))
            {
                return;
            }

            Logger.LogWarn("Adding worker " + id + " to the list of down workers.");
            downWorkers.Add(id);
        }

        public static void UpdateNodeInformation(string id, NodeRepresentation node)
        {
            if (!knownWorkers.ContainsKey(id))
            {
                throw new Exception("Trying to update worker information but he does not exist!");
            }

            knownWorkers[id] = node;
            string myUrl;
            
            node.info.TryGetValue(NodeRepresentation.SERVICE_URL, out myUrl);

            if (myUrl == null || (myUrl != null && myUrl == ""))
            {
                Logger.LogWarn("Worker " + id + " did not provide a service url!! Bad update!");
                return;
            }

            //node is back up again
            if (downWorkers.Contains(id))
            {
                downWorkers.Remove(id);
            }

            knownKUrlWorkers[myUrl] = node;
        }

        public static void RegisterNewWorker(string id, string url)
        {
            Logger.LogInfo("Register worker: " + id + " : " + url);

            if (knownWorkers.ContainsKey(id))
            {
                Logger.LogWarn("Already registered " + id + " worker. Skipping.");
                return;
            }
            
            NodeRepresentation n = new NodeRepresentation(id, url);
            knownWorkers.Add(id, n);
            knownKUrlWorkers.Add(url, n);
             
            IWorker w = (IWorker)Activator.GetObject(typeof(IWorker), url);
            workersObj.Add(id, w);
        }
    }
}
