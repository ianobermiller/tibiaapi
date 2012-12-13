using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tibia.Objects;

namespace BattlelistTest
{
    public partial class uxForm : Form
    {
        Client client;
        System.Threading.Timer timer;
        List<Creature> creatures;
        ContextMenuStrip cms;

        public uxForm()
        {
            InitializeComponent();
            creatures = new List<Creature>();
            cms = new ContextMenuStrip();
            cms.Items.Add("Attack", null, new EventHandler(Attack));
            cms.Items.Add("Follow", null, new EventHandler(Follow));
            cms.Items.Add("Approach", null, new EventHandler(Approach));
            cms.Items.Add("Look", null, new EventHandler(Look));
        }

        private void uxForm_Load(object sender, EventArgs e)
        {

            client = Tibia.Util.ClientChooser.ShowBox();
            if (client == null)
            {
                MessageBox.Show("No active client.");
                Application.Exit();
                return;
            }
        }

        void UpdateList(object o)
        {

            uxBattleListTV.BeginInvoke(new EventHandler(delegate
            {
                uxBattleListTV.Nodes.Clear();
                if (client.Status == Tibia.Constants.LoginStatus.LoggedIn)
                {
                    creatures = new List<Creature>(client.BattleList.GetCreatures());
                    foreach (Creature c in creatures)
                    {
                        TreeNode tnc = new TreeNode(c.Name);                            
                        foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(c))
                        {
                            tnc.Nodes.Add(new TreeNode(pd.Name + ":" + pd.GetValue(c)));
                        }
                        tnc.ContextMenuStrip = cms;
                        tnc.Tag = c;
                        uxBattleListTV.Nodes.Add(tnc);
                    }
                }
            }));

        }

        private void uxUpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateList(null);
        }

        private void uxTimerEnableChk_CheckedChanged(object sender, EventArgs e)
        {
            if (uxTimerEnableChk.Checked)
            {
                timer = new System.Threading.Timer(new TimerCallback(UpdateList), null, 0, (long)uxMilisecondsNUD.Value);
            }
            else
            {
                timer.Dispose();
            }

        }


        private void Attack(object sender, EventArgs e)
        {
            if (uxBattleListTV.SelectedNode != null && uxBattleListTV.SelectedNode.Tag is Creature)
                ((Creature)uxBattleListTV.SelectedNode.Tag).Attack();
        }

        private void Follow(object sender, EventArgs e)
        {
            if (uxBattleListTV.SelectedNode != null && uxBattleListTV.SelectedNode.Tag is Creature)
                ((Creature)uxBattleListTV.SelectedNode.Tag).Follow();
        }

        private void Approach(object sender, EventArgs e)
        {
            if (uxBattleListTV.SelectedNode != null && uxBattleListTV.SelectedNode.Tag is Creature)
                ((Creature)uxBattleListTV.SelectedNode.Tag).Approach();
        }

        private void Look(object sender, EventArgs e)
        {
            if (uxBattleListTV.SelectedNode != null && uxBattleListTV.SelectedNode.Tag is Creature)
                ((Creature)uxBattleListTV.SelectedNode.Tag).Look();
        }
    }
}
