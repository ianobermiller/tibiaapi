using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class CreatureMovePacket : Packet
    {
        private Objects.Location from;
        private Objects.Location to;
        private byte id;
        public Objects.Location From
        {
            get { return from; }
        }
        public Objects.Location To
        {
            get { return to; }
        }
        public CreatureMovePacket()
        {
            type = PacketType.CreatureMove;
            destination = PacketDestination.Client;
        }
        public CreatureMovePacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.CreatureMove) return false;
                int index = 3;
                from.X = BitConverter.ToInt16(packet, index);
                index += 2;
                from.Y = BitConverter.ToInt16(packet, index);
                index += 2;
                from.Z = packet[index];
                index += 2;
                to.X = BitConverter.ToInt16(packet, index);
                index += 2;
                to.Y = BitConverter.ToInt16(packet, index);
                index += 2;
                to.Z = packet[index];
                index += 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CreatureMovePacket Create(Objects.Location from, Objects.Location to)
        {
            byte[] packet = new byte[14];
            packet[0] = 0x0C;
            packet[2] = (byte)PacketType.CreatureHealth;
            Array.Copy(BitConverter.GetBytes((short)(from.X)), 0, packet, 3, 2);
            Array.Copy(BitConverter.GetBytes((short)(from.Y)), 0, packet, 5, 2);
            packet[7] = (byte)from.Z;
            Array.Copy(BitConverter.GetBytes((short)(to.X)), 0, packet, 9, 2);
            Array.Copy(BitConverter.GetBytes((short)(to.Y)), 0, packet, 11, 2);
            packet[13] = (byte)to.Z;
            CreatureMovePacket cmp = new CreatureMovePacket(packet);
            return cmp;
        }
    }
}
