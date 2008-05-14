using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class DefaultTemplatePacket : Packet
    {
        public DefaultTemplatePacket()
        {
            type = PacketType.DefaultTemplate;
            destination = PacketDestination.Client;
        }
        public DefaultTemplatePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.DefaultTemplate) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);

                index = p.Index;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static DefaultTemplatePacket Create()
        {
            PacketBuilder p = new PacketBuilder(PacketType.DefaultTemplate);

            DefaultTemplatePacket pkt = new DefaultTemplatePacket(p.GetPacket());
            return pkt;
        }
    }
}
