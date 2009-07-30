using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Tibia.Objects;
using System.Diagnostics;

namespace Tibia.Util
{
    public partial class ClientChooser : Form
    {
        private static ClientChooser newClientChooser;
        private static Objects.Client client;

        private ClientChooserOptions options;
        
        public ClientChooser()
        {
            InitializeComponent();
            client = null;
        }

        /// <summary>
        /// Opens a box to pick a client.
        /// </summary>
        /// <returns></returns>
        public static Client ShowBox()
        {
            return ShowBox(new ClientChooserOptions());
        }
        /// <summary>
        /// Open a box to pick a client with the desired options.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Client ShowBox(ClientChooserOptions options)
        {
            List<Objects.Client> clients = null;
            if (options.LookUpClients)
            {
                clients = Objects.Client.GetClients(options.Version);
            }
            if (options.Smart && 
                options.LookUpClients && 
                !options.ShowOTOption && 
                clients != null && 
                clients.Count == 1)
            {
                return clients[0];
            }
            else
            {
                newClientChooser = new ClientChooser();
                newClientChooser.Text = String.IsNullOrEmpty(options.Title) ? "Choose a client." : options.Title;

                if (options.LookUpClients)
                {
                    foreach (Client c in clients)
                    {
                        newClientChooser.uxClients.Items.Add(c);
                    }
                }

                if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Tibia\tibia.exe")))
                {
                    newClientChooser.uxClients.Items.Add(ClientChooserBase.NewClientDefaultText);
                }

                foreach (ClientPathInfo cpi in
                    ClientChooserBase.GetClientPaths(options.SavedClientPathsLocation))
                {
                    newClientChooser.uxClients.Items.Add(cpi);
                }

                newClientChooser.uxClients.Items.Add(ClientChooserBase.NewClientCustomText);
                newClientChooser.uxClients.SelectedIndex = 0;

                foreach (string address in options.Addresses)
                {
                    newClientChooser.uxLoginServer.Items.Add(address);
                }

                if (newClientChooser.uxLoginServer.Items.Count > 0)
                    newClientChooser.uxLoginServer.SelectedIndex = 0;

                if (options.ShowOTOption)
                {
                    newClientChooser.Height = 109;
                    newClientChooser.uxUseOT.Checked = options.UseOT;
                    newClientChooser.SetOTState();
                    newClientChooser.uxLoginServer.Text = options.Server + ":" + options.Port.ToString();
                }
                else
                {
                    newClientChooser.Height = 54;
                }

                newClientChooser.options = options;
                newClientChooser.ShowDialog();
                return client;
            }
        }

        private void uxChoose_Click(object sender, EventArgs e)
        {
            ChooseClient();
        }

        private void uxUseOT_CheckedChanged(object sender, EventArgs e)
        {
            SetOTState();
            if (uxUseOT.Checked)
                uxLoginServer.Focus();
        }

        public void SetOTState()
        {
            newClientChooser.uxLoginServer.Enabled = uxUseOT.Checked;
            newClientChooser.uxLoginServerLabel.Enabled = uxUseOT.Checked;
        }

        private void ChooseClient()
        {
            options.UseOT = uxUseOT.Checked;
            LoginServer ls = null;
            if (options.UseOT)
            {
                string[] split = uxLoginServer.Text.Split(new char[] { ':' });
                ls = new LoginServer(split[0], short.Parse(split[1]));
            }
            client = ClientChooserBase.ChooseClient(options, uxClients.SelectedItem, ls);
            newClientChooser.Dispose();
        }

        private void CommonKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChooseClient();
            }
        }
    }
}