namespace SmartPacketSniffer
{
    partial class MainForm
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chkServer = new System.Windows.Forms.CheckBox();
            this.chkClient = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numProxyPort = new System.Windows.Forms.NumericUpDown();
            this.radioProxy = new System.Windows.Forms.RadioButton();
            this.radioSpecial = new System.Windows.Forms.RadioButton();
            this.radioDefault = new System.Windows.Forms.RadioButton();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnClient = new System.Windows.Forms.Button();
            this.txtType = new System.Windows.Forms.TextBox();
            this.chkType = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PacketList = new System.Windows.Forms.ListView();
            this.ReceivedHeader = new System.Windows.Forms.ColumnHeader();
            this.SourceHeader = new System.Windows.Forms.ColumnHeader();
            this.DestinationHeader = new System.Windows.Forms.ColumnHeader();
            this.LengthHeader = new System.Windows.Forms.ColumnHeader();
            this.TypeHeader = new System.Windows.Forms.ColumnHeader();
            this.txtPacket = new System.Windows.Forms.TextBox();
            this.NameHeader = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(167, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // chkServer
            // 
            this.chkServer.AutoSize = true;
            this.chkServer.Checked = true;
            this.chkServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkServer.Location = new System.Drawing.Point(6, 57);
            this.chkServer.Name = "chkServer";
            this.chkServer.Size = new System.Drawing.Size(83, 17);
            this.chkServer.TabIndex = 2;
            this.chkServer.Text = "From Server";
            this.chkServer.UseVisualStyleBackColor = true;
            // 
            // chkClient
            // 
            this.chkClient.AutoSize = true;
            this.chkClient.Checked = true;
            this.chkClient.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClient.Location = new System.Drawing.Point(6, 80);
            this.chkClient.Name = "chkClient";
            this.chkClient.Size = new System.Drawing.Size(78, 17);
            this.chkClient.TabIndex = 3;
            this.chkClient.Text = "From Client";
            this.chkClient.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numProxyPort);
            this.groupBox1.Controls.Add(this.radioProxy);
            this.groupBox1.Controls.Add(this.radioSpecial);
            this.groupBox1.Controls.Add(this.radioDefault);
            this.groupBox1.Controls.Add(this.btnReload);
            this.groupBox1.Controls.Add(this.btnLog);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Controls.Add(this.btnClient);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.chkType);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.chkClient);
            this.groupBox1.Controls.Add(this.chkServer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 370);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet Options";
            // 
            // numProxyPort
            // 
            this.numProxyPort.Location = new System.Drawing.Point(117, 183);
            this.numProxyPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numProxyPort.Name = "numProxyPort";
            this.numProxyPort.Size = new System.Drawing.Size(56, 20);
            this.numProxyPort.TabIndex = 15;
            this.numProxyPort.Value = new decimal(new int[] {
            16000,
            0,
            0,
            0});
            // 
            // radioProxy
            // 
            this.radioProxy.AutoSize = true;
            this.radioProxy.Location = new System.Drawing.Point(6, 183);
            this.radioProxy.Name = "radioProxy";
            this.radioProxy.Size = new System.Drawing.Size(109, 17);
            this.radioProxy.TabIndex = 14;
            this.radioProxy.Text = "Using Proxy. Port:";
            this.radioProxy.UseVisualStyleBackColor = true;
            this.radioProxy.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // radioSpecial
            // 
            this.radioSpecial.AutoSize = true;
            this.radioSpecial.Location = new System.Drawing.Point(6, 159);
            this.radioSpecial.Name = "radioSpecial";
            this.radioSpecial.Size = new System.Drawing.Size(122, 17);
            this.radioSpecial.TabIndex = 13;
            this.radioSpecial.Text = "Special Remote Port";
            this.radioSpecial.UseVisualStyleBackColor = true;
            this.radioSpecial.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // radioDefault
            // 
            this.radioDefault.AutoSize = true;
            this.radioDefault.Checked = true;
            this.radioDefault.Location = new System.Drawing.Point(6, 136);
            this.radioDefault.Name = "radioDefault";
            this.radioDefault.Size = new System.Drawing.Size(121, 17);
            this.radioDefault.TabIndex = 12;
            this.radioDefault.TabStop = true;
            this.radioDefault.Text = "Default Remote Port";
            this.radioDefault.UseVisualStyleBackColor = true;
            this.radioDefault.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(6, 254);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(167, 23);
            this.btnReload.TabIndex = 7;
            this.btnReload.Text = "Reload Clients";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(6, 341);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(167, 23);
            this.btnLog.TabIndex = 10;
            this.btnLog.Text = "Stop Packet Logging";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 312);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(167, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear Log";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(6, 283);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(167, 23);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Copy Packet To Clipboard";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnClient
            // 
            this.btnClient.Location = new System.Drawing.Point(6, 225);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(167, 23);
            this.btnClient.TabIndex = 6;
            this.btnClient.Text = "Select Client";
            this.btnClient.UseVisualStyleBackColor = true;
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(98, 101);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(47, 20);
            this.txtType.TabIndex = 5;
            // 
            // chkType
            // 
            this.chkType.AutoSize = true;
            this.chkType.Location = new System.Drawing.Point(6, 103);
            this.chkType.Name = "chkType";
            this.chkType.Size = new System.Drawing.Size(86, 17);
            this.chkType.TabIndex = 4;
            this.chkType.Text = "Only of Type";
            this.chkType.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Location = new System.Drawing.Point(201, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(502, 371);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Packet Log";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PacketList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtPacket);
            this.splitContainer1.Size = new System.Drawing.Size(496, 352);
            this.splitContainer1.SplitterDistance = 174;
            this.splitContainer1.TabIndex = 5;
            // 
            // PacketList
            // 
            this.PacketList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ReceivedHeader,
            this.SourceHeader,
            this.DestinationHeader,
            this.LengthHeader,
            this.TypeHeader,
            this.NameHeader});
            this.PacketList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PacketList.FullRowSelect = true;
            this.PacketList.GridLines = true;
            this.PacketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PacketList.Location = new System.Drawing.Point(0, 0);
            this.PacketList.MultiSelect = false;
            this.PacketList.Name = "PacketList";
            this.PacketList.Size = new System.Drawing.Size(496, 174);
            this.PacketList.TabIndex = 11;
            this.PacketList.UseCompatibleStateImageBehavior = false;
            this.PacketList.View = System.Windows.Forms.View.Details;
            this.PacketList.SelectedIndexChanged += new System.EventHandler(this.PacketList_SelectedIndexChanged);
            // 
            // ReceivedHeader
            // 
            this.ReceivedHeader.Text = "Received";
            this.ReceivedHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ReceivedHeader.Width = 83;
            // 
            // SourceHeader
            // 
            this.SourceHeader.Text = "Source";
            this.SourceHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DestinationHeader
            // 
            this.DestinationHeader.Text = "Destination";
            this.DestinationHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DestinationHeader.Width = 71;
            // 
            // LengthHeader
            // 
            this.LengthHeader.Text = "Length";
            this.LengthHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TypeHeader.Width = 88;
            // 
            // txtPacket
            // 
            this.txtPacket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPacket.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtPacket.Location = new System.Drawing.Point(0, 0);
            this.txtPacket.Multiline = true;
            this.txtPacket.Name = "txtPacket";
            this.txtPacket.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPacket.Size = new System.Drawing.Size(496, 174);
            this.txtPacket.TabIndex = 12;
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Name";
            this.NameHeader.Width = 121;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 391);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(719, 372);
            this.Name = "MainForm";
            this.Text = "Smart Packet Sniffer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkServer;
        private System.Windows.Forms.CheckBox chkClient;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.CheckBox chkType;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView PacketList;
        private System.Windows.Forms.ColumnHeader ReceivedHeader;
        private System.Windows.Forms.ColumnHeader SourceHeader;
        private System.Windows.Forms.ColumnHeader DestinationHeader;
        private System.Windows.Forms.ColumnHeader LengthHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.TextBox txtPacket;
        private System.Windows.Forms.RadioButton radioProxy;
        private System.Windows.Forms.RadioButton radioSpecial;
        private System.Windows.Forms.RadioButton radioDefault;
        private System.Windows.Forms.NumericUpDown numProxyPort;
        private System.Windows.Forms.ColumnHeader NameHeader;
    }
}

