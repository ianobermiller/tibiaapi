using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChallengePacket : IncomingPacket
    {
        public ChallengePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Challenge;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Challenge)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Challenge;

            msg.GetUInt32();
            msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}