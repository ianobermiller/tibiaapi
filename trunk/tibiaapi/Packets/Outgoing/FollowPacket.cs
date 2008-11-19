using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    /// <summary>
    /// Packet sent to the server to indicate which creature is to be followed.
    /// </summary>
    public class FollowPacket : Packet
    {
        int id;

        public int Id
        {
            get { return id; }
        }

        public FollowPacket(Client c)
            : base(c)
        {
            type = PacketType.FlagUpdate;
            destination = PacketDestination.Server;
        }

        public FollowPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.FlagUpdate) return false;
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

        public static FollowPacket Create(Client c, int id)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.FlagUpdate);
            p.AddLong(id);
            return new FollowPacket(c, p.GetPacket());
        }
    }
}