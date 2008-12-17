using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureHealthPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public byte CREATURE_HEALTH { get; set; }

        public CreatureHealthPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CREATURE_HEALTH;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CREATURE_HEALTH)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CREATURE_HEALTH;

            CreatureId = msg.GetUInt32();
            CREATURE_HEALTH = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);
            msg.AddByte(CREATURE_HEALTH);

            return msg.Packet;
        }
    }
}