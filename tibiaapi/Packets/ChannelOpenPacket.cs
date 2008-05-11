using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class ChannelOpenPacket : Packet
    {
        private int channelId;
        private int lenChannelName;
        private string channelName;

        public int ChannelId
        {
            get { return channelId; }
        }
        public string ChannelName
        {
            get { return channelName; }
        }

        public ChannelOpenPacket()
        {
            type = PacketType.ChannelOpen;
            destination = PacketDestination.Client;
        }

        public ChannelOpenPacket(byte[] data) : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ChannelOpen) return false;
                int index = 3;
                channelId = BitConverter.ToInt16(packet, index);
                index += 2;
                lenChannelName = BitConverter.ToInt16(packet, index);
                index += 2;
                channelName = Encoding.ASCII.GetString(packet, index, lenChannelName);
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static ChannelOpenPacket Create(Tibia.Objects.Channel channel)
        {
            return Create(channel.Id, channel.Name);
        }

        public static ChannelOpenPacket Create(ChatChannel channelId, string channelName)
        {
            byte[] packet = new byte[7 + channelName.Length];
            Array.Copy(BitConverter.GetBytes((short)5 + channelName.Length), 0, packet, 0, 2);
            packet[2] = (byte)PacketType.ChannelOpen;
            Array.Copy(BitConverter.GetBytes((short)channelId), 0, packet, 3, 2);
            Array.Copy(BitConverter.GetBytes((short)channelName.Length), 0, packet, 5, 2);
            Array.Copy(Encoding.ASCII.GetBytes(channelName), 0, packet, 7, channelName.Length);
            ChannelOpenPacket ocp = new ChannelOpenPacket(packet);
            return ocp;
        }
    }
}
