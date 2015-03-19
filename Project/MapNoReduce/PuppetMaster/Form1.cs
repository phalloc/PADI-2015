﻿using System;
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

        public void Log(string s){
            consoleMessageBox.AppendText("\r\n" + s);
        }

        public void Log(List<string> listS)
        {
            foreach (string s in listS)
            {
                Log(s);

            }
        }

        private void submitCommand_Click(object sender, EventArgs e)
        {
            Log(cm.ExecuteCommand(cm.ParseCommand(commandMsgBox.Text)));
        }

        private void submitScript_Click(object sender, EventArgs e)
        {
            if (cm.LoadFile(scriptLocMsgBox.Text))
            {
                Log("Parse successfull");
                Log(cm.ExecuteScript());
            }
            else
            {
                Log("File parse error");
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
