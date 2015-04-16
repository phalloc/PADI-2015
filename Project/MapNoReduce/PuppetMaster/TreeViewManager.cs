using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PADIMapNoReduce
{
    class TreeViewManager
    {
        public static string TIMESTAMP_TAG = "TIME";

        public static string ACTIVE_WORKERS_TAG = "ACTIVE_WORKERS";
        private static Color ACTIVE_WORKERS_COLOR = Color.Green;
        
        public static string DOWN_WORKERS_TAG = "DOWN_WORKERS";
        private static Color DOWN_WORKERS_COLOR = Color.Red;

        public static string RING_BACK_URL_TAG = "RING_BACK_URL";
        private static Color RING_BACK_URL_COLOR = Color.Bisque;
        
        public static string RING_NEXT_URL_TAG = "RING_NEXT_URL";
        private static Color RING_NEXT_COLOR = Color.SteelBlue;
        
        public static string RING_NEXT_NEXT_URL_START1_TAG = "RING_NEXTNEXT_URL_1";
        private static Color RING_NEXT_NEXT_1_COLOR = Color.DodgerBlue;
        
        public static string RING_NEXT_NEXT_URL_START2_TAG = "RING_NEXTNEXT_URL_2";
        private static Color RING_NEXT_NEXT_2_COLOR = Color.SkyBlue;
        
        public static string CURRENT_JTS_TAG = "CURRENT_JTS";
        private static Color CURRENT_JTS_COLOR = Color.Gainsboro;

        public static string SPLITS_TAG = "SPLITS";
        private static Color SPLITS_COLOR = Color.AntiqueWhite;

        private TreeView NetworkTreeView;

        private TreeNode mostRecentNode;
        public TreeViewManager(TreeView t)
        {
            this.NetworkTreeView = t;
        }

        private string GenerateTimeStamp()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        private TreeNode CreateNode(string id, string tag)
        {
            TreeNode rootNode = new TreeNode();
            rootNode.Text = id;
            rootNode.Name = id;
            rootNode.Tag = tag;

            return rootNode;
        }

        private void AddNodeRepresentation(string rootNodeKey, NodeRepresentation node)
        {
            TreeNode root = TreeViewUtil.FindNode(NetworkTreeView, rootNodeKey);
            
            if (root == null)
            {
                Logger.LogErr("Something bad happened generating tree: cant find rootNode " + rootNodeKey);
                return;
            }

            string id = "";
            node.info.TryGetValue(NodeRepresentation.ID, out id);

            TreeNode nodeTree = CreateNode(id, rootNodeKey + ":" + id);
            foreach (TreeNode t in NodeAtributesRepresentationToTree(node))
            {
                nodeTree.Nodes.Add(t);
            }

            root.Nodes.Add(nodeTree);
            root.Expand();
        }

        private List<TreeNode> NodeAtributesRepresentationToTree(NodeRepresentation node)
        {
            IDictionary<string, string> values = node.info;
            List<TreeNode> result = new List<TreeNode>();

            foreach (KeyValuePair<string, string> entry in values)
            {
                string fieldName = entry.Key;
                string value = entry.Value;

                TreeNode field = CreateNode(fieldName, fieldName);
                field.Text = fieldName + " = " + value;
                result.Add(field);
            }

            return result;
        }


        public void RefreshNetWorkConfiguration()
        {
            //collapse
            NetworkTreeView.CollapseAll();

            string timeStamp = TIMESTAMP_TAG + ":" + GenerateTimeStamp();
            TreeNode now = CreateNode(timeStamp, timeStamp);
            NetworkTreeView.Nodes.Add(now);

            
            IDictionary<string, NodeRepresentation> knownNodes = NetworkManager.GetKnownWorkers();
            List<string> downNodes = NetworkManager.GetDownWorkers();
            List<string> currentJTs = new List<string>();
            
            //check if active nodes
            if (knownNodes.Count != 0 && knownNodes.Count != downNodes.Count)
            {
                string activeTag = ACTIVE_WORKERS_TAG + ":" + timeStamp;
                TreeNode active = CreateNode(ACTIVE_WORKERS_TAG, activeTag);
                active.BackColor = ACTIVE_WORKERS_COLOR;
                now.Nodes.Add(active);

                string splitTag = SPLITS_TAG + ":" + timeStamp;
                TreeNode splitNode = CreateNode(SPLITS_TAG, splitTag);
                splitNode.BackColor = SPLITS_COLOR;
                now.Nodes.Add(splitNode);

                //Generating Active workers
                foreach (KeyValuePair<string, NodeRepresentation> entry in knownNodes)
                {

                    string splits;
                    entry.Value.info.TryGetValue(NodeRepresentation.PROCESSED_SPLITS, out splits);

                    if (!(splits == null || (splits != null && splits == "")))
                    {
                        splitNode.Nodes.Add(CreateNode(splits, splitTag + ":" + splits));
                    }

                    //check if it not flagged as down
                    if (!downNodes.Contains(entry.Key))
                    {
                        AddNodeRepresentation(activeTag, entry.Value);

                        string jobTracker = "";
                        entry.Value.info.TryGetValue(NodeRepresentation.CURRENT_JT, out jobTracker);

                        if (jobTracker != null && !currentJTs.Contains(jobTracker))
                        {
                            currentJTs.Add(jobTracker);
                        }
                    }



                }
            }
            
            if (downNodes.Count != 0)
            {
                string downTag = DOWN_WORKERS_TAG + ":" + timeStamp;
                TreeNode down = CreateNode(DOWN_WORKERS_TAG, downTag);
                down.BackColor = DOWN_WORKERS_COLOR;
                now.Nodes.Add(down);
                
                foreach (string id in downNodes)
                {
                    AddNodeRepresentation(downTag, knownNodes[id]);
                }
            }

            if (currentJTs.Count != 0)
            {
                string jtTag = CURRENT_JTS_TAG + ":" + timeStamp;
                TreeNode jt = CreateNode(CURRENT_JTS_TAG, jtTag);
                jt.BackColor = CURRENT_JTS_COLOR;
                now.Nodes.Add(jt);

                foreach (string id in currentJTs)
                {
                    if (knownNodes.ContainsKey(id))
                    {
                        AddNodeRepresentation(jtTag, knownNodes[id]);
                    }
                    else
                    {
                        NodeRepresentation node = new NodeRepresentation();
                        node.info.Add("UNKNOWN JOBTRACKER WORKER ID", id);
                        AddNodeRepresentation(jtTag, node);
                    }
                }
            }

            now.Expand();
            mostRecentNode = now;
        }

        public string GenerateFieldIteratorThroughNodes(IDictionary<string, NodeRepresentation> knownNodes, NodeRepresentation node, string fieldName, string separator)
        {
            int numberOfTimes = knownNodes.Count * 2 + 1;

            string id = "";
            string nextFieldNameValue = "";
            string result = "";
            for (int i = 0; i < numberOfTimes; i++)
            {
                node.info.TryGetValue(NodeRepresentation.ID, out id);
                node.info.TryGetValue(fieldName, out nextFieldNameValue);

                result += separator + id;

                if (nextFieldNameValue == null || (nextFieldNameValue != null && nextFieldNameValue == ""))
                {
                    throw new Exception("Node " + id + " has no " + fieldName + " !");
                }

                knownNodes.TryGetValue(nextFieldNameValue, out node);

                if (node == null)
                {
                    throw new Exception("No information on " + fieldName + ".");
                }
            }

            return result;
        }




        public void GenerateGraph()
        {
            //reconstruction of the ring again
            IDictionary<string, NodeRepresentation> knownNodes = NetworkManager.GetKnownUrlWorkers();

            if (knownNodes.Count == 0)
            {
                Logger.LogWarn("There are no known nodes");
                return;
            }

            NodeRepresentation node = knownNodes[knownNodes.Keys.First(t => true)];
            //Generating nextUrlGraph
            try { 
                string result = GenerateFieldIteratorThroughNodes(knownNodes, node, NodeRepresentation.NEXT_URL, " -> ");
                TreeNode tagNode = CreateNode(RING_NEXT_URL_TAG + " : " + result, RING_NEXT_URL_TAG);
                tagNode.BackColor = RING_NEXT_COLOR;
                mostRecentNode.Nodes.Add(tagNode);
            }catch(Exception ex){
                Logger.LogWarn(ex.Message);
            }


            //Generating nextNextUrlGraph 1
            try
            {
                string resultNextNextUrl1 = GenerateFieldIteratorThroughNodes(knownNodes, node, NodeRepresentation.NEXT_NEXT_URL, " ->> ");
                TreeNode tagNode1 = CreateNode(RING_NEXT_NEXT_URL_START1_TAG + " : " + resultNextNextUrl1, RING_NEXT_NEXT_URL_START1_TAG);
                tagNode1.BackColor = RING_NEXT_NEXT_1_COLOR;
                mostRecentNode.Nodes.Add(tagNode1);
            }catch(Exception ex){
                Logger.LogWarn(ex.Message);
            }

            //Generating nextNextUrlGraph 2
            try {
                string nextUrl;
                node.info.TryGetValue(NodeRepresentation.NEXT_URL, out nextUrl);
                NodeRepresentation node2;

                if (nextUrl == null) {
                    throw new Exception("Not enough information to process nextNextGraph 2");
                }

                knownNodes.TryGetValue(nextUrl, out node2);
                string resultNextNextUrl2 = GenerateFieldIteratorThroughNodes(knownNodes, node2, NodeRepresentation.NEXT_NEXT_URL, " ->> ");
                TreeNode tagNode2 = CreateNode(RING_NEXT_NEXT_URL_START2_TAG + " : " + resultNextNextUrl2, RING_NEXT_NEXT_URL_START2_TAG);
                tagNode2.BackColor = RING_NEXT_NEXT_2_COLOR;
                mostRecentNode.Nodes.Add(tagNode2);
                }
            catch (Exception ex)
            {
                Logger.LogWarn(ex.Message);
            }


            //Generating backUrlGraph 1
            try
            {
                string resultBackUrl = GenerateFieldIteratorThroughNodes(knownNodes, node, NodeRepresentation.BACK_URL, " <- ");
                TreeNode backNode = CreateNode(RING_BACK_URL_TAG + " : " + resultBackUrl, RING_BACK_URL_TAG);
                backNode.BackColor = RING_BACK_URL_COLOR;
                mostRecentNode.Nodes.Add(backNode);
            }
            catch (Exception ex)
            {
                Logger.LogWarn(ex.Message);
            }

        }

        public void Clear()
        {
            NetworkTreeView.Nodes.Clear();
        }
    }
}
