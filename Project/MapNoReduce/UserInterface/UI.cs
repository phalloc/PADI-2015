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
    public partial class UI : FormRemoteGUI
    {
        private Client client = new Client();

        public UI()
        {
            InitializeComponent();
        }

        override public RichTextBox getConsoleRichTextBox()
        {
            return consoleMsgBox;
        }

        private void SubmitFile_Click(object sender, EventArgs e)
        {
            try
            {
                string destPath = submitTaskDestFileMsgBox.Text;
                string sourcePath = FileTextBox.Text;
                string entryUrl = entryUrlTextBox.Text;
                int numSplits = Convert.ToInt32(submitTaskNumberSplits.Value);
                string mappperInfo =  submitJobMapTxtBox.Text + ","  + submitJobDllTxtBox.Text;
                IMapper map = (IMapper)Activator.CreateInstance(Type.GetType(mappperInfo));
                LogInfo("Submitting: ");   
                LogInfo("Source Path: " + sourcePath);
                LogInfo("Destination Path: " + destPath);
                LogInfo("Entry Url: " + entryUrl);
                LogInfo("Number of splits " + numSplits);
                LogInfo("Mapper: " + mappperInfo);

                client.submitJob(sourcePath, destPath, entryUrl, numSplits, map);
            }
            catch (Exception ex)
            {
                LogErr("Error while submitting: " + ex.Message);
            }
        }

        private void Find_file_btn_hdlr(object sender, EventArgs e)
        {
            FindSourceFile(FileTextBox, "Input files (*.in)|*.in", "Choose Source File" );
        }

        private void destFileBtn_Click(object sender, EventArgs e)
        {
            FindDestinationFile(submitTaskDestFileMsgBox, "Output File | *.out", "Choose Destination File");
        }
    }
}
