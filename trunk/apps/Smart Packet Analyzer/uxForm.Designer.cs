namespace SmartPacketAnalyzer
{
    partial class uxForm
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
            this.uxPacketMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConvertToInt = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyAllBytes = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uxTypes = new System.Windows.Forms.ComboBox();
            this.uxLogClient = new System.Windows.Forms.CheckBox();
            this.uxLogServer = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.uxClearPackets = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uxPacketList = new System.Windows.Forms.ListView();
            this.TimeCol = new System.Windows.Forms.ColumnHeader();
            this.SourceCol = new System.Windows.Forms.ColumnHeader();
            this.DestinationCol = new System.Windows.Forms.ColumnHeader();
            this.LengthCol = new System.Windows.Forms.ColumnHeader();
            this.TypeCol = new System.Windows.Forms.ColumnHeader();
            this.NameCol = new System.Windows.Forms.ColumnHeader();
            this.uxPacketDisplay = new System.Windows.Forms.TextBox();
            this.uxStart = new System.Windows.Forms.Button();
            this.uxShowMemoryWatcher = new System.Windows.Forms.Button();
            this.uxPacketMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxPacketMenu
            // 
            this.uxPacketMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConvertToInt,
            this.CopyAllBytes});
            this.uxPacketMenu.Name = "ctxMenuPacket";
            this.uxPacketMenu.Size = new System.Drawing.Size(151, 48);
            // 
            // ConvertToInt
            // 
            this.ConvertToInt.Name = "ConvertToInt";
            this.ConvertToInt.Size = new System.Drawing.Size(150, 22);
            this.ConvertToInt.Text = "Convert To Int";
            this.ConvertToInt.Click += new System.EventHandler(this.ConvertToInt_Click);
            // 
            // CopyAllBytes
            // 
            this.CopyAllBytes.Name = "CopyAllBytes";
            this.CopyAllBytes.Size = new System.Drawing.Size(150, 22);
            this.CopyAllBytes.Text = "Copy All Bytes";
            this.CopyAllBytes.Click += new System.EventHandler(this.CopyAllBytes_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.uxTypes);
            this.groupBox1.Controls.Add(this.uxLogClient);
            this.groupBox1.Controls.Add(this.uxLogServer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 47);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Only of Type:";
            // 
            // uxTypes
            // 
            this.uxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxTypes.FormattingEnabled = true;
            this.uxTypes.Location = new System.Drawing.Point(260, 15);
            this.uxTypes.Name = "uxTypes";
            this.uxTypes.Size = new System.Drawing.Size(200, 21);
            this.uxTypes.Sorted = true;
            this.uxTypes.TabIndex = 32;
            this.uxTypes.SelectedIndexChanged += new System.EventHandler(this.uxTypes_SelectedIndexChanged);
            // 
            // uxLogClient
            // 
            this.uxLogClient.AutoSize = true;
            this.uxLogClient.Checked = true;
            this.uxLogClient.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uxLogClient.Location = new System.Drawing.Point(99, 19);
            this.uxLogClient.Name = "uxLogClient";
            this.uxLogClient.Size = new System.Drawing.Size(78, 17);
            this.uxLogClient.TabIndex = 31;
            this.uxLogClient.Text = "From Client";
            this.uxLogClient.UseVisualStyleBackColor = true;
            // 
            // uxLogServer
            // 
            this.uxLogServer.AutoSize = true;
            this.uxLogServer.Checked = true;
            this.uxLogServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uxLogServer.Location = new System.Drawing.Point(10, 19);
            this.uxLogServer.Name = "uxLogServer";
            this.uxLogServer.Size = new System.Drawing.Size(83, 17);
            this.uxLogServer.TabIndex = 30;
            this.uxLogServer.Text = "From Server";
            this.uxLogServer.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.uxClearPackets);
            this.groupBox3.Controls.Add(this.splitContainer1);
            this.groupBox3.Controls.Add(this.uxStart);
            this.groupBox3.Location = new System.Drawing.Point(12, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(642, 399);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Packet Log";
            // 
            // uxClearPackets
            // 
            this.uxClearPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uxClearPackets.Location = new System.Drawing.Point(6, 351);
            this.uxClearPackets.Name = "uxClearPackets";
            this.uxClearPackets.Size = new System.Drawing.Size(78, 39);
            this.uxClearPackets.TabIndex = 11;
            this.uxClearPackets.Text = "Clear Log";
            this.uxClearPackets.UseVisualStyleBackColor = true;
            this.uxClearPackets.Click += new System.EventHandler(this.uxClearPackets_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 19);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uxPacketList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uxPacketDisplay);
            this.splitContainer1.Size = new System.Drawing.Size(630, 326);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.TabIndex = 10;
            // 
            // uxPacketList
            // 
            this.uxPacketList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TimeCol,
            this.SourceCol,
            this.DestinationCol,
            this.LengthCol,
            this.TypeCol,
            this.NameCol});
            this.uxPacketList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxPacketList.FullRowSelect = true;
            this.uxPacketList.GridLines = true;
            this.uxPacketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.uxPacketList.Location = new System.Drawing.Point(0, 0);
            this.uxPacketList.MultiSelect = false;
            this.uxPacketList.Name = "uxPacketList";
            this.uxPacketList.Size = new System.Drawing.Size(630, 151);
            this.uxPacketList.TabIndex = 8;
            this.uxPacketList.UseCompatibleStateImageBehavior = false;
            this.uxPacketList.View = System.Windows.Forms.View.Details;
            this.uxPacketList.SelectedIndexChanged += new System.EventHandler(this.uxPacketList_SelectedIndexChanged);
            this.uxPacketList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.uxPacketList_KeyUp);
            // 
            // TimeCol
            // 
            this.TimeCol.Text = "Received";
            this.TimeCol.Width = 107;
            // 
            // SourceCol
            // 
            this.SourceCol.Text = "Source";
            this.SourceCol.Width = 85;
            // 
            // DestinationCol
            // 
            this.DestinationCol.Text = "Destination";
            this.DestinationCol.Width = 83;
            // 
            // LengthCol
            // 
            this.LengthCol.Text = "Length";
            this.LengthCol.Width = 49;
            // 
            // TypeCol
            // 
            this.TypeCol.Text = "Type";
            this.TypeCol.Width = 38;
            // 
            // NameCol
            // 
            this.NameCol.Text = "Name";
            this.NameCol.Width = 141;
            // 
            // uxPacketDisplay
            // 
            this.uxPacketDisplay.ContextMenuStrip = this.uxPacketMenu;
            this.uxPacketDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxPacketDisplay.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxPacketDisplay.Location = new System.Drawing.Point(0, 0);
            this.uxPacketDisplay.Multiline = true;
            this.uxPacketDisplay.Name = "uxPacketDisplay";
            this.uxPacketDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uxPacketDisplay.Size = new System.Drawing.Size(630, 171);
            this.uxPacketDisplay.TabIndex = 7;
            this.uxPacketDisplay.Resize += new System.EventHandler(this.uxPacketDisplay_Resize);
            // 
            // uxStart
            // 
            this.uxStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxStart.Location = new System.Drawing.Point(486, 351);
            this.uxStart.Name = "uxStart";
            this.uxStart.Size = new System.Drawing.Size(150, 39);
            this.uxStart.TabIndex = 9;
            this.uxStart.Text = "Stop Packet Logging";
            this.uxStart.UseVisualStyleBackColor = true;
            this.uxStart.Click += new System.EventHandler(this.uxStart_Click);
            // 
            // uxShowMemoryWatcher
            // 
            this.uxShowMemoryWatcher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxShowMemoryWatcher.Location = new System.Drawing.Point(500, 18);
            this.uxShowMemoryWatcher.Name = "uxShowMemoryWatcher";
            this.uxShowMemoryWatcher.Size = new System.Drawing.Size(149, 36);
            this.uxShowMemoryWatcher.TabIndex = 29;
            this.uxShowMemoryWatcher.Text = "Show Memory Watcher";
            this.uxShowMemoryWatcher.UseVisualStyleBackColor = true;
            this.uxShowMemoryWatcher.Click += new System.EventHandler(this.uxShowMemoryWatcher_Click);
            // 
            // uxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 476);
            this.Controls.Add(this.uxShowMemoryWatcher);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "uxForm";
            this.Text = "Smart Packet Analyzer";
            this.Load += new System.EventHandler(this.uxForm_Load);
            this.uxPacketMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip uxPacketMenu;
        private System.Windows.Forms.ToolStripMenuItem ConvertToInt;
        private System.Windows.Forms.ToolStripMenuItem CopyAllBytes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox uxLogClient;
        private System.Windows.Forms.CheckBox uxLogServer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button uxClearPackets;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView uxPacketList;
        private System.Windows.Forms.ColumnHeader TimeCol;
        private System.Windows.Forms.ColumnHeader SourceCol;
        private System.Windows.Forms.ColumnHeader DestinationCol;
        private System.Windows.Forms.ColumnHeader TypeCol;
        private System.Windows.Forms.Button uxStart;
        private System.Windows.Forms.ColumnHeader LengthCol;
        private System.Windows.Forms.TextBox uxPacketDisplay;
        private System.Windows.Forms.Button uxShowMemoryWatcher;
        private System.Windows.Forms.ColumnHeader NameCol;
        private System.Windows.Forms.ComboBox uxTypes;
        private System.Windows.Forms.Label label1;
    }
}

