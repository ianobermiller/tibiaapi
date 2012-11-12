using Tibia.Constants;
using Tibia.Objects;

namespace Tibia.Packets.Incoming
{
    public class MagicEffectPacket : IncomingPacket
    {
        public Location Location { get; set; }
        public Effect Effect { get; set; }

        public MagicEffectPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MagicEffect;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MagicEffect)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MagicEffect;

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

        public static bool Send(Objects.Client client, Location location, Effect effect)
        {
            MagicEffectPacket p = new MagicEffectPacket(client);
            p.Location = location;
            p.Effect = effect;

            return p.Send();
        }
    }
}