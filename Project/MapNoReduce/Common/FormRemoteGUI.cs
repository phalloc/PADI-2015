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

    public delegate void LogInfoDel(Color prefixTextColor, Color prefixBackColor, Color textBackColor, Color msgTextColor, string prefixMsg, string text);
    public delegate void LogErrDel(Color prefixTextColor, Color prefixBackColor, Color textBackColor, Color msgTextColor, string prefixMsg, string text);
    public delegate void LogWarnDel(Color prefixTextColor, Color prefixBackColor, Color textBackColor, Color msgTextColor, string prefixMsg, string text);

    public delegate void RefreshDel();


    abstract public class FormRemoteGUI : Form
    {
        abstract public RichTextBox getConsoleRichTextBox();
        abstract public void RefreshRemote();

        public FormRemoteGUI()
        {
            Logger.initializeForm(this);
        }

        protected void ClearConsole()
        {
            getConsoleRichTextBox().Clear();
        }

        
        public void AppendText(Color prefixTextColor, Color prefixBackColor, Color textBackColor, Color msgTextColor, string prefixMsg, string text)
        {
            RichTextBox box = getConsoleRichTextBox();

            AppendTextAux(prefixTextColor, prefixBackColor, prefixMsg, false);
            AppendTextAux(msgTextColor, textBackColor, text, true);


            box.SelectionStart = box.Text.Length;
            box.ScrollToCaret();
        }
        


        public void AppendTextAux(Color textColor, Color backColor, string text, bool newLine)
        {
            RichTextBox box = getConsoleRichTextBox();

            int start = box.TextLength;

            string s = newLine ? text + "\r\n" : text;

            box.AppendText(s);
            int end = box.TextLength;

            box.Select(start, end - start);
            {
                box.SelectionColor = textColor;
                box.SelectionBackColor = backColor;
            }

            box.SelectionLength = 0; // clear
        }


        protected string FindDestinationFile(string filter, string msg)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = ".";
            saveFileDialog1.Filter = filter;
            saveFileDialog1.Title = msg;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }

            return "";
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
            saveFileDialog1.InitialDirectory = ".";
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
                Logger.LogInfo("Saved to " + fileName);
            }
        }

        protected string FindSourceFile(string filter, string msg)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = ".";
            openFileDialog1.Filter = filter;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = msg ;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }

            return "";
        }

    }
}
