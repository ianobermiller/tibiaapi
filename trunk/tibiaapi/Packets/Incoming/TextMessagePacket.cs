using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class TextMessagePacket : IncomingPacket
    {
        public MessageClasses_t Color { get; set; }
        public string Message { get; set; }

        public TextMessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.TEXT_MESSAGE;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.TEXT_MESSAGE)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.TEXT_MESSAGE;
            Color = (MessageClasses_t)msg.GetByte();
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

        public static bool Send(Objects.Client client, MessageClasses_t color, string msg)
        {
            TextMessagePacket p = new TextMessagePacket(client);
            p.Color = color;
            p.Message = msg;

            return p.Send();
        }

    }
}