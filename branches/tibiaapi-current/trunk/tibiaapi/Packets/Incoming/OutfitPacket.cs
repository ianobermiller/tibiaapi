using System.Collections.Generic;
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

    public class MountDescription
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
    }

    public class OutfitPacket : IncomingPacket
    {
        public List<AvalibleOutfit> OutfitList { get; set; }
        public List<MountDescription> MountList { get; set; }
        public Objects.Outfit Default { get; set; }

        public OutfitPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Outfit;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Outfit)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Outfit;

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

            if (Client.VersionNumber >= 870)
            {
                count = msg.GetByte();
                MountList = new List<MountDescription> { };

                for (int i = 0; i < count; i++)
                {
                    MountDescription mount = new MountDescription();

                    mount.Id = msg.GetUInt16();
                    mount.Name = msg.GetString();

                    MountList.Add(mount);
                }
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
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

            if (Client.VersionNumber >= 870)
            {
                msg.AddByte((byte)MountList.Count);

                foreach (MountDescription i in MountList)
                {
                    msg.AddUInt16(i.Id);
                    msg.AddString(i.Name);
                }
            }
        }
    }
}