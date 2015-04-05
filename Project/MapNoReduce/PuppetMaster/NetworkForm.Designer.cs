namespace PADIMapNoReduce
{
    partial class NetworkForm
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
            this.NetworkTreeView = new System.Windows.Forms.TreeView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NetworkTreeView
            // 
            this.NetworkTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NetworkTreeView.Location = new System.Drawing.Point(12, 44);
            this.NetworkTreeView.Margin = new System.Windows.Forms.Padding(20);
            this.NetworkTreeView.Name = "NetworkTreeView";
            this.NetworkTreeView.Size = new System.Drawing.Size(285, 501);
            this.NetworkTreeView.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(306, 44);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(285, 501);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Ring Configuration will be shown here. Refresh in Main Window, then refresh here." +
    "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshTreeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(598, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // refreshTreeToolStripMenuItem
            // 
            this.refreshTreeToolStripMenuItem.Name = "refreshTreeToolStripMenuItem";
            this.refreshTreeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshTreeToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.refreshTreeToolStripMenuItem.Text = "Refresh Tree F5";
            this.refreshTreeToolStripMenuItem.Click += new System.EventHandler(this.refreshTreeToolStripMenuItem_Click);
            // 
            // GraphLabel
            // 
            this.GraphLabel.AutoSize = true;
            this.GraphLabel.Location = new System.Drawing.Point(303, 29);
            this.GraphLabel.Name = "GraphLabel";
            this.GraphLabel.Size = new System.Drawing.Size(36, 13);
            this.GraphLabel.TabIndex = 4;
            this.GraphLabel.Text = "Graph";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tree View";
            // 
            // NetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 552);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GraphLabel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.NetworkTreeView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(614, 591);
            this.Name = "NetworkForm";
            this.Text = "Network";
            this.Resize += new System.EventHandler(this.NetworkForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView NetworkTreeView;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshTreeToolStripMenuItem;
        private System.Windows.Forms.Label GraphLabel;
        private System.Windows.Forms.Label label2;
    }
}