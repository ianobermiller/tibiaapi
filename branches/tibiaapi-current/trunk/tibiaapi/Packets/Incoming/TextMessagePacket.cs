using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TextMessagePacket : IncomingPacket
    {
        public TextMessageColor Color { get; set; }
        public string Message { get; set; }

        public TextMessagePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TextMessage;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TextMessage)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TextMessage;

            Color = (TextMessageColor)msg.GetByte();
            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte((byte)Color);
            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, TextMessageColor color, string msg)
        {
            TextMessagePacket p = new TextMessagePacket(client);
            p.Color = color;
            p.Message = msg;

            return p.Send();
        }

    }
}