using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tibia;
using Tibia.Objects;
using Tibia.Util;
using Tibia.Packets;

namespace SmartPacketAnalyzer
{
    public partial class uxForm : Form
    {
        bool LogPackets = true;
        List<CapturedPacket> packetList = new List<CapturedPacket>();
        byte[] displayedPacket = null;

        Client client;


        public uxForm()
        {
            InitializeComponent();
        }

        private void uxStart_Click(object sender, EventArgs e)
        {
            if (LogPackets)
            {
                LogPackets = false;
                uxStart.Text = "Start Packet Logging";
            }
            else
            {
                LogPackets = true;
                uxStart.Text = "Stop Packet Logging";
            }
        }

        private void uxForm_Load(object sender, EventArgs e)
        {
            client = Tibia.Util.ClientChooser.ShowBox();

            if (client == null)
            {
                MessageBox.Show("No active client.");
                Application.Exit();
            }
            else
            {
                client.StartProxy();
                client.Proxy.ReceivedPacketFromClient += PacketFromClient;
                client.Proxy.ReceivedPacketFromServer += PacketFromServer;
                client.Proxy.SplitPacketFromServer += SplitPacketFromServer;
                client.Proxy.ReceivedStatusMessagePacket += ReceivedStatusMessagePacket;
                uxTimerShort.Enabled = true;
            }

            uxMemoryList.Items.Add(new ListViewItem(new string[]{
                "Current See ID",
                Convert.ToString(Tibia.Addresses.Client.See_Id, 16).ToUpper(),
                String.Empty,
                DataTypes.Integer.ToString()
            }));
        }

        private bool PacketFromClient(Packet packet)
        {
            if (uxLogClient.Checked)
            {
                if (uxLogHeader.Checked)
                {
                    if (uxHeaderByte.Text.Length == 2 && 
                        (byte)packet.Type == uxHeaderByte.Text.ToBytesAsHex()[0])
                    {
                        LogPacket(packet.Data, "CLIENT", "SERVER");
                    }
                }
                else
                {
                    LogPacket(packet.Data, "CLIENT", "SERVER");
                }
            }
            return true;
        }

        private bool SplitPacketFromServer(Packet packet)
        {
            if (uxLogSplit.Checked && uxLogServer.Checked)
            {
                if (uxLogHeader.Checked)
                {
                    if (uxHeaderByte.Text.Length == 2 &&
                        (byte)packet.Type == uxHeaderByte.Text.ToBytesAsHex()[0])
                    {
                        LogPacket(packet.Data, "SERVER*", "CLIENT");
                    }
                }
                else
                {
                    LogPacket(packet.Data, "SERVER*", "CLIENT");
                }
            }
            return true;
        }

        private bool PacketFromServer(Packet packet)
        {
            if (uxLogServer.Checked)
            {
                if (uxLogHeader.Checked)
                {
                    if (uxHeaderByte.Text.Length == 2 &&
                        (byte)packet.Type == uxHeaderByte.Text.ToBytesAsHex()[0])
                    {
                        LogPacket(packet.Data, "SERVER", "CLIENT");
                    }
                }
                else
                {
                    LogPacket(packet.Data, "SERVER", "CLIENT");
                }
            }
            return true;
        }

        private bool ReceivedStatusMessagePacket(Packet packet)
        {
            StatusMessagePacket p = (StatusMessagePacket)packet;
            if (p.Color == StatusMessageType.Description && p.Message.StartsWith("You see "))
            {
                client.Send(
                    StatusMessagePacket.Create(
                        client,
                        StatusMessageType.Description,
                        p.Message + " [" + client.ReadInt(Tibia.Addresses.Client.Click_Id) + "]"));
                return false;
            }
            return true;
        }

        private void LogPacket(byte[] packet, string from, string to)
        {
            if (LogPackets)
            {
                uxPacketList.Invoke(new EventHandler(delegate
                {
                    CapturedPacket cp = new CapturedPacket(packet, from, packet.Length, to);
                    packetList.Add(cp);
                    uxPacketList.Items.Add(new ListViewItem(new string[]{
                        cp.Time,
                        cp.Source,
                        cp.Destination,
                        cp.Length.ToString(),
                        Convert.ToString(cp.Type, 16).PadLeft(2, '0').ToUpper()
                    }));
                    uxPacketList.EnsureVisible(uxPacketList.Items.Count - 1);
                }));
            }
        }

