namespace SmartLocator
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
            this.components = new System.ComponentModel.Container();
            this.uxZoomOut = new System.Windows.Forms.Button();
            this.uxZoomIn = new System.Windows.Forms.Button();
            this.uxDown = new System.Windows.Forms.Button();
            this.uxUp = new System.Windows.Forms.Button();
            this.uxButton1 = new System.Windows.Forms.Button();
            this.uxTimer = new System.Windows.Forms.Timer(this.components);
            this.uxMap = new Tibia.Util.MapViewer();
            this.SuspendLayout();
            // 
            // uxZoomOut
            // 
            this.uxZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxZoomOut.Location = new System.Drawing.Point(402, 155);
            this.uxZoomOut.Name = "uxZoomOut";
            this.uxZoomOut.Size = new System.Drawing.Size(23, 23);
            this.uxZoomOut.TabIndex = 19;
            this.uxZoomOut.Text = "-";
            this.uxZoomOut.UseVisualStyleBackColor = true;
            this.uxZoomOut.Click += new System.EventHandler(this.uxZoomOut_Click);
            // 
            // uxZoomIn
            // 
            this.uxZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxZoomIn.Location = new System.Drawing.Point(402, 126);
            this.uxZoomIn.Name = "uxZoomIn";
            this.uxZoomIn.Size = new System.Drawing.Size(23, 23);
            this.uxZoomIn.TabIndex = 18;
            this.uxZoomIn.Text = "+";
            this.uxZoomIn.UseVisualStyleBackColor = true;
            this.uxZoomIn.Click += new System.EventHandler(this.uxZoomIn_Click);
            // 
            // uxDown
            // 
            this.uxDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxDown.Location = new System.Drawing.Point(393, 97);
            this.uxDown.Name = "uxDown";
            this.uxDown.Size = new System.Drawing.Size(45, 23);
            this.uxDown.TabIndex = 16;
            this.uxDown.Text = "Down";
            this.uxDown.UseVisualStyleBackColor = true;
            this.uxDown.Click += new System.EventHandler(this.uxDown_Click);
            // 
            // uxUp
            // 
            this.uxUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxUp.Location = new System.Drawing.Point(393, 69);
            this.uxUp.Name = "uxUp";
            this.uxUp.Size = new System.Drawing.Size(45, 23);
            this.uxUp.TabIndex = 15;
            this.uxUp.Text = "Up";
            this.uxUp.UseVisualStyleBackColor = true;
            this.uxUp.Click += new System.EventHandler(this.uxUp_Click);
            // 
            // uxButton1
            // 
            this.uxButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxButton1.Location = new System.Drawing.Point(393, 12);
            this.uxButton1.Name = "uxButton1";
            this.uxButton1.Size = new System.Drawing.Size(45, 51);
            this.uxButton1.TabIndex = 14;
            this.uxButton1.Text = "Load";
            this.uxButton1.UseVisualStyleBackColor = true;
            this.uxButton1.Click += new System.EventHandler(this.uxButton1_Click);
            // 
            // uxTimer
            // 
            this.uxTimer.Interval = 500;
            this.uxTimer.Tick += new System.EventHandler(this.uxTimer_Tick);
            // 
            // uxMap
            // 
            this.uxMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxMap.Directory = "C:\\Users\\Ian\\AppData\\Roaming\\Tibia\\Automap\\";
            this.uxMap.DrawCoors = true;
            this.uxMap.DrawCrosshair = true;
            this.uxMap.Location = new System.Drawing.Point(13, 13);
            this.uxMap.Name = "uxMap";
            this.uxMap.OpenTibiaMaps = false;
            this.uxMap.Size = new System.Drawing.Size(374, 363);
            this.uxMap.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 388);
            this.Controls.Add(this.uxMap);
            this.Controls.Add(this.uxZoomOut);
            this.Controls.Add(this.uxZoomIn);
            this.Controls.Add(this.uxDown);
            this.Controls.Add(this.uxUp);
            this.Controls.Add(this.uxButton1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxZoomOut;
        private System.Windows.Forms.Button uxZoomIn;
        private System.Windows.Forms.Button uxDown;
        private System.Windows.Forms.Button uxUp;
        private System.Windows.Forms.Button uxButton1;
        private System.Windows.Forms.Timer uxTimer;
        private Tibia.Util.MapViewer uxMap;
    }
}