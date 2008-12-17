using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CanReportBugsPacket : IncomingPacket
    {

        public byte ReportBugs { get; set; }

        public CanReportBugsPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CAN_REPORT_BUGS;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CAN_REPORT_BUGS)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CAN_REPORT_BUGS;

            ReportBugs = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddByte(ReportBugs);

            return msg.Packet;
        }
    }
}