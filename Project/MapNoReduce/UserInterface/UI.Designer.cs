namespace PADIMapNoReduce
{
    partial class UI
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
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.SubmitFile = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.entryUrlTextBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.consoleMsgBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.submitJobDllTxtBox = new System.Windows.Forms.TextBox();
            this.submitJobMapTxtBox = new System.Windows.Forms.TextBox();
            this.destFileBtn = new System.Windows.Forms.Button();
            this.sourceFileBtn = new System.Windows.Forms.Button();
            this.submitTaskNumberSplits = new System.Windows.Forms.NumericUpDown();
            this.submitTaskButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.submitTaskDestFileMsgBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.submitTaskSourceFileMsgBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.submitTaskNumberSplits)).BeginInit();
            this.SuspendLayout();
            // 
            // FileTextBox
            // 
            this.FileTextBox.Location = new System.Drawing.Point(600, 26);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(481, 20);
            this.FileTextBox.TabIndex = 0;
            // 
            // SubmitFile
            // 
            this.SubmitFile.Location = new System.Drawing.Point(1125, 26);
            this.SubmitFile.Name = "SubmitFile";
            this.SubmitFile.Size = new System.Drawing.Size(115, 20);
            this.SubmitFile.TabIndex = 1;
            this.SubmitFile.Text = "Submit";
            this.SubmitFile.UseVisualStyleBackColor = true;
            this.SubmitFile.Click += new System.EventHandler(this.SubmitFile_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1087, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 20);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Find_file_btn_hdlr);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Entry URL: ";
            // 
            // entryUrlTextBox
            // 
            this.entryUrlTextBox.Location = new System.Drawing.Point(94, 215);
            this.entryUrlTextBox.Name = "entryUrlTextBox";
            this.entryUrlTextBox.Size = new System.Drawing.Size(478, 20);
            this.entryUrlTextBox.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(255, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 20);
            this.button2.TabIndex = 6;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // consoleMsgBox
            // 
            this.consoleMsgBox.BackColor = System.Drawing.Color.Black;
            this.consoleMsgBox.ForeColor = System.Drawing.Color.Lime;
            this.consoleMsgBox.Location = new System.Drawing.Point(12, 354);
            this.consoleMsgBox.Name = "consoleMsgBox";
            this.consoleMsgBox.Size = new System.Drawing.Size(710, 169);
            this.consoleMsgBox.TabIndex = 7;
            this.consoleMsgBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 338);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Log";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(401, 297);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label19.Size = new System.Drawing.Size(27, 13);
            this.label19.TabIndex = 112;
            this.label19.Text = "DLL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 298);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 111;
            this.label4.Text = "Map";
            // 
            // submitJobDllTxtBox
            // 
            this.submitJobDllTxtBox.Location = new System.Drawing.Point(434, 294);
            this.submitJobDllTxtBox.Name = "submitJobDllTxtBox";
            this.submitJobDllTxtBox.Size = new System.Drawing.Size(138, 20);
            this.submitJobDllTxtBox.TabIndex = 110;
            this.submitJobDllTxtBox.Text = "Common";
            // 
            // submitJobMapTxtBox
            // 
            this.submitJobMapTxtBox.Location = new System.Drawing.Point(201, 295);
            this.submitJobMapTxtBox.Name = "submitJobMapTxtBox";
            this.submitJobMapTxtBox.Size = new System.Drawing.Size(194, 20);
            this.submitJobMapTxtBox.TabIndex = 109;
            this.submitJobMapTxtBox.Text = "PADIMapNoReduce.DummyMapper";
            // 
            // destFileBtn
            // 
            this.destFileBtn.Location = new System.Drawing.Point(513, 268);
            this.destFileBtn.Name = "destFileBtn";
            this.destFileBtn.Size = new System.Drawing.Size(59, 20);
            this.destFileBtn.TabIndex = 108;
            this.destFileBtn.Text = "Choose";
            this.destFileBtn.UseVisualStyleBackColor = true;
            // 
            // sourceFileBtn
            // 
            this.sourceFileBtn.Location = new System.Drawing.Point(513, 241);
            this.sourceFileBtn.Name = "sourceFileBtn";
            this.sourceFileBtn.Size = new System.Drawing.Size(59, 20);
            this.sourceFileBtn.TabIndex = 107;
            this.sourceFileBtn.Text = "Choose";
            this.sourceFileBtn.UseVisualStyleBackColor = true;
            // 
            // submitTaskNumberSplits
            // 
            this.submitTaskNumberSplits.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitTaskNumberSplits.Location = new System.Drawing.Point(95, 295);
            this.submitTaskNumberSplits.Name = "submitTaskNumberSplits";
            this.submitTaskNumberSplits.Size = new System.Drawing.Size(62, 22);
            this.submitTaskNumberSplits.TabIndex = 106;
            // 
            // submitTaskButton
            // 
            this.submitTaskButton.Location = new System.Drawing.Point(578, 215);
            this.submitTaskButton.Name = "submitTaskButton";
            this.submitTaskButton.Size = new System.Drawing.Size(146, 100);
            this.submitTaskButton.TabIndex = 105;
            this.submitTaskButton.Text = "Submit [J]ob";
            this.submitTaskButton.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 297);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 104;
            this.label10.Text = "# Splits";
            // 
            // submitTaskDestFileMsgBox
            // 
            this.submitTaskDestFileMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitTaskDestFileMsgBox.Location = new System.Drawing.Point(94, 269);
            this.submitTaskDestFileMsgBox.Name = "submitTaskDestFileMsgBox";
            this.submitTaskDestFileMsgBox.Size = new System.Drawing.Size(412, 20);
            this.submitTaskDestFileMsgBox.TabIndex = 103;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 270);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 102;
            this.label11.Text = "Destination File";
            // 
            // submitTaskSourceFileMsgBox
            // 
            this.submitTaskSourceFileMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitTaskSourceFileMsgBox.Location = new System.Drawing.Point(94, 241);
            this.submitTaskSourceFileMsgBox.Name = "submitTaskSourceFileMsgBox";
            this.submitTaskSourceFileMsgBox.Size = new System.Drawing.Size(412, 20);
            this.submitTaskSourceFileMsgBox.TabIndex = 101;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 244);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 99;
            this.label12.Text = "Source File";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(47, 107);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 97;
            this.label14.Text = "SUBMIT";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 535);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.submitJobDllTxtBox);
            this.Controls.Add(this.submitJobMapTxtBox);
            this.Controls.Add(this.destFileBtn);
            this.Controls.Add(this.sourceFileBtn);
            this.Controls.Add(this.submitTaskNumberSplits);
            this.Controls.Add(this.submitTaskButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.submitTaskDestFileMsgBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.submitTaskSourceFileMsgBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.consoleMsgBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.entryUrlTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SubmitFile);
            this.Controls.Add(this.FileTextBox);
            this.Name = "UI";
            this.Text = "User Application";
            ((System.ComponentModel.ISupportInitialize)(this.submitTaskNumberSplits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FileTextBox;
        private System.Windows.Forms.Button SubmitFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox entryUrlTextBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox consoleMsgBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox submitJobDllTxtBox;
        private System.Windows.Forms.TextBox submitJobMapTxtBox;
        private System.Windows.Forms.Button destFileBtn;
        private System.Windows.Forms.Button sourceFileBtn;
        private System.Windows.Forms.NumericUpDown submitTaskNumberSplits;
        private System.Windows.Forms.Button submitTaskButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox submitTaskDestFileMsgBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox submitTaskSourceFileMsgBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
    }
}

