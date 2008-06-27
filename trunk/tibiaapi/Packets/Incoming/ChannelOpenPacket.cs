using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ChannelOpenPacket : Packet
    {
        private int channelId;
        private string channelName;

        public int ChannelId
        {
            get { return channelId; }
        }
        public string ChannelName
        {
            get { return channelName; }
        }

        public ChannelOpenPacket(Client c) : base(c)
        {
            type = PacketType.ChannelOpen;
            destination = PacketDestination.Client;
        }

        public ChannelOpenPacket(Client c, byte[] data) : this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ChannelOpen) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);
                channelId = p.GetInt();
                channelName = p.GetString();
                index = p.Index;
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static ChannelOpenPacket Create(Client c, Tibia.Objects.Channel channel)
        {
            return Create(c, channel.Id, channel.Name);
        }

        public static ChannelOpenPacket Create(Client c, ChatChannel channelId, string channelName)
        {
            PacketBuilder p = new PacketBuilder(c, PacketType.ChannelOpen);
            p.AddInt((int)channelId);
            p.AddString(channelName);
            return new ChannelOpenPacket(c, p.GetPacket());
        }
    }
}
