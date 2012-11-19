using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Incoming
{
    public class GraphicalEffectPacket : IncomingPacket
    {
        public Location Location { get; set; }
        public Effect Effect { get; set; }

        public GraphicalEffectPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.GraphicalEffect;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.GraphicalEffect)
                return false;

            Destination = destination;
            Type = IncomingPacketType.GraphicalEffect;

            Location = msg.GetLocation();
            Effect = (Effect)msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Location);
            msg.AddByte((byte)Effect);
        }
    }
}