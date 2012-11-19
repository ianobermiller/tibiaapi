using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MissileEffectPacket : IncomingPacket
    {

        public Objects.Location FromPosition { get; set; }
        public Objects.Location ToPosition { get; set; }
        public ProjectileType Effect { get; set; }

        public MissileEffectPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MissileEffect;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MissileEffect)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MissileEffect;

            FromPosition = msg.GetLocation();
            ToPosition = msg.GetLocation();
            Effect = (ProjectileType)msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(FromPosition);
            msg.AddLocation(ToPosition);
            msg.AddByte((byte)Effect);
        }
    }
}