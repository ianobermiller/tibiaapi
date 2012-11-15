using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tibia;
using Tibia.Objects;

namespace SmartPacketAnalyzer
{
    public partial class MemoryForm : Form
    {
        Client client;
        public MemoryForm(Client c)
        {
            client = c;
            InitializeComponent();
        }

        private void uxMemory_Load(object sender, EventArgs e)
        {
            uxTimerShort.Enabled = true;
            uxMemoryList.Items.Add(new ListViewItem(new string[]{
                "Current See ID",
                Convert.ToString(client.Addresses.Client.SeeId, 16).ToUpper(),
                String.Empty,
                DataType.Integer.ToString()
            }));
        }

        private void uxAddAddress_Click(object sender, EventArgs e)
        {
            string[] s = MemoryEntryForm.ShowNew();
            if (s != null)
                uxMemoryList.Items.Add(new ListViewItem(s));
        }

        private void uxMemoryDelete_Click(object sender, EventArgs e)
        {
            if (uxMemoryList.SelectedIndices.Count > 0)
            {
                uxMemoryList.Items.RemoveAt(uxMemoryList.SelectedIndices[0]);
            }
        }

        private void uxMemoryEdit_Click(object sender, EventArgs e)
        {
            if (uxMemoryList.SelectedIndices.Count > 0)
            {
                int index = uxMemoryList.SelectedIndices[0];
                string[] s = MemoryEntryForm.ShowEdit(new string[]{
                    uxMemoryList.Items[index].SubItems[0].Text,
                    uxMemoryList.Items[index].SubItems[1].Text,
                    uxMemoryList.Items[index].SubItems[2].Text,
                    uxMemoryList.Items[index].SubItems[3].Text
                });
                if (s != null)
                    uxMemoryList.Items[index] = new ListViewItem(s);
            }
        }

        private void uxClearAddresses_Click(object sender, EventArgs e)
        {
            uxMemoryList.Items.Clear();
        }

        private void uxTimerShort_Tick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in uxMemoryList.Items)
            {
                try
                {
                    long address = Int32.Parse(item.SubItems[1].Text, System.Globalization.NumberStyles.HexNumber);
                    switch ((DataType)Enum.Parse(typeof(DataType), item.SubItems[3].Text))
                    {
                        case DataType.Byte:
                            item.SubItems[2].Text = client.Memory.ReadByte(address).ToString();
                            break;
                        case DataType.Integer:
                            item.SubItems[2].Text = client.Memory.ReadInt32(address).ToString();
                            break;
                        case DataType.Double:
                            item.SubItems[2].Text = client.Memory.ReadDouble(address).ToString();
                            break;
                        case DataType.String:
                            item.SubItems[2].Text = client.Memory.ReadString(address, 255).ToString();
                            break;
                        case DataType.Pointer:
                            item.SubItems[2].Text = "0x" + Convert.ToString(client.Memory.ReadInt32(address), 16).ToUpper();
                            break;
                    }
                }
                catch
                {
                    item.SubItems[2].Text = "N/A";
                }
            }
        }
    }

    public enum DataType
    {
        Byte,
        Integer,
        Double,
        String,
        Pointer
    }
}
