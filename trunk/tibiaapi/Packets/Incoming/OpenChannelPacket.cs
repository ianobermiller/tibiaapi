using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class OpenChannelPacket : IncomingPacket
    {
        public ChatChannel ChannelId { get; set; }
        public string ChannelName { get; set; }

        public OpenChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.OPEN_CHANNEL;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.OPEN_CHANNEL)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.OPEN_CHANNEL;
            ChannelId = (ChatChannel)msg.GetUInt16();
            ChannelName = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddUInt16((ushort)ChannelId);
            msg.AddString(ChannelName);

            return msg.Packet;
        }
    }
}