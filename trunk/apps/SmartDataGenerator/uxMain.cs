using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tibia.Util;

namespace SmartDataGenerator {
    public partial class uxMain : Form {
        public uxMain() {
            InitializeComponent();
        }

        private void uxStart_Click(object sender, EventArgs e) {
            if (CreatureData.State == DataState.Idle) {
                CreatureData.CreaturesToFile();
                uxStart.Text = "Stop";
                uxTimer.Start();
            }
            else {
                CreatureData.Thread.Abort();
                CreatureData.ListsTextWriter.Close();
                CreatureData.MainTextWriter.Close();
                CreatureData.State = DataState.Idle;
                uxStart.Text = "Start";
                uxTimer.Stop();
                uxMessage.Text = "Idle";
                this.Text = "SmartDataGenerator";
            }
        }

        private void uxTimer_Tick(object sender, EventArgs e) {
            int percent = 0;
            if (CreatureData.State != DataState.Idle && CreatureData.TotalCreatures > 0)
                percent = 100 * (3 * CreatureData.LoadedCreatures + CreatureData.ParsedCreatures) / (CreatureData.TotalCreatures * 4);
            string message = CreatureData.State + " - " + ((CreatureData.State == DataState.Downloading) ? CreatureData.LoadedCreatures : CreatureData.ParsedCreatures) + "/" + CreatureData.TotalCreatures + " (" + percent + "%)";
            uxMessage.Text = message;
            uxProgress.Value = percent;
            this.Text = message;
        }

        private void uxMain_FormClosed(object sender, FormClosedEventArgs e) {
            if (CreatureData.Thread.IsAlive)
                CreatureData.Thread.Abort();
            CreatureData.MainTextWriter.Close();
            CreatureData.ListsTextWriter.Close();
        }
    }
}
