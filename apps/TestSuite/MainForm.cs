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
using Tibia.Packets.Incoming;
using Tibia.Packets.Outgoing;
using Tibia.Util;

namespace TestSuite
{
    public partial class MainForm : Form
    {
        private Client client;
        private Proxy proxy;
        private HookProxy hookProxy;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            client = Client.Open();
            client.IO.StartProxy();
            proxy = client.IO.Proxy;
            hookProxy = new HookProxy(client);

            proxy.ReceivedSelfAppearIncomingPacket += new ProxyBase.IncomingPacketListener(
                delegate(IncomingPacket p)
                {
                    Success(uxProxyStatus);
                    return true;
                });

            hookProxy.ReceivedSelfAppearIncomingPacket += new ProxyBase.IncomingPacketListener(
                delegate(IncomingPacket p)
                {
                    Success(uxHookProxyStatus);
                    return true;
                });
        }

        private void uxProxySendServerTest_Click(object sender, EventArgs e)
        {
            TurnPacket p = new TurnPacket(client, Tibia.Constants.Direction.Up);
            MarkButton((Button)sender, p.Send(Packet.SendMethod.Proxy));
        }

        private void uxProxySendClientTest_Click(object sender, EventArgs e)
        {
            AnimatedTextPacket p = new AnimatedTextPacket(client);
            p.Message = "Testing";
            p.Position = client.PlayerLocation;
            p.Color = TextColor.Platinum;
            MarkButton((Button)sender, p.Send(Packet.SendMethod.Proxy));
        }

        private void uxHookProxySendServerTest_Click(object sender, EventArgs e)
        {
            TurnPacket p = new TurnPacket(client, Tibia.Constants.Direction.Down);
            MarkButton((Button)sender, p.Send(Packet.SendMethod.HookProxy));
        }

        private void MarkButton(Button b, bool passed)
        {
            if (passed)
                b.ForeColor = Color.Green;
            else
                b.ForeColor = Color.Red;
        }

        private void Success(Label l)
        {
            l.BeginInvoke(new EventHandler(delegate
            {
                l.Text = "Passed";
                l.ForeColor = Color.Green;
            }));
        }
    }
}
