using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TextEffectPacket : IncomingPacket
    {
        public Objects.Location Location { get; set; }
        public string Message { get; set; }
        public TextColor Color { get; set; }

        public TextEffectPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TextEffect;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TextEffect)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TextEffect;

            Location = msg.GetLocation();
            Color = (TextColor)msg.GetByte();
            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Location);
            msg.AddByte((byte)Color);
            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, string message, Objects.Location position, TextColor color)
        {
            TextEffectPacket p = new TextEffectPacket(client);
            p.Message = message;
            p.Location = position;
            p.Color = color;
            return p.Send();
        }
    }
}
