using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ChannelClosePacket : OutgoingPacket
    {

        public ChatChannel ChannelId { get; set; }

        public ChannelClosePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ChannelClose;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ChannelClose)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ChannelClose;

            ChannelId = (ChatChannel)msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16((ushort)ChannelId);
        }

        public static bool Send(Objects.Client client, ChatChannel channel)
        {
            ChannelClosePacket p = new ChannelClosePacket(client);
            p.ChannelId = channel;
            return p.Send();
        }

    }
}