using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class AvalibleOutfit
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public byte Addons { get; set; }

        public AvalibleOutfit() { }
    }

    public class OutfitWindowPacket : IncomingPacket
    {
        public List<AvalibleOutfit> OutfitList { get; set; }
        public Objects.Outfit Default { get; set; }

        public OutfitWindowPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.OutfitWindow;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.OutfitWindow)
                return false;

            Destination = destination;
            Type = IncomingPacketType.OutfitWindow;

            Default = msg.GetOutfit();

            byte count = msg.GetByte();
            OutfitList = new List<AvalibleOutfit> { };

            for (int i = 0; i < count; i++)
            {
                AvalibleOutfit outfit = new AvalibleOutfit();

                outfit.Id = msg.GetUInt16();
                outfit.Name = msg.GetString();
                outfit.Addons = msg.GetByte();

                OutfitList.Add(outfit);
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddOutfit(Default);

            msg.AddByte((byte)OutfitList.Count);

            foreach (AvalibleOutfit i in OutfitList)
            {
                msg.AddUInt16(i.Id);
                msg.AddString(i.Name);
                msg.AddByte(i.Addons);
            }
        }
    }
}