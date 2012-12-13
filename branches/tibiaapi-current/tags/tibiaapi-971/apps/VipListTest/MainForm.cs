using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tibia.Objects;


namespace VipListTest
{
    public partial class uxForm : Form
    {
        Client client;
        VipList vipList;
        System.Threading.Timer timer;

        public uxForm()
        {
            InitializeComponent();
            uxVipListDGV.DataSource = new List<Vip>();
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
            vipList = new VipList(client);
        }


        void UpdateGrid(object o)
        {
            uxVipListDGV.BeginInvoke(new EventHandler(delegate
            {

                if (client.Status == Tibia.Constants.LoginStatus.LoggedIn)
                {
                    uxVipListDGV.DataSource = vipList.GetPlayers();
                }
                else
                {

                    uxVipListDGV.DataSource = new List<Vip>();
                }
            }));

        }

        private void uxUpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateGrid(null);
        }

        private void uxTimerEnableChk_CheckedChanged(object sender, EventArgs e)
        {
            if (uxTimerEnableChk.Checked)
            {
                timer = new System.Threading.Timer(new TimerCallback(UpdateGrid), null, 0, (long)uxMilisecondsNUD.Value);
            }
            else
            {
                timer.Dispose();
            }

        }

    }
}
