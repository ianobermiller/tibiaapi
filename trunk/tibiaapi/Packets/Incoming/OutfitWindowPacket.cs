using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
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

            try
            {
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
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
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