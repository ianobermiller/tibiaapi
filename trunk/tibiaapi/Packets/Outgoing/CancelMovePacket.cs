using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class CancelMovePacket : OutgoingPacket
    {
        public CancelMovePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType_t.CANCEL_MOVE;
            Destination = PacketDestination_t.SERVER;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType_t.CANCEL_MOVE)
                return false;

            Destination = destination;
            Type = OutgoingPacketType_t.CANCEL_MOVE;

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);
            msg.AddByte((byte)Type);
            return msg.Packet;
        }

        public static bool Send(Objects.Client client)
        {
            AttackPacket p = new AttackPacket(client);
            return p.Send();
        }
    }
}