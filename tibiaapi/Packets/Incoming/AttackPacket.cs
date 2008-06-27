using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    /// <summary>
    /// Packet sent to the server to indicate which creature is to be attacked.
    /// </summary>
    public class AttackPacket : Packet
    {
        int id;

        public int Id
        {
            get { return id; }
        }

        public AttackPacket(Client c) : base(c)
        {
            type = PacketType.Attack;
            destination = PacketDestination.Server;
        }

        public AttackPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.Attack) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                id = p.GetLong();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static AttackPacket Create(Client c, int id)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.Attack);
            p.AddLong(id);
            return new AttackPacket(c, p.GetPacket());
        }
    }
}
