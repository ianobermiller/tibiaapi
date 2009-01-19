using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class TextMessagePacket : IncomingPacket
    {
        public StatusMessage Color { get; set; }
        public string Message { get; set; }

        public TextMessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.StatusMessage;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.StatusMessage)
                return false;

            Destination = destination;
            Type = IncomingPacketType.StatusMessage;
            Color = (StatusMessage)msg.GetByte();
            Message = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);
            msg.AddByte((byte)Color);
            msg.AddString(Message);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, StatusMessage color, string msg)
        {
            TextMessagePacket p = new TextMessagePacket(client);
            p.Color = color;
            p.Message = msg;

            return p.Send();
        }

    }
}