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
        private static string TIMESTAMP_TAG = "TIME";
        public static string ACTIVE_WORKERS_TAG = "ACTIVE_WORKERS";
        private static string DOWN_WORKERS_TAG = "DOWN_WORKERS";
        private static string RING_TAG = "RING";

        private static Color ACTIVE_WORKERS_COLOR = Color.Green;
        private static Color DOWN_WORKERS_COLOR = Color.Red;
        private static Color RING_COLOR = Color.SteelBlue;

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
            TreeNode root = FindNode(NetworkTreeView, rootNodeKey);

            string id;
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
            NetworkTreeView.CollapseAll();


            string timeStamp = TIMESTAMP_TAG + ":" + GenerateTimeStamp();
            TreeNode now = CreateNode(timeStamp, timeStamp);


            string activeTag = ACTIVE_WORKERS_TAG + ":" + timeStamp;
            TreeNode active = CreateNode(ACTIVE_WORKERS_TAG, activeTag);
            active.BackColor = ACTIVE_WORKERS_COLOR;

            string downTag = RING_TAG + ":" + timeStamp;
            TreeNode down = CreateNode(DOWN_WORKERS_TAG, downTag);
            down.BackColor = DOWN_WORKERS_COLOR;

            now.Nodes.Add(active);
            now.Nodes.Add(down);

            NetworkTreeView.Nodes.Add(now);

            IDictionary<string, NodeRepresentation> knownNodes = NetworkManager.GetKnownWorkers();
            foreach (KeyValuePair<string, IWorker> entry in NetworkManager.GetActiveRemoteWorkers())
            {
                AddNodeRepresentation(activeTag, knownNodes[entry.Key]);
            }

            foreach (string id in NetworkManager.GetDownWorkers())
            {
                AddNodeRepresentation(downTag, knownNodes[id]);
            }

            now.Expand();
            mostRecentNode = now;
        }

        public void GenerateGraph()
        {
            //reconstruction of the ring again
            IDictionary<string, NodeRepresentation> knownNodes = NetworkManager.GetKnownUrlWorkers();

            int numberOfTimes = knownNodes.Count;
            if (knownNodes.Count == 0)
            {
                Logger.LogErr("There are no known nodes");
                return;
            }

            NodeRepresentation node = knownNodes[knownNodes.Keys.First(t => true)];

            string id;
            string nextUrl;
            string result = "";
            for (int i = 0; i < numberOfTimes; i++)
            {
                node.info.TryGetValue(NodeRepresentation.ID, out id);
                node.info.TryGetValue(NodeRepresentation.NEXT_URL, out nextUrl);

                result += " => " + id;

                if (nextUrl == null || (nextUrl != null && nextUrl == ""))
                {
                    Logger.LogErr("Node " + id + " as no nextUrl!");
                    break;
                }

                knownNodes.TryGetValue(nextUrl, out node);

                if (node == null)
                {
                    Logger.LogErr("No information on nextUrl...");
                    break;
                }
            }

            TreeNode tagNode = CreateNode(RING_TAG + " : " + result, RING_TAG);
            tagNode.BackColor = RING_COLOR;
            mostRecentNode.Nodes.Add(tagNode);
        }

        private TreeNode FindNode(TreeView treeView, string matchTag)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                if (node.Tag.ToString() == matchTag)
                {
                    return node;
                }
                else
                {
                    TreeNode nodeChild = FindChildNode(node, matchTag);
                    if (nodeChild != null) return nodeChild;
                }
            }
            return (TreeNode)null;
        }

        private TreeNode FindChildNode(TreeNode parentNode, string matchTag)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Tag.ToString() == matchTag)
                {
                    return node;
                }
                else
                {
                    TreeNode nodeChild = FindChildNode(node, matchTag);
                    if (nodeChild != null) return nodeChild;
                }
            }
            return (TreeNode)null;
        }

        public void Clear()
        {
            NetworkTreeView.Nodes.Clear();
        }
    }
}
