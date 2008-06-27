using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class CreatureHealthPacket : Packet
    {
        private int creatureId;
        private byte creatureHP;
        public int CreatureId
        {
            get { return creatureId; }
        }
        public byte CreatureHP
        {
            get { return creatureHP; }
        }
        public CreatureHealthPacket()
        {
            type = PacketType.CreatureHealth;
            destination = PacketDestination.Client;
        }
        public CreatureHealthPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureHealth) return false;
                int index = 3;
                creatureId = BitConverter.ToInt32(packet, index);
                index += 4;
                creatureHP = packet[index];
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CreatureHealthPacket Create(Objects.Creature creature, byte hp)
        {
            return Create(creature.Id, hp);
        }

        public static CreatureHealthPacket Create(int id, byte hp)
        {
            byte[] packet = new byte[8];
            packet[0] = 0x06;
            packet[2] = (byte)PacketType.CreatureHealth;
            Array.Copy(BitConverter.GetBytes(id), 0, packet, 3, 4);
            packet[7] = hp;
            CreatureHealthPacket chp = new CreatureHealthPacket(packet);
            return chp;
        }
    }
}
