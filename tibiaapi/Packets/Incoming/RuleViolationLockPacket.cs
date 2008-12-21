using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class RuleViolationLockPacket : IncomingPacket
    {

        public RuleViolationLockPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.RuleViolationLock;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.RuleViolationLock)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.RuleViolationLock;

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);
            msg.AddByte((byte)Type);

            return msg.Packet;
        }
    }
}