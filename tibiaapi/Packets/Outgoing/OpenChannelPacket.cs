using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class OpenChannelPacket : OutgoingPacket
    {

        public ChatChannel ChannelId { get; set; }

        public OpenChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ChannelOpen;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ChannelOpen)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ChannelOpen;

            ChannelId = (ChatChannel)msg.GetUInt16();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt16((ushort)ChannelId);

            return msg.Packet;
        }
    }
}