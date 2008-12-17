using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class FYIMessagePacket : IncomingPacket
    {

        public string Message { get; set; }

        public FYIMessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.FYI_MESSAGE;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.FYI_MESSAGE)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.FYI_MESSAGE;

            Message = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddString(Message);

            return msg.Packet;
        }
    }
}