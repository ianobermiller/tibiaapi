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
using Tibia.Packets;
using Tibia.Util;

namespace SmartPacketSniffer
{
    public partial class MainForm : Form
    {
        Client client;
        List<Client> lc;
        List<CapturedPacket> captPackets = new List<CapturedPacket>();
        Dictionary<byte, string> incomingPacketTypeNames = new Dictionary<byte, string>();
        Dictionary<byte, string> outgoingPacketTypeNames = new Dictionary<byte, string>();
        byte[] displayedPacket = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadClients();
            SetSocket();
            foreach (byte t in Enum.GetValues(typeof(Tibia.Packets.IncomingPacketType)))
            {
                incomingPacketTypeNames.Add(t, Enum.GetName(typeof(Tibia.Packets.IncomingPacketType), t));
            }

            foreach (byte t in Enum.GetValues(typeof(Tibia.Packets.OutgoingPacketType)))
            {
                outgoingPacketTypeNames.Add(t, Enum.GetName(typeof(Tibia.Packets.OutgoingPacketType), t));
            }
        }

        private void btnClient_Click(object sender, EventArgs e)
        {         
            SetSocket();
        }


        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadClients();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            if (btnLog.Text == "Stop Packet Logging")
            {
                btnLog.Text = "Start Packet Logging";
                client.IO.StopRawSocket();
            }
            else if (btnLog.Text == "Start Packet Logging")
            {
                btnLog.Text = "Stop Packet Logging";
                client.IO.StartRawSocket();
            }
        }

        private void ReloadClients()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            lc = Client.GetClients();
            if (lc.Count > 0)
            {
                foreach (Client c in lc)
                {
                    comboBox1.Items.Add(c.ToString());
                }
                comboBox1.SelectedIndex = 0;
            }
        }

        private void SetSocket()
        {
            if (comboBox1.Items.Count > 0)
            {
                client = lc[comboBox1.SelectedIndex];
                //if (client.IO.RawSocket == null)
                //{
                //    client.IO.StartRawSocket();
                //    client.IO.RawSocket.IncomingSplitPacket += new RawSocket.SplitPacket(RawSocket_IncomingSplitPacket);
                //    client.IO.RawSocket.OutgoingSplitPacket += new RawSocket.SplitPacket(RawSocket_OutgoingSplitPacket);
                //    EventHandler e = new EventHandler(RadioChanged);
                //    e.Invoke(new Object(), new EventArgs());
                //}
            }
        }



        private void RawSocket_OutgoingSplitPacket(byte type, byte[] packet)
        {
            if (chkClient.Checked)
            {
                if (chkType.Checked)
                {
                    if (txtType.Text.Length == 2 &&
                        type == txtType.Text.ToBytesAsHex()[0])
                    {
                        LogPacket(packet, "CLIENT", "SERVER");
                    }                    
                }
                else LogPacket(packet, "CLIENT", "SERVER");
            }
        }

        private void RawSocket_IncomingSplitPacket(byte type, byte[] packet)
        {
            if (chkServer.Checked)
            {
                if (chkType.Checked)
                {
                    if (txtType.Text.Length == 2 &&
                        type== txtType.Text.ToBytesAsHex()[0])
                    {
                        LogPacket(packet, "SERVER", "CLIENT");
                    }                    
                }
                else LogPacket(packet, "SERVER", "CLIENT");
            }
        }

        private void LogPacket(byte[] packet, string from, string to)
        {
            //if (client.IO.RawSocket.Enabled)
            //{
            //    PacketList.Invoke(new EventHandler(delegate
            //    {
            //        CapturedPacket cp = new CapturedPacket(packet, from, packet.Length, to);
            //        captPackets.Add(cp);
            //        string name = "";

            //        if (cp.Source == "SERVER" && incomingPacketTypeNames.ContainsKey(cp.Type))
            //            name = incomingPacketTypeNames[cp.Type];
            //        if (cp.Source == "CLIENT" && outgoingPacketTypeNames.ContainsKey(cp.Type))
            //            name = outgoingPacketTypeNames[cp.Type];

            //        PacketList.Items.Add(new ListViewItem(new string[]{
            //            cp.Time,
            //            cp.Source,
            //            cp.Destination,
            //            cp.Length.ToString(),
            //            Convert.ToString(cp.Type, 16).PadLeft(2, '0').ToUpper(),
            //            name
            //        }));
            //        PacketList.EnsureVisible(PacketList.Items.Count - 1);
            //    }));
            //}
        }



        private void PacketList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PacketList.SelectedIndices.Count > 0)
            {
                CapturedPacket cp = captPackets[PacketList.SelectedIndices[0]];
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
                int charWidth = TextRenderer.MeasureText("0", txtPacket.Font).Width / 2;
                int widthInChars = txtPacket.Width / charWidth - 2;
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

                txtPacket.Text = s;
            }
            else
            {
                txtPacket.Clear();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if(displayedPacket!=null)
            Clipboard.SetText(displayedPacket.ToHexString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            captPackets.Clear();
            PacketList.Items.Clear();
            txtPacket.Clear();
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

        private void RadioChanged(object sender, EventArgs e)
        {
            //if (radioDefault.Checked) client.IO.RawSocket.Mode = RawSocket.SocketMode.UsingDefaultRemotePort;
            //else if (radioSpecial.Checked) client.IO.RawSocket.Mode = RawSocket.SocketMode.UsingSpecialRemotePort;
            //else if (radioProxy.Checked)
            //{
            //    client.IO.RawSocket.Mode = RawSocket.SocketMode.UsingProxy;
            //    client.IO.RawSocket.ProxyPort = Convert.ToUInt16(numProxyPort.Value);
            //}
        }


    }
}
