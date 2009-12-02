using System;
using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class SelfAppearPacket : IncomingPacket
    {
        public uint YourId { get; set; }
        public ushort Unknown { get; set; }
        public byte CanReportBug { get; set; }

        public SelfAppearPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SelfAppear;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.SelfAppear)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SelfAppear;

            try
            {
                YourId = msg.GetUInt32();
                Unknown = msg.GetUInt16(); // Related to client-side drawing speed
                CanReportBug = msg.GetByte();
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
            msg.AddUInt32(YourId);
            msg.AddUInt16(0x0032); // Related to client-side drawing speed
            msg.AddByte(CanReportBug);
        }
    }
}