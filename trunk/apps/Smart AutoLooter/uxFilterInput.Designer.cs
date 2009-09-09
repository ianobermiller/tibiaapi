namespace SmartAutoLooter
{
    partial class uxFilterInput
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
            this.label1 = new System.Windows.Forms.Label();
            this.uxRatio = new System.Windows.Forms.TextBox();
            this.uxOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDownContainer = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Minimun ValueRatio:";
            // 
            // uxRatio
            // 
            this.uxRatio.Location = new System.Drawing.Point(122, 6);
            this.uxRatio.Name = "uxRatio";
            this.uxRatio.Size = new System.Drawing.Size(74, 20);
            this.uxRatio.TabIndex = 1;
            this.uxRatio.Text = "10";
            this.uxRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.uxRatio_KeyPress);
            // 
            // uxOk
            // 
            this.uxOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uxOk.Location = new System.Drawing.Point(144, 59);
            this.uxOk.Name = "uxOk";
            this.uxOk.Size = new System.Drawing.Size(52, 20);
            this.uxOk.TabIndex = 5;
            this.uxOk.Text = "Ok";
            this.uxOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(86, 59);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(52, 20);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numericUpDownContainer
            // 
            this.numericUpDownContainer.Location = new System.Drawing.Point(122, 33);
            this.numericUpDownContainer.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownContainer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownContainer.Name = "numericUpDownContainer";
            this.numericUpDownContainer.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownContainer.TabIndex = 10;
            this.numericUpDownContainer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Container:";
            // 
            // uxFilterInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 91);
            this.Controls.Add(this.numericUpDownContainer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.uxOk);
            this.Controls.Add(this.uxRatio);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "uxFilterInput";
            this.Text = "Add Filter";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uxRatio;
        private System.Windows.Forms.Button uxOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownContainer;
        private System.Windows.Forms.Label label2;
    }
}