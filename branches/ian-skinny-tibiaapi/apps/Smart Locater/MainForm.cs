using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tibia;
using Tibia.Util;
using Tibia.Objects;
using System.Drawing.Drawing2D;

namespace SmartLocator
{
    public partial class MainForm : Form
    {
        Client client;
        Player player;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            client = ClientChooser.ShowBox();
            if (client != null)
                if (client.LoggedIn)
                {
                    player = client.GetPlayer();
                    uxMap.Markers.Add(new MapViewer.MapMarker(player));
                }
        }

        private void uxUp_Click(object sender, EventArgs e)
        {
            uxMap.LevelUp();
        }

        private void uxDown_Click(object sender, EventArgs e)
        {
            uxMap.LevelDown();
        }

        private void uxZoomIn_Click(object sender, EventArgs e)
        {
            uxMap.Zoom(2);
        }

        private void uxZoomOut_Click(object sender, EventArgs e)
        {
            uxMap.Zoom(0.5);
        }

        private void uxButton1_Click(object sender, EventArgs e)
        {
            uxMap.LoadMap();
            uxTimer.Start();
        }

        private void uxTimer_Tick(object sender, EventArgs e)
        {
            if (player != null)
            {
                // mv.SetLevel(player.Z);
                uxMap.SetMapCenter(player.Location);
            }
        }
    }
}