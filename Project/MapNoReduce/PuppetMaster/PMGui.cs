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

        public GUIPupperMaster()
        {
            InitializeComponent();
        }

        void AppendText(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText("\r\n " + text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }

        public void LogInfo(string s){
            AppendText(consoleMessageBox, System.Drawing.Color.Lime, "[INFO]: " + s);
        }

        public void LogError(string s)
        {
            AppendText(consoleMessageBox, System.Drawing.Color.Red, "[ERROR]: " + s);
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


        private void submitCommand_Click(object sender, EventArgs e)
        {
            try { 
                LogInfo(cm.ExecuteCommand(cm.ParseCommand(commandMsgBox.Text)));
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
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

        
        private void GUIPupperMaster_Resize(object sender, EventArgs e)
        {
            int width = consoleMessageBox.Location.X + consoleMessageBox.Size.Width - ConsoleLabel.Location.X;
            consoleMessageBox.SetBounds(ConsoleLabel.Location.X, consoleMessageBox.Location.Y, width, consoleMessageBox.Size.Height);
            refreshBtn.SetBounds(ConsoleLabel.Location.X, refreshBtn.Location.Y, width, refreshBtn.Size.Height);
        }
        

    }
}
