using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ChannelOpenPacket : OutgoingPacket
    {

        public ChatChannel ChannelId { get; set; }

        public ChannelOpenPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ChannelOpen;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ChannelOpen)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ChannelOpen;

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
            ChannelOpenPacket p = new ChannelOpenPacket(client);
            p.ChannelId = channel;
            return p.Send();
        }
    }
}