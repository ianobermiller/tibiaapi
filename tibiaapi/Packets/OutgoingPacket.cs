using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Util;
using System.Runtime.InteropServices;

namespace Tibia.Packets
{
    public class OutgoingPacket : Packet
    {
        public OutgoingPacketType Type { get; set; }

        public OutgoingPacket(Objects.Client c)
            : base(c) {}
    }
}
