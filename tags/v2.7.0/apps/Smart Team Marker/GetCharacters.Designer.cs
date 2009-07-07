namespace SmartTeamMarker
{
    partial class GetCharacters
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
            this.cmdGetMembers = new System.Windows.Forms.Button();
            this.txtGuildName = new System.Windows.Forms.TextBox();
            this.MembersList = new System.Windows.Forms.ListView();
            this.mName = new System.Windows.Forms.ColumnHeader();
            this.mGuild = new System.Windows.Forms.ColumnHeader();
            this.mGuildNick = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClearMembers = new System.Windows.Forms.Button();
            this.btnRemoveMembers = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetChar = new System.Windows.Forms.Button();
            this.txtCharName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAddMan = new System.Windows.Forms.Button();
            this.txtManNick = new System.Windows.Forms.TextBox();
            this.txtManGuild = new System.Windows.Forms.TextBox();
            this.txtManName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdGetMembers);
            this.groupBox1.Controls.Add(this.txtGuildName);
            this.groupBox1.Location = new System.Drawing.Point(11, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "by Guild";
            // 
            // cmdGetMembers
            // 
            this.cmdGetMembers.AutoSize = true;
            this.cmdGetMembers.Location = new System.Drawing.Point(147, 20);
            this.cmdGetMembers.Name = "cmdGetMembers";
            this.cmdGetMembers.Size = new System.Drawing.Size(80, 23);
            this.cmdGetMembers.TabIndex = 1;
            this.cmdGetMembers.Text = "Get Members";
            this.cmdGetMembers.UseVisualStyleBackColor = true;
            this.cmdGetMembers.Click += new System.EventHandler(this.cmdGetMembers_Click);
            // 
            // txtGuildName
            // 
            this.txtGuildName.Location = new System.Drawing.Point(6, 22);
            this.txtGuildName.Name = "txtGuildName";
            this.txtGuildName.Size = new System.Drawing.Size(135, 20);
            this.txtGuildName.TabIndex = 0;
            // 
            // MembersList
            // 
            this.MembersList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mName,
            this.mGuild,
            this.mGuildNick});
            this.MembersList.FullRowSelect = true;
            this.MembersList.GridLines = true;
            this.MembersList.Location = new System.Drawing.Point(6, 19);
            this.MembersList.Name = "MembersList";
            this.MembersList.Size = new System.Drawing.Size(499, 93);
            this.MembersList.TabIndex = 1;
            this.MembersList.UseCompatibleStateImageBehavior = false;
            this.MembersList.View = System.Windows.Forms.View.Details;
            // 
            // mName
            // 
            this.mName.Text = "Name";
            this.mName.Width = 150;
            // 
            // mGuild
            // 
            this.mGuild.Text = "Guild";
            this.mGuild.Width = 162;
            // 
            // mGuildNick
            // 
            this.mGuildNick.Text = "Guild Nickname";
            this.mGuildNick.Width = 187;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClearMembers);
            this.groupBox2.Controls.Add(this.btnRemoveMembers);
            this.groupBox2.Controls.Add(this.MembersList);
            this.groupBox2.Location = new System.Drawing.Point(11, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(513, 153);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Members";
            // 
            // btnClearMembers
            // 
            this.btnClearMembers.AutoSize = true;
            this.btnClearMembers.Location = new System.Drawing.Point(123, 118);
            this.btnClearMembers.Name = "btnClearMembers";
            this.btnClearMembers.Size = new System.Drawing.Size(104, 29);
            this.btnClearMembers.TabIndex = 3;
            this.btnClearMembers.Text = "Clear Members";
            this.btnClearMembers.UseVisualStyleBackColor = true;
            this.btnClearMembers.Click += new System.EventHandler(this.btnClearMembers_Click);
            // 
            // btnRemoveMembers
            // 
            this.btnRemoveMembers.AutoSize = true;
            this.btnRemoveMembers.Location = new System.Drawing.Point(6, 118);
            this.btnRemoveMembers.Name = "btnRemoveMembers";
            this.btnRemoveMembers.Size = new System.Drawing.Size(111, 29);
            this.btnRemoveMembers.TabIndex = 2;
            this.btnRemoveMembers.Text = "Remove Members";
            this.btnRemoveMembers.UseVisualStyleBackColor = true;
            this.btnRemoveMembers.Click += new System.EventHandler(this.btnRemoveMembers_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGetChar);
            this.groupBox3.Controls.Add(this.txtCharName);
            this.groupBox3.Location = new System.Drawing.Point(256, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 55);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "by Character";
            // 
            // btnGetChar
            // 
            this.btnGetChar.AutoSize = true;
            this.btnGetChar.Location = new System.Drawing.Point(177, 20);
            this.btnGetChar.Name = "btnGetChar";
            this.btnGetChar.Size = new System.Drawing.Size(83, 23);
            this.btnGetChar.TabIndex = 1;
            this.btnGetChar.Text = "Get Character";
            this.btnGetChar.UseVisualStyleBackColor = true;
            this.btnGetChar.Click += new System.EventHandler(this.btnGetChar_Click);
            // 
            // txtCharName
            // 
            this.txtCharName.Location = new System.Drawing.Point(6, 22);
            this.txtCharName.Name = "txtCharName";
            this.txtCharName.Size = new System.Drawing.Size(165, 20);
            this.txtCharName.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnAddMan);
            this.groupBox4.Controls.Add(this.txtManNick);
            this.groupBox4.Controls.Add(this.txtManGuild);
            this.groupBox4.Controls.Add(this.txtManName);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(11, 68);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(511, 103);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Manually (Guild and Nickname are optional)";
            // 
            // btnAddMan
            // 
            this.btnAddMan.AutoSize = true;
            this.btnAddMan.Location = new System.Drawing.Point(420, 71);
            this.btnAddMan.Name = "btnAddMan";
            this.btnAddMan.Size = new System.Drawing.Size(85, 23);
            this.btnAddMan.TabIndex = 6;
            this.btnAddMan.Text = "Add Character";
            this.btnAddMan.UseVisualStyleBackColor = true;
            this.btnAddMan.Click += new System.EventHandler(this.btnAddMan_Click);
            // 
            // txtManNick
            // 
            this.txtManNick.Location = new System.Drawing.Point(67, 68);
            this.txtManNick.Name = "txtManNick";
            this.txtManNick.Size = new System.Drawing.Size(222, 20);
            this.txtManNick.TabIndex = 5;
            // 
            // txtManGuild
            // 
            this.txtManGuild.Location = new System.Drawing.Point(67, 45);
            this.txtManGuild.Name = "txtManGuild";
            this.txtManGuild.Size = new System.Drawing.Size(222, 20);
            this.txtManGuild.TabIndex = 4;
            // 
            // txtManName
            // 
            this.txtManName.Location = new System.Drawing.Point(67, 21);
            this.txtManName.Name = "txtManName";
            this.txtManName.Size = new System.Drawing.Size(222, 20);
            this.txtManName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nickname";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Guild:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // btnOK
            // 
            this.btnOK.AutoSize = true;
            this.btnOK.Location = new System.Drawing.Point(11, 337);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 33);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Submit";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(86, 337);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 33);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // GetCharacters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 382);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GetCharacters";
            this.Text = "Add Characters";
            this.Load += new System.EventHandler(this.GetCharacters_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdGetMembers;
        private System.Windows.Forms.TextBox txtGuildName;
        private System.Windows.Forms.ListView MembersList;
        private System.Windows.Forms.ColumnHeader mName;
        private System.Windows.Forms.ColumnHeader mGuild;
        private System.Windows.Forms.ColumnHeader mGuildNick;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRemoveMembers;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGetChar;
        private System.Windows.Forms.TextBox txtCharName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddMan;
        private System.Windows.Forms.TextBox txtManNick;
        private System.Windows.Forms.TextBox txtManGuild;
        private System.Windows.Forms.TextBox txtManName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClearMembers;
        private System.Windows.Forms.Button btnCancel;
    }
}