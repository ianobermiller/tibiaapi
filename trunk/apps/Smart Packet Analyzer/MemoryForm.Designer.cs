namespace SmartPacketAnalyzer
{
    partial class MemoryForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uxAddAddress = new System.Windows.Forms.Button();
            this.uxClearAddresses = new System.Windows.Forms.Button();
            this.uxMemoryList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.uxMemoryMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uxMemoryEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMemoryDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.uxTimerShort = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            this.uxMemoryMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.uxAddAddress);
            this.groupBox2.Controls.Add(this.uxClearAddresses);
            this.groupBox2.Controls.Add(this.uxMemoryList);
            this.groupBox2.Location = new System.Drawing.Point(7, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(383, 237);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Memory";
            // 
            // uxAddAddress
            // 
            this.uxAddAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uxAddAddress.Location = new System.Drawing.Point(12, 202);
            this.uxAddAddress.Name = "uxAddAddress";
            this.uxAddAddress.Size = new System.Drawing.Size(123, 26);
            this.uxAddAddress.TabIndex = 30;
            this.uxAddAddress.Text = "Add Address";
            this.uxAddAddress.UseVisualStyleBackColor = true;
            this.uxAddAddress.Click += new System.EventHandler(this.uxAddAddress_Click);
            // 
            // uxClearAddresses
            // 
            this.uxClearAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uxClearAddresses.Location = new System.Drawing.Point(262, 202);
            this.uxClearAddresses.Name = "uxClearAddresses";
            this.uxClearAddresses.Size = new System.Drawing.Size(109, 26);
            this.uxClearAddresses.TabIndex = 29;
            this.uxClearAddresses.Text = "Clear Addresses";
            this.uxClearAddresses.UseVisualStyleBackColor = true;
            this.uxClearAddresses.Click += new System.EventHandler(this.uxClearAddresses_Click);
            // 
            // uxMemoryList
            // 
            this.uxMemoryList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.uxMemoryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.uxMemoryList.ContextMenuStrip = this.uxMemoryMenu;
            this.uxMemoryList.FullRowSelect = true;
            this.uxMemoryList.GridLines = true;
            this.uxMemoryList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.uxMemoryList.Location = new System.Drawing.Point(9, 19);
            this.uxMemoryList.MultiSelect = false;
            this.uxMemoryList.Name = "uxMemoryList";
            this.uxMemoryList.Size = new System.Drawing.Size(362, 177);
            this.uxMemoryList.TabIndex = 9;
            this.uxMemoryList.UseCompatibleStateImageBehavior = false;
            this.uxMemoryList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Description";
            this.columnHeader1.Width = 117;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Address";
            this.columnHeader2.Width = 71;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Value";
            this.columnHeader3.Width = 102;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 52;
            // 
            // uxMemoryMenu
            // 
            this.uxMemoryMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxMemoryEdit,
            this.uxMemoryDelete});
            this.uxMemoryMenu.Name = "uxMemoryMenu";
            this.uxMemoryMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // uxMemoryEdit
            // 
            this.uxMemoryEdit.Name = "uxMemoryEdit";
            this.uxMemoryEdit.Size = new System.Drawing.Size(107, 22);
            this.uxMemoryEdit.Text = "Edit";
            this.uxMemoryEdit.Click += new System.EventHandler(this.uxMemoryEdit_Click);
            // 
            // uxMemoryDelete
            // 
            this.uxMemoryDelete.Name = "uxMemoryDelete";
            this.uxMemoryDelete.Size = new System.Drawing.Size(107, 22);
            this.uxMemoryDelete.Text = "Delete";
            this.uxMemoryDelete.Click += new System.EventHandler(this.uxMemoryDelete_Click);
            // 
            // uxTimerShort
            // 
            this.uxTimerShort.Interval = 300;
            this.uxTimerShort.Tick += new System.EventHandler(this.uxTimerShort_Tick);
            // 
            // uxMemory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 248);
            this.Controls.Add(this.groupBox2);
            this.Name = "uxMemory";
            this.Text = "SmartPacketAnalyzer - Memory";
            this.Load += new System.EventHandler(this.uxMemory_Load);
            this.groupBox2.ResumeLayout(false);
            this.uxMemoryMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button uxAddAddress;
        private System.Windows.Forms.Button uxClearAddresses;
        private System.Windows.Forms.ListView uxMemoryList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip uxMemoryMenu;
        private System.Windows.Forms.ToolStripMenuItem uxMemoryEdit;
        private System.Windows.Forms.ToolStripMenuItem uxMemoryDelete;
        private System.Windows.Forms.Timer uxTimerShort;
    }
}