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

namespace SmartPacketAnalyzer
{
    public partial class uxForm : Form
    {
        bool LogPackets = true;

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
                timer1.Enabled = true;
            }
        }
        private Tibia.Packets.Packet PacketFromClient(Tibia.Packets.Packet packet)
        {
            if (lbLog.CheckedItems.Contains("Outgoing"))
            {
                if (cbOHead.Checked)
                {
                    if (packet.Data[2] == Tibia.Packets.Packet.HexStringToByteArray(tbHead.Text)[0])
                    {
                        LogPacket(packet.Data, "CLIENT");
                    }
                }
                else
                {
                    LogPacket(packet.Data, "CLIENT");
                }
            }
            return packet;
        }

        private Tibia.Packets.Packet PacketFromServer(Tibia.Packets.Packet packet)
        {
            if (lbLog.CheckedItems.Contains("Incoming"))
            {
                if (cbOHead.Checked)
                {
                    if (packet.Data[2] == Tibia.Packets.Packet.HexStringToByteArray(tbHead.Text)[0])
                    {
                        LogPacket(packet.Data, "SERVER");
                    }
                }
                else
                {
                    LogPacket(packet.Data, "SERVER");
                }
            }
            return packet;
        }

        private void LogPacket(byte[] packet, string from)
        {
            if (LogPackets)
            {
                uxPackets.Invoke(new EventHandler(delegate
                {
                    CapturedPacket cp = new CapturedPacket(packet, from);
                    uxPackets.Items.Add(cp);
                }));
            }
        }

        private void uxPackets_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapturedPacket cp = (CapturedPacket) uxPackets.SelectedItem;
            string hex = Tibia.Packets.Packet.ByteArrayToHexString(cp.Data);
            uxPacketDisplay.Text = hex;
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            uxPackets.Items.Clear() ;
        }

        private void MenToInt_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Tibia.Packets.Packet.HexStringToInt(uxPacketDisplay.SelectedText).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Tibia.Packets.Packet.HexStringToByteArray(tbHead.Text)[0].ToString());
            MessageBox.Show(client.ReadShort(Tibia.Addresses.Client.See_Id).ToString());
        }

        private void convertToStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Tibia.Packets.Packet.HexStringToASCII(uxPacketDisplay.SelectedText));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tbSee.Text = client.ReadShort(Tibia.Addresses.Client.See_Id).ToString();
        }

        private void btMem_Click(object sender, EventArgs e)
        {
            int temp = Int32.Parse(tbMem.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            if (rbMem1.Checked)
            {
                MessageBox.Show(client.ReadString(temp));
            }
            else if (rbMem2.Checked)
            {
                MessageBox.Show(client.ReadInt(temp).ToString());
            }
        }

        private void btCln_Click(object sender, EventArgs e)
        {
            client.SendToClient(Tibia.Packets.Packet.HexStringToByteArray(tbPkt.Text));
        }

        private void btSrv_Click(object sender, EventArgs e)
        {
            client.Send(Tibia.Packets.Packet.HexStringToByteArray(tbPkt.Text));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            VipList vip = new VipList(client);
            Vip teMP = vip.GetPlayer("Bubble");
            Tibia.Packets.VipLoginPacket tmp = Tibia.Packets.VipLoginPacket.Create(teMP);
            client.SendToClient(tmp);           

            MessageBox.Show("NU");
            tmp.Data[2] = 0xD4;
            client.SendToClient(tmp);           
        }
    }

    public struct CapturedPacket
    {
        public byte[] Data;
        public string From;
        public string Time;

        public CapturedPacket(byte[] data, string from)
        {
            Data = data;
            From = from;
            Time = DateTime.Now.ToString();
        }

        public override string ToString()
        {
            return Time + "\t from " + From;
        }
    }
}