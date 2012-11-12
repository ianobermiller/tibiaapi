using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class LoginErrorPacket : IncomingPacket
    {
        public string Message { get; set; }

        public LoginErrorPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.LoginError;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.LoginError)
                return false;

            Destination = destination;
            Type = IncomingPacketType.LoginError;

            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}