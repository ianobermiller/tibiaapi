using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class VipLogoutPacket : Packet
    {
        private int playerId;
        public int PlayerId
        {
            get { return playerId; }
        }

        public VipLogoutPacket(Client c)
            : base(c)
        {
            type = PacketType.VipLogout;
            destination = PacketDestination.Client;
        }

        public VipLogoutPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.VipLogout) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                playerId = p.GetLong();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static VipLogoutPacket Create(Client c, Objects.Vip player)
        {
            return Create(c, player.Id);
        }

        public static VipLogoutPacket Create(Client c, int id)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.VipLogout);
            p.AddLong(id);
            return new VipLogoutPacket(c, p.GetPacket());
        }
    }
}
