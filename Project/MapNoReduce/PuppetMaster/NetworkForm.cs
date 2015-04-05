using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace PADIMapNoReduce
{
    public partial class NetworkForm : Form
    {

        private Color activeWorkersColor = Color.Green;
        private Color downWorkersColor = Color.Red;

        PuppetMaster pm;
        public NetworkForm(PuppetMaster pm)
        {
            InitializeComponent();
            this.pm = pm;
            Clear();
        }

        private void Clear()
        {
            NetworkTreeView.Nodes.Clear();
            AddRootNode("Active Workers", activeWorkersColor);
            AddRootNode("Down Nodes", downWorkersColor);
        }

        private void AddRootNode(string id, Color c){
            TreeNode rootNode = new TreeNode();
            rootNode.Text = id;
            rootNode.Name = id;
            rootNode.BackColor = c;
            rootNode.Expand();
            NetworkTreeView.Nodes.Add(rootNode);
        }

        private void AddNodeRepresentation(string rootNodeKey, NodeRepresentation node) 
        {
            Logger.LogInfo(rootNodeKey);

            TreeNode root = NetworkTreeView.Nodes.Find(rootNodeKey, false)[0];
            
            TreeNode nodeTree = new TreeNode();
            nodeTree.Text = node.id;
            nodeTree.Name = node.id;

            foreach (TreeNode t in NodeAtributesRepresentationToTree(node))
            {
                nodeTree.Nodes.Add(t);            }

            root.Nodes.Add(nodeTree);
        }

        private List<TreeNode> NodeAtributesRepresentationToTree(NodeRepresentation node){
            List<TreeNode> result = new List<TreeNode>();

            foreach (FieldInfo f in node.GetType().GetFields()){
                TreeNode field = new TreeNode();
                field.Text = f.Name + " = " + (string)f.GetValue(node);
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

        private void refreshTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshNetWorkConfiguration();
        }

        private void NetworkForm_Resize(object sender, EventArgs e)
        {
            int halfWidth = Convert.ToInt32(this.Size.Width * 0.5);

            GraphLabel.SetBounds(NetworkTreeView.Location.X + halfWidth, GraphLabel.Location.Y, GraphLabel.Size.Width, GraphLabel.Size.Height);
            NetworkTreeView.SetBounds(NetworkTreeView.Location.X, NetworkTreeView.Location.Y, halfWidth - 10, NetworkTreeView.Size.Height);
            
            int width = richTextBox1.Location.X + richTextBox1.Size.Width - GraphLabel.Location.X;
            richTextBox1.SetBounds(GraphLabel.Location.X, richTextBox1.Location.Y, width, richTextBox1.Size.Height);
        }


    }
}
