namespace SmartRunemaker
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
                this.notifyIcon.Dispose(); //we dispose our tray icon here
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
            this.uxShortTimer = new System.Windows.Forms.Timer(this.components);
            this.uxLongTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxShortTimer
            // 
            this.uxShortTimer.Interval = 1000;
            this.uxShortTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // uxLongTimer
            // 
            this.uxLongTimer.Interval = 10000;
            this.uxLongTimer.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconMenu;
            this.notifyIcon.Text = "Smart Runemaker";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyIconMenu
            // 
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitNotifyIconMenuItem_Click);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 123);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Smart Runemaker Options";
            this.TopMost = true;
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.notifyIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer uxShortTimer;
        private System.Windows.Forms.Timer uxLongTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
    }
}

