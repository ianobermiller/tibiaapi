using System;
using System.Collections.Generic;
using System.Text;

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

        public AttackPacket()
        {
            type = PacketType.Attack;
            destination = PacketDestination.Server;
        }

        public AttackPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.Attack) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                id = p.GetLong();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static AttackPacket Create(int id)
        {
            PacketBuilder p = new PacketBuilder(PacketType.Attack);
            p.AddLong(id);
            return new AttackPacket(p.GetPacket());
        }
    }
}
