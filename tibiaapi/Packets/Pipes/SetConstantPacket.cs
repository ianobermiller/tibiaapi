using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class SetConstantPacket : PipePacket
    {
        string constantname;
        int value;

        public string ConstantName
        {
            get { return constantname; }
        }

        public int Value
        {
            get { return value; }
        }

        public SetConstantPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.SetConstant;
            destination = PacketDestination.Pipe;
        }

        public SetConstantPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.SetConstant || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                constantname = p.GetString();
                value = p.GetLong();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SetConstantPacket Create(Client c, string ConstantName, int Value)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.SetConstant);
            p.AddString(ConstantName);
            p.AddLong(Value);
            return new SetConstantPacket(c, p.GetPacket());
        }
    }
}