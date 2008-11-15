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
        RawSocket sock;
        Client client;
        List<Client> lc;
        List<CapturedPacket> captPackets=new List<CapturedPacket>();
        byte[] displayedPacket = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadClients();
            SetSocket();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            if(sock!=null)
                sock.Close();            
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
                if (sock != null) sock.Enabled = false;
            }
            else if (btnLog.Text == "Start Packet Logging")
            {
                btnLog.Text = "Stop Packet Logging";
                if (sock != null) sock.Enabled = true;
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
                if (client != null)
                {
                    sock = new RawSocket(client, true);
                    sock.ReceivedGamePacketFromClient += ClientPacket;
                    sock.ReceivedGamePacketFromServer += ServerPacket;
                    sock.SplitPacketFromServer+=SplitPacket;
                    EventHandler e = new EventHandler(RadioChanged);
                    e.Invoke(new Object(), new EventArgs());
                    sock.Enabled = true;
                }
            }
        }

        private void ClientPacket(Packet p)
        {
            if (chkClient.Checked)
            {
                if (chkType.Checked)
                {
                    txtType.Invoke(new EventHandler(delegate
                    {
                        if (txtType.Text.Length == 2)
                        {
                            if (p.Data[2] == txtType.Text.ToBytesAsHex()[0])
                            {
                                LogPacket(p.Client, p.Data, "CLIENT", "SERVER");
                            }
                        }
                    }));
                }
                else LogPacket(p.Client, p.Data, "CLIENT", "SERVER");
            }
        }
        private void ServerPacket(Packet p)
        {
            if (chkServer.Checked)
            {
                if (chkType.Checked)
                {
                    txtType.Invoke(new EventHandler(delegate
                    {
                        if (txtType.Text.Length == 2)
                        {
                            if (p.Data[2] == txtType.Text.ToBytesAsHex()[0])
                            {
                                LogPacket(p.Client, p.Data, "CLIENT", "SERVER");
                            }
                        }
                    }));
                }
                else LogPacket(p.Client, p.Data, "SERVER", "CLIENT");
            }
        }
        private void SplitPacket(Packet p)
        {
            if (chkServer.Checked && chkSplit.Checked)
            {
                if (chkType.Checked)
                {
                    txtType.Invoke(new EventHandler(delegate
                    {
                        if (txtType.Text.Length == 2)
                        {
                            if (p.Data[2] == txtType.Text.ToBytesAsHex()[0])
                            {
                                LogPacket(p.Client, p.Data, "CLIENT*", "SERVER");
                            }
                        }
                    }));
                }
                else LogPacket(p.Client, p.Data, "SERVER*", "CLIENT");
            }
        }
        private void LogPacket(Client client,byte[] packet, string from, string to)
        {
            if (sock.Enabled)
            {
                PacketList.Invoke(new EventHandler(delegate
                {
                    CapturedPacket cp = new CapturedPacket(client.ToString(),packet, from, packet.Length, to);
                    captPackets.Add(cp);
                    PacketList.Items.Add(new ListViewItem(new string[]{
                        cp.client,
                        cp.Time,
                        cp.Source,
                        cp.Destination,
                        cp.Length.ToString(),
                        Convert.ToString(cp.Type, 16).PadLeft(2, '0').ToUpper()
                    }));
                    PacketList.EnsureVisible(PacketList.Items.Count - 1);
                }));
            }
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
            public string client;
            public byte[] Data;
            public string Time;
            public string Source;
            public string Destination;
            public int Length;
            public byte Type;

            public CapturedPacket(string ClientToString,byte[] data, string source, int length, string destination)
            {
                client = ClientToString;
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

        private void RadioChanged(object sender, EventArgs e)
        {
            if (radioDefault.Checked) sock.Mode = RawSocket.SocketMode.UsingDefaultRemotePort;
            else if (radioSpecial.Checked) sock.Mode = RawSocket.SocketMode.UsingSpecialRemotePort;
            else if (radioProxy.Checked)
            {
                sock.Mode = RawSocket.SocketMode.UsingProxy;
                sock.ProxyPort = Convert.ToUInt16(numProxyPort.Value);
            }
        }


    }
}
