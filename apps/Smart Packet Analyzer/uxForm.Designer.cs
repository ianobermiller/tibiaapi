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
            this.uxStart = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uxPackets = new System.Windows.Forms.ListBox();
            this.uxPacketDisplay = new System.Windows.Forms.TextBox();
            this.ctxMenuPacket = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenToInt = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClr = new System.Windows.Forms.Button();
            this.cbOHead = new System.Windows.Forms.CheckBox();
            this.tbHead = new System.Windows.Forms.TextBox();
            this.lbLog = new System.Windows.Forms.CheckedListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbSee = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMem = new System.Windows.Forms.TextBox();
            this.btMem = new System.Windows.Forms.Button();
            this.rbMem1 = new System.Windows.Forms.RadioButton();
            this.rbMem2 = new System.Windows.Forms.RadioButton();
            this.tbPkt = new System.Windows.Forms.TextBox();
            this.btCln = new System.Windows.Forms.Button();
            this.btSrv = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctxMenuPacket.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxStart
            // 
            this.uxStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uxStart.Location = new System.Drawing.Point(12, 425);
            this.uxStart.Name = "uxStart";
            this.uxStart.Size = new System.Drawing.Size(150, 30);
            this.uxStart.TabIndex = 1;
            this.uxStart.Text = "Stop Packet Logging";
            this.uxStart.UseVisualStyleBackColor = true;
            this.uxStart.Click += new System.EventHandler(this.uxStart_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uxPackets);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uxPacketDisplay);
            this.splitContainer1.Size = new System.Drawing.Size(446, 407);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 7;
            // 
            // uxPackets
            // 
            this.uxPackets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxPackets.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxPackets.FormattingEnabled = true;
            this.uxPackets.ItemHeight = 15;
            this.uxPackets.Location = new System.Drawing.Point(0, 0);
            this.uxPackets.Name = "uxPackets";
            this.uxPackets.Size = new System.Drawing.Size(446, 169);
            this.uxPackets.TabIndex = 7;
            this.uxPackets.SelectedIndexChanged += new System.EventHandler(this.uxPackets_SelectedIndexChanged);
            // 
            // uxPacketDisplay
            // 
            this.uxPacketDisplay.ContextMenuStrip = this.ctxMenuPacket;
            this.uxPacketDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxPacketDisplay.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxPacketDisplay.Location = new System.Drawing.Point(0, 0);
            this.uxPacketDisplay.Multiline = true;
            this.uxPacketDisplay.Name = "uxPacketDisplay";
            this.uxPacketDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uxPacketDisplay.Size = new System.Drawing.Size(446, 234);
            this.uxPacketDisplay.TabIndex = 7;
            // 
            // ctxMenuPacket
            // 
            this.ctxMenuPacket.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenToInt,
            this.convertToStringToolStripMenuItem});
            this.ctxMenuPacket.Name = "ctxMenuPacket";
            this.ctxMenuPacket.Size = new System.Drawing.Size(171, 48);
            // 
            // MenToInt
            // 
            this.MenToInt.Name = "MenToInt";
            this.MenToInt.Size = new System.Drawing.Size(170, 22);
            this.MenToInt.Text = "Convert To Int";
            this.MenToInt.Click += new System.EventHandler(this.MenToInt_Click);
            // 
            // convertToStringToolStripMenuItem
            // 
            this.convertToStringToolStripMenuItem.Name = "convertToStringToolStripMenuItem";
            this.convertToStringToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.convertToStringToolStripMenuItem.Text = "Convert To String";
            this.convertToStringToolStripMenuItem.Click += new System.EventHandler(this.convertToStringToolStripMenuItem_Click);
            // 
            // btnClr
            // 
            this.btnClr.Location = new System.Drawing.Point(168, 425);
            this.btnClr.Name = "btnClr";
            this.btnClr.Size = new System.Drawing.Size(78, 30);
            this.btnClr.TabIndex = 8;
            this.btnClr.Text = "Clear Log";
            this.btnClr.UseVisualStyleBackColor = true;
            this.btnClr.Click += new System.EventHandler(this.btnClr_Click);
            // 
            // cbOHead
            // 
            this.cbOHead.AutoSize = true;
            this.cbOHead.Location = new System.Drawing.Point(464, 16);
            this.cbOHead.Name = "cbOHead";
            this.cbOHead.Size = new System.Drawing.Size(110, 17);
            this.cbOHead.TabIndex = 11;
            this.cbOHead.Text = "Only With Header";
            this.cbOHead.UseVisualStyleBackColor = true;
            // 
            // tbHead
            // 
            this.tbHead.Location = new System.Drawing.Point(580, 16);
            this.tbHead.MaxLength = 2;
            this.tbHead.Name = "tbHead";
            this.tbHead.Size = new System.Drawing.Size(41, 20);
            this.tbHead.TabIndex = 12;
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Items.AddRange(new object[] {
            "Incoming",
            "Outgoing"});
            this.lbLog.Location = new System.Drawing.Point(464, 42);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(76, 34);
            this.lbLog.TabIndex = 13;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbSee
            // 
            this.tbSee.Location = new System.Drawing.Point(568, 362);
            this.tbSee.Name = "tbSee";
            this.tbSee.Size = new System.Drawing.Size(85, 20);
            this.tbSee.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(464, 365);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Current See ID: ";
            // 
            // tbMem
            // 
            this.tbMem.Location = new System.Drawing.Point(568, 290);
            this.tbMem.Name = "tbMem";
            this.tbMem.Size = new System.Drawing.Size(85, 20);
            this.tbMem.TabIndex = 16;
            // 
            // btMem
            // 
            this.btMem.Location = new System.Drawing.Point(467, 287);
            this.btMem.Name = "btMem";
            this.btMem.Size = new System.Drawing.Size(95, 23);
            this.btMem.TabIndex = 17;
            this.btMem.Text = "ReadMem";
            this.btMem.UseVisualStyleBackColor = true;
            this.btMem.Click += new System.EventHandler(this.btMem_Click);
            // 
            // rbMem1
            // 
            this.rbMem1.AutoSize = true;
            this.rbMem1.Location = new System.Drawing.Point(468, 318);
            this.rbMem1.Name = "rbMem1";
            this.rbMem1.Size = new System.Drawing.Size(52, 17);
            this.rbMem1.TabIndex = 18;
            this.rbMem1.TabStop = true;
            this.rbMem1.Text = "String";
            this.rbMem1.UseVisualStyleBackColor = true;
            // 
            // rbMem2
            // 
            this.rbMem2.AutoSize = true;
            this.rbMem2.Location = new System.Drawing.Point(467, 341);
            this.rbMem2.Name = "rbMem2";
            this.rbMem2.Size = new System.Drawing.Size(58, 17);
            this.rbMem2.TabIndex = 19;
            this.rbMem2.TabStop = true;
            this.rbMem2.Text = "Integer";
            this.rbMem2.UseVisualStyleBackColor = true;
            // 
            // tbPkt
            // 
            this.tbPkt.Location = new System.Drawing.Point(461, 110);
            this.tbPkt.Name = "tbPkt";
            this.tbPkt.Size = new System.Drawing.Size(220, 20);
            this.tbPkt.TabIndex = 20;
            // 
            // btCln
            // 
            this.btCln.Location = new System.Drawing.Point(461, 136);
            this.btCln.Name = "btCln";
            this.btCln.Size = new System.Drawing.Size(75, 26);
            this.btCln.TabIndex = 21;
            this.btCln.Text = "To Client";
            this.btCln.UseVisualStyleBackColor = true;
            this.btCln.Click += new System.EventHandler(this.btCln_Click);
            // 
            // btSrv
            // 
            this.btSrv.Location = new System.Drawing.Point(542, 136);
            this.btSrv.Name = "btSrv";
            this.btSrv.Size = new System.Drawing.Size(75, 26);
            this.btSrv.TabIndex = 22;
            this.btSrv.Text = "To Server";
            this.btSrv.UseVisualStyleBackColor = true;
            this.btSrv.Click += new System.EventHandler(this.btSrv_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(465, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Send Packet";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(559, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // uxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 465);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btSrv);
            this.Controls.Add(this.btCln);
            this.Controls.Add(this.tbPkt);
            this.Controls.Add(this.btMem);
            this.Controls.Add(this.rbMem2);
            this.Controls.Add(this.rbMem1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMem);
            this.Controls.Add(this.tbSee);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.btnClr);
            this.Controls.Add(this.uxStart);
            this.Controls.Add(this.tbHead);
            this.Controls.Add(this.cbOHead);
            this.Controls.Add(this.splitContainer1);
            this.Name = "uxForm";
            this.Text = "Smart Packet Analyzer";
            this.Load += new System.EventHandler(this.uxForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ctxMenuPacket.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxStart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox uxPackets;
        private System.Windows.Forms.TextBox uxPacketDisplay;
        private System.Windows.Forms.Button btnClr;
        private System.Windows.Forms.ContextMenuStrip ctxMenuPacket;
        private System.Windows.Forms.ToolStripMenuItem MenToInt;
        private System.Windows.Forms.ToolStripMenuItem convertToStringToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbOHead;
        private System.Windows.Forms.TextBox tbHead;
        private System.Windows.Forms.CheckedListBox lbLog;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox tbSee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMem;
        private System.Windows.Forms.Button btMem;
        private System.Windows.Forms.RadioButton rbMem1;
        private System.Windows.Forms.RadioButton rbMem2;
        private System.Windows.Forms.TextBox tbPkt;
        private System.Windows.Forms.Button btCln;
        private System.Windows.Forms.Button btSrv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

