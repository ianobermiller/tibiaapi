namespace SmartIPChanger
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
            this.uxGo = new System.Windows.Forms.Button();
            this.uxServer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // uxGo
            // 
            this.uxGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxGo.Location = new System.Drawing.Point(244, 3);
            this.uxGo.Name = "uxGo";
            this.uxGo.Size = new System.Drawing.Size(58, 23);
            this.uxGo.TabIndex = 2;
            this.uxGo.Text = "Go";
            this.uxGo.UseVisualStyleBackColor = true;
            this.uxGo.Click += new System.EventHandler(this.uxGo_Click);
            // 
            // uxServer
            // 
            this.uxServer.AllowDrop = true;
            this.uxServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxServer.FormattingEnabled = true;
            this.uxServer.Location = new System.Drawing.Point(6, 3);
            this.uxServer.Name = "uxServer";
            this.uxServer.Size = new System.Drawing.Size(232, 21);
            this.uxServer.TabIndex = 3;
            // 
            // uxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 30);
            this.Controls.Add(this.uxServer);
            this.Controls.Add(this.uxGo);
            this.Name = "uxForm";
            this.Text = "Smart IPChanger";
            this.Load += new System.EventHandler(this.uxForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxGo;
        private System.Windows.Forms.ComboBox uxServer;
    }
}

