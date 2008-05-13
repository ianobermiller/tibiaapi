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
                int index = 3;

                return true;
            }
            else 
            {
                return false;
            }
        }

        public static DefaultTemplatePacket Create()
        {
            byte[] packet = new byte[1];

            DefaultTemplatePacket p = new DefaultTemplatePacket(packet);
            return p;
        }
    }
}
