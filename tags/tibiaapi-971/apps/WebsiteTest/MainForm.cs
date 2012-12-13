using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Tibia;

namespace WebsiteTest
{
    public partial class uxForm : Form
    {
        public uxForm()
        {
            InitializeComponent();
        }

        private void uxPlayerBtn_Click(object sender, EventArgs e)
        {
            Website.LookupPlayer(uxKeywordTxt.Text, new Website.LookupReceived(LookupReceivedCallback));
        }

        private void uxGuildBtn_Click(object sender, EventArgs e)
        {
            Website.GuildMembers(uxKeywordTxt.Text, new Website.GuildMembersReceived(GuildMembersReceivedCallback));
        }

        private void uxServerBtn_Click(object sender, EventArgs e)
        {
            Website.WhoIsOnline(uxKeywordTxt.Text, new Website.WhoIsOnlineReceived(WhoIsOnlineReceivedCallback));
        }

        void LookupReceivedCallback(Website.CharInfo ci)
        {
            uxDataDGV.BeginInvoke(new EventHandler(delegate
            {
                if (ci.Name == String.Empty)
                {
                    uxDataDGV.DataSource = null;
                }
                else
                {
                    var lci = new List<Website.CharInfo>();
                    lci.Add(ci);
                    uxDataDGV.AutoGenerateColumns = true;
                    uxDataDGV.DataSource = lci;
                }
            }));
        }

        void GuildMembersReceivedCallback(List<Website.CharInfo> lci)
        {
            uxDataDGV.BeginInvoke(new EventHandler(delegate
            {
                if (lci.Count == 0)
                {
                    uxDataDGV.DataSource = null;
                }
                else
                {
                    uxDataDGV.AutoGenerateColumns = false;
                    DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
                    nameColumn.DataPropertyName = "Name";
                    nameColumn.HeaderText = "Name";
                    DataGridViewTextBoxColumn nameGuildNickName = new DataGridViewTextBoxColumn();
                    nameGuildNickName.DataPropertyName = "GuildNickName";
                    nameGuildNickName.HeaderText = "NickName";
                    DataGridViewTextBoxColumn nameGuildName = new DataGridViewTextBoxColumn();
                    nameGuildName.DataPropertyName = "GuildName";
                    nameGuildName.HeaderText = "GuildName";
                    uxDataDGV.Columns.Add(nameColumn);
                    uxDataDGV.Columns.Add(nameGuildNickName);
                    uxDataDGV.Columns.Add(nameGuildName);
                    uxDataDGV.DataSource = lci;
                }
            }));
        }

        void WhoIsOnlineReceivedCallback(List<Website.CharOnline> lco)
        {
            uxDataDGV.BeginInvoke(new EventHandler(delegate
            {
                if (lco.Count == 0)
                {
                    uxDataDGV.DataSource = null;
                }
                else
                {
                    uxDataDGV.DataSource = lco;
                }
            }));
        }


    }
}
