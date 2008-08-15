namespace Tibia.Util
{
    partial class ClientChooser
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
            this.uxClients = new System.Windows.Forms.ComboBox();
            this.uxChoose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uxPort = new System.Windows.Forms.TextBox();
            this.uxServer = new System.Windows.Forms.TextBox();
            this.uxPortLabel = new System.Windows.Forms.Label();
            this.uxServerLabel = new System.Windows.Forms.Label();
            this.uxUseOT = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxClients
            // 
            this.uxClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxClients.FormattingEnabled = true;
            this.uxClients.Location = new System.Drawing.Point(4, 4);
            this.uxClients.Name = "uxClients";
            this.uxClients.Size = new System.Drawing.Size(211, 21);
            this.uxClients.TabIndex = 0;
            this.uxClients.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CommonKeyUp);
            // 
            // uxChoose
            // 
            this.uxChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxChoose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uxChoose.Location = new System.Drawing.Point(221, 3);
            this.uxChoose.Name = "uxChoose";
            this.uxChoose.Size = new System.Drawing.Size(62, 23);
            this.uxChoose.TabIndex = 1;
            this.uxChoose.Text = "Choose";
            this.uxChoose.UseVisualStyleBackColor = true;
            this.uxChoose.Click += new System.EventHandler(this.uxChoose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.uxPort);
            this.groupBox1.Controls.Add(this.uxServer);
            this.groupBox1.Controls.Add(this.uxPortLabel);
            this.groupBox1.Controls.Add(this.uxServerLabel);
            this.groupBox1.Controls.Add(this.uxUseOT);
            this.groupBox1.Location = new System.Drawing.Point(4, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 75);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // uxPort
            // 
            this.uxPort.Enabled = false;
            this.uxPort.Location = new System.Drawing.Point(52, 47);
            this.uxPort.Name = "uxPort";
            this.uxPort.Size = new System.Drawing.Size(39, 20);
            this.uxPort.TabIndex = 4;
            this.uxPort.Text = "7171";
            this.uxPort.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CommonKeyUp);
            // 
            // uxServer
            // 
            this.uxServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxServer.Enabled = false;
            this.uxServer.Location = new System.Drawing.Point(52, 20);
            this.uxServer.Name = "uxServer";
            this.uxServer.Size = new System.Drawing.Size(219, 20);
            this.uxServer.TabIndex = 3;
            this.uxServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CommonKeyUp);
            // 
            // uxPortLabel
            // 
            this.uxPortLabel.AutoSize = true;
            this.uxPortLabel.Enabled = false;
            this.uxPortLabel.Location = new System.Drawing.Point(7, 47);
            this.uxPortLabel.Name = "uxPortLabel";
            this.uxPortLabel.Size = new System.Drawing.Size(26, 13);
            this.uxPortLabel.TabIndex = 5;
            this.uxPortLabel.Text = "Port";
            // 
            // uxServerLabel
            // 
            this.uxServerLabel.AutoSize = true;
            this.uxServerLabel.Enabled = false;
            this.uxServerLabel.Location = new System.Drawing.Point(7, 20);
            this.uxServerLabel.Name = "uxServerLabel";
            this.uxServerLabel.Size = new System.Drawing.Size(38, 13);
            this.uxServerLabel.TabIndex = 4;
            this.uxServerLabel.Text = "Server";
            // 
            // uxUseOT
            // 
            this.uxUseOT.AutoSize = true;
            this.uxUseOT.Location = new System.Drawing.Point(8, 0);
            this.uxUseOT.Name = "uxUseOT";
            this.uxUseOT.Size = new System.Drawing.Size(149, 17);
            this.uxUseOT.TabIndex = 2;
            this.uxUseOT.Text = "Use an Open Tibia Server";
            this.uxUseOT.UseVisualStyleBackColor = true;
            this.uxUseOT.CheckedChanged += new System.EventHandler(this.uxUseOT_CheckedChanged);
            // 
            // ClientChooser
            // 
            this.AcceptButton = this.uxChoose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 110);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.uxChoose);
            this.Controls.Add(this.uxClients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ClientChooser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose a Client";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox uxClients;
        private System.Windows.Forms.Button uxChoose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox uxPort;
        private System.Windows.Forms.TextBox uxServer;
        private System.Windows.Forms.Label uxPortLabel;
        private System.Windows.Forms.Label uxServerLabel;
        private System.Windows.Forms.CheckBox uxUseOT;
    }
}