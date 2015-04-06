namespace PADIMapNoReduce
{
    partial class CreateWorkerForm
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
            this.submitWorkerButton = new System.Windows.Forms.Button();
            this.submitWorkerEntryUrlMsgBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.submitWorkerServiceUrlMsgBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.submitWorkerPMUrlMsgBox = new System.Windows.Forms.TextBox();
            this.submitWorkerWorkerIdMsgBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // submitWorkerButton
            // 
            this.submitWorkerButton.Location = new System.Drawing.Point(580, 12);
            this.submitWorkerButton.Name = "submitWorkerButton";
            this.submitWorkerButton.Size = new System.Drawing.Size(146, 98);
            this.submitWorkerButton.TabIndex = 43;
            this.submitWorkerButton.Text = "Generate Worker";
            this.submitWorkerButton.UseVisualStyleBackColor = true;
            this.submitWorkerButton.Click += new System.EventHandler(this.submitWorkerButton_Click);
            // 
            // submitWorkerEntryUrlMsgBox
            // 
            this.submitWorkerEntryUrlMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitWorkerEntryUrlMsgBox.Location = new System.Drawing.Point(96, 90);
            this.submitWorkerEntryUrlMsgBox.Name = "submitWorkerEntryUrlMsgBox";
            this.submitWorkerEntryUrlMsgBox.Size = new System.Drawing.Size(478, 20);
            this.submitWorkerEntryUrlMsgBox.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Entry-Url:";
            // 
            // submitWorkerServiceUrlMsgBox
            // 
            this.submitWorkerServiceUrlMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitWorkerServiceUrlMsgBox.Location = new System.Drawing.Point(96, 64);
            this.submitWorkerServiceUrlMsgBox.Name = "submitWorkerServiceUrlMsgBox";
            this.submitWorkerServiceUrlMsgBox.Size = new System.Drawing.Size(478, 20);
            this.submitWorkerServiceUrlMsgBox.TabIndex = 40;
            this.submitWorkerServiceUrlMsgBox.Text = "tcp://localhost:30001/W";
            this.submitWorkerServiceUrlMsgBox.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Service-Url:";
            // 
            // submitWorkerPMUrlMsgBox
            // 
            this.submitWorkerPMUrlMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitWorkerPMUrlMsgBox.Location = new System.Drawing.Point(96, 38);
            this.submitWorkerPMUrlMsgBox.Name = "submitWorkerPMUrlMsgBox";
            this.submitWorkerPMUrlMsgBox.Size = new System.Drawing.Size(478, 20);
            this.submitWorkerPMUrlMsgBox.TabIndex = 38;
            // 
            // submitWorkerWorkerIdMsgBox
            // 
            this.submitWorkerWorkerIdMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitWorkerWorkerIdMsgBox.Location = new System.Drawing.Point(96, 12);
            this.submitWorkerWorkerIdMsgBox.Name = "submitWorkerWorkerIdMsgBox";
            this.submitWorkerWorkerIdMsgBox.Size = new System.Drawing.Size(478, 20);
            this.submitWorkerWorkerIdMsgBox.TabIndex = 37;
            this.submitWorkerWorkerIdMsgBox.Text = "W1";
            this.submitWorkerWorkerIdMsgBox.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "PupperMaster-Url:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(74, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, -4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 34;
            // 
            // CreateWorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 124);
            this.Controls.Add(this.submitWorkerButton);
            this.Controls.Add(this.submitWorkerEntryUrlMsgBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.submitWorkerServiceUrlMsgBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.submitWorkerPMUrlMsgBox);
            this.Controls.Add(this.submitWorkerWorkerIdMsgBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(754, 180);
            this.Name = "CreateWorkerForm";
            this.Text = "Create Worker";
            this.Load += new System.EventHandler(this.CreateWorkerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submitWorkerButton;
        private System.Windows.Forms.TextBox submitWorkerEntryUrlMsgBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox submitWorkerServiceUrlMsgBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox submitWorkerPMUrlMsgBox;
        private System.Windows.Forms.TextBox submitWorkerWorkerIdMsgBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}