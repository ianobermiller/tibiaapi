using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tibia.Objects;


namespace ClientTest
{
    public partial class uxForm : Form
    {
        Client client;
        System.Threading.Timer timer;
        string clientPath;
        //0=normal, 1=worldonly, 2=widescreen
        int mode = 0;

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
            fpsUpdateTimer.Start();
        }


        void UpdateGrid(object o)
        {
            uxClientDGV.BeginInvoke(new EventHandler(delegate
            {
                var stuff = GetClientProperties(client);
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

        private List<ClientProperty> GetClientProperties(Client client)
        {
            var list = new List<ClientProperty>();
            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(client))
            {
                list.Add(new ClientProperty { Property = pd.Name, Value =pd.GetValue(client) });
            }
            return list;
        }

        class ClientProperty
        {
            public string Property { get; set; }
            public object Value { get; set; }
        }

        private void uxOpenMC_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.exe)|*.exe";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            ofd.Multiselect = false;
            ofd.Title = "Please select Tibia.exe";
            if (!string.IsNullOrEmpty(clientPath))
            {
                ofd.InitialDirectory = clientPath;
            }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Client.OpenMC(ofd.FileName, "");
                    clientPath = System.IO.Path.GetDirectoryName(ofd.FileName);
                }
                catch(Exception ex)
                {
                    throw ex;
                }

            }
        }

        private void uxWorldRB_CheckedChanged(object sender, EventArgs e)
        {
            if (uxWorldRB.Checked)
            {
                if (mode == 2)
                {
                    client.Window.WideScreenView = false;
                }
                client.Window.WorldOnlyView = true;
                mode = 1;
            }
        }

        private void uxWideRB_CheckedChanged(object sender, EventArgs e)
        {
            if (uxWideRB.Checked)
            {
                if (mode == 1)
                {
                    client.Window.WorldOnlyView = false;
                }
                client.Window.WideScreenView = true;
                mode = 2;
            }
        }

        private void uxNormalRB_CheckedChanged(object sender, EventArgs e)
        {
            if (uxNormalRB.Checked)
            {
                if (mode == 1)
                {
                    client.Window.WorldOnlyView = false;
                }
                else if (mode == 2)
                {
                    client.Window.WideScreenView = false;
                }
                mode = 0;
            }
        }

        private void uxLoginBtn_Click(object sender, EventArgs e)
        {
            client.Login.Login(uxAccountTxt.Text, uxPasswordTxt.Text, uxCharacterTxt.Text);
        }

        private void uxFreezeChk_CheckedChanged(object sender, EventArgs e)
        {
            client.Window.ActionStateFreezer = uxFreezeChk.Checked;
        }

        private void fpsUpdateTimer_Tick(object sender, EventArgs e)
        {
            var fps = Convert.ToDecimal(Math.Floor(client.Window.FPSCurrent));
            fps = (fps >= 1) ? fps : 1;
            fps = (fps <= 300) ? fps : 300;
            uxCurrentFPSNUD.Value = fps;
        }

        private void uxLimitFPSNUD_ValueChanged(object sender, EventArgs e)
        {
            client.Window.FPSLimit = Convert.ToDouble(uxLimitFPSNUD.Value);
        }

        private void uxNameSpyChk_CheckedChanged(object sender, EventArgs e)
        {
            if(uxNameSpyChk.Checked)
                client.Map.NameSpyOn();
            else
                client.Map.NameSpyOff();
        }

        private void uxFullLightChk_CheckedChanged(object sender, EventArgs e)
        {
            if (uxFullLightChk.Checked)
                client.Map.FullLightOn();
            else
                client.Map.FullLightOff();
        }

        private void uxLevelSpyChk_CheckedChanged(object sender, EventArgs e)
        {
            if (uxLevelSpyChk.Checked)
                client.Map.LevelSpyOn((int)uxLevelSpyNUD.Value);
            else
                client.Map.LevelSpyOff();
        }

        private void uxLevelSpyNUD_ValueChanged(object sender, EventArgs e)
        {
            if (uxLevelSpyChk.Checked)
                client.Map.LevelSpyOn((int)uxLevelSpyNUD.Value);
        }

        private void uxReplaceTreesBtn_Click(object sender, EventArgs e)
        {
            client.Map.ReplaceTrees();
        }

    }
}
