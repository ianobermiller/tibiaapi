using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tibia.Objects;


namespace PlayerTest
{
    public partial class uxForm : Form
    {
        Client client;
        System.Threading.Timer timer;

        public uxForm()
        {
            InitializeComponent();
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
            uxClientDGV.BeginInvoke(new EventHandler(delegate
            {
                var stuff = GetPlayerProperties(client);
                uxClientDGV.DataSource = stuff;
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

        private List<ClientProperty> GetPlayerProperties(Client client)
        {
            var list = new List<ClientProperty>();
            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(client.Player))
            {
                list.Add(new ClientProperty { Property = pd.Name, Value =pd.GetValue(client.Player) });
            }
            return list;
        }

        class ClientProperty
        {
            public string Property { get; set; }
            public object Value { get; set; }
        }
    }
}
