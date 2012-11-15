using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tibia.Objects;


namespace HotkeyTest
{
    public partial class uxForm : Form
    {
        Client client;
        List<Hotkey> hotkeyList;
        System.Threading.Timer timer;

        public uxForm()
        {
            InitializeComponent();
            uxHotkeysDGV.DataSource = new List<Hotkey>();
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


        void UpdateGrid(object o)
        {
            uxHotkeysDGV.BeginInvoke(new EventHandler(delegate
            {
                hotkeyList = new List<Hotkey>((int)client.Addresses.Hotkey.MaxHotkeys);
                for (byte num = 0; num < client.Addresses.Hotkey.MaxHotkeys; num++)
                {
                    hotkeyList.Add(new Hotkey(client, num));
                }
                uxHotkeysDGV.DataSource = hotkeyList;
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
