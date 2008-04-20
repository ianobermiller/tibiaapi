using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class VipLoginPacket: Packet
    {
        private int id;
        public int Id
        {
            get { return id; }
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
                BitConverter.ToInt32(packet, index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static VipLoginPacket Create(Objects.Creature creature)
        {
            byte[] packet = new byte[7];
            packet[0] = 0x05;
            packet[2] = (byte)PacketType.VipLogin;
            byte[] idBytes = BitConverter.GetBytes(creature.Id);
            Array.Copy(idBytes, 0, packet, 3, idBytes.Length);
            VipLoginPacket vlp = new VipLoginPacket(packet);
            return packet;
        }
    }
}
