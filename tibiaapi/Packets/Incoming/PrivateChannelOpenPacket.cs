using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class PrivateChannelOpenPacket : Packet
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        public PrivateChannelOpenPacket(Client c)
            : base(c)
        {
            type = PacketType.PrivateChannelOpen;
            destination = PacketDestination.Client;
        }
        public PrivateChannelOpenPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.PrivateChannelOpen) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                name = p.GetString();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static PrivateChannelOpenPacket Create(Client c,string Name)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.PrivateChannelOpen);
            p.AddString(Name);
            return new PrivateChannelOpenPacket(c, p.GetPacket());
        }
    }
}