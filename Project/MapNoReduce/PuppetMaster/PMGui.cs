using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapNoReduce
{
    public partial class GUIPupperMaster : Form
    {
        CommandsManager cm = new CommandsManager();
        int numLines = 0;
        public GUIPupperMaster()
        {
            InitializeComponent();
        }

        private void AppendText(RichTextBox box, Color color, string text)
        {
            string formatString = String.Format("[{0, 4}]:", numLines++);

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

        private void submitScript_Click(object sender, EventArgs e)
        {
            try
            {
                cm.LoadFile(scriptLocMsgBox.Text);
                LogInfo("Parse successfull");
                LogInfo(cm.ExecuteScript());
            }catch(Exception ex){
                LogError(ex.Message);
            }
            
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
            submitCommandAux(CommandsManager.WAIT_CMD + " " + numSecondsWait.Value);
        }

        private void submitWorkerButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.CREATE_WORK_PROCESS_CMD + " " + submitWorkerPMUrlMsgBox.Text + " " + submitWorkerPMUrlMsgBox.Text + " " + submitWorkerServiceUrlMsgBox.Text + " " + submitWorkerEntryUrlMsgBox.Text);
        }

        private void submitTaskButton_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.SUBMIT_JOB_CMD + " " + submitTaskEntryUrlMsgBox.Text + " " + submitTaskSourceFileMsgBox.Text + " " + submitTaskDestFileMsgBox.Text + " " + submitTaskNumberSplits.Value + " " + submitTaskClassMapperComboBox.Text);
        }

        private void freezewBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.FREEZE_WORKER_CMD + " " + workerId.Text);
        }

        private void unfreezewBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.UNFREEZE_WORKER_CMD + " " + workerId.Text);
        }

        private void freezecBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.DISABLE_JOBTRACKER_CMD + " " + workerId.Text);
        }

        private void unfreezecBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.ENABLE_JOBTRACKER_CMD + " " + workerId.Text);
        }

        private void slowBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.DELAY_WORKER_CMD + " " + workerId.Text + " " + slowNumSeconds.Value); 
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            submitCommandAux(CommandsManager.STATUS_CMD);
        }
        

    }
}
