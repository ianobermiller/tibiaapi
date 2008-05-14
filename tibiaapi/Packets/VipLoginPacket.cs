using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class VipLoginPacket: Packet
    {
        private int playerId;
        public int PlayerId
        {
            get { return playerId; }
        }
        
        public VipLoginPacket()
        {
            type = PacketType.VipLogin;
            destination = PacketDestination.Client;
        }

        public VipLoginPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.VipLogin) return false;
                PacketBuilder p = new PacketBuilder(packet, 3);
                playerId = p.GetLong();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static VipLoginPacket Create(Objects.Vip player)
        {
            return Create(player.Id);
        }

        public static VipLoginPacket Create(int id)
        {
            PacketBuilder p = new PacketBuilder(PacketType.VipLogin);
            p.AddLong(id);
            VipLoginPacket vlp = new VipLoginPacket(p.GetPacket());
            return vlp;
        }
    }
}
