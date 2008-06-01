using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Tibia;
using Tibia.Objects;
using Tibia.Util;

namespace SmartIPChanger
{
    public partial class uxForm : Form
    {
        public uxForm()
        {
            InitializeComponent();
        }

        private void uxGo_Click(object sender, EventArgs e)
        {
            Go();
        }

        private void Go()
        {
            Client client = ClientChooser.ShowBox(new ClientChooserOptions() { ShowOTOption = false });
            if (client == null)
            {
                client = Client.Open();
                System.Threading.Thread.Sleep(1000);
            }
            client.SetOT(uxServer.Text, short.Parse(uxPort.Text));
        }

        private void CommonKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Go();
            }
        }
    }
}