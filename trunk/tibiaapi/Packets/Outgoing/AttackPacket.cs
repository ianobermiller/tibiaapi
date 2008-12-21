using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class AttackPacket : OutgoingPacket
    {
        public uint CreatureId { get; set; }

        public AttackPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Attack;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Attack)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Attack;

            CreatureId = msg.GetUInt32();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, uint creatureId)
        {
            AttackPacket p = new AttackPacket(client);
            p.CreatureId = creatureId;
            return p.Send();
        }
    }
}