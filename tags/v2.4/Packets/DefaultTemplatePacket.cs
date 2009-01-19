using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class DefaultTemplatePacket : Packet
    {
        public DefaultTemplatePacket(Client c)
            : base(c)
        {
            type = PacketType.DefaultTemplate;
            destination = PacketDestination.Client;
        }
        public DefaultTemplatePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.DefaultTemplate) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);

                index = p.Index;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static DefaultTemplatePacket Create(Client c)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.DefaultTemplate);

            return new DefaultTemplatePacket(c, p.GetPacket());
        }
    }
}