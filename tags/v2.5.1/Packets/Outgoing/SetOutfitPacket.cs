using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class SetOutfitPacket : OutgoingPacket
    {
        public Objects.Outfit Outfit { get; set; }

        public SetOutfitPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.SetOutfit;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.SetOutfit)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.SetOutfit;

            Outfit = msg.GetOutfit();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);
            msg.AddOutfit(Outfit);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, Objects.Outfit outfit)
        {
            SetOutfitPacket p = new SetOutfitPacket(client);
            p.Outfit = outfit;
            return p.Send();
        }
    }
}