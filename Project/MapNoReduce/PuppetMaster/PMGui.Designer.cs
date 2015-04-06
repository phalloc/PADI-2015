namespace PADIMapNoReduce
{
    partial class GUIPuppetMaster
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
            this.components = new System.ComponentModel.Container();
            this.consoleMessageBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.commandMsgBox = new System.Windows.Forms.TextBox();
            this.submitCommand = new System.Windows.Forms.Button();
            this.slowNumSeconds = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.ConsoleLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.myScripttxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createWorkerstxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pMpropertiesconfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createWorkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshF5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workerexeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientexeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NetworkTreeView = new System.Windows.Forms.TreeView();
            this.workerMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.freezeWorkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unfreezeWorkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freezeJobTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unfreezeJobTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseAllBtn = new System.Windows.Forms.Button();
            this.ExpandAllBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.ClearTreeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.slowNumSeconds)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.workerMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // consoleMessageBox
            // 
            this.consoleMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleMessageBox.BackColor = System.Drawing.Color.Black;
            this.consoleMessageBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleMessageBox.ForeColor = System.Drawing.Color.Lime;
            this.consoleMessageBox.Location = new System.Drawing.Point(746, 27);
            this.consoleMessageBox.Name = "consoleMessageBox";
            this.consoleMessageBox.ReadOnly = true;
            this.consoleMessageBox.Size = new System.Drawing.Size(556, 575);
            this.consoleMessageBox.TabIndex = 85;
            this.consoleMessageBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Command:";
            // 
            // commandMsgBox
            // 
            this.commandMsgBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandMsgBox.Location = new System.Drawing.Point(71, 43);
            this.commandMsgBox.Name = "commandMsgBox";
            this.commandMsgBox.Size = new System.Drawing.Size(577, 20);
            this.commandMsgBox.TabIndex = 74;
            this.commandMsgBox.Text = "SUBMIT <ENTRY-URL> <FILE> <OUTPUT> 9999 PADIMapNoReduce.DummyMapper Common";
            this.commandMsgBox.TextChanged += new System.EventHandler(this.commandMsgBox_TextChanged);
            // 
            // submitCommand
            // 
            this.submitCommand.Location = new System.Drawing.Point(654, 41);
            this.submitCommand.Name = "submitCommand";
            this.submitCommand.Size = new System.Drawing.Size(86, 23);
            this.submitCommand.TabIndex = 73;
            this.submitCommand.Text = "Run";
            this.submitCommand.UseVisualStyleBackColor = true;
            this.submitCommand.Click += new System.EventHandler(this.submitCommand_Click);
            // 
            // slowNumSeconds
            // 
            this.slowNumSeconds.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slowNumSeconds.Location = new System.Drawing.Point(517, 121);
            this.slowNumSeconds.Name = "slowNumSeconds";
            this.slowNumSeconds.Size = new System.Drawing.Size(61, 22);
            this.slowNumSeconds.TabIndex = 68;
            this.slowNumSeconds.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.slowNumSeconds.Leave += new System.EventHandler(this.slowNumSeconds_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(72, 703);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(0, 13);
            this.label20.TabIndex = 40;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(449, 125);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 37;
            this.label18.Text = "# Seconds:";
            // 
            // ConsoleLabel
            // 
            this.ConsoleLabel.AutoSize = true;
            this.ConsoleLabel.Location = new System.Drawing.Point(743, 9);
            this.ConsoleLabel.Name = "ConsoleLabel";
            this.ConsoleLabel.Size = new System.Drawing.Size(45, 13);
            this.ConsoleLabel.TabIndex = 6;
            this.ConsoleLabel.Text = "Console";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.createWorkerToolStripMenuItem,
            this.submitJobToolStripMenuItem,
            this.refreshF5ToolStripMenuItem,
            this.consoleToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(1314, 24);
            this.menuStrip1.TabIndex = 109;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadScriptToolStripMenuItem,
            this.loadSeedToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.loadToolStripMenuItem.Text = "File";
            // 
            // loadScriptToolStripMenuItem
            // 
            this.loadScriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromFileToolStripMenuItem,
            this.toolStripSeparator2,
            this.myScripttxtToolStripMenuItem,
            this.createWorkerstxtToolStripMenuItem});
            this.loadScriptToolStripMenuItem.Name = "loadScriptToolStripMenuItem";
            this.loadScriptToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.loadScriptToolStripMenuItem.Text = "Load Script";
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            this.fromFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.fromFileToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.fromFileToolStripMenuItem.Text = "From File";
            this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fromFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(253, 6);
            // 
            // myScripttxtToolStripMenuItem
            // 
            this.myScripttxtToolStripMenuItem.Name = "myScripttxtToolStripMenuItem";
            this.myScripttxtToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.myScripttxtToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.myScripttxtToolStripMenuItem.Text = "..\\..\\..\\MyScript.script";
            this.myScripttxtToolStripMenuItem.Click += new System.EventHandler(this.myScripttxtToolStripMenuItem_Click);
            // 
            // createWorkerstxtToolStripMenuItem
            // 
            this.createWorkerstxtToolStripMenuItem.Name = "createWorkerstxtToolStripMenuItem";
            this.createWorkerstxtToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.createWorkerstxtToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.createWorkerstxtToolStripMenuItem.Text = "..\\..\\..\\CreateWorkers.script";
            this.createWorkerstxtToolStripMenuItem.Click += new System.EventHandler(this.createWorkerstxtToolStripMenuItem_Click);
            // 
            // loadSeedToolStripMenuItem
            // 
            this.loadSeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromFileToolStripMenuItem1,
            this.toolStripSeparator3,
            this.pMpropertiesconfToolStripMenuItem});
            this.loadSeedToolStripMenuItem.Name = "loadSeedToolStripMenuItem";
            this.loadSeedToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.loadSeedToolStripMenuItem.Text = "Load Seed";
            // 
            // fromFileToolStripMenuItem1
            // 
            this.fromFileToolStripMenuItem1.Name = "fromFileToolStripMenuItem1";
            this.fromFileToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
            this.fromFileToolStripMenuItem1.Size = new System.Drawing.Size(246, 22);
            this.fromFileToolStripMenuItem1.Text = "From File";
            this.fromFileToolStripMenuItem1.Click += new System.EventHandler(this.fromFileToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(243, 6);
            // 
            // pMpropertiesconfToolStripMenuItem
            // 
            this.pMpropertiesconfToolStripMenuItem.Name = "pMpropertiesconfToolStripMenuItem";
            this.pMpropertiesconfToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
            this.pMpropertiesconfToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.pMpropertiesconfToolStripMenuItem.Text = "..\\..\\..\\PM_properties.seed";
            this.pMpropertiesconfToolStripMenuItem.Click += new System.EventHandler(this.pMpropertiesconfToolStripMenuItem_Click);
            // 
            // createWorkerToolStripMenuItem
            // 
            this.createWorkerToolStripMenuItem.Name = "createWorkerToolStripMenuItem";
            this.createWorkerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.createWorkerToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.createWorkerToolStripMenuItem.Text = "Create Worker F1";
            this.createWorkerToolStripMenuItem.Click += new System.EventHandler(this.createWorkerToolStripMenuItem_Click);
            // 
            // submitJobToolStripMenuItem
            // 
            this.submitJobToolStripMenuItem.Name = "submitJobToolStripMenuItem";
            this.submitJobToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.submitJobToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.submitJobToolStripMenuItem.Text = "Submit Job F2";
            this.submitJobToolStripMenuItem.Click += new System.EventHandler(this.submitJobToolStripMenuItem_Click);
            // 
            // refreshF5ToolStripMenuItem
            // 
            this.refreshF5ToolStripMenuItem.Name = "refreshF5ToolStripMenuItem";
            this.refreshF5ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshF5ToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.refreshF5ToolStripMenuItem.Text = "Status F5";
            this.refreshF5ToolStripMenuItem.Click += new System.EventHandler(this.SendStatusCommandAllNodes);
            // 
            // consoleToolStripMenuItem
            // 
            this.consoleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToFileToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.consoleToolStripMenuItem.Text = "Console";
            // 
            // exportToFileToolStripMenuItem
            // 
            this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
            this.exportToFileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exportToFileToolStripMenuItem.Text = "Export To File";
            this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workerexeToolStripMenuItem,
            this.clientexeToolStripMenuItem,
            this.toolStripSeparator1,
            this.showSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // workerexeToolStripMenuItem
            // 
            this.workerexeToolStripMenuItem.Name = "workerexeToolStripMenuItem";
            this.workerexeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.workerexeToolStripMenuItem.Text = "Set Worker *.exe";
            this.workerexeToolStripMenuItem.ToolTipText = "..\\..\\..\\NodeConsole\\bin\\Debug\\NodeConsole.exe";
            this.workerexeToolStripMenuItem.Click += new System.EventHandler(this.workerexeToolStripMenuItem_Click);
            // 
            // clientexeToolStripMenuItem
            // 
            this.clientexeToolStripMenuItem.Name = "clientexeToolStripMenuItem";
            this.clientexeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.clientexeToolStripMenuItem.Text = "Set Client *.exe";
            this.clientexeToolStripMenuItem.ToolTipText = "FIXME";
            this.clientexeToolStripMenuItem.Click += new System.EventHandler(this.clientexeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // showSettingsToolStripMenuItem
            // 
            this.showSettingsToolStripMenuItem.Name = "showSettingsToolStripMenuItem";
            this.showSettingsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.showSettingsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showSettingsToolStripMenuItem.Text = "Show Settings";
            this.showSettingsToolStripMenuItem.Click += new System.EventHandler(this.showSettingsToolStripMenuItem_Click);
            // 
            // NetworkTreeView
            // 
            this.NetworkTreeView.Location = new System.Drawing.Point(12, 150);
            this.NetworkTreeView.Name = "NetworkTreeView";
            this.NetworkTreeView.Size = new System.Drawing.Size(728, 452);
            this.NetworkTreeView.TabIndex = 110;
            this.NetworkTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // workerMenuStrip
            // 
            this.workerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.freezeWorkerToolStripMenuItem,
            this.unfreezeWorkerToolStripMenuItem,
            this.freezeJobTrackerToolStripMenuItem,
            this.unfreezeJobTrackerToolStripMenuItem,
            this.toolStripMenuItem1,
            this.statusToolStripMenuItem});
            this.workerMenuStrip.Name = "workerMenuStrip";
            this.workerMenuStrip.Size = new System.Drawing.Size(184, 136);
            this.workerMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.workerMenuStrip_Opening);
            // 
            // freezeWorkerToolStripMenuItem
            // 
            this.freezeWorkerToolStripMenuItem.Name = "freezeWorkerToolStripMenuItem";
            this.freezeWorkerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.freezeWorkerToolStripMenuItem.Text = "Freeze Worker";
            this.freezeWorkerToolStripMenuItem.Click += new System.EventHandler(this.freezeWorkerToolStripMenuItem_Click);
            // 
            // unfreezeWorkerToolStripMenuItem
            // 
            this.unfreezeWorkerToolStripMenuItem.Name = "unfreezeWorkerToolStripMenuItem";
            this.unfreezeWorkerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.unfreezeWorkerToolStripMenuItem.Text = "Unfreeze Worker";
            this.unfreezeWorkerToolStripMenuItem.Click += new System.EventHandler(this.unfreezeWorkerToolStripMenuItem_Click);
            // 
            // freezeJobTrackerToolStripMenuItem
            // 
            this.freezeJobTrackerToolStripMenuItem.Name = "freezeJobTrackerToolStripMenuItem";
            this.freezeJobTrackerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.freezeJobTrackerToolStripMenuItem.Text = "Freeze Job Tracker";
            this.freezeJobTrackerToolStripMenuItem.Click += new System.EventHandler(this.freezeJobTrackerToolStripMenuItem_Click);
            // 
            // unfreezeJobTrackerToolStripMenuItem
            // 
            this.unfreezeJobTrackerToolStripMenuItem.Name = "unfreezeJobTrackerToolStripMenuItem";
            this.unfreezeJobTrackerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.unfreezeJobTrackerToolStripMenuItem.Text = "Unfreeze Job Tracker";
            this.unfreezeJobTrackerToolStripMenuItem.Click += new System.EventHandler(this.unfreezeJobTrackerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 22);
            this.toolStripMenuItem1.Text = "Sleep X Seconds";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.statusToolStripMenuItem.Text = "Single Status";
            this.statusToolStripMenuItem.Click += new System.EventHandler(this.statusToolStripMenuItem_Click);
            // 
            // CollapseAllBtn
            // 
            this.CollapseAllBtn.Location = new System.Drawing.Point(93, 121);
            this.CollapseAllBtn.Name = "CollapseAllBtn";
            this.CollapseAllBtn.Size = new System.Drawing.Size(75, 23);
            this.CollapseAllBtn.TabIndex = 112;
            this.CollapseAllBtn.Text = "Collapse All";
            this.CollapseAllBtn.UseVisualStyleBackColor = true;
            this.CollapseAllBtn.Click += new System.EventHandler(this.CollapseAllBtn_Click);
            // 
            // ExpandAllBtn
            // 
            this.ExpandAllBtn.Location = new System.Drawing.Point(12, 121);
            this.ExpandAllBtn.Name = "ExpandAllBtn";
            this.ExpandAllBtn.Size = new System.Drawing.Size(75, 23);
            this.ExpandAllBtn.TabIndex = 113;
            this.ExpandAllBtn.Text = "Expand All";
            this.ExpandAllBtn.UseVisualStyleBackColor = true;
            this.ExpandAllBtn.Click += new System.EventHandler(this.ExpandAllBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(8, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(736, 20);
            this.label3.TabIndex = 114;
            this.label3.Text = "Network Information   _                                                          " +
    "                                                  ";
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.BackColor = System.Drawing.SystemColors.Control;
            this.RefreshBtn.Location = new System.Drawing.Point(584, 121);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(75, 23);
            this.RefreshBtn.TabIndex = 115;
            this.RefreshBtn.Text = "Status F5";
            this.RefreshBtn.UseVisualStyleBackColor = false;
            this.RefreshBtn.Click += new System.EventHandler(this.SendStatusCommandAllNodes);
            // 
            // ClearTreeBtn
            // 
            this.ClearTreeBtn.Location = new System.Drawing.Point(665, 121);
            this.ClearTreeBtn.Name = "ClearTreeBtn";
            this.ClearTreeBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearTreeBtn.TabIndex = 117;
            this.ClearTreeBtn.Text = "Clear Tree";
            this.ClearTreeBtn.UseVisualStyleBackColor = true;
            this.ClearTreeBtn.Click += new System.EventHandler(this.ClearTreeBtn_Click);
            // 
            // GUIPuppetMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 614);
            this.Controls.Add(this.ClearTreeBtn);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExpandAllBtn);
            this.Controls.Add(this.CollapseAllBtn);
            this.Controls.Add(this.NetworkTreeView);
            this.Controls.Add(this.consoleMessageBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.commandMsgBox);
            this.Controls.Add(this.submitCommand);
            this.Controls.Add(this.slowNumSeconds);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.ConsoleLabel);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUIPuppetMaster";
            this.Text = "Puppet Master";
            this.Load += new System.EventHandler(this.GUIPuppetMaster_Load);
            this.Resize += new System.EventHandler(this.GUIPupperMaster_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.slowNumSeconds)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.workerMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ConsoleLabel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown slowNumSeconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox commandMsgBox;
        private System.Windows.Forms.Button submitCommand;
        private System.Windows.Forms.RichTextBox consoleMessageBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workerexeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientexeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshF5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem showSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem myScripttxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem pMpropertiesconfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createWorkerstxtToolStripMenuItem;
        private System.Windows.Forms.TreeView NetworkTreeView;
        private System.Windows.Forms.ContextMenuStrip workerMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem freezeWorkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unfreezeWorkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freezeJobTrackerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unfreezeJobTrackerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.Button CollapseAllBtn;
        private System.Windows.Forms.Button ExpandAllBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem createWorkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submitJobToolStripMenuItem;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.Button ClearTreeBtn;

    }
}

