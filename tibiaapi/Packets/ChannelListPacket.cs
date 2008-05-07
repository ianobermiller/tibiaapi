using System;
using System.Text;
using System.Collections.Generic;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ChannelListPacket : Packet
    {
        private int numChannels;
        private List<Channel> channels;

        public int NumChannels
        {
            get { return numChannels; }
        }

        public List<Channel> Channels
        {
            get { return channels; }
        }

        public ChannelListPacket()
        {
            type = PacketType.ChannelList;
            destination = PacketDestination.Client;
        }
        public ChannelListPacket(byte[] data) : this()
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ChannelList) return false;
                numChannels = packet[3];
                channels = new List<Channel>(numChannels);
                int index = 4;
                ushort id, len;
                for (int i = 0; i < numChannels; i++)
                {
                    id = BitConverter.ToUInt16(packet, index);
                    index += 2;
                    len = BitConverter.ToUInt16(packet, index);
                    index += 2;
                    channels.Add(new Channel(
                        (ChatChannel)id,
                        Encoding.ASCII.GetString(packet, index, len)
                    ));
                    index += len;
                }
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static ChannelListPacket Create(List<Channel> channels)
        {
            short len = 0;
            foreach (Channel c in channels)
                len += (short)(4 + c.Name.Length);
            byte[] packet = new byte[4 + len];

            Array.Copy(BitConverter.GetBytes((short)(2 + len)), 0, packet, 0, 2);
            packet[2] = (byte)PacketType.ChannelList;
            packet[3] = (byte)channels.Count;
            int index = 4;
            foreach (Channel c in channels)
            {
                Array.Copy(BitConverter.GetBytes((ushort)c.Id), 0, packet, index, 2);
                index += 2;
                Array.Copy(BitConverter.GetBytes((short)c.Name.Length), 0, packet, index, 2);
                index += 2;
                Array.Copy(Encoding.ASCII.GetBytes(c.Name), 0, packet, index, c.Name.Length);
                index += c.Name.Length;
            }
            ChannelListPacket clp = new ChannelListPacket(packet);
            return clp;
        }
    }
}
