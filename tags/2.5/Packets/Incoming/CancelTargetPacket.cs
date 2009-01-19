using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CancelTargetPacket : IncomingPacket
    {

        public CancelTargetPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CancelTarget;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CancelTarget)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType.CancelTarget;

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);
            msg.AddByte((byte)Type);

            return msg.Packet;
        }
    }
}