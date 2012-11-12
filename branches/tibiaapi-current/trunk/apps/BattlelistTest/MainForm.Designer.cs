namespace BattlelistTest
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uxTimerEnableChk = new System.Windows.Forms.CheckBox();
            this.uxMilisecondsNUD = new System.Windows.Forms.NumericUpDown();
            this.uxUpdateBtn = new System.Windows.Forms.Button();
            this.uxBattleListTV = new System.Windows.Forms.TreeView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxMilisecondsNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uxTimerEnableChk);
            this.splitContainer1.Panel1.Controls.Add(this.uxMilisecondsNUD);
            this.splitContainer1.Panel1.Controls.Add(this.uxUpdateBtn);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uxBattleListTV);
            this.splitContainer1.Size = new System.Drawing.Size(500, 360);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 4;
            // 
            // uxTimerEnableChk
            // 
            this.uxTimerEnableChk.AutoSize = true;
            this.uxTimerEnableChk.Location = new System.Drawing.Point(12, 12);
            this.uxTimerEnableChk.Name = "uxTimerEnableChk";
            this.uxTimerEnableChk.Size = new System.Drawing.Size(158, 17);
            this.uxTimerEnableChk.TabIndex = 3;
            this.uxTimerEnableChk.Text = "Update every x miliseconds:";
            this.uxTimerEnableChk.UseVisualStyleBackColor = true;
            this.uxTimerEnableChk.CheckedChanged += new System.EventHandler(this.uxTimerEnableChk_CheckedChanged);
            // 
            // uxMilisecondsNUD
            // 
            this.uxMilisecondsNUD.Location = new System.Drawing.Point(176, 11);
            this.uxMilisecondsNUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.uxMilisecondsNUD.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.uxMilisecondsNUD.Name = "uxMilisecondsNUD";
            this.uxMilisecondsNUD.Size = new System.Drawing.Size(101, 20);
            this.uxMilisecondsNUD.TabIndex = 2;
            this.uxMilisecondsNUD.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // uxUpdateBtn
            // 
            this.uxUpdateBtn.Location = new System.Drawing.Point(288, 8);
            this.uxUpdateBtn.Margin = new System.Windows.Forms.Padding(20);
            this.uxUpdateBtn.Name = "uxUpdateBtn";
            this.uxUpdateBtn.Size = new System.Drawing.Size(101, 23);
            this.uxUpdateBtn.TabIndex = 0;
            this.uxUpdateBtn.Text = "Update now";
            this.uxUpdateBtn.UseVisualStyleBackColor = true;
            this.uxUpdateBtn.Click += new System.EventHandler(this.uxUpdateBtn_Click);
            // 
            // uxBattleListTV
            // 
            this.uxBattleListTV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxBattleListTV.Location = new System.Drawing.Point(0, 0);
            this.uxBattleListTV.Name = "uxBattleListTV";
            this.uxBattleListTV.Size = new System.Drawing.Size(500, 316);
            this.uxBattleListTV.TabIndex = 0;
            // 
            // uxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 360);
            this.Controls.Add(this.splitContainer1);
            this.Name = "uxForm";
            this.Text = "BattleListTest";
            this.Load += new System.EventHandler(this.uxForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxMilisecondsNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox uxTimerEnableChk;
        private System.Windows.Forms.NumericUpDown uxMilisecondsNUD;
        private System.Windows.Forms.Button uxUpdateBtn;
        private System.Windows.Forms.TreeView uxBattleListTV;
    }
}