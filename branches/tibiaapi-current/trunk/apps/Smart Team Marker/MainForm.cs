using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tibia;
using Tibia.Util;
using Tibia.Objects;
using Tibia.Constants;
namespace SmartTeamMarker
{
    public partial class MainForm : Form
    {
        /* Pipe Related Variables */
        Client client; //Pipe is named after client's process id
        Tibia.Objects.Screen screen;

        /* Other Variables */ 
        List<Website.CharInfo> AllyMembers = new List<Website.CharInfo>();
        List<Website.CharInfo> EnemyMembers = new List<Website.CharInfo>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            do
            {
                client = ClientChooser.ShowBox();
            } while (client == null && MessageBox.Show("Please select a Tibia client", "Notification", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk) == DialogResult.Retry);

            if (client == null)
            {
                Application.Exit();
                return;
            }
            else
            {
                screen = client.Screen;
                client.ContextMenu.AddContextMenu(1000, "Add as Ally", ContextMenuType.CopyNameContextMenu, true);
                client.ContextMenu.AddContextMenu(1001, "Add as Enemy", ContextMenuType.CopyNameContextMenu, true);
                client.ContextMenu.AddContextMenu(1002, "Show Item Id", ContextMenuType.LookContextMenu, true);
                client.ContextMenu.Click += new Tibia.Objects.ContextMenu.ContextMenuEvent(ContextMenu_Click);

                client.Icon.AddIcon(1, 20, 30, 64, 1841, 0, ClientFont.NormalBorder, Color.White);
                client.Screen.DrawScreenText("ally",new Location(20,84,0),Color.LightBlue, ClientFont.NormalBorder, "Display/Hide Allies");
                client.Icon.AddIcon(2, 20, 110, 64, 9438, 0, ClientFont.NormalBorder, Color.White);
                client.Screen.DrawScreenText("enemy", new Location(20, 164, 0), Color.Red, ClientFont.NormalBorder, "Display/Hide Enemies");

                client.Icon.Click+=new Tibia.Objects.Icon.IconEvent(Icon_Click);
            }
        }

