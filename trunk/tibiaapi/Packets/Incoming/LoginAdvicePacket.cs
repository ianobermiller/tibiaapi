using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class LoginAdvicePacket : IncomingPacket
    {

        public string Message { get; set; }

        public LoginAdvicePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.LoginAdvice;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.LoginAdvice)
                return false;

            Destination = destination;
            Type = IncomingPacketType.LoginAdvice;

            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, string message)
        {
            LoginAdvicePacket p = new LoginAdvicePacket(client);
            p.Message = message;
            return p.Send();
        }
    }
}