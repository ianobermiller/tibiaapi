using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureLightPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public byte LightColor { get; set; }
        public byte LightLevel { get; set; }

        public CreatureLightPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CREATURE_LIGHT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CREATURE_LIGHT)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CREATURE_LIGHT;

            CreatureId = msg.GetUInt32();
            LightLevel = msg.GetByte();
            LightColor = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);
            msg.AddByte(LightLevel);
            msg.AddByte(LightColor);

            return msg.Packet;
        }
    }
}