namespace ChatForm
{
    partial class Chatter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RegisterButton = new System.Windows.Forms.Button();
            this.NickField = new System.Windows.Forms.TextBox();
            this.nick = new System.Windows.Forms.Label();
            this.portField = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.Label();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.Messages = new System.Windows.Forms.Label();
            this.sendMessageBox = new System.Windows.Forms.TextBox();
            this.Message = new System.Windows.Forms.Label();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(247, 53);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(75, 23);
            this.RegisterButton.TabIndex = 0;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // NickField
            // 
            this.NickField.Location = new System.Drawing.Point(12, 56);
            this.NickField.Name = "NickField";
            this.NickField.Size = new System.Drawing.Size(110, 20);
            this.NickField.TabIndex = 1;
            // 
            // nick
            // 
            this.nick.AutoSize = true;
            this.nick.Location = new System.Drawing.Point(12, 31);
            this.nick.Name = "nick";
            this.nick.Size = new System.Drawing.Size(29, 13);
            this.nick.TabIndex = 2;
            this.nick.Text = "Nick";
            // 
            // portField
            // 
            this.portField.Location = new System.Drawing.Point(148, 56);
            this.portField.Name = "portField";
            this.portField.Size = new System.Drawing.Size(66, 20);
            this.portField.TabIndex = 3;
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(145, 31);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(26, 13);
            this.Port.TabIndex = 4;
            this.Port.Text = "Port";
            // 
            // MessageBox
            // 
            this.MessageBox.Location = new System.Drawing.Point(12, 162);
            this.MessageBox.Multiline = true;
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.ReadOnly = true;
            this.MessageBox.Size = new System.Drawing.Size(310, 225);
            this.MessageBox.TabIndex = 5;
            // 
            // Messages
            // 
            this.Messages.AutoSize = true;
            this.Messages.Location = new System.Drawing.Point(12, 146);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(55, 13);
            this.Messages.TabIndex = 6;
            this.Messages.Text = "Messages";
            // 
            // sendMessageBox
            // 
            this.sendMessageBox.Location = new System.Drawing.Point(15, 110);
            this.sendMessageBox.Name = "sendMessageBox";
            this.sendMessageBox.Size = new System.Drawing.Size(199, 20);
            this.sendMessageBox.TabIndex = 7;
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Location = new System.Drawing.Point(15, 91);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(50, 13);
            this.Message.TabIndex = 8;
            this.Message.Text = "Message";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(247, 110);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 9;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // Chatter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 399);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.sendMessageBox);
            this.Controls.Add(this.Messages);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.portField);
            this.Controls.Add(this.nick);
            this.Controls.Add(this.NickField);
            this.Controls.Add(this.RegisterButton);
            this.Name = "Chatter";
            this.Text = "Chatter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.TextBox NickField;
        private System.Windows.Forms.Label nick;
        private System.Windows.Forms.TextBox portField;
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.TextBox MessageBox;
        private System.Windows.Forms.Label Messages;
        private System.Windows.Forms.TextBox sendMessageBox;
        private System.Windows.Forms.Label Message;
        private System.Windows.Forms.Button SendButton;
    }
}

