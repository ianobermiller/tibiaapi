namespace SmartAutoLooter
{
    partial class frmMain
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
            this.checkBoxEatFood = new System.Windows.Forms.CheckBox();
            this.checkBoxOpenBodys = new System.Windows.Forms.CheckBox();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.listBoxItems = new System.Windows.Forms.ListBox();
            this.uxContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFilteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.uxContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxEatFood
            // 
            this.checkBoxEatFood.AutoSize = true;
            this.checkBoxEatFood.Location = new System.Drawing.Point(12, 194);
            this.checkBoxEatFood.Name = "checkBoxEatFood";
            this.checkBoxEatFood.Size = new System.Drawing.Size(69, 17);
            this.checkBoxEatFood.TabIndex = 9;
            this.checkBoxEatFood.Text = "Eat Food";
            this.checkBoxEatFood.UseVisualStyleBackColor = true;
            this.checkBoxEatFood.CheckedChanged += new System.EventHandler(this.checkBoxEatFood_CheckedChanged);
            // 
            // checkBoxOpenBodys
            // 
            this.checkBoxOpenBodys.AutoSize = true;
            this.checkBoxOpenBodys.Location = new System.Drawing.Point(12, 171);
            this.checkBoxOpenBodys.Name = "checkBoxOpenBodys";
            this.checkBoxOpenBodys.Size = new System.Drawing.Size(84, 17);
            this.checkBoxOpenBodys.TabIndex = 8;
            this.checkBoxOpenBodys.Text = "Open Bodys";
            this.checkBoxOpenBodys.UseVisualStyleBackColor = true;
            this.checkBoxOpenBodys.CheckedChanged += new System.EventHandler(this.checkBoxOpenBodys_CheckedChanged);
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Location = new System.Drawing.Point(12, 148);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(59, 17);
            this.checkBoxEnable.TabIndex = 7;
            this.checkBoxEnable.Text = "Enable";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
            // 
            // listBoxItems
            // 
            this.listBoxItems.ContextMenuStrip = this.uxContext;
            this.listBoxItems.FormattingEnabled = true;
            this.listBoxItems.Location = new System.Drawing.Point(12, 21);
            this.listBoxItems.Name = "listBoxItems";
            this.listBoxItems.Size = new System.Drawing.Size(263, 121);
            this.listBoxItems.TabIndex = 6;
            // 
            // uxContext
            // 
            this.uxContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.uxContext.Name = "contextMenuStrip1";
            this.uxContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.uxContext.ShowImageMargin = false;
            this.uxContext.Size = new System.Drawing.Size(100, 120);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.idToolStripMenuItem,
            this.fFilteToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.addToolStripMenuItem.Text = "&Add";
            // 
            // idToolStripMenuItem
            // 
            this.idToolStripMenuItem.Name = "idToolStripMenuItem";
            this.idToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.idToolStripMenuItem.Text = "&Id";
            this.idToolStripMenuItem.Click += new System.EventHandler(this.idToolStripMenuItem_Click);
            // 
            // fFilteToolStripMenuItem
            // 
            this.fFilteToolStripMenuItem.Name = "fFilteToolStripMenuItem";
            this.fFilteToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.fFilteToolStripMenuItem.Text = "&Filter";
            this.fFilteToolStripMenuItem.Click += new System.EventHandler(this.fFilteToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.removeToolStripMenuItem.Text = "&Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.clearToolStripMenuItem.Text = "&Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(96, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Items:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 219);
            this.Controls.Add(this.checkBoxEatFood);
            this.Controls.Add(this.checkBoxOpenBodys);
            this.Controls.Add(this.checkBoxEnable);
            this.Controls.Add(this.listBoxItems);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Smart AutoLooter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.uxContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxEatFood;
        private System.Windows.Forms.CheckBox checkBoxOpenBodys;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private System.Windows.Forms.ListBox listBoxItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip uxContext;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fFilteToolStripMenuItem;
    }
}