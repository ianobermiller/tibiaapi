namespace SmartTeamMarker
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
            this.DisplayAllies = new System.Windows.Forms.CheckBox();
            this.cmdClearAlly = new System.Windows.Forms.Button();
            this.cmdRemoveAlly = new System.Windows.Forms.Button();
            this.cmdAddAlly = new System.Windows.Forms.Button();
            this.AlliesList = new System.Windows.Forms.ListView();
            this.aName = new System.Windows.Forms.ColumnHeader();
            this.aGuild = new System.Windows.Forms.ColumnHeader();
            this.aGuildNick = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DisplayEnemies = new System.Windows.Forms.CheckBox();
            this.cmdClearEnemy = new System.Windows.Forms.Button();
            this.cmdRemoveEnemy = new System.Windows.Forms.Button();
            this.cmdAddEnemy = new System.Windows.Forms.Button();
            this.EnemiesList = new System.Windows.Forms.ListView();
            this.eName = new System.Windows.Forms.ColumnHeader();
            this.eGuild = new System.Windows.Forms.ColumnHeader();
            this.eGuildNickname = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DisplayAllies);
            this.groupBox1.Controls.Add(this.cmdClearAlly);
            this.groupBox1.Controls.Add(this.cmdRemoveAlly);
            this.groupBox1.Controls.Add(this.cmdAddAlly);
            this.groupBox1.Controls.Add(this.AlliesList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Allies";
            // 
            // DisplayAllies
            // 
            this.DisplayAllies.AutoSize = true;
            this.DisplayAllies.Location = new System.Drawing.Point(414, 156);
            this.DisplayAllies.Name = "DisplayAllies";
            this.DisplayAllies.Size = new System.Drawing.Size(87, 17);
            this.DisplayAllies.TabIndex = 4;
            this.DisplayAllies.Text = "Display Allies";
            this.DisplayAllies.UseVisualStyleBackColor = true;
            this.DisplayAllies.CheckedChanged += new System.EventHandler(this.DisplayAllies_CheckedChanged);
            // 
            // cmdClearAlly
            // 
            this.cmdClearAlly.AutoSize = true;
            this.cmdClearAlly.Location = new System.Drawing.Point(250, 149);
            this.cmdClearAlly.Name = "cmdClearAlly";
            this.cmdClearAlly.Size = new System.Drawing.Size(94, 28);
            this.cmdClearAlly.TabIndex = 3;
            this.cmdClearAlly.Text = "Clear Allies List";
            this.cmdClearAlly.UseVisualStyleBackColor = true;
            this.cmdClearAlly.Click += new System.EventHandler(this.cmdClearAlly_Click);
            // 
            // cmdRemoveAlly
            // 
            this.cmdRemoveAlly.AutoSize = true;
            this.cmdRemoveAlly.Location = new System.Drawing.Point(102, 149);
            this.cmdRemoveAlly.Name = "cmdRemoveAlly";
            this.cmdRemoveAlly.Size = new System.Drawing.Size(111, 28);
            this.cmdRemoveAlly.TabIndex = 2;
            this.cmdRemoveAlly.Text = "Remove Allies";
            this.cmdRemoveAlly.UseVisualStyleBackColor = true;
            this.cmdRemoveAlly.Click += new System.EventHandler(this.cmdRemoveAlly_Click);
            // 
            // cmdAddAlly
            // 
            this.cmdAddAlly.AutoSize = true;
            this.cmdAddAlly.Location = new System.Drawing.Point(6, 149);
            this.cmdAddAlly.Name = "cmdAddAlly";
            this.cmdAddAlly.Size = new System.Drawing.Size(90, 28);
            this.cmdAddAlly.TabIndex = 1;
            this.cmdAddAlly.Text = "Add Allies";
            this.cmdAddAlly.UseVisualStyleBackColor = true;
            this.cmdAddAlly.Click += new System.EventHandler(this.cmdAddAlly_Click);
            // 
            // AlliesList
            // 
            this.AlliesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.aName,
            this.aGuild,
            this.aGuildNick});
            this.AlliesList.FullRowSelect = true;
            this.AlliesList.GridLines = true;
            this.AlliesList.Location = new System.Drawing.Point(6, 19);
            this.AlliesList.Name = "AlliesList";
            this.AlliesList.Size = new System.Drawing.Size(495, 124);
            this.AlliesList.TabIndex = 0;
            this.AlliesList.UseCompatibleStateImageBehavior = false;
            this.AlliesList.View = System.Windows.Forms.View.Details;
            // 
            // aName
            // 
            this.aName.Text = "Name";
            this.aName.Width = 150;
            // 
            // aGuild
            // 
            this.aGuild.Text = "Guild";
            this.aGuild.Width = 162;
            // 
            // aGuildNick
            // 
            this.aGuildNick.Text = "Guild Nickname";
            this.aGuildNick.Width = 187;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DisplayEnemies);
            this.groupBox2.Controls.Add(this.cmdClearEnemy);
            this.groupBox2.Controls.Add(this.cmdRemoveEnemy);
            this.groupBox2.Controls.Add(this.cmdAddEnemy);
            this.groupBox2.Controls.Add(this.EnemiesList);
            this.groupBox2.Location = new System.Drawing.Point(12, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(507, 183);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enemies";
            // 
            // DisplayEnemies
            // 
            this.DisplayEnemies.AutoSize = true;
            this.DisplayEnemies.Location = new System.Drawing.Point(398, 156);
            this.DisplayEnemies.Name = "DisplayEnemies";
            this.DisplayEnemies.Size = new System.Drawing.Size(103, 17);
            this.DisplayEnemies.TabIndex = 4;
            this.DisplayEnemies.Text = "Display Enemies";
            this.DisplayEnemies.UseVisualStyleBackColor = true;
            this.DisplayEnemies.CheckedChanged += new System.EventHandler(this.DisplayEnemies_CheckedChanged);
            // 
            // cmdClearEnemy
            // 
            this.cmdClearEnemy.AutoSize = true;
            this.cmdClearEnemy.Location = new System.Drawing.Point(250, 149);
            this.cmdClearEnemy.Name = "cmdClearEnemy";
            this.cmdClearEnemy.Size = new System.Drawing.Size(103, 28);
            this.cmdClearEnemy.TabIndex = 3;
            this.cmdClearEnemy.Text = "Clear Enemies List";
            this.cmdClearEnemy.UseVisualStyleBackColor = true;
            this.cmdClearEnemy.Click += new System.EventHandler(this.cmdClearEnemy_Click);
            // 
            // cmdRemoveEnemy
            // 
            this.cmdRemoveEnemy.AutoSize = true;
            this.cmdRemoveEnemy.Location = new System.Drawing.Point(102, 149);
            this.cmdRemoveEnemy.Name = "cmdRemoveEnemy";
            this.cmdRemoveEnemy.Size = new System.Drawing.Size(111, 28);
            this.cmdRemoveEnemy.TabIndex = 2;
            this.cmdRemoveEnemy.Text = "Remove Enemy";
            this.cmdRemoveEnemy.UseVisualStyleBackColor = true;
            this.cmdRemoveEnemy.Click += new System.EventHandler(this.cmdRemoveEnemy_Click);
            // 
            // cmdAddEnemy
            // 
            this.cmdAddEnemy.AutoSize = true;
            this.cmdAddEnemy.Location = new System.Drawing.Point(6, 149);
            this.cmdAddEnemy.Name = "cmdAddEnemy";
            this.cmdAddEnemy.Size = new System.Drawing.Size(90, 28);
            this.cmdAddEnemy.TabIndex = 1;
            this.cmdAddEnemy.Text = "Add Enemy";
            this.cmdAddEnemy.UseVisualStyleBackColor = true;
            this.cmdAddEnemy.Click += new System.EventHandler(this.cmdAddEnemy_Click);
            // 
            // EnemiesList
            // 
            this.EnemiesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.eName,
            this.eGuild,
            this.eGuildNickname});
            this.EnemiesList.FullRowSelect = true;
            this.EnemiesList.GridLines = true;
            this.EnemiesList.Location = new System.Drawing.Point(6, 19);
            this.EnemiesList.Name = "EnemiesList";
            this.EnemiesList.Size = new System.Drawing.Size(495, 124);
            this.EnemiesList.TabIndex = 0;
            this.EnemiesList.UseCompatibleStateImageBehavior = false;
            this.EnemiesList.View = System.Windows.Forms.View.Details;
            // 
            // eName
            // 
            this.eName.Text = "Name";
            this.eName.Width = 150;
            // 
            // eGuild
            // 
            this.eGuild.Text = "Guild";
            this.eGuild.Width = 162;
            // 
            // eGuildNickname
            // 
            this.eGuildNickname.Text = "Guild Nickname";
            this.eGuildNickname.Width = 187;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 409);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Smart Team Marker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView AlliesList;
        private System.Windows.Forms.ColumnHeader aName;
        private System.Windows.Forms.ColumnHeader aGuild;
        private System.Windows.Forms.ColumnHeader aGuildNick;
        private System.Windows.Forms.Button cmdClearAlly;
        private System.Windows.Forms.Button cmdRemoveAlly;
        private System.Windows.Forms.Button cmdAddAlly;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdClearEnemy;
        private System.Windows.Forms.Button cmdRemoveEnemy;
        private System.Windows.Forms.Button cmdAddEnemy;
        private System.Windows.Forms.ListView EnemiesList;
        private System.Windows.Forms.ColumnHeader eName;
        private System.Windows.Forms.ColumnHeader eGuild;
        private System.Windows.Forms.ColumnHeader eGuildNickname;
        private System.Windows.Forms.CheckBox DisplayAllies;
        private System.Windows.Forms.CheckBox DisplayEnemies;
    }
}

