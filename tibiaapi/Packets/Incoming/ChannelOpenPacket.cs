using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChannelOpenPacket : IncomingPacket
    {
        public ChatChannel ChannelId { get; set; }
        public string ChannelName { get; set; }

        public ChannelOpenPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChannelOpen;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ChannelOpen)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ChannelOpen;

            ChannelId = (ChatChannel)msg.GetUInt16();
            ChannelName = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16((ushort)ChannelId);
            msg.AddString(ChannelName);
        }

        public static bool Send(Objects.Client client, ChatChannel channel, string name)
        {
            ChannelOpenPacket p = new ChannelOpenPacket(client);
            p.ChannelId = channel;
            p.ChannelName = name;
            return p.Send();
        }
    }
}