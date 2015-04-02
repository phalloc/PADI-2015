using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PADIMapNoReduce
{

    public delegate void LogInfoDel(string msg);
    public delegate void LogErrDel(string msg);
    public delegate void LogWarnDel(string msg);

    abstract public class FormRemoteGUI : Form
    {
        private static Color WARN_COLOR = Color.Yellow;
        private static Color ERROR_COLOR = Color.Red;
        private static Color INFO_COLOR = Color.Lime;

        private int numLines = 0;

        abstract public RichTextBox getConsoleRichTextBox();

        protected void ClearConsole()
        {
            getConsoleRichTextBox().Clear();
        }

        public void LogInfo(string msg) 
        {
            AppendText(getConsoleRichTextBox(), INFO_COLOR, msg);
        }

        public void LogErr(string msg) 
        {
            AppendText(getConsoleRichTextBox(), ERROR_COLOR, msg);
        }
        public void LogWarn(string msg)
        {
            AppendText(getConsoleRichTextBox(), WARN_COLOR, msg);
        }


        public void LogInfo(List<string> listS)
        {
            foreach (string s in listS)
            {
                LogInfo(s);
            }
        }

        public void LogErr(List<string> listS)
        {
            foreach (string s in listS)
            {
                LogErr(s);
            }
        }
        public void LogWarn(List<string> listS)
        {
            foreach (string s in listS)
            {
                LogWarn(s);
            }
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


        protected void FindDestinationFile(TextBox dest, string filter, string msg)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = filter;
            saveFileDialog1.Title = msg;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dest.Text = saveFileDialog1.FileName;
            }
        }

        protected void ClearConsoleAction()
        {
            DialogResult dr = MessageBox.Show("Are you sure? \r\n\r\nThis will clear the Log.",
                        "Caption", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes: ClearConsole(); break;
                case DialogResult.No: break;
            }
        }

        protected void ExportConsoleToFile(RichTextBox source)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File | *.txt";
            saveFileDialog1.Title = "Export to a text File";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // create a writer and open the file
                string fileName = saveFileDialog1.FileName;
                TextWriter tw = new StreamWriter(fileName);
                tw.WriteLine(source.Text);
                // close the stream
                tw.Close();
                LogInfo("Saved to " + fileName);
            }
        }

        protected void FindSourceFile(TextBox dest, string filter, string msg)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = filter;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = msg ;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                dest.Text = openFileDialog1.FileName;
            }
        }

    }
}
