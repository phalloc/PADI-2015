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
   
        public GUIPuppetMaster() : base()
        {
            InitializeComponent();
        }

        override public RichTextBox getConsoleRichTextBox()
        {
            return consoleMessageBox;
        }

        private void submitCommandAux(string line)
        {

            Thread runThread = new Thread(() => RunCommand(line));
            runThread.Start();
        }

        private void RunCommand(string line)
        {
            try
            {
                puppetMaster.ExecuteCommand(line);
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.Message);
            }
        }

        private void RunScript()
        {
            try
            {
                puppetMaster.LoadFile(scriptLocMsgBox.Text);
                puppetMaster.ExecuteScript();   
            }
            catch (Exception ex)
            {
                Logger.LogErr(ex.Message);
            }
        }


        private void submitScriptAux()
        {
            Thread runThread = new Thread(RunScript);
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

        private void submitScript_Click(object sender, EventArgs e)
        {
            submitScriptAux();            
        }

        //auto resize elements on maximize
        private void GUIPupperMaster_Resize(object sender, EventArgs e)
        {
            int width = consoleMessageBox.Location.X + consoleMessageBox.Size.Width - ConsoleLabel.Location.X;
            consoleMessageBox.SetBounds(ConsoleLabel.Location.X, consoleMessageBox.Location.Y, width, consoleMessageBox.Size.Height);
            refreshBtn.SetBounds(ConsoleLabel.Location.X, refreshBtn.Location.Y, width, refreshBtn.Size.Height);
        }

        private void waitButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateWaitCmd());
        }

        private void submitWorkerButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateCreateWorkProcess());
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateRefreshStatus());
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

        

        private void exportConsole_Click(object sender, EventArgs e)
        {
            ExportConsoleToFile(getConsoleRichTextBox());
        }

        private void commandMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkRunScriptTextBox();
        }

        private void scriptLocMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkLocationTextBox();
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

        private void consoleMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                submitCommandAux(generateRefreshStatus());
                e.Handled = true;
            }

            else if (e.KeyCode == Keys.F10)
            {
                ClearConsoleAction();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                ExportConsoleToFile(getConsoleRichTextBox());
                e.Handled = true;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearConsoleAction();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenScriptFile();
        }

        private void OpenScriptFile()
        {
            FindSourceFile(scriptLocMsgBox, "txt files (*.txt)|*.txt|All files (*.*)|*.*", "Choose Script Source File");
        }

        private void sourceFileBtn_Click(object sender, EventArgs e)
        {
            FindSourceFile(submitTaskSourceFileMsgBox, "Input files (*.in)|*.in", "Choose Source File");
        }

        private void destFileBtn_Click(object sender, EventArgs e)
        {
            FindDestinationFile(submitTaskDestFileMsgBox, "Output File | *.out", "Choose Destination File");
        }

        private void workerId_TextChanged(object sender, EventArgs e)
        {
            setWorkerCommandsBtnsState(checkWorkerIdMsgBox());
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            FindSourceFile(propertiesMsgBox, "Properties files (*.conf)|*.conf", "Choose properties File");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string source_file = propertiesMsgBox.Text;
            IDictionary<string, string> result = PropertiesPM.ReadDictionaryFile(source_file);

            puppetMaster.InstaciateWorkers(result);
        }

        private void GUIPuppetMaster_Load(object sender, EventArgs e)
        {
            puppetMaster.InitializeService();
            PropertiesPM.workerExeLocation = workerExeMsgBox.Text;
            PropertiesPM.clientExeLocation = clientExeMsgBox.Text;
            setWorkerCommandsBtnsState(checkWorkerIdMsgBox());
            checkLocationTextBox();
            checkCreateJob();
            checkCreateWorkerMsgsBox();
        }


        private void workerExeFindBtn_Click(object sender, EventArgs e)
        {
            FindSourceFile(workerExeMsgBox, "Executable files (*.exe)|*.exe", "Choose worker executable");
        }

        
        private void clientFindBtn_Click(object sender, EventArgs e)
        {
            FindSourceFile(clientExeMsgBox, "Executable files (*.exe)|*.exe", "Choose client executable");
        }


        private void workerExeMsgBox_TextChanged(object sender, EventArgs e)
        {
            PropertiesPM.workerExeLocation = workerExeMsgBox.Text;
        }


        private void clientExeMsgBox_TextChanged(object sender, EventArgs e)
        {
            PropertiesPM.clientExeLocation = clientExeMsgBox.Text;
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

        private bool checkLocationTextBox()
        {

            bool value = scriptLocMsgBox.Text != "";
            submitScript.Enabled = value;
            return value;
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
                submitWorkerServiceUrlMsgBox.Text == "" ||
                workerExeMsgBox.Text == "")
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
                clientExeMsgBox.Text == "" ||
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







        
    }
}
