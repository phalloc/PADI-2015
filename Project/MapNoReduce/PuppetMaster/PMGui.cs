#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PADIMapNoReduce.Commands;
using System.Reflection;

namespace PADIMapNoReduce
{
    //had to be done to render the form using abstract classes
    //http://stackoverflow.com/questions/1620847/how-can-i-get-visual-studio-2008-windows-forms-designer-to-render-a-form-that-im/2406058#2406058
    //
    // It is a limitation of visual studio

    public partial class GUIPuppetMaster

#if DEBUG
        : DummyClass
#else
        : FormRemoteGUI
#endif

    {
        PuppetMaster puppetMaster = new PuppetMaster();
        CreateWorkerForm createWorkerForm;
        SubmitJobForm submitJobForm;
        
        private Color activeWorkersColor = Color.Green;
        private Color downWorkersColor = Color.Red;


        public GUIPuppetMaster() : base()
        {
            InitializeComponent();
            createWorkerForm = new CreateWorkerForm(this);
            submitJobForm = new SubmitJobForm(this);
        }

        override public RichTextBox getConsoleRichTextBox()
        {
            return consoleMessageBox;
        }

        public void submitCommandAux(string line)
        {
            Thread runThread = new Thread(() =>
            {
                try
                {
                    puppetMaster.ExecuteCommand(line);
                }
                catch (Exception ex)
                {
                    Logger.LogErr(ex.Message);
                }
            });

            runThread.Start();
        }

        private void RunScript(string filePath)
        {
            Thread runThread = new Thread(() =>
            {
                try
                {
                    puppetMaster.LoadFile(filePath);
                    puppetMaster.ExecuteScript();
                }
                catch (Exception ex)
                {
                    Logger.LogErr(ex.Message);
                }
            });

            runThread.Start();
        }

        private void Seed(string source_file)
        {
            Thread runThread = new Thread(() =>
            {
                IDictionary<string, string> result = PropertiesPM.ReadDictionaryFile(source_file);

                puppetMaster.InstaciateWorkers(result);
            });

            runThread.Start();
        }

        /************************************************
         * ************************************************
         * 
         *    COMMANDS GENERATORS 
         *    
         * **********************************************
         * ************************************************/

        public string generateCreateWorkProcess(string workerId, string puppetMasterUrl, string serviceUrl, string entryUrl)
        {
            return CreateWorkerCmd.COMMAND + " " + workerId + " " + puppetMasterUrl + " " + serviceUrl + " " + entryUrl;
        }

        public string generateCreateJob(string entryUrl, string sourceFile, string destFile, int numberSplits, string mapper, string mapperDll)
        {
            return SubmitJobCmd.COMMAND + " " + entryUrl + " " + sourceFile + " " + destFile + " " + numberSplits + " " + mapper + " " + mapperDll;
        }


        private string generateFreezeWorker(string workerId)
        {
            return FreezeWorkerCmd.COMMAND + " " + workerId;
        }

        private string generateUnfreezeWorker(string workerId)
        {
            return UnfreezeWorkerCmd.COMMAND + " " + workerId;
        }

        private string generateDisableJobTracker(string workerId)
        {
            return FreezeJobTrackerCmd.COMMAND + " " + workerId;
        }

        private string generateEnableJobTracker(string workerId)
        {
            return UnfreezeJobTrackerCmd.COMMAND + " " + workerId;
        }

        private string generateSlowWorker(string workerId, int seconds)
        {
            return SleepCmd.COMMAND + " " + workerId + " " + seconds;
        }

        private static string generateRefreshStatus()
        {
            return StatusCmd.COMMAND;
        }


        public string generateStatusIndividual(string workerId)
        {
            return StatusIndividualCmd.COMMAND + " " + workerId;
        }

        /*************************************************
       * ************************************************
       * 
       *    CHECK BOX CHECKERS
       *    
       * ***********************************************
       * ************************************************/


        private bool checkRunScriptTextBox()
        {
            bool value = commandMsgBox.Text != "";
            submitCommand.Enabled = value;
            return value;
        }

        /*************************
         * ************************
         * 
         *      EVENT HANDLERS
         * 
         * ************************
         * ************************/


        private void submitCommand_Click(object sender, EventArgs e)
        {
            submitCommandAux(commandMsgBox.Text);
        }

        //auto resize elements on maximize
        private void GUIPupperMaster_Resize(object sender, EventArgs e)
        {
            int width = consoleMessageBox.Location.X + consoleMessageBox.Size.Width - ConsoleLabel.Location.X;
            consoleMessageBox.SetBounds(ConsoleLabel.Location.X, consoleMessageBox.Location.Y, width, consoleMessageBox.Size.Height);
            NetworkTreeView.SetBounds(NetworkTreeView.Location.X, NetworkTreeView.Location.Y, NetworkTreeView.Size.Width, consoleMessageBox.Location.Y + consoleMessageBox.Size.Height - NetworkTreeView.Location.Y);
        }


        private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportConsoleToFile(getConsoleRichTextBox());
        }

