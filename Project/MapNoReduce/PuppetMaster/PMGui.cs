using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MapNoReduce
{
    public partial class GUIPupperMaster : Form
    {

        static string INTRO_TEXT = 
            "     __  ___            _   __      ____           __              \r\n" +
            "    /  |/  /___ _____  / | / /___  / __ \\___  ____/ /_  __________ \r\n" + 
            "   / /|_/ / __ `/ __ \\/  |/ / __ \\/ /_/ / _ \\/ __  / / / / ___/ _ \\ \r\n" + 
            "  / /  / / /_/ / /_/ / /|  / /_/ / _, _/  __/ /_/ / /_/ / /__/  __/ \r\n" +
            " /_/  /_/\\__,_/ .___/_/ |_/\\____/_/ |_|\\___/\\__,_/\\__,_/\\___/\\___/ \r\n" + 
            "             /_/                                                   \r\n\r\n";

        CommandsManager cm = new CommandsManager();
        int numLines = 0;
        public GUIPupperMaster()
        {
            InitializeComponent();

            checkLocationTextBox();
            checkCreateJob();
            setWorkerCommandsBtnsState(false);
            checkCreateWorkerMsgsBox();
            
            ClearConsole();
        }

        private void ClearConsole()
        {
            consoleMessageBox.Clear();
            AppendTextAux(consoleMessageBox, System.Drawing.Color.DeepSkyBlue, System.Drawing.Color.Black, INTRO_TEXT, false);
        }

        private void AppendText(RichTextBox box, Color color, string text)
        {
            string formatString = String.Format("[{0, 4} - " + DateTime.Now.ToString("HH:mm:ss") + "]:", numLines++);

            AppendTextAux(box, System.Drawing.Color.Black, System.Drawing.Color.Gainsboro, formatString, true);

            string padding = "";
            for (int i = 0; i < formatString.Length; i++)
            {
                padding += " ";
            }

            AppendTextAux(box, color, System.Drawing.Color.Black, " " + text.Replace("\r\n", "\r\n" + padding), false);

            box.SelectionStart = box.Text.Length;
            box.ScrollToCaret();
        }
        
        private void AppendTextAux(RichTextBox box, Color textColor, Color backColor, string text, bool isLineNumber)
        {
            
            int start = box.TextLength;

            string s = isLineNumber ? (numLines == 1 ? text : "\r\n" + text) : text ;

            box.AppendText(s);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = textColor;
                box.SelectionBackColor = backColor;
            }

            box.SelectionLength = 0; // clear
        }

        public void LogInfo(string s){
            AppendText(consoleMessageBox, System.Drawing.Color.Lime, s);
        }

        public void LogError(string s)
        {
            AppendText(consoleMessageBox, System.Drawing.Color.Red, s);
        }

        public void LogInfo(List<string> listS)
        {
            foreach (string s in listS)
            {
                LogInfo(s);
            }
        }


        public void LogError(List<string> listS)
        {
            foreach (string s in listS)
            {
                LogError(s);
            }
        }

        private void submitCommandAux(string line)
        {
            try
            {
                LogInfo(cm.ExecuteCommand(cm.ParseCommand(line)));
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
        }

        private void submitCommand_Click(object sender, EventArgs e)
        {
            submitCommandAux(commandMsgBox.Text);
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
                LogError(ex.Message);
            }
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

        private string generateWaitCmd()
        {
            return CommandsManager.WAIT_CMD + " " + numSecondsWait.Value;
        }

        private void submitWorkerButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateCreateWorkProcess());
        }

        private string generateCreateWorkProcess()
        {
            return CommandsManager.CREATE_WORK_PROCESS_CMD + " " + submitWorkerPMUrlMsgBox.Text + " " + submitWorkerPMUrlMsgBox.Text + " " + submitWorkerServiceUrlMsgBox.Text + " " + submitWorkerEntryUrlMsgBox.Text;
        }

        private void submitTaskButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateCreateJob());
        }

        private string generateCreateJob()
        {
            return CommandsManager.SUBMIT_JOB_CMD + " " + submitTaskEntryUrlMsgBox.Text + " " + submitTaskSourceFileMsgBox.Text + " " + submitTaskDestFileMsgBox.Text + " " + submitTaskNumberSplits.Value + " " + submitTaskClassMapperComboBox.Text;
        }

        private void freezewBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateFreezeWorker());
        }

        private string generateFreezeWorker()
        {
            return CommandsManager.FREEZE_WORKER_CMD + " " + workerId.Text;
        }

        private void unfreezewBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateUnfreezeWorker());
        }

        private string generateUnfreezeWorker()
        {
            return CommandsManager.UNFREEZE_WORKER_CMD + " " + workerId.Text;
        }

        private void freezecBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateDisableJobTracker());
        }

        private string generateDisableJobTracker()
        {
            return CommandsManager.DISABLE_JOBTRACKER_CMD + " " + workerId.Text;
        }

        private void unfreezecBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateEnableJobTracker());
        }

        private string generateEnableJobTracker()
        {
            return CommandsManager.ENABLE_JOBTRACKER_CMD + " " + workerId.Text;
        }

        private void slowBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(generateSlowWorker()); 
        }

        private string generateSlowWorker()
        {
            return CommandsManager.DELAY_WORKER_CMD + " " + workerId.Text + " " + slowNumSeconds.Value;
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.STATUS_CMD);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportConsoleToFile();
        }

        private void ExportConsoleToFile()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File | *.txt";
            saveFileDialog1.Title = "Export to a text File";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // create a writer and open the file
                string fileName = saveFileDialog1.FileName;
                TextWriter tw = new StreamWriter(fileName);
                tw.WriteLine(consoleMessageBox.Text);
                // close the stream
                tw.Close();
                LogInfo("Saved to " + fileName);
            }
        }

        private void OpenScriptFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Choose Script Source File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                scriptLocMsgBox.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenScriptFile();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Choose Script Source File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                scriptLocMsgBox.Text = openFileDialog1.FileName;
            }
        }

        private void sourceFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Choose Source File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                submitTaskSourceFileMsgBox.Text = openFileDialog1.FileName;
            }
        }

        private void destFileBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File | *.txt";
            saveFileDialog1.Title = "Choose Destination File";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                submitTaskDestFileMsgBox.Text = saveFileDialog1.FileName;
            }

        }

        private void workerId_TextChanged(object sender, EventArgs e)
        {
            setWorkerCommandsBtnsState(checkWorkerIdMsgBox());            
        }

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

        private void commandMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkRunScriptTextBox();
        }

        private void scriptLocMsgBox_TextChanged(object sender, EventArgs e)
        {
            checkLocationTextBox();
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
                submitTaskClassMapperComboBox.Text == "")
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
                ExportConsoleToFile();
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

        private void ClearConsoleAction()
        {
            DialogResult dr = MessageBox.Show("Are you sure? \r\n\r\nThis will clear the Log.",
                        "Caption", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes: ClearConsole(); break;
                case DialogResult.No: break;
            }
        }

    }
}
