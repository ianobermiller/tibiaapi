namespace SmartDataGenerator {
    partial class uxMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.uxProgress = new System.Windows.Forms.ProgressBar();
            this.uxMessage = new System.Windows.Forms.Label();
            this.uxStart = new System.Windows.Forms.Button();
            this.uxTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // uxProgress
            // 
            this.uxProgress.Location = new System.Drawing.Point(12, 35);
            this.uxProgress.Name = "uxProgress";
            this.uxProgress.Size = new System.Drawing.Size(305, 19);
            this.uxProgress.TabIndex = 0;
            // 
            // uxMessage
            // 
            this.uxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxMessage.Location = new System.Drawing.Point(12, 9);
            this.uxMessage.Name = "uxMessage";
            this.uxMessage.Size = new System.Drawing.Size(305, 23);
            this.uxMessage.TabIndex = 1;
            this.uxMessage.Text = "Idle";
            this.uxMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uxStart
            // 
            this.uxStart.Location = new System.Drawing.Point(12, 60);
            this.uxStart.Name = "uxStart";
            this.uxStart.Size = new System.Drawing.Size(305, 23);
            this.uxStart.TabIndex = 2;
            this.uxStart.Text = "Start";
            this.uxStart.UseVisualStyleBackColor = true;
            this.uxStart.Click += new System.EventHandler(this.uxStart_Click);
            // 
            // uxTimer
            // 
            this.uxTimer.Tick += new System.EventHandler(this.uxTimer_Tick);
            // 
            // uxMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 95);
            this.Controls.Add(this.uxStart);
            this.Controls.Add(this.uxMessage);
            this.Controls.Add(this.uxProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "uxMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartDataGenerator";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.uxMain_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ProgressBar uxProgress;
        public System.Windows.Forms.Label uxMessage;
        public System.Windows.Forms.Button uxStart;
        public System.Windows.Forms.Timer uxTimer;
    }
}

