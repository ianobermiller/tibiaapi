using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class WaitingListPacket : IncomingPacket
    {
        public string Message { get; set; }
        public byte Time { get; set; }

        public WaitingListPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.WAITING_LIST;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.WAITING_LIST)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.WAITING_LIST;

            Message = msg.GetString();
            Time = msg.GetByte();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddString(Message);
            msg.AddByte(Time);

            return msg.Packet;
        }
    }
}