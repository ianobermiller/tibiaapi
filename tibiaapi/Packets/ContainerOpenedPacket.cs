using System;
using System.Collections.Generic;
using Tibia.Objects;
using System.Text;

namespace Tibia.Packets
{
    public class ContainerOpenedPacket : Packet
    {
        private Container container;

        public Container Container
        {
            get { return container; }
        }

        public ContainerOpenedPacket()
        {
            type = PacketType.ContainerOpened;
            destination = PacketDestination.Client;
        }
        public ContainerOpenedPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ContainerOpened) return false;
                int index = 3;

                return true;
            }
            else 
            {
                return false;
            }
        }

        public static ContainerOpenedPacket Create(Container container, string name)
        {
            return Create(container.Id, container.Number, name, container.Volume, container.GetItems());
        }

        public static ContainerOpenedPacket Create(int icon, byte number, string name, int volume, List<Item> items)
        {
            short countable = 0;
            foreach (Item item in items)
            {
                if (item.Count > 0)
                    countable++;
            }
            short length = (short)(6 + name.Length + 3 + items.Count * 2 + countable);
            byte[] packet = new byte[length + 2];

            Array.Copy(BitConverter.GetBytes(length), packet, 2);
            packet[2] = 0x6E;
            packet[3] = number;
            Array.Copy(BitConverter.GetBytes((short)icon), 0, packet, 4, 2);
            Array.Copy(BitConverter.GetBytes((short)name.Length), 0, packet, 6, 2);
            Array.Copy(Encoding.ASCII.GetBytes(name), 0, packet, 8, name.Length);
            int index = 8 + name.Length;
            packet[index++] = Packet.Lo(volume);
            packet[index++] = Packet.Hi(volume);
            packet[index++] = (byte)items.Count;
            foreach (Item item in items)
            {
                packet[index++] = Packet.Lo(item.Id);
                packet[index++] = Packet.Hi(item.Id);
                if (item.Count > 0)
                    packet[index++] = item.Count;
            }

            ContainerOpenedPacket p = new ContainerOpenedPacket(packet);
            return p;
        }
    }
}
