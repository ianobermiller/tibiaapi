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
            Type = IncomingPacketType.CanReportBugs;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CanReportBugs)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CanReportBugs;

            ReportBugs = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(ReportBugs);
        }
    }
}