using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class AttackedPacket : Packet
    {
        private int creatureId;
        public int CreatureId
        {
            get { return creatureId; }
        }
        public AttackedPacket()
        {
            type = PacketType.Attacked;
            destination = PacketDestination.Client;
        }
        public AttackedPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.Attacked) return false;
                int index = 3;
                creatureId = BitConverter.ToInt32(packet, index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static AttackedPacket Create(Objects.Creature creature)
        {
            byte[] packet = new byte[8];
            packet[0] = 0x06;
            packet[2] = (byte)PacketType.Attacked;
            Array.Copy(BitConverter.GetBytes(creature.Id), 0, packet, 3, 4);
            AttackedPacket atp = new AttackedPacket(packet);
            return atp;
        }
    }
}
