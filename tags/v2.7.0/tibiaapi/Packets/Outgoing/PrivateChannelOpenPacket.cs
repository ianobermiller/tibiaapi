using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class PrivateChannelOpenPacket : OutgoingPacket
    {
        public string Receiver { get; set; }

        public PrivateChannelOpenPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.PrivateChannelOpen;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.PrivateChannelOpen)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.PrivateChannelOpen;
            Receiver = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Receiver);
        }

        public static bool Send(Objects.Client client, string receiver)
        {
            PrivateChannelOpenPacket p = new PrivateChannelOpenPacket(client);
            p.Receiver = receiver;
            return p.Send();   
        }
    }
}