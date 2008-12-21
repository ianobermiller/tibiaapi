using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class SelfAppearPacket : IncomingPacket
    {
        public uint YourId { get; set; }
        public byte Unknow1 { get; set; }
        public byte Unknow2 { get; set; }
        public byte CanReportBug { get; set; }

        public SelfAppearPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SelfAppear;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, ref Objects.Location pos)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.SelfAppear)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SelfAppear;

            try
            {
                YourId = msg.GetUInt32();
                Unknow1 = msg.GetByte();
                Unknow2 = msg.GetByte();
                CanReportBug = msg.GetByte();
            }
            catch (Exception)
            {
                msg.Position = position;
                return false;
            }

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(Client, 0);

            msg.AddByte((byte)Type);

            msg.AddUInt32(YourId);
            msg.AddByte(Unknow1);
            msg.AddByte(Unknow2);
            msg.AddByte(CanReportBug);

            return msg.Packet;
        }
    }
}