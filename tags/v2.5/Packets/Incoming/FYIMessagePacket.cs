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
            Type = IncomingPacketType.FyiMessage;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.FyiMessage)
                return false;

            Destination = destination;
            Type = IncomingPacketType.FyiMessage;

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