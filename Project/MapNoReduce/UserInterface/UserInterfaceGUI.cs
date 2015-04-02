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

    public partial class UI
#if DEBUG
        : DummyClass
#else
        : FormRemoteGUI
#endif
    {
        private Client client;

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

                client = new Client(this);
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
