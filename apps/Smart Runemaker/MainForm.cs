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

namespace SmartRunemaker
{
    public partial class MainForm : Form
    {
        private Rune rune;
        private Client client;
        private Player player;
        
        public MainForm()
        {
           InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            client = ClientChooser.ShowBox();

            if (client == null || !client.LoggedIn)
            {
                MessageBox.Show("You must have at least one client open and logged in to start this program.");
                Application.Exit();
            }
            else
            {
                rune = RuneChooser.ShowBox();
                notifyIcon.Icon = new Icon(GetType(), "icon.ico");
                start();
            }
        }

        private void start()
        {
            try
            {
                player = client.GetPlayer();
                uxShortTimer.Start();
                uxLongTimer.Start();
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (client.LoggedIn)
            {
                if (player.Mana == player.Mana_Max)
                    client.MakeRune(rune);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (client.LoggedIn)
            {
                client.EatFood();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show();
            //WindowState = FormWindowState.Normal;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        private void exitNotifyIconMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Application.Exit();
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewForm preview = new PreviewForm();
            preview.showPreview(client);
        }
    }
}