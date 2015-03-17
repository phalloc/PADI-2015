using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotingSample
{
    public partial class Chatter : FormRemoteGUI
    {

        private Client client = null;

        public Chatter()
        {
            InitializeComponent();
            client = new Client(this);
        }

        override public void UpdateForm(string msg)
        {
            MessageBox.AppendText("\r\n" + msg);
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            client.Register(NickField.Text, portField.Text);
        }

        private void SendButton_Click(object sender, System.EventArgs e)
        {
            client.SendMessage(sendMessageBox.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
