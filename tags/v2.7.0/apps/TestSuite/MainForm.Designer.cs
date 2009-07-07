namespace TestSuite
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uxHookProxySendServerTest = new System.Windows.Forms.Button();
            this.uxProxySendClientTest = new System.Windows.Forms.Button();
            this.uxProxySendServerTest = new System.Windows.Forms.Button();
            this.uxHookProxyStatus = new System.Windows.Forms.Label();
            this.uxProxyStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uxHookProxySendServerTest);
            this.groupBox1.Controls.Add(this.uxProxySendClientTest);
            this.groupBox1.Controls.Add(this.uxProxySendServerTest);
            this.groupBox1.Controls.Add(this.uxHookProxyStatus);
            this.groupBox1.Controls.Add(this.uxProxyStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 86);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packets";
            // 
            // uxHookProxySendServerTest
            // 
            this.uxHookProxySendServerTest.Location = new System.Drawing.Point(134, 48);
            this.uxHookProxySendServerTest.Name = "uxHookProxySendServerTest";
            this.uxHookProxySendServerTest.Size = new System.Drawing.Size(99, 23);
            this.uxHookProxySendServerTest.TabIndex = 6;
            this.uxHookProxySendServerTest.Text = "Send to Server";
            this.uxHookProxySendServerTest.UseVisualStyleBackColor = true;
            this.uxHookProxySendServerTest.Click += new System.EventHandler(this.uxHookProxySendServerTest_Click);
            // 
            // uxProxySendClientTest
            // 
            this.uxProxySendClientTest.Location = new System.Drawing.Point(239, 19);
            this.uxProxySendClientTest.Name = "uxProxySendClientTest";
            this.uxProxySendClientTest.Size = new System.Drawing.Size(99, 23);
            this.uxProxySendClientTest.TabIndex = 8;
            this.uxProxySendClientTest.Text = "Send to Client";
            this.uxProxySendClientTest.UseVisualStyleBackColor = true;
            this.uxProxySendClientTest.Click += new System.EventHandler(this.uxProxySendClientTest_Click);
            // 
            // uxProxySendServerTest
            // 
            this.uxProxySendServerTest.Location = new System.Drawing.Point(134, 19);
            this.uxProxySendServerTest.Name = "uxProxySendServerTest";
            this.uxProxySendServerTest.Size = new System.Drawing.Size(99, 23);
            this.uxProxySendServerTest.TabIndex = 7;
            this.uxProxySendServerTest.Text = "Send to Server";
            this.uxProxySendServerTest.UseVisualStyleBackColor = true;
            this.uxProxySendServerTest.Click += new System.EventHandler(this.uxProxySendServerTest_Click);
            // 
            // uxHookProxyStatus
            // 
            this.uxHookProxyStatus.AutoSize = true;
            this.uxHookProxyStatus.ForeColor = System.Drawing.Color.Red;
            this.uxHookProxyStatus.Location = new System.Drawing.Point(87, 53);
            this.uxHookProxyStatus.Name = "uxHookProxyStatus";
            this.uxHookProxyStatus.Size = new System.Drawing.Size(35, 13);
            this.uxHookProxyStatus.TabIndex = 5;
            this.uxHookProxyStatus.Text = "Failed";
            // 
            // uxProxyStatus
            // 
            this.uxProxyStatus.AutoSize = true;
            this.uxProxyStatus.ForeColor = System.Drawing.Color.Red;
            this.uxProxyStatus.Location = new System.Drawing.Point(87, 24);
            this.uxProxyStatus.Name = "uxProxyStatus";
            this.uxProxyStatus.Size = new System.Drawing.Size(35, 13);
            this.uxProxyStatus.TabIndex = 3;
            this.uxProxyStatus.Text = "Failed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hook Proxy.......";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Proxy...............";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 108);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "TibiaAPI Test Suite";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button uxHookProxySendServerTest;
        private System.Windows.Forms.Button uxProxySendClientTest;
        private System.Windows.Forms.Button uxProxySendServerTest;
        private System.Windows.Forms.Label uxHookProxyStatus;
        private System.Windows.Forms.Label uxProxyStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}

