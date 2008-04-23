namespace SmartIPChanger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uxForm));
            this.uxServer = new System.Windows.Forms.TextBox();
            this.uxPort = new System.Windows.Forms.TextBox();
            this.uxGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uxServer
            // 
            this.uxServer.Location = new System.Drawing.Point(4, 6);
            this.uxServer.Name = "uxServer";
            this.uxServer.Size = new System.Drawing.Size(148, 20);
            this.uxServer.TabIndex = 0;
            // 
            // uxPort
            // 
            this.uxPort.Location = new System.Drawing.Point(158, 5);
            this.uxPort.Name = "uxPort";
            this.uxPort.Size = new System.Drawing.Size(45, 20);
            this.uxPort.TabIndex = 1;
            this.uxPort.Text = "7171";
            // 
            // uxGo
            // 
            this.uxGo.Location = new System.Drawing.Point(209, 3);
            this.uxGo.Name = "uxGo";
            this.uxGo.Size = new System.Drawing.Size(58, 23);
            this.uxGo.TabIndex = 2;
            this.uxGo.Text = "Go";
            this.uxGo.UseVisualStyleBackColor = true;
            this.uxGo.Click += new System.EventHandler(this.uxGo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 30);
            this.Controls.Add(this.uxGo);
            this.Controls.Add(this.uxPort);
            this.Controls.Add(this.uxServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Smart IPChanger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uxServer;
        private System.Windows.Forms.TextBox uxPort;
        private System.Windows.Forms.Button uxGo;
    }
}

