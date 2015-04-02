#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        CommandsManager cm = new CommandsManager();
   
        public GUIPuppetMaster()
        {
            InitializeComponent();

            checkLocationTextBox();
            checkCreateJob();
            setWorkerCommandsBtnsState(false);
            checkCreateWorkerMsgsBox();
            
            ClearConsole();
        }

        override public RichTextBox getConsoleRichTextBox()
        {
            return consoleMessageBox;
        }

        private void submitCommandAux(string line)
        {
            try
            {
                LogInfo(cm.ExecuteCommand(cm.ParseCommand(line)));
            }
            catch (Exception ex)
            {
                LogErr(ex.Message);
            }
        }

        private void submitScriptAux()
        {
            try
            {
                cm.LoadFile(scriptLocMsgBox.Text);
                LogInfo(cm.ExecuteScript());
            }
            catch (Exception ex)
            {
                LogErr(ex.Message);
            }
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
            return CommandsManager.WAIT_CMD + " " + numSecondsWait.Value;
        }

        private string generateCreateWorkProcess()
        {
            return CommandsManager.CREATE_WORK_PROCESS_CMD + " " + submitWorkerPMUrlMsgBox.Text + " " + submitWorkerPMUrlMsgBox.Text + " " + submitWorkerServiceUrlMsgBox.Text + " " + submitWorkerEntryUrlMsgBox.Text;
        }

        private string generateCreateJob()
        {
            return CommandsManager.SUBMIT_JOB_CMD + " " + submitTaskEntryUrlMsgBox.Text + " " + submitTaskSourceFileMsgBox.Text + " " + submitTaskDestFileMsgBox.Text + " " + submitTaskNumberSplits.Value + " " + submitJobMapTxtBox.Text + " " + submitJobDllTxtBox.Text;
        }

        private string generateFreezeWorker()
        {
            return CommandsManager.FREEZE_WORKER_CMD + " " + workerId.Text;
        }

        private string generateUnfreezeWorker()
        {
            return CommandsManager.UNFREEZE_WORKER_CMD + " " + workerId.Text;
        }

        private string generateDisableJobTracker()
        {
            return CommandsManager.DISABLE_JOBTRACKER_CMD + " " + workerId.Text;
        }

        private string generateEnableJobTracker()
        {
            return CommandsManager.ENABLE_JOBTRACKER_CMD + " " + workerId.Text;
        }

        private string generateSlowWorker()
        {
            return CommandsManager.DELAY_WORKER_CMD + " " + workerId.Text + " " + slowNumSeconds.Value;
        }

        private static string generateRefreshStatus()
        {
            return CommandsManager.STATUS_CMD;
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
            if (e.KeyCode == Keys.F5 || (e.KeyCode == Keys.R && e.Modifiers == Keys.Control))
            {
                submitCommandAux(CommandsManager.STATUS_CMD);
                e.Handled = true;
            }

            else if (e.KeyCode == Keys.C && e.Modifiers == (Keys.Control | Keys.Shift))
            {
                ClearConsoleAction();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F && e.Modifiers == (Keys.Control | Keys.Shift))
            {
                ExportConsoleToFile(getConsoleRichTextBox());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.C && e.Modifiers != (Keys.Control | Keys.Shift) && checkRunScriptTextBox())
            {
                submitCommandAux(commandMsgBox.Text);
                e.Handled = true;
            }


            else if (e.KeyCode == Keys.O)
            {
                OpenScriptFile();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.R && checkLocationTextBox())
            {
                submitScriptAux();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.W)
            {
                submitCommandAux(generateWaitCmd());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.G && checkCreateWorkerMsgsBox())
            {
                submitCommandAux(generateCreateWorkProcess());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.J && checkCreateJob())
            {
                submitCommandAux(generateCreateJob());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F && checkWorkerIdMsgBox())
            {
                submitCommandAux(generateFreezeWorker());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.U && checkWorkerIdMsgBox())
            {
                submitCommandAux(generateUnfreezeWorker());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.E && checkWorkerIdMsgBox())
            {
                submitCommandAux(generateEnableJobTracker());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.D && checkWorkerIdMsgBox())
            {
                submitCommandAux(generateDisableJobTracker());
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.S && checkWorkerIdMsgBox())
            {
                submitCommandAux(generateSlowWorker());
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
                submitWorkerPMUrlMsgBox.Text == "" || 
                submitWorkerServiceUrlMsgBox.Text == "" || 
                submitWorkerEntryUrlMsgBox.Text == "")
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
    }
}