        private void uxPacketList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uxPacketList.SelectedIndices.Count > 0)
            {
                CapturedPacket cp = packetList[uxPacketList.SelectedIndices[0]];
                displayedPacket = cp.Data;
            }
            else
            {
                displayedPacket = null;
            }
            DisplayPacket();
        }

        private void DisplayPacket()
        {
            if (displayedPacket != null)
            {
                int charWidth = TextRenderer.MeasureText("0", uxPacketDisplay.Font).Width / 2;
                int widthInChars = uxPacketDisplay.Width / charWidth - 2;
                int bytesPerLine = (widthInChars - 1) / 4;
                string s = string.Empty;
                int index = 0;
                int left = displayedPacket.Length;

                while (index < displayedPacket.Length)
                {
                    int byteCount = (bytesPerLine < left) ? bytesPerLine : left;
                    string line = displayedPacket.ToHexString(index, byteCount);
                    s += line.PadRight(bytesPerLine * 3);
                    s += " " + displayedPacket.ToPrintableString(index, byteCount) + Environment.NewLine;
                    index += bytesPerLine;
                    left -= bytesPerLine;
                }

                uxPacketDisplay.Text = s;
            }
            else
            {
                uxPacketDisplay.Clear();
            }
        }

        private void uxClearPackets_Click(object sender, EventArgs e)
        {
            packetList.Clear();
            uxPacketList.Items.Clear();
            uxPacketDisplay.Clear();
        }

        private void ConvertToInt_Click(object sender, EventArgs e)
        {
            if (uxPacketDisplay.SelectedText.Length >= 5)
                MessageBox.Show(uxPacketDisplay.SelectedText.ToIntAsHex().ToString());
        }

        private void CopyAllBytes_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(displayedPacket.ToHexString());
        }

        private void uxTimerShort_Tick(object sender, EventArgs e)
        {
            foreach(ListViewItem item in uxMemoryList.Items)
            {
                try
                {
                    long address = Int32.Parse(item.SubItems[1].Text, System.Globalization.NumberStyles.HexNumber);
                    switch ((DataTypes)Enum.Parse(typeof(DataTypes), item.SubItems[3].Text))
                    {
                        case DataTypes.Byte:
                            item.SubItems[2].Text = client.ReadByte(address).ToString();
                            break;
                        case DataTypes.Integer:
                            item.SubItems[2].Text = client.ReadInt(address).ToString();
                            break;
                        case DataTypes.Double:
                            item.SubItems[2].Text = client.ReadDouble(address).ToString();
                            break;
                        case DataTypes.String:
                            item.SubItems[2].Text = client.ReadString(address, 255).ToString();
                            break;
                        case DataTypes.Pointer:
                            item.SubItems[2].Text = "0x" + Convert.ToString(client.ReadInt(address), 16).ToUpper();
                            break;
                    }
                }
                catch
                {
                    item.SubItems[2].Text = "N/A";
                }
            }
        }

        private void uxSendToClient_Click(object sender, EventArgs e)
        {
            client.SendToClient(uxSend.Text.ToBytesAsHex());
        }

        private void uxSendToServer_Click(object sender, EventArgs e)
        {
            client.Send(uxSend.Text.ToBytesAsHex());
        }

        private void uxAddAddress_Click(object sender, EventArgs e)
        {
            string[] s = uxMemory.ShowNew();
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
                string[] s = uxMemory.ShowEdit(new string[]{
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

        private void uxPacketDisplay_Resize(object sender, EventArgs e)
        {
            DisplayPacket();
        }
    }

    public struct CapturedPacket
    {
        public byte[] Data;
        public string Time;
        public string Source;
        public string Destination;
        public int Length;
        public byte Type;

        public CapturedPacket(byte[] data, string source, int length, string destination)
        {
            Data = data;
            Time = DateTime.Now.ToString("hh:mm:ss.ffff");
            Source = source;
            Destination = destination;
            Length = length;
            Type = data[2];
        }

        public override string ToString()
        {
            return Time + "\t from " + Source;
        }
    }

    public enum DataTypes
    {
        Byte,
        Integer,
        Double,
        String,
        Pointer
    }
}