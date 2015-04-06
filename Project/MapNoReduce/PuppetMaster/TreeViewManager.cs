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

        private Color activeWorkersColor = Color.Green;
        private Color downWorkersColor = Color.Red;

        private TreeView NetworkTreeView;

        public TreeViewManager(TreeView t)
        {
            this.NetworkTreeView = t;
        }

        private void Clear()
        {
            NetworkTreeView.Nodes.Clear();
            AddRootNode("Active Workers", activeWorkersColor);
            AddRootNode("Down Nodes", downWorkersColor);
        }

        private void AddRootNode(string id, Color c)
        {
            TreeNode rootNode = new TreeNode();
            rootNode.Text = id;
            rootNode.Name = id;
            rootNode.BackColor = c;
            rootNode.Expand();
            NetworkTreeView.Nodes.Add(rootNode);
        }

        private void AddNodeRepresentation(string rootNodeKey, NodeRepresentation node)
        {
            TreeNode root = NetworkTreeView.Nodes.Find(rootNodeKey, false)[0];

            TreeNode nodeTree = new TreeNode();
            nodeTree.Text = node.id;
            nodeTree.Name = node.id;
            nodeTree.Tag = rootNodeKey + ":" + node.id;
            foreach (TreeNode t in NodeAtributesRepresentationToTree(node))
            {
                nodeTree.Nodes.Add(t);
            }


            root.Nodes.Add(nodeTree);
            root.Expand();
        }

        private List<TreeNode> NodeAtributesRepresentationToTree(NodeRepresentation node)
        {

            IDictionary<string, string> values = NodeRepresentation.FieldValues(node);

            List<TreeNode> result = new List<TreeNode>();

            foreach (KeyValuePair<string, string> entry in values)
            {
                string fieldName = entry.Key;
                string value = entry.Value;

                TreeNode field = new TreeNode();
                field.Text = fieldName + " = " + value;
                result.Add(field);
            }

            return result;
        }


        public void RefreshNetWorkConfiguration()
        {
            Clear();
            IDictionary<string, NodeRepresentation> knownNodes = NetworkManager.GetKnownWorkers();
            foreach (KeyValuePair<string, IWorker> entry in NetworkManager.GetActiveRemoteWorkers())
            {
                AddNodeRepresentation("Active Workers", knownNodes[entry.Key]);
            }

            foreach (string id in NetworkManager.GetDownWorkers())
            {
                AddNodeRepresentation("Down Nodes", knownNodes[id]);
            }
        }

        public void GenerateGraph()
        {


            //reconstruction of the ring again
            IDictionary<string, NodeRepresentation> knownNodes = NetworkManager.GetKnownUrlWorkers();

            int numberOfTimes = knownNodes.Count;
            if (knownNodes.Count == 0)
            {
                Logger.LogErr("Pleae refresh first");
                return;
            }

            NodeRepresentation node = knownNodes[knownNodes.Keys.First(t => true)];

            string result = node.id;
            for (int i = 0; i < numberOfTimes; i++)
            {
                if (node.nextUrl == "")
                {
                    Logger.LogErr("Node " + node.id + " as no nextUrl!");
                    return;
                }
                node = knownNodes[node.nextUrl];
                result += " => " + node.id;
            }

            Logger.LogInfo(result);
        }

    }
}
