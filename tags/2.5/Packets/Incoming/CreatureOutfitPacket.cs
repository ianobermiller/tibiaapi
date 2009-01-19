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
            Type = IncomingPacketType.CreatureOutfit;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CreatureOutfit)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureOutfit;

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