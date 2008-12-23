using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class AutoWalkCancelPacket : OutgoingPacket
    {
        public AutoWalkCancelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.AutoWalkCancel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.AutoWalkCancel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.AutoWalkCancel;

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
            LogoutPacket p = new LogoutPacket(client);
            return p.Send();
        }
    }
}