using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class CreatureMovePacket : Packet
    {
        private Objects.Location from;
        private byte fromStackPos;
        private Objects.Location to;

        public Objects.Location From
        {
            get { return from; }
        }

        public byte FromStackPos
        {
            get { return fromStackPos; }
        }

        public Objects.Location To
        {
            get { return to; }
        }

        public CreatureMovePacket(Client c) : base(c)
        {
            type = PacketType.CreatureMove;
            destination = PacketDestination.Client;
        }
        public CreatureMovePacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureMove) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                from = p.GetLocation();
                fromStackPos = p.GetByte();
                to = p.GetLocation();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CreatureMovePacket Create(Client c, Objects.Location from, byte fromStackPos, Objects.Location to)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.CreatureMove);
            p.AddLocation(from);
            p.AddByte(fromStackPos);
            p.AddLocation(to);
            return new CreatureMovePacket(c, p.GetPacket());
        }
    }
}
