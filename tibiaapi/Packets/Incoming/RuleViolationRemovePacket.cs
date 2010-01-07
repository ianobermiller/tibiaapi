using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class RuleViolationRemovePacket : IncomingPacket
    {
        public string Name { get; set; }

        public RuleViolationRemovePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.RuleViolationRemove;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.RuleViolationRemove)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.RuleViolationRemove;

            Name = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Name);
        }
    }
}