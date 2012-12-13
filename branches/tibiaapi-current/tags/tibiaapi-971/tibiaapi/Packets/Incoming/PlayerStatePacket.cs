using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerStatePacket : IncomingPacket
    {

        public ushort Flag { get; set; }

        public PlayerStatePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerState;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerState)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerState;

            Flag = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(Flag);
        }
    }
}