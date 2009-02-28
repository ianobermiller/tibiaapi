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
        Dictionary<byte, string> incomingPacketTypeNames = new Dictionary<byte, string>();
        Dictionary<byte, string> outgoingPacketTypeNames = new Dictionary<byte, string>();
        byte[] displayedPacket = null;
        uxMemory memoryForm;

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
                client.Proxy.ReceivedTextMessageIncomingPacket += new Proxy.IncomingPacketListener(Proxy_ReceivedTextMessageIncomingPacket);
                client.Proxy.IncomingSplitPacket += new Proxy.SplitPacket(Proxy_IncomingSplitPacket);
                client.Proxy.OutgoingSplitPacket += new Proxy.SplitPacket(Proxy_OutgoingSplitPacket);
            }

            foreach (byte t in Enum.GetValues(typeof(Tibia.Packets.IncomingPacketType)))
            {
                incomingPacketTypeNames.Add(t, Enum.GetName(typeof(Tibia.Packets.IncomingPacketType), t));
            }

            foreach (byte t in Enum.GetValues(typeof(Tibia.Packets.OutgoingPacketType)))
            {
                outgoingPacketTypeNames.Add(t, Enum.GetName(typeof(Tibia.Packets.OutgoingPacketType), t));
            }
        }

        bool Proxy_ReceivedTextMessageIncomingPacket(IncomingPacket packet)
        {
            Tibia.Packets.Incoming.TextMessagePacket p = (Tibia.Packets.Incoming.TextMessagePacket)packet;
            
            if (p.Color == StatusMessage.DescriptionGreen && p.Message.StartsWith("You see "))
                p.Message = p.Message + " [" + client.ReadInt32(Tibia.Addresses.Client.See_Id) + "]";

            return true;
        }

        void Proxy_OutgoingSplitPacket(byte type, byte[] packet)
        {
            if (uxLogClient.Checked)
            {
                if (uxLogHeader.Checked)
                {
                    if (uxHeaderByte.Text.Length == 2 &&
                        type == uxHeaderByte.Text.ToBytesAsHex()[0])
                    {
                        LogPacket(packet, "CLIENT", "SERVER");
                    }
                }
                else
                {
                    LogPacket(packet, "CLIENT", "SERVER");
                }
            }
        }

        void Proxy_IncomingSplitPacket(byte type, byte[] packet)
        {
            if (uxLogServer.Checked)
            {
                if (uxLogHeader.Checked)
                {
                    if (uxHeaderByte.Text.Length == 2 &&
                        type == uxHeaderByte.Text.ToBytesAsHex()[0])
                    {
                        LogPacket(packet, "SERVER", "CLIENT");
                    }
                }
                else
                {
                    LogPacket(packet, "SERVER", "CLIENT");
                }
            }
        }

        private void LogPacket(byte[] packet, string from, string to)
        {
            if (LogPackets)
            {
                uxPacketList.Invoke(new EventHandler(delegate
                {
                    CapturedPacket cp = new CapturedPacket(packet, from, packet.Length, to);
                    packetList.Add(cp);
                    string name = "";

                    if (cp.Source == "SERVER" && incomingPacketTypeNames.ContainsKey(cp.Type))
                        name = incomingPacketTypeNames[cp.Type];
                    if (cp.Source == "CLIENT" && outgoingPacketTypeNames.ContainsKey(cp.Type))
                        name = outgoingPacketTypeNames[cp.Type];

                    uxPacketList.Items.Add(new ListViewItem(new string[]{
                        cp.Time,
                        cp.Source,
                        cp.Destination,
                        cp.Length.ToString(),
                        Convert.ToString(cp.Type, 16).PadLeft(2, '0').ToUpper(),
                        name
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

        private void uxPacketDisplay_Resize(object sender, EventArgs e)
        {
            DisplayPacket();
        }

        private void uxPacketList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (uxPacketList.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in uxPacketList.SelectedItems)
                    {
                        uxPacketList.Items.Remove(item);
                    }
                }
            }
        }

        private void uxShowMemoryWatcher_Click(object sender, EventArgs e)
        {
            if (memoryForm == null || memoryForm.Disposing || memoryForm.IsDisposed)
            {
                memoryForm = new uxMemory(client);
            }
            memoryForm.Show();
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
            Type = data[0];
        }

        public override string ToString()
        {
            return Time + "\t from " + Source;
        }
    }
}