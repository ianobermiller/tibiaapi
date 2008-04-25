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
                client.Proxy.ReceivedPacketFromClient += (Proxy.PacketListener)PacketFromClient;
                client.Proxy.ReceivedPacketFromServer += (Proxy.PacketListener)PacketFromServer;
                uxTimerShort.Enabled = true;
            }

            uxMemoryList.Items.Add(new ListViewItem(new string[]{
                "Current See ID",
                Convert.ToString(Tibia.Addresses.Client.See_Id, 16).ToUpper(),
                String.Empty,
                DataTypes.Integer.ToString()
            }));
        }

        private Tibia.Packets.Packet PacketFromClient(Tibia.Packets.Packet packet)
        {
            if (uxLogClient.Checked)
            {
                if (uxLogHeader.Checked)
                {
                    if (uxHeaderByte.Text.Length == 2 && 
                        (byte)packet.Type == Packet.HexStringToByteArray(uxHeaderByte.Text)[0])
                    {
                        LogPacket(packet.Data, "CLIENT", "SERVER");
                    }
                }
                else
                {
                    LogPacket(packet.Data, "CLIENT", "SERVER");
                }
            }
            return packet;
        }

        private Tibia.Packets.Packet PacketFromServer(Tibia.Packets.Packet packet)
        {
            if (uxLogServer.Checked)
            {
                if (uxLogHeader.Checked)
                {
                    if (uxHeaderByte.Text.Length == 2 &&
                        (byte)packet.Type == Packet.HexStringToByteArray(uxHeaderByte.Text)[0])
                    {
                        LogPacket(packet.Data, "SERVER", "CLIENT");
                    }
                }
                else
                {
                    LogPacket(packet.Data, "SERVER", "CLIENT");
                }
            }
            return packet;
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
                string hex = Tibia.Packets.Packet.ByteArrayToHexString(cp.Data);
                uxPacketDisplay.Text = hex;
            }
            else
            {
                uxPacketDisplay.Text = String.Empty;
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
                MessageBox.Show(Tibia.Packets.Packet.HexStringToInt(uxPacketDisplay.SelectedText).ToString());
        }

        private void ConvertToString_Click(object sender, EventArgs e)
        {
            if (uxPacketDisplay.SelectedText.Length >= 1)
                MessageBox.Show(Tibia.Packets.Packet.HexStringToASCII(uxPacketDisplay.SelectedText));
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
            client.SendToClient(Tibia.Packets.Packet.HexStringToByteArray(uxSend.Text));
        }

        private void uxSendToServer_Click(object sender, EventArgs e)
        {
            client.Send(Tibia.Packets.Packet.HexStringToByteArray(uxSend.Text));
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
            Time = DateTime.Now.ToString();
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