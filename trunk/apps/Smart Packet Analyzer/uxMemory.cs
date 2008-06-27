using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartPacketAnalyzer
{
    public partial class uxMemory : Form
    {
        private static uxMemory newMemory;
        private static string[] s;
        
        public uxMemory()
        {
            InitializeComponent();
            foreach (string name in Enum.GetNames(typeof(DataTypes)))
                uxType.Items.Add(name);
            s = null;
        }

        public static string[] ShowNew()
        {
            newMemory = new uxMemory();
            newMemory.Text = "New memory address";
            newMemory.uxAdd.Text = "Add";
            newMemory.uxType.SelectedIndex = 1;
            s = null;
            newMemory.ShowDialog();
            return s;
        }

        public static string[] ShowEdit(string[] data)
        {
            newMemory = new uxMemory();
            newMemory.Text = "Edit memory address";
            newMemory.uxAdd.Text = "Edit";
            newMemory.uxDescription.Text = data[0];
            newMemory.uxAddress.Text = data[1];
            newMemory.uxType.SelectedIndex = newMemory.uxType.Items.IndexOf(data[3]);
            newMemory.ShowDialog();
            return s;
        }



        private void uxAdd_Click(object sender, EventArgs e)
        {
            s = new string[]{
                uxDescription.Text,
                uxAddress.Text,
                String.Empty,
                uxType.Text
            };
            newMemory.Dispose();
        }

        private void uxCancel_Click(object sender, EventArgs e)
        {
            newMemory.Dispose();
        }

        private void AllControls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                uxAdd_Click(null, null);
        }
    }
}