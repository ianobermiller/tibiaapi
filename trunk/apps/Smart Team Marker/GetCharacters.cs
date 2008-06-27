using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tibia;

namespace SmartTeamMarker
{
    public partial class GetCharacters : Form
    {
        private static List<Tibia.Website.CharInfo> Members = new List<Website.CharInfo>();
        private static GetCharacters newGetCharacters;

        public GetCharacters()
        {
            InitializeComponent();
        }

        #region Get Guild/Char Info

        private void GuildMembersReceived(List<Website.CharInfo> GuildMembers)
        {
            
            if (GuildMembers.Count == 0)
            {
                MessageBox.Show("Guild doesn't exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default; //Reset cursor to deafult
                return;
            }
            bool AlreadyInList = false;
            foreach (Website.CharInfo Character in GuildMembers)
            {
                foreach (Website.CharInfo MemberChar in Members)
                {
                    if (MemberChar.Name == Character.Name)
                    {
                        MessageBox.Show("Character " + Character.Name + " is already at the list.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AlreadyInList = true;
                        break;
                    }
                }
                if (!AlreadyInList)
                {
                    Members.Add(Character);

                    MembersList.Invoke(new EventHandler(delegate
                        {
                            //Update the ListView
                            MembersList.Items.Add(new ListViewItem(new string[] {
                            Character.Name,
                            Character.GuildName,
                            Character.GuildNickName
                            }));
                        }));
                }
                AlreadyInList = false;
            }
            this.Cursor = Cursors.Default; //Reset cursor to deafult
        }

        private void CharacterReceived(Website.CharInfo Character)
        {
            if (Character.Name == string.Empty)
            {
                MessageBox.Show("Character doesn't exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default; //Reset cursor to deafult
                return;
            }

            foreach (Website.CharInfo MemberChar in Members)
            {
                if (MemberChar.Name == Character.Name)
                {
                    MessageBox.Show("Character is already at the list", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default; //Reset cursor to deafult
                    return;
                }
            }

            Members.Add(Character);

            MembersList.Invoke(new EventHandler(delegate
            {
                //Update the ListView
                MembersList.Items.Add(new ListViewItem(new string[] {
                Character.Name,
                Character.GuildName,
                Character.GuildNickName
                }));
            }));
            this.Cursor = Cursors.Default; //Reset cursor to deafult
        }

        #endregion

        public static List<Tibia.Website.CharInfo> ShowBox()
        {
            newGetCharacters = new GetCharacters();
            newGetCharacters.ShowDialog();
            return Members;
        }

        private void cmdGetMembers_Click(object sender, EventArgs e)
        {
            if (txtGuildName.Text == string.Empty)
            {
                MessageBox.Show("Enter Guild Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.Cursor = Cursors.WaitCursor; //Change the cursor
            Website.GuildMembers(txtGuildName.Text, GuildMembersReceived);
            txtGuildName.Text = string.Empty;
        }

        private void btnGetChar_Click(object sender, EventArgs e)
        {
            if (txtCharName.Text == string.Empty)
            {
                MessageBox.Show("Enter Character Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.Cursor = Cursors.WaitCursor; //Change the cursor
            Website.LookupPlayer(txtCharName.Text, CharacterReceived);
            txtCharName.Text = string.Empty;
        }

        private void btnAddMan_Click(object sender, EventArgs e)
        {
            if (txtManName.Text == string.Empty)
            {
                MessageBox.Show("Enter Character Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (Website.CharInfo MemberChar in Members)
            {
                if (MemberChar.Name.ToLower() == txtManName.Text.ToLower())
                {
                    MessageBox.Show("Character is already at the list", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Website.CharInfo Character = new Website.CharInfo();
            Character.GuildName = txtManGuild.Text;
            Character.Name = txtManName.Text;
            Character.GuildNickName = txtManNick.Text;

            txtManGuild.Text = string.Empty;
            txtManName.Text = string.Empty;
            txtManNick.Text = string.Empty;

            Members.Add(Character);

            MembersList.Items.Add(new ListViewItem(new string[] {
                Character.Name,
                Character.GuildName,
                Character.GuildNickName
            }));
                
        }

        private void btnClearMembers_Click(object sender, EventArgs e)
        {
            Members.Clear();
            MembersList.Items.Clear();
        }

        private void btnRemoveMembers_Click(object sender, EventArgs e)
        {
            if (MembersList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select at least one character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Website.CharInfo Character = new Website.CharInfo();
            foreach (ListViewItem SelItem in MembersList.SelectedItems)
            {
                Character = Members.Find(delegate(Website.CharInfo c) { return c.Name == SelItem.SubItems[0].Text; });
                Members.Remove(Character);
                MembersList.Items.Remove(SelItem);
            }
                
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Members.Clear();
            this.Dispose();
        }

        private void GetCharacters_Load(object sender, EventArgs e)
        {
            Members.Clear();
        }          

    }
}
