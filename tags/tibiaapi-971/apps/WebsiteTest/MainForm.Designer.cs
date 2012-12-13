namespace WebsiteTest
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
            this.uxServerBtn = new System.Windows.Forms.Button();
            this.uxGuildBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uxPlayerBtn = new System.Windows.Forms.Button();
            this.uxKeywordTxt = new System.Windows.Forms.TextBox();
            this.uxDataDGV = new System.Windows.Forms.DataGridView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxDataDGV)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.uxServerBtn);
            this.splitContainer1.Panel1.Controls.Add(this.uxGuildBtn);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.uxPlayerBtn);
            this.splitContainer1.Panel1.Controls.Add(this.uxKeywordTxt);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uxDataDGV);
            this.splitContainer1.Size = new System.Drawing.Size(438, 473);
            this.splitContainer1.SplitterDistance = 33;
            this.splitContainer1.TabIndex = 0;
            // 
            // uxServerBtn
            // 
            this.uxServerBtn.Location = new System.Drawing.Point(352, 4);
            this.uxServerBtn.Name = "uxServerBtn";
            this.uxServerBtn.Size = new System.Drawing.Size(75, 23);
            this.uxServerBtn.TabIndex = 5;
            this.uxServerBtn.Text = "Server";
            this.uxServerBtn.UseVisualStyleBackColor = true;
            this.uxServerBtn.Click += new System.EventHandler(this.uxServerBtn_Click);
            // 
            // uxGuildBtn
            // 
            this.uxGuildBtn.Location = new System.Drawing.Point(271, 4);
            this.uxGuildBtn.Name = "uxGuildBtn";
            this.uxGuildBtn.Size = new System.Drawing.Size(75, 23);
            this.uxGuildBtn.TabIndex = 4;
            this.uxGuildBtn.Text = "Guild";
            this.uxGuildBtn.UseVisualStyleBackColor = true;
            this.uxGuildBtn.Click += new System.EventHandler(this.uxGuildBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "up";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Look";
            // 
            // uxPlayerBtn
            // 
            this.uxPlayerBtn.Location = new System.Drawing.Point(190, 4);
            this.uxPlayerBtn.Name = "uxPlayerBtn";
            this.uxPlayerBtn.Size = new System.Drawing.Size(75, 23);
            this.uxPlayerBtn.TabIndex = 1;
            this.uxPlayerBtn.Text = "Player";
            this.uxPlayerBtn.UseVisualStyleBackColor = true;
            this.uxPlayerBtn.Click += new System.EventHandler(this.uxPlayerBtn_Click);
            // 
            // uxKeywordTxt
            // 
            this.uxKeywordTxt.Location = new System.Drawing.Point(49, 6);
            this.uxKeywordTxt.Name = "uxKeywordTxt";
            this.uxKeywordTxt.Size = new System.Drawing.Size(100, 20);
            this.uxKeywordTxt.TabIndex = 0;
            this.uxKeywordTxt.Text = "Farsa Svart";
            // 
            // uxDataDGV
            // 
            this.uxDataDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uxDataDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxDataDGV.Location = new System.Drawing.Point(0, 0);
            this.uxDataDGV.Name = "uxDataDGV";
            this.uxDataDGV.Size = new System.Drawing.Size(438, 436);
            this.uxDataDGV.TabIndex = 0;
            // 
            // uxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 473);
            this.Controls.Add(this.splitContainer1);
            this.Name = "uxForm";
            this.Text = "WebsiteTest";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxDataDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button uxServerBtn;
        private System.Windows.Forms.Button uxGuildBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button uxPlayerBtn;
        private System.Windows.Forms.TextBox uxKeywordTxt;
        private System.Windows.Forms.DataGridView uxDataDGV;
    }
}