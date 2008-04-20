using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class CreatureHealthPacket : Packet
    {
        private int id;
        private byte hp;
        public int Id
        {
            get { return id; }
        }
        public byte HP
        {
            get { return hp; }
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
                id = BitConverter.ToInt32(packet, index);
                index += 4;
                hp = packet[index];
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CreatureHealthPacket Create(Objects.Creature creature, byte hp)
        {
            byte[] packet = new byte[8];
            packet[0] = 0x06;
            packet[2] = (byte)PacketType.CreatureHealth;
            byte[] idBytes = BitConverter.GetBytes(creature.Id);
            Array.Copy(idBytes, 0, packet, 3, idBytes.Length);
            packet[7]=hp;
            CreatureHealthPacket chp = new CreatureHealthPacket(packet);
            return chp;
        }
    }
}
