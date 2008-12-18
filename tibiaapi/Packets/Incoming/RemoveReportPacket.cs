using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class RemoveReportPacket : IncomingPacket
    {
        public string Name { get; set; }

        public RemoveReportPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.REMOVE_REPORT;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.REMOVE_REPORT)
                throw new Exception();

            Destination = destination;
            Type = IncomingPacketType_t.REMOVE_REPORT;

            Name = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddString(Name);

            return msg.Packet;
        }
    }
}