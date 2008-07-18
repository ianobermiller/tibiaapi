using System;
using Tibia.Objects;

namespace Tibia.Packets.Pipes
{
    public class RemoveTextPacket : PipePacket
    {
        string textname;

        public string TextName
        {
            get { return textname; }
        }

        public RemoveTextPacket(Client c)
            : base(c)
        {
            type = PacketType.PipePacket;
            pipetype = PipePacketType.RemoveText;
            destination = PacketDestination.Pipe;
        }

        public RemoveTextPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (pipetype != PipePacketType.RemoveText || type != PacketType.PipePacket) { return false; }
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                textname = p.GetString();

                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static RemoveTextPacket Create(Client c, string TextName)
        {
            PacketBuilder p = new PacketBuilder(c, (PacketType)PipePacketType.RemoveText);
            p.AddString(TextName);

            return new RemoveTextPacket(c, p.GetPacket());
        }

    }
}
