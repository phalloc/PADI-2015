using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    public partial class UI : Form
    {

        int numLines = 0;

        public UI()
        {
            InitializeComponent();
        }


        private void ClearConsole()
        {
            consoleMsgBox.Clear();
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

            string s = isLineNumber ? (numLines == 1 ? text : "\r\n" + text) : text;

            box.AppendText(s);
            int end = box.TextLength;

            box.Select(start, end - start);
            {
                box.SelectionColor = textColor;
                box.SelectionBackColor = backColor;
            }

            box.SelectionLength = 0; // clear
        }

        public void LogInfo(string s)
        {
            AppendText(consoleMsgBox, System.Drawing.Color.Lime, s);
        }

        public void LogError(string s)
        {
            AppendText(consoleMsgBox, System.Drawing.Color.Red, s);
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

        private void ChooseJobSourceFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Input files (*.in)|*.in";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Choose Source File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileTextBox.Text = openFileDialog1.FileName;
            }
        }

        private void SubmitFile_Click(object sender, EventArgs e)
        {
            LogInfo("Submitting...");
        }

        private void Find_file_btn_hdlr(object sender, EventArgs e)
        {
            ChooseJobSourceFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogInfo("Connecting...");
        }


    }
}