        private void commandMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkRunScriptTextBox();
        }

        private void slowNumSeconds_Leave(object sender, EventArgs e)
        {
            if (slowNumSeconds.Text == "")
            {
                slowNumSeconds.Text = "0";
            }
        }

        private void GUIPuppetMaster_Load(object sender, EventArgs e)
        {
            puppetMaster.InitializeService();
            PropertiesPM.workerExeLocation = workerexeToolStripMenuItem.ToolTipText;
            PropertiesPM.clientExeLocation = clientexeToolStripMenuItem.ToolTipText;
            
        }

        private void workerExeMsgBox_TextChanged(object sender, EventArgs e)
        {
            PropertiesPM.workerExeLocation = workerexeToolStripMenuItem.ToolTipText;
        }


        private void clientExeMsgBox_TextChanged(object sender, EventArgs e)
        {
            PropertiesPM.clientExeLocation = clientexeToolStripMenuItem.ToolTipText;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearConsoleAction();
        }

        private void workerexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = FileUtil.FindSourceFile("Executable files (*.exe)|*.exe", "Choose worker executable");

            if (path != "") {
                workerexeToolStripMenuItem.ToolTipText = path;
                PropertiesPM.workerExeLocation = workerexeToolStripMenuItem.ToolTipText;
            }
        }

        private void clientexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = FileUtil.FindSourceFile("Executable files (*.exe)|*.exe", "Choose client executable");

            if (result != "") { 
                clientexeToolStripMenuItem.ToolTipText = result;
                PropertiesPM.clientExeLocation = clientexeToolStripMenuItem.ToolTipText;
            }
        }


        private void showSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string print = "\r\n------ CURRENT SETTINGS ------\r\n"
                + "Worker *.exe: " + workerexeToolStripMenuItem.ToolTipText + "\r\n"
                + "Client *.exe: " + clientexeToolStripMenuItem.ToolTipText + "\r\n"
                + "-------------------------------";
            Logger.LogWarn(print);
        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = FileUtil.FindSourceFile("Script files (*.script)|*.script|All files (*.*)|*.*", "Choose Script Source File");

            if (file != "") {
                RunScript(file);Seed(pMpropertiesconfToolStripMenuItem.Text);
            }
        }

        private void myScripttxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunScript(myScripttxtToolStripMenuItem.Text);
        }

        private void fromFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string result = FileUtil.FindSourceFile("Properties files (*.seed)|*.seed", "Choose seed File");

            if (result != "")
            {
                Seed(result);
            }
        }

        private void pMpropertiesconfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Seed(pMpropertiesconfToolStripMenuItem.Text);
        }

        private void createWorkerstxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunScript(createWorkerstxtToolStripMenuItem.Text);
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
            List<TreeNode> result = new List<TreeNode>();

            foreach (FieldInfo f in node.GetType().GetFields())
            {
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


        public override void RefreshRemote()
        {
            RefreshNetWorkConfiguration();
        }



        /**************************** HANDLERS ******************************/

        private void treeView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                // Point where the mouse is clicked.
                Point p = new Point(e.X, e.Y);

                // Get the node that the user has clicked.
                TreeNode node = NetworkTreeView.GetNodeAt(p);
                if (node != null)
                {

                    NetworkTreeView.SelectedNode = node;

                    if (Convert.ToString(node.Tag).Contains("Active Workers"))
                    {
                        workerMenuStrip.Show(NetworkTreeView, p);
                    }
                }
            }
        }

        private void freezeWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;
            if (node != null) { 
                submitCommandAux(generateFreezeWorker(node.Text));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void unfreezeWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;

            if (node != null)
            {
                submitCommandAux(generateUnfreezeWorker(node.Text));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void freezeJobTrackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;

            if (node != null)
            {
                submitCommandAux(generateDisableJobTracker(node.Text));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void unfreezeJobTrackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;
            if (node != null)
            {
                submitCommandAux(generateEnableJobTracker(node.Text));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;

            if (node != null)
            {
                submitCommandAux(generateSlowWorker(node.Text, Convert.ToInt32(slowNumSeconds.Value)));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void workerMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            workerMenuStrip.Items[4].Text = "Sleep " + slowNumSeconds.Value + " seconds";
        }

        private void ExpandAllBtn_Click(object sender, EventArgs e)
        {
            NetworkTreeView.ExpandAll();
        }

        private void CollapseAllBtn_Click(object sender, EventArgs e)
        {
            NetworkTreeView.CollapseAll();
        }

        private void createWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (createWorkerForm.IsDisposed)
            {
                createWorkerForm = new CreateWorkerForm(this);
            }

            createWorkerForm.Show();
        }

        private void submitJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (submitJobForm.IsDisposed)
            {
                submitJobForm = new SubmitJobForm(this);
            }

            submitJobForm.Show();
        }

        private void SendStatusCommandAllNodes(object sender, EventArgs e)
        {
            submitCommandAux(generateRefreshStatus());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateGraph();
        }

        private void generateGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateGraph();
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;

            string command = generateStatusIndividual(node.Text);
            submitCommandAux(command);
        }


    }
}
