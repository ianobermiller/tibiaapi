using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChallengePacket : IncomingPacket
    {
        public uint TimeStamp { get; set; }
        public byte Random { get; set; }

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

            TimeStamp=msg.GetUInt32();
            Random=msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddUInt32(TimeStamp);
            msg.AddByte(Random);
        }
    }
}