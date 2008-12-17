using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class SelfAppearPacket : IncomingPacket
    {
        public uint YourId { get; set; }
        public byte Unknow32 { get; set; }
        public byte Unknow { get; set; }
        public byte CanReportBug { get; set; }

        public SelfAppearPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.SELF_APPEAR;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.SELF_APPEAR)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.SELF_APPEAR;

            YourId = msg.GetUInt32();
            Unknow32 = msg.GetByte();
            Unknow = msg.GetByte();
            CanReportBug = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(YourId);
            msg.AddByte(Unknow32);
            msg.AddByte(Unknow);
            msg.AddByte(CanReportBug);

            return msg.Packet;
        }
    }
}