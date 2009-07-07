using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class RuleViolationCancelPacket : IncomingPacket
    {
        public string Name { get; set; }

        public RuleViolationCancelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.RuleViolationCancel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.RuleViolationCancel)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.RuleViolationCancel;

            try
            {
                Name = msg.GetString();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Name);
        }
    }
}