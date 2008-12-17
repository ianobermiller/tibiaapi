using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureOutfitPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public Objects.Outfit Outfit { get; set; }

        public CreatureOutfitPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CREATURE_OUTFIT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CREATURE_OUTFIT)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CREATURE_OUTFIT;

            CreatureId = msg.GetUInt32();
            Outfit = msg.GetOutfit();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(CreatureId);
            msg.AddOutfit(Outfit);

            return msg.Packet;
        }
    }
}