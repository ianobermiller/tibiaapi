using System;
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

        public ChannelListPacket(Client c) : base(c)
        {
            type = PacketType.ChannelList;
            destination = PacketDestination.Client;
        }
        public ChannelListPacket(Client c, byte[] data) : this(c)
        {
            ParseData(data);
        }
        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ChannelList) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                numChannels = p.GetByte();
                channels = new List<Channel>(numChannels);
                ushort id;
                for (int i = 0; i < numChannels; i++)
                {
                    id = p.GetInt();
                    channels.Add(new Channel(
                        (ChatChannel)id,
                        p.GetString()
                    ));
                }
                index = p.Index;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static ChannelListPacket Create(Client c, List<Channel> channels)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ChannelList);
            p.AddByte((byte)channels.Count);

            foreach (Channel chan in channels)
            {
                p.AddInt((int)chan.Id);
                p.AddString(chan.Name);
            }
            return new ChannelListPacket(c, p.GetPacket());
        }
    }
}
