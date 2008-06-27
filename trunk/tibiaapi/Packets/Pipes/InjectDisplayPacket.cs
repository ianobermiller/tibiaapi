using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class InjectDisplayPacket : PipePacket
    {
        bool injected;

        public bool Injected
        {
            get { return injected; }
        }

        public InjectDisplayPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.InjectDisplayText;
            destination = PacketDestination.Pipe;
        }

        public InjectDisplayPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.InjectDisplayText || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                injected = Convert.ToBoolean(p.GetByte());

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static InjectDisplayPacket Create(Client c, bool Injected)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.InjectDisplayText);
            p.AddByte(Convert.ToByte(Injected));

            return new InjectDisplayPacket(c, p.GetPacket());
        }
    }
}
