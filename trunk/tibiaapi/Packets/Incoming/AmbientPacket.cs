using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class AmbientPacket : IncomingPacket
    {

        public byte LightLevel { get; set; }
        public byte LightColor { get; set; }

        public AmbientPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Ambient;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Ambient)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Ambient;

            LightLevel = msg.GetByte();
            LightColor = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(LightLevel);
            msg.AddByte(LightColor);
        }

    }
}