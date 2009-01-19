using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveAllTextPacket : PipePacket
    {
        public RemoveAllTextPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.RemoveAllText;
            destination = PacketDestination.Pipe;
        }

        public RemoveAllTextPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.RemoveAllText || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                index = p.Index;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static RemoveAllTextPacket Create(Client c)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.RemoveAllText);
            return new RemoveAllTextPacket(c, p.GetPacket());
        }
    }
}