using PADIMapNoReduce.Commands;
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
    public partial class CreateWorkerForm : Form
    {
        GUIPuppetMaster pmGUI;

        public CreateWorkerForm(GUIPuppetMaster pmGUI)
        {
            InitializeComponent();
            this.pmGUI = pmGUI;
        }

        private void CreateWorkerForm_Load(object sender, EventArgs e)
        {
            checkCreateWorkerMsgsBox();
        }


        public bool checkCreateWorkerMsgsBox()
        {
            if (submitWorkerWorkerIdMsgBox.Text == "" ||
                submitWorkerServiceUrlMsgBox.Text == "")
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

        private void textChanged(object sender, EventArgs e)
        {
            checkCreateWorkerMsgsBox();
        }

        private void submitWorkerButton_Click(object sender, EventArgs e)
        {
            string workerId = submitWorkerWorkerIdMsgBox.Text;
            string pmUrl = submitWorkerPMUrlMsgBox.Text;
            string serviceUrl = submitWorkerServiceUrlMsgBox.Text;
            string entryUrl = submitWorkerEntryUrlMsgBox.Text;

            string command = pmGUI.generateCreateWorkProcess(workerId, pmUrl, serviceUrl, entryUrl);

            pmGUI.submitCommandAux(command);
        }
    }
}
