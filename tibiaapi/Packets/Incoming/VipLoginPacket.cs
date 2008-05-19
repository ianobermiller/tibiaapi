using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class VipLoginPacket: Packet
    {
        private int playerId;
        public int PlayerId
        {
            get { return playerId; }
        }
        
        public VipLoginPacket(Client c) : base(c)
        {
            type = PacketType.VipLogin;
            destination = PacketDestination.Client;
        }

        public VipLoginPacket(Client c, byte[] data)
            : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.VipLogin) return false;
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

        public static VipLoginPacket Create(Client c, Objects.Vip player)
        {
            return Create(c, player.Id);
        }

        public static VipLoginPacket Create(Client c, int id)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.VipLogin);
            p.AddLong(id);
            return new VipLoginPacket(c, p.GetPacket());
        }
    }
}
