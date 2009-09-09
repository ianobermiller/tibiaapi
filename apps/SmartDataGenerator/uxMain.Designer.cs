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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uxMain));
            this.uxTimer = new System.Windows.Forms.Timer(this.components);
            this.uxFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.uxCreatureData = new System.Windows.Forms.GroupBox();
            this.uxStart = new System.Windows.Forms.Button();
            this.uxMessage = new System.Windows.Forms.Label();
            this.uxProgress = new System.Windows.Forms.ProgressBar();
            this.uxItemID = new System.Windows.Forms.GroupBox();
            this.uxItems = new System.Windows.Forms.TextBox();
            this.uxLoadItemID = new System.Windows.Forms.Button();
            this.uxItemData = new System.Windows.Forms.GroupBox();
            this.uxItemStart = new System.Windows.Forms.Button();
            this.uxItemMessage = new System.Windows.Forms.Label();
            this.uxItemProgress = new System.Windows.Forms.ProgressBar();
            this.uxItemTimer = new System.Windows.Forms.Timer(this.components);
            this.uxFlow.SuspendLayout();
            this.uxCreatureData.SuspendLayout();
            this.uxItemID.SuspendLayout();
            this.uxItemData.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxTimer
            // 
            this.uxTimer.Tick += new System.EventHandler(this.uxTimer_Tick);
            // 
            // uxFlow
            // 
            this.uxFlow.AutoSize = true;
            this.uxFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxFlow.Controls.Add(this.uxCreatureData);
            this.uxFlow.Controls.Add(this.uxItemData);
            this.uxFlow.Controls.Add(this.uxItemID);
            this.uxFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.uxFlow.Location = new System.Drawing.Point(0, 0);
            this.uxFlow.Name = "uxFlow";
            this.uxFlow.Size = new System.Drawing.Size(331, 436);
            this.uxFlow.TabIndex = 13;
            // 
            // uxCreatureData
            // 
            this.uxCreatureData.Controls.Add(this.uxStart);
            this.uxCreatureData.Controls.Add(this.uxMessage);
            this.uxCreatureData.Controls.Add(this.uxProgress);
            this.uxCreatureData.Location = new System.Drawing.Point(3, 3);
            this.uxCreatureData.Name = "uxCreatureData";
            this.uxCreatureData.Size = new System.Drawing.Size(320, 100);
            this.uxCreatureData.TabIndex = 10;
            this.uxCreatureData.TabStop = false;
            this.uxCreatureData.Text = "Creature Data";
            // 
            // uxStart
            // 
            this.uxStart.Location = new System.Drawing.Point(6, 67);
            this.uxStart.Name = "uxStart";
            this.uxStart.Size = new System.Drawing.Size(305, 23);
            this.uxStart.TabIndex = 11;
            this.uxStart.Text = "Start";
            this.uxStart.UseVisualStyleBackColor = true;
            this.uxStart.Click += new System.EventHandler(this.uxStart_Click);
            // 
            // uxMessage
            // 
            this.uxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxMessage.Location = new System.Drawing.Point(6, 16);
            this.uxMessage.Name = "uxMessage";
            this.uxMessage.Size = new System.Drawing.Size(305, 23);
            this.uxMessage.TabIndex = 10;
            this.uxMessage.Text = "Idle";
            this.uxMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // uxProgress
            // 
            this.uxProgress.Location = new System.Drawing.Point(6, 42);
            this.uxProgress.Name = "uxProgress";
            this.uxProgress.Size = new System.Drawing.Size(305, 19);
            this.uxProgress.TabIndex = 9;
            // 
            // uxItemID
            // 
            this.uxItemID.Controls.Add(this.uxItems);
            this.uxItemID.Controls.Add(this.uxLoadItemID);
            this.uxItemID.Location = new System.Drawing.Point(3, 215);
            this.uxItemID.Name = "uxItemID";
            this.uxItemID.Size = new System.Drawing.Size(320, 212);
            this.uxItemID.TabIndex = 13;
            this.uxItemID.TabStop = false;
            this.uxItemID.Text = "Item ID";
            // 
            // uxItems
            // 
            this.uxItems.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxItems.Location = new System.Drawing.Point(6, 19);
            this.uxItems.Multiline = true;
            this.uxItems.Name = "uxItems";
            this.uxItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uxItems.Size = new System.Drawing.Size(305, 153);
            this.uxItems.TabIndex = 13;
            this.uxItems.Text = resources.GetString("uxItems.Text");
            // 
            // uxLoadItemID
            // 
            this.uxLoadItemID.Location = new System.Drawing.Point(6, 178);
            this.uxLoadItemID.Name = "uxLoadItemID";
            this.uxLoadItemID.Size = new System.Drawing.Size(305, 24);
            this.uxLoadItemID.TabIndex = 11;
            this.uxLoadItemID.Text = "Load";
            this.uxLoadItemID.UseVisualStyleBackColor = true;
            // 
            // uxItemData
            // 
            this.uxItemData.Controls.Add(this.uxItemStart);
            this.uxItemData.Controls.Add(this.uxItemMessage);
            this.uxItemData.Controls.Add(this.uxItemProgress);
            this.uxItemData.Location = new System.Drawing.Point(3, 109);
            this.uxItemData.Name = "uxItemData";
            this.uxItemData.Size = new System.Drawing.Size(320, 100);
            this.uxItemData.TabIndex = 14;
            this.uxItemData.TabStop = false;
            this.uxItemData.Text = "Item Data";
            // 
            // uxItemStart
            // 
            this.uxItemStart.Location = new System.Drawing.Point(6, 67);
            this.uxItemStart.Name = "uxItemStart";
            this.uxItemStart.Size = new System.Drawing.Size(305, 23);
            this.uxItemStart.TabIndex = 11;
            this.uxItemStart.Text = "Start";
            this.uxItemStart.UseVisualStyleBackColor = true;
            this.uxItemStart.Click += new System.EventHandler(this.uxItemStart_Click);
            // 
            // uxItemMessage
            // 
            this.uxItemMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxItemMessage.Location = new System.Drawing.Point(6, 16);
            this.uxItemMessage.Name = "uxItemMessage";
            this.uxItemMessage.Size = new System.Drawing.Size(305, 23);
            this.uxItemMessage.TabIndex = 10;
            this.uxItemMessage.Text = "Idle";
            this.uxItemMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // uxItemProgress
            // 
            this.uxItemProgress.Location = new System.Drawing.Point(6, 42);
            this.uxItemProgress.Name = "uxItemProgress";
            this.uxItemProgress.Size = new System.Drawing.Size(305, 19);
            this.uxItemProgress.TabIndex = 9;
            // 
            // uxItemTimer
            // 
            this.uxItemTimer.Tick += new System.EventHandler(this.uxItemTimer_Tick);
            // 
            // uxMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(331, 436);
            this.Controls.Add(this.uxFlow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "uxMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartDataGenerator";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.uxMain_FormClosed);
            this.uxFlow.ResumeLayout(false);
            this.uxCreatureData.ResumeLayout(false);
            this.uxItemID.ResumeLayout(false);
            this.uxItemID.PerformLayout();
            this.uxItemData.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Timer uxTimer;
        private System.Windows.Forms.FlowLayoutPanel uxFlow;
        private System.Windows.Forms.GroupBox uxCreatureData;
        public System.Windows.Forms.Button uxStart;
        public System.Windows.Forms.Label uxMessage;
        public System.Windows.Forms.ProgressBar uxProgress;
        private System.Windows.Forms.GroupBox uxItemData;
        public System.Windows.Forms.Button uxItemStart;
        public System.Windows.Forms.Label uxItemMessage;
        public System.Windows.Forms.ProgressBar uxItemProgress;
        private System.Windows.Forms.GroupBox uxItemID;
        private System.Windows.Forms.TextBox uxItems;
        public System.Windows.Forms.Button uxLoadItemID;
        public System.Windows.Forms.Timer uxItemTimer;
    }
}

