using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib;

namespace ClientInterface
{
    public partial class AgentStorageForm : Form
    {

        Client client = new Client();

        public AgentStorageForm()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                client.registerPerson(NameBox.Text, Int32.Parse(AgeBox.Text), Int32.Parse(AgentIDBox.Text));
                ErrorBox.ResetText();
            }
            catch (MyFormatException fException)
            {
                NameBox.Text = fException.getProblem();
            }
        }

        private void GetButton_Click(object sender, EventArgs e)
        {
            try
            {
                Person p = client.retrievePerson(Int32.Parse(AgentIDBox.Text));
                ErrorBox.ResetText();
                NameBox.Text = p.getName();
                AgeBox.Text = p.getAge().ToString();
                AgentIDBox.Text = p.getAgentID().ToString();
            }
            catch (MyFormatException fException)
            {
                ErrorBox.Text = fException.getProblem();
            }
        }


    }
}
