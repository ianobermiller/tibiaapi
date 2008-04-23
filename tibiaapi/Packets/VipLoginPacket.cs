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
                int index = 3;
                playerId = BitConverter.ToInt32(packet, index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static VipLoginPacket Create(Objects.Vip creature)
        {
            byte[] packet = new byte[7];
            packet[0] = 0x05;
            packet[2] = (byte)PacketType.VipLogin;
            Array.Copy(BitConverter.GetBytes(creature.Id), 0, packet, 3, 4);
            VipLoginPacket vlp = new VipLoginPacket(packet);
            return vlp;
        }
    }
}
