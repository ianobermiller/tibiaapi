using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class PingPacket : OutgoingPacket
    {
        public PingPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Ping;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Ping)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Ping;

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);
            return msg.Packet;
        }

        public static bool Send(Objects.Client client)
        {
            PingPacket p = new PingPacket(client);
            return p.Send();
        }
    }
}