using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class OutfitWindowPacket : IncomingPacket
    {

        public List<AvalibleOutfit_t> OutfitList { get; set; }
        public Objects.Outfit Default { get; set; }

        public OutfitWindowPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.OUTFIT_WINDOW;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.OUTFIT_WINDOW)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.OUTFIT_WINDOW;

            Default = msg.GetOutfit();

            byte count = msg.GetByte();
            OutfitList = new List<AvalibleOutfit_t> { };

            for (int i = 0; i < count; i++)
            {
                AvalibleOutfit_t outfit = new AvalibleOutfit_t();

                outfit.Id = msg.GetUInt16();
                outfit.Name = msg.GetString();
                outfit.Addons = msg.GetByte();

                OutfitList.Add(outfit);
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddOutfit(Default);

            msg.AddByte((byte)OutfitList.Count);

            foreach (AvalibleOutfit_t i in OutfitList)
            {
                msg.AddUInt16(i.Id);
                msg.AddString(i.Name);
                msg.AddByte(i.Addons);
            }

            return msg.Packet;
        }
    }
}