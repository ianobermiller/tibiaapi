using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureSpeedPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public ushort CREATURE_SPEED { get; set; }

        public CreatureSpeedPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureSpeed;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CreatureSpeed)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureSpeed;

            CreatureId = msg.GetUInt32();
            CREATURE_SPEED = msg.GetUInt16();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);
            msg.AddUInt16(CREATURE_SPEED);

            return msg.Packet;
        }
    }
}