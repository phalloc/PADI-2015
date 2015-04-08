namespace PADIMapNoReduce
{
    partial class SubmitJobForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.submitJobMapTxtBox = new System.Windows.Forms.TextBox();
            this.destFileBtn = new System.Windows.Forms.Button();
            this.sourceFileBtn = new System.Windows.Forms.Button();
            this.submitTaskNumberSplits = new System.Windows.Forms.NumericUpDown();
            this.submitTaskButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.submitTaskDestFileMsgBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.submitTaskSourceFileMsgBox = new System.Windows.Forms.TextBox();
            this.submitTaskEntryUrlMsgBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dllLocationChooseBtn = new System.Windows.Forms.Button();
            this.dllLocationMsgBox = new System.Windows.Forms.TextBox();
            this.dllLocation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.submitTaskNumberSplits)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 94);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 111;
            this.label3.Text = "Map:";
            // 
            // submitJobMapTxtBox
            // 
            this.submitJobMapTxtBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitJobMapTxtBox.Location = new System.Drawing.Point(104, 92);
            this.submitJobMapTxtBox.Name = "submitJobMapTxtBox";
            this.submitJobMapTxtBox.Size = new System.Drawing.Size(515, 20);
            this.submitJobMapTxtBox.TabIndex = 109;
            this.submitJobMapTxtBox.Text = "PADIMapNoReduce.DummyMapper";
            this.submitJobMapTxtBox.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // destFileBtn
            // 
            this.destFileBtn.Location = new System.Drawing.Point(560, 64);
            this.destFileBtn.Name = "destFileBtn";
            this.destFileBtn.Size = new System.Drawing.Size(59, 20);
            this.destFileBtn.TabIndex = 108;
            this.destFileBtn.Text = "Choose";
            this.destFileBtn.UseVisualStyleBackColor = true;
            this.destFileBtn.Click += new System.EventHandler(this.destFileBtn_Click);
            // 
            // sourceFileBtn
            // 
            this.sourceFileBtn.Location = new System.Drawing.Point(560, 38);
            this.sourceFileBtn.Name = "sourceFileBtn";
            this.sourceFileBtn.Size = new System.Drawing.Size(59, 20);
            this.sourceFileBtn.TabIndex = 107;
            this.sourceFileBtn.Text = "Choose";
            this.sourceFileBtn.UseVisualStyleBackColor = true;
            this.sourceFileBtn.Click += new System.EventHandler(this.sourceFileBtn_Click);
            // 
            // submitTaskNumberSplits
            // 
            this.submitTaskNumberSplits.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitTaskNumberSplits.Location = new System.Drawing.Point(557, 146);
            this.submitTaskNumberSplits.Name = "submitTaskNumberSplits";
            this.submitTaskNumberSplits.Size = new System.Drawing.Size(62, 22);
            this.submitTaskNumberSplits.TabIndex = 106;
            this.submitTaskNumberSplits.Leave += new System.EventHandler(this.submitTaskNumberSplits_Leave);
            // 
            // submitTaskButton
            // 
            this.submitTaskButton.Location = new System.Drawing.Point(625, 12);
            this.submitTaskButton.Name = "submitTaskButton";
            this.submitTaskButton.Size = new System.Drawing.Size(154, 154);
            this.submitTaskButton.TabIndex = 105;
            this.submitTaskButton.Text = "Submit Job";
            this.submitTaskButton.UseVisualStyleBackColor = true;
            this.submitTaskButton.Click += new System.EventHandler(this.submitTaskButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(506, 153);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 104;
            this.label10.Text = "# Splits:";
            // 
            // submitTaskDestFileMsgBox
            // 
            this.submitTaskDestFileMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitTaskDestFileMsgBox.Location = new System.Drawing.Point(104, 64);
            this.submitTaskDestFileMsgBox.Name = "submitTaskDestFileMsgBox";
            this.submitTaskDestFileMsgBox.Size = new System.Drawing.Size(450, 20);
            this.submitTaskDestFileMsgBox.TabIndex = 103;
            this.submitTaskDestFileMsgBox.Text = "*.out directory";
            this.submitTaskDestFileMsgBox.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 13);
            this.label11.TabIndex = 102;
            this.label11.Text = "Destination Folder:";
            // 
            // submitTaskSourceFileMsgBox
            // 
            this.submitTaskSourceFileMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitTaskSourceFileMsgBox.Location = new System.Drawing.Point(104, 38);
            this.submitTaskSourceFileMsgBox.Name = "submitTaskSourceFileMsgBox";
            this.submitTaskSourceFileMsgBox.Size = new System.Drawing.Size(450, 20);
            this.submitTaskSourceFileMsgBox.TabIndex = 101;
            this.submitTaskSourceFileMsgBox.Text = "*.in Input file";
            this.submitTaskSourceFileMsgBox.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // submitTaskEntryUrlMsgBox
            // 
            this.submitTaskEntryUrlMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitTaskEntryUrlMsgBox.Location = new System.Drawing.Point(104, 12);
            this.submitTaskEntryUrlMsgBox.Name = "submitTaskEntryUrlMsgBox";
            this.submitTaskEntryUrlMsgBox.Size = new System.Drawing.Size(515, 20);
            this.submitTaskEntryUrlMsgBox.TabIndex = 100;
            this.submitTaskEntryUrlMsgBox.Text = "tcp://localhost:30001/W";
            this.submitTaskEntryUrlMsgBox.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 99;
            this.label12.Text = "Source File:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(48, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 98;
            this.label13.Text = "Entry-Url:";
            // 
            // dllLocationChooseBtn
            // 
            this.dllLocationChooseBtn.Location = new System.Drawing.Point(560, 120);
            this.dllLocationChooseBtn.Name = "dllLocationChooseBtn";
            this.dllLocationChooseBtn.Size = new System.Drawing.Size(59, 20);
            this.dllLocationChooseBtn.TabIndex = 115;
            this.dllLocationChooseBtn.Text = "Choose";
            this.dllLocationChooseBtn.UseVisualStyleBackColor = true;
            this.dllLocationChooseBtn.Click += new System.EventHandler(this.dllLocationChooseBtn_Click);
            // 
            // dllLocationMsgBox
            // 
            this.dllLocationMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dllLocationMsgBox.Location = new System.Drawing.Point(104, 118);
            this.dllLocationMsgBox.Name = "dllLocationMsgBox";
            this.dllLocationMsgBox.Size = new System.Drawing.Size(450, 20);
            this.dllLocationMsgBox.TabIndex = 114;
            this.dllLocationMsgBox.Text = "Common.dll";
            this.dllLocationMsgBox.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // dllLocation
            // 
            this.dllLocation.AutoSize = true;
            this.dllLocation.Location = new System.Drawing.Point(54, 120);
            this.dllLocation.Name = "dllLocation";
            this.dllLocation.Size = new System.Drawing.Size(44, 13);
            this.dllLocation.TabIndex = 113;
            this.dllLocation.Text = "Dll File: ";
            // 
            // SubmitJobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 176);
            this.Controls.Add(this.dllLocationChooseBtn);
            this.Controls.Add(this.dllLocationMsgBox);
            this.Controls.Add(this.dllLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.submitJobMapTxtBox);
            this.Controls.Add(this.destFileBtn);
            this.Controls.Add(this.sourceFileBtn);
            this.Controls.Add(this.submitTaskNumberSplits);
            this.Controls.Add(this.submitTaskButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.submitTaskDestFileMsgBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.submitTaskSourceFileMsgBox);
            this.Controls.Add(this.submitTaskEntryUrlMsgBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.MaximizeBox = false;
            this.Name = "SubmitJobForm";
            this.Text = "Submit Job";
            this.Load += new System.EventHandler(this.SubmitJobForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.submitTaskNumberSplits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox submitJobMapTxtBox;
        private System.Windows.Forms.Button destFileBtn;
        private System.Windows.Forms.Button sourceFileBtn;
        private System.Windows.Forms.NumericUpDown submitTaskNumberSplits;
        private System.Windows.Forms.Button submitTaskButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox submitTaskDestFileMsgBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox submitTaskSourceFileMsgBox;
        private System.Windows.Forms.TextBox submitTaskEntryUrlMsgBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button dllLocationChooseBtn;
        private System.Windows.Forms.TextBox dllLocationMsgBox;
        private System.Windows.Forms.Label dllLocation;
    }
}