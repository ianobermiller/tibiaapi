using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class RuleViolationsChannelPacket : IncomingPacket
    {
        public ushort ChannelId { get; set; }

        public RuleViolationsChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.RULE_VIOLATIONS_CHANNEL;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.RULE_VIOLATIONS_CHANNEL)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType_t.RULE_VIOLATIONS_CHANNEL;

            ChannelId = msg.GetUInt16();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddUInt16(ChannelId);

            return msg.Packet;
        }
    }
}