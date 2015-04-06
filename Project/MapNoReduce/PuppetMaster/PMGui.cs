#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PADIMapNoReduce.Commands;
using System.Drawing;

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
        TreeViewManager treeViewManager;
       

        public GUIPuppetMaster() : base()
        {
            InitializeComponent();
            createWorkerForm = new CreateWorkerForm(this);
            submitJobForm = new SubmitJobForm(this);
            treeViewManager = new TreeViewManager(NetworkTreeView);
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

                puppetMaster.RegisterWorkers(result);
            });

            runThread.Start();
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
            puppetMaster.StartService();
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

        


        public override void RefreshRemote()
        {
            treeViewManager.RefreshNetWorkConfiguration();
            treeViewManager.GenerateGraph();
        }


        /**************************** HANDLERS ******************************/

        private void treeView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Point where the mouse is clicked.
            Point p = new Point(e.X, e.Y);
            
            // Get the node that the user has clicked.
            TreeNode node = NetworkTreeView.GetNodeAt(p);
            if (e.Button == MouseButtons.Right)
            {
                if (node != null)
                {
                    NetworkTreeView.SelectedNode = node;

                    string tag = Convert.ToString(node.Tag);
                    if (tag.Contains(TreeViewManager.ACTIVE_WORKERS_TAG) || tag.Contains(TreeViewManager.DOWN_WORKERS_TAG))
                    {
                        workerMenuStrip.Show(NetworkTreeView, p);
                    }
                }
            }
            else
            {

                if (node != null && node.Parent == null)
                {
                    foreach (TreeNode n in node.Nodes)
                    {
                        n.Expand();
                    }
                }
            }
        }

        private void freezeWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;
            if (node != null) {
                submitCommandAux(CommandsManager.generateFreezeWorker(node.Text));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void unfreezeWorkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;

            if (node != null)
            {
                submitCommandAux(CommandsManager.generateUnfreezeWorker(node.Text));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void freezeJobTrackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;

            if (node != null)
            {
                submitCommandAux(CommandsManager.generateDisableJobTracker(node.Text));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void unfreezeJobTrackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;
            if (node != null)
            {
                submitCommandAux(CommandsManager.generateEnableJobTracker(node.Text));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;

            if (node != null)
            {
                submitCommandAux(CommandsManager.generateSlowWorker(node.Text, Convert.ToInt32(slowNumSeconds.Value)));
            }

            NetworkTreeView.SelectedNode = null;
        }

        private void workerMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            string workerId = NetworkTreeView.SelectedNode.Text;

            workerMenuStrip.Items[0].Text = CommandsManager.generateFreezeWorker(workerId);
            workerMenuStrip.Items[1].Text = CommandsManager.generateUnfreezeWorker(workerId);
            workerMenuStrip.Items[2].Text = CommandsManager.generateDisableJobTracker(workerId);
            workerMenuStrip.Items[3].Text = CommandsManager.generateEnableJobTracker(workerId);
            workerMenuStrip.Items[4].Text = CommandsManager.generateSlowWorker(workerId, Convert.ToInt32(slowNumSeconds.Value));
            
            workerMenuStrip.Items[6].Text = CommandsManager.generateStatusIndividual(workerId);
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
            submitCommandAux(CommandsManager.generateRefreshStatus());
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = NetworkTreeView.SelectedNode;

            string command = CommandsManager.generateStatusIndividual(node.Text);
            submitCommandAux(command);
        }

        private void ClearTreeBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure? \r\n\r\nThis will clear the Tree.",
                        "Caption", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes: treeViewManager.Clear(); break;
                case DialogResult.No: break;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = FileUtil.FindDestinationFile("Xml file (*.xml)|*.xml", "Choose destination");
            
            if(output != ""){
                TreeViewUtil tv = new TreeViewUtil();
                tv.exportToXml(NetworkTreeView, output);
            }
        }

    }
}
