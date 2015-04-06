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
        NetworkForm networkForm;

        public GUIPuppetMaster() : base()
        {
            InitializeComponent();
            networkForm = new NetworkForm(puppetMaster);
        }

        override public RichTextBox getConsoleRichTextBox()
        {
            return consoleMessageBox;
        }

        private void submitCommandAux(string line)
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

        private string generateWaitCmd()
        {
            return WaitCmd.COMMAND + " " + numSecondsWait.Value;
        }

        private string generateCreateWorkProcess()
        {
            return CreateWorkerCmd.COMMAND + " " + submitWorkerWorkerIdMsgBox.Text + " " + submitWorkerPMUrlMsgBox.Text + " " + submitWorkerServiceUrlMsgBox.Text + " " + submitWorkerEntryUrlMsgBox.Text;
        }

        private string generateCreateJob()
        {
            return SubmitJobCmd.COMMAND + " " + submitTaskEntryUrlMsgBox.Text + " " + submitTaskSourceFileMsgBox.Text + " " + submitTaskDestFileMsgBox.Text + " " + submitTaskNumberSplits.Value + " " + submitJobMapTxtBox.Text + " " + submitJobDllTxtBox.Text;
        }

        private string generateFreezeWorker()
        {
            return FreezeWorkerCmd.COMMAND + " " + workerId.Text;
        }

        private string generateUnfreezeWorker()
        {
            return UnfreezeWorkerCmd.COMMAND + " " + workerId.Text;
        }

        private string generateDisableJobTracker()
        {
            return FreezeJobTrackerCmd.COMMAND + " " + workerId.Text;
        }

        private string generateEnableJobTracker()
        {
            return UnfreezeJobTrackerCmd.COMMAND + " " + workerId.Text;
        }

        private string generateSlowWorker()
        {
            return SleepCmd.COMMAND + " " + workerId.Text + " " + slowNumSeconds.Value;
        }

        private static string generateRefreshStatus()
        {
            return StatusCmd.COMMAND;
        }

        /*************************************************
       * ************************************************
       * 
       *    CHECK BOX CHECKERS
       *    
       * ***********************************************
       * ************************************************/

        private bool checkWorkerIdMsgBox()
        {
            return workerId.Text != "";
        }

        private void setWorkerCommandsBtnsState(bool value)
        {
            freezewBtn.Enabled = value;
            unfreezewBtn.Enabled = value;
            unfreezecBtn.Enabled = value;
            freezecBtn.Enabled = value;
            slowBtn.Enabled = value;
        }

        private bool checkRunScriptTextBox()
        {
            bool value = commandMsgBox.Text != "";
            submitCommand.Enabled = value;
            return value;
        }

        public bool checkCreateWorkerMsgsBox()
        {
            if (submitWorkerWorkerIdMsgBox.Text == "" ||
                submitWorkerServiceUrlMsgBox.Text == "")
            {
                submitWorkerButton.Enabled = false;
                return false;
            }
            else
            {
                submitWorkerButton.Enabled = true;
                return true;
            }
        }

        public bool checkCreateJob()
        {
            if (submitTaskEntryUrlMsgBox.Text == "" ||
                submitTaskSourceFileMsgBox.Text == "" ||
                submitTaskDestFileMsgBox.Text == "" ||
                submitJobMapTxtBox.Text == "" ||
                submitJobDllTxtBox.Text == "")
            {
                submitTaskButton.Enabled = false;
                return false;
            }
            else
            {
                submitTaskButton.Enabled = true;
                return true;
            }
        }

        private void submitJobMapTxtBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateJob();
        }

        private void submitJobDllTxtBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateJob();
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
        }

        private void waitButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateWaitCmd());
        }

        private void submitWorkerButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateCreateWorkProcess());
        }

        private void submitTaskButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateCreateJob());
        }

        private void freezewBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateFreezeWorker());
        }

        
        private void unfreezewBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateUnfreezeWorker());
        }

        

        private void freezecBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateDisableJobTracker());
        }

        

        private void unfreezecBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateEnableJobTracker());
        }

        

        private void slowBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateSlowWorker()); 
        }


        private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportConsoleToFile(getConsoleRichTextBox());
        }

        private void commandMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkRunScriptTextBox();
        }

        private void submitWorkerWorkerIdMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateWorkerMsgsBox();
        }

        private void submitWorkerPMUrlMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateWorkerMsgsBox();
        }

        private void submitWorkerServiceUrlMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateWorkerMsgsBox();
        }

        private void submitWorkerEntryUrlMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateWorkerMsgsBox();
        }

        private void submitTaskEntryUrlMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateJob();
        }

        private void submitTaskSourceFileMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateJob();
        }

        private void submitTaskDestFileMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateJob();
        }

        private void submitTaskClassMapperComboBox_TextChanged(object sender, EventArgs e)
        {
            checkCreateJob();
        }

        private void numSecondsWait_Leave(object sender, EventArgs e)
        {
            if (numSecondsWait.Text == "")
            {
                numSecondsWait.Text = "0";
            }
        }

        private void submitTaskNumberSplits_Leave(object sender, EventArgs e)
        {
            if (submitTaskNumberSplits.Text == "")
            {
                submitTaskNumberSplits.Text = "0";
            }
        }

        private void slowNumSeconds_Leave(object sender, EventArgs e)
        {
            if (slowNumSeconds.Text == "")
            {
                slowNumSeconds.Text = "0";
            }
        }

        private void sourceFileBtn_Click(object sender, EventArgs e)
        {
            submitTaskSourceFileMsgBox.Text = FindSourceFile("Input files (*.in)|*.in", "Choose Source File");
        }

        private void destFileBtn_Click(object sender, EventArgs e)
        {
            submitTaskDestFileMsgBox.Text = FindDestinationFile("Output File | *.out", "Choose Destination File");
        }

        private void workerId_TextChanged(object sender, EventArgs e)
        {
            setWorkerCommandsBtnsState(checkWorkerIdMsgBox());
        }

        private void GUIPuppetMaster_Load(object sender, EventArgs e)
        {
            puppetMaster.InitializeService();
            PropertiesPM.workerExeLocation = workerexeToolStripMenuItem.ToolTipText;
            PropertiesPM.clientExeLocation = clientexeToolStripMenuItem.ToolTipText;
            setWorkerCommandsBtnsState(checkWorkerIdMsgBox());
            checkCreateJob();
            checkCreateWorkerMsgsBox();

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
            string path = FindSourceFile("Executable files (*.exe)|*.exe", "Choose worker executable");

            if (path != "") {
                workerexeToolStripMenuItem.ToolTipText = path;
                PropertiesPM.workerExeLocation = workerexeToolStripMenuItem.ToolTipText;
            }
        }

        private void clientexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = FindSourceFile("Executable files (*.exe)|*.exe", "Choose client executable");

            if (result != "") { 
                clientexeToolStripMenuItem.ToolTipText = result;
                PropertiesPM.clientExeLocation = clientexeToolStripMenuItem.ToolTipText;
            }
        }




        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateRefreshStatus());
        }

        private void refreshF5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateRefreshStatus());
        }

        private void showSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string print = "\r\n------ CURRENT SETTINGS ------\r\n"
                + "Worker *.exe: " + workerexeToolStripMenuItem.ToolTipText + "\r\n"
                + "Client *.exe: " + clientexeToolStripMenuItem.ToolTipText + "\r\n"
                + "-------------------------------";
            Logger.LogWarn(print);
        }

        private void showNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (networkForm.IsDisposed)
            {
                networkForm = new NetworkForm(puppetMaster);
            }
            networkForm.Show();
        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = FindSourceFile("Script files (*.script)|*.script|All files (*.*)|*.*", "Choose Script Source File");

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
            string result = FindSourceFile("Properties files (*.seed)|*.seed", "Choose seed File");

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
    }
}
