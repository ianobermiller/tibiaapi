namespace SmartRunemaker
{
    partial class RuneChooser
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
            this.uxChoose = new System.Windows.Forms.Button();
            this.uxRunes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // uxChoose
            // 
            this.uxChoose.Location = new System.Drawing.Point(170, 3);
            this.uxChoose.Name = "uxChoose";
            this.uxChoose.Size = new System.Drawing.Size(62, 23);
            this.uxChoose.TabIndex = 3;
            this.uxChoose.Text = "Choose";
            this.uxChoose.UseVisualStyleBackColor = true;
            this.uxChoose.Click += new System.EventHandler(this.uxChoose_Click);
            // 
            // uxRunes
            // 
            this.uxRunes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uxRunes.FormattingEnabled = true;
            this.uxRunes.Location = new System.Drawing.Point(4, 4);
            this.uxRunes.Name = "uxRunes";
            this.uxRunes.Size = new System.Drawing.Size(161, 21);
            this.uxRunes.TabIndex = 2;
            // 
            // RuneChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 30);
            this.Controls.Add(this.uxChoose);
            this.Controls.Add(this.uxRunes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RuneChooser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose a Rune";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxChoose;
        private System.Windows.Forms.ComboBox uxRunes;
    }
}