        void Icon_Click(int iconId)
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                if (iconId == 1) DisplayAllies.Checked = !DisplayAllies.Checked;
                else if (iconId == 2) DisplayEnemies.Checked = !DisplayEnemies.Checked;
            }));
        }

        private void ContextMenu_Click(int eventId)
        {
            if (eventId == 1002)
            {
                client.Statusbar = client.Memory.ReadInt32(Tibia.Addresses.Client.ClickContextMenuItemId).ToString();
            }
            else
            {
                Creature clicked = client.BattleList.GetCreatures().FirstOrDefault(
                    creature => creature.Id == client.Memory.ReadInt32(Tibia.Addresses.Client.ClickContextMenuCreatureId));
                if (clicked != null)
                {
                    Website.CharInfo character = new Website.CharInfo();
                    character.Name = clicked.Name;
                    this.Invoke(new MethodInvoker(delegate
                    {
                        if (eventId == 1000)
                        {
                            AllyMembers.Add(character);
                            AlliesList.Items.Add(new ListViewItem(new string[] {
                    character.Name,
                    character.GuildName,
                    character.GuildNickName
                }));
                        }
                        else if (eventId == 1001)
                        {
                            EnemyMembers.Add(character);
                            EnemiesList.Items.Add(new ListViewItem(new string[] {
                    character.Name,
                    character.GuildName,
                    character.GuildNickName
                }));
                        }
                    }));
                }
            }
        }



        private void cmdAddAlly_Click(object sender, EventArgs e)
        {
            List<Website.CharInfo> AllyChars = GetCharacters.ShowBox();
            //Add to Allies listview
            foreach (Website.CharInfo Ally in AllyChars)
            {
                AllyMembers.Add(Ally);
                AlliesList.Items.Add(new ListViewItem(new string[] {
                    Ally.Name,
                    Ally.GuildName,
                    Ally.GuildNickName
                }));
            }
        }

        private void cmdAddEnemy_Click(object sender, EventArgs e)
        {
            List<Website.CharInfo> EnemyChars = GetCharacters.ShowBox();
            foreach (Website.CharInfo Enemy in EnemyChars)
            {
                EnemyMembers.Add(Enemy);
                EnemiesList.Items.Add(new ListViewItem(new string[] {
                    Enemy.Name,
                    Enemy.GuildName,
                    Enemy.GuildNickName
                }));
            }
        }

        private void cmdRemoveAlly_Click(object sender, EventArgs e)
        {
            if (AlliesList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select at least one character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Website.CharInfo Character = new Website.CharInfo();
            foreach (ListViewItem SelItem in AlliesList.SelectedItems)
            {
                Character = AllyMembers.Find(delegate(Website.CharInfo c) { return c.Name == SelItem.SubItems[0].Text; });
                screen.RemoveCreatureText(Character.Name);
                AllyMembers.Remove(Character);
                AlliesList.Items.Remove(SelItem);
            }


        }

        private void cmdRemoveEnemy_Click(object sender, EventArgs e)
        {
            if (EnemiesList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select at least one character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Website.CharInfo Character = new Website.CharInfo();
            foreach (ListViewItem SelItem in AlliesList.SelectedItems)
            {
                Character = EnemyMembers.Find(delegate(Website.CharInfo c) { return c.Name == SelItem.SubItems[0].Text; });
                screen.RemoveCreatureText(Character.Name);
                EnemyMembers.Remove(Character);
                EnemiesList.Items.Remove(SelItem);
            }
        }

        private void cmdClearAlly_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all the characters?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                foreach (Website.CharInfo Character in AllyMembers)
                    screen.RemoveCreatureText(Character.Name);
                
                AlliesList.Items.Clear();
                AllyMembers.Clear();
            }
        }

        private void cmdClearEnemy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all the characters?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                foreach (Website.CharInfo Character in EnemyMembers)
                    screen.RemoveCreatureText(Character.Name);

                EnemiesList.Items.Clear();
                EnemyMembers.Clear();
            }
        }

        //Function to take care of telling DLL what characters to display
        private void ShowTexts(List<Website.CharInfo> Members, string Team)
        {
            Color color;
            if (Team == "A")
            {
                color = Color.LightBlue;
            }
            else
            {
                color = Color.Red;
            }
            foreach(Website.CharInfo Character in Members)
            {
                screen.DrawCreatureText(Character.Name, new Location(-10, 0, 0), color, ClientFont.NormalBorder, Team);
            }
        }

        private void DisplayAllies_CheckedChanged(object sender, EventArgs e)
        {
            if (DisplayAllies.Checked)
            {
                ShowTexts(AllyMembers, "A");
                cmdAddAlly.Enabled = false;
                cmdClearAlly.Enabled = false;
                cmdRemoveAlly.Enabled = false;
            }
            else
            {
                cmdAddAlly.Enabled = true;
                cmdClearAlly.Enabled = true;
                cmdRemoveAlly.Enabled = true;
                foreach (Website.CharInfo Character in AllyMembers)
                {
                    screen.RemoveCreatureText(Character.Name);
                }
                
            }
        }

        private void DisplayEnemies_CheckedChanged(object sender, EventArgs e)
        {
            if (DisplayEnemies.Checked)
            {
                ShowTexts(EnemyMembers, "E");
                cmdAddEnemy.Enabled = false;
                cmdRemoveEnemy.Enabled = false;
                cmdClearEnemy.Enabled = false;
            }
            else
            {
                cmdAddEnemy.Enabled = true;
                cmdRemoveEnemy.Enabled = true;
                cmdClearEnemy.Enabled = true;
                foreach (Website.CharInfo Character in EnemyMembers)
                    screen.RemoveCreatureText(Character.Name);
                
            }
        }
    }
}
