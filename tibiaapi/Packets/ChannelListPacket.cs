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
                PacketBuilder p = new PacketBuilder(packet, 3);
                numChannels = p.GetByte();
                channels = new List<Channel>(numChannels);
                ushort id, len;
                for (int i = 0; i < numChannels; i++)
                {
                    id = p.GetInt();
                    len = p.GetInt();
                    channels.Add(new Channel(
                        (ChatChannel)id,
                        p.GetString(len)
                    ));
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
            PacketBuilder p = new PacketBuilder(PacketType.ChannelList);
            p.AddByte((byte)channels.Count);

            foreach (Channel c in channels)
            {
                p.AddInt((int)c.Id);
                p.AddInt(c.Name.Length);
                p.AddString(c.Name);
            }
            ChannelListPacket clp = new ChannelListPacket(p.GetPacket());
            return clp;
        }
    }
}
