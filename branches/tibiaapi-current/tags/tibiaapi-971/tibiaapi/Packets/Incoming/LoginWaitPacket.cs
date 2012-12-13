using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class LoginWaitPacket : IncomingPacket
    {
        public string Message { get; set; }
        public byte Time { get; set; }

        public LoginWaitPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.LoginWait;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.LoginWait)
                return false;

            Destination = destination;
            Type = IncomingPacketType.LoginWait;

            Message = msg.GetString();
            Time = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Message);
            msg.AddByte(Time);
        }
    }
}