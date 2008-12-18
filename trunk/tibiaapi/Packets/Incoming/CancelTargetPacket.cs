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
            Type = IncomingPacketType_t.CANCEL_TARGET;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CANCEL_TARGET)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType_t.CANCEL_TARGET;

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