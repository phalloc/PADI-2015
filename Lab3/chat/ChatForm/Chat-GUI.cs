using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RemotingSample;

namespace ChatForm
{
    public partial class Chatter : Form
    {

        private Client client = new Client();

        public Chatter()
        {
            InitializeComponent();
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
