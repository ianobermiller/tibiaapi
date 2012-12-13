using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class DeathPacket : IncomingPacket
    {
        public byte Penalty { get; set; }
        public DeathPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Death;
            Destination = PacketDestination.Client;
            Penalty = 100;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.Death)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Death;

            Penalty = msg.GetByte(); //?

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Penalty);
        }
    }
}