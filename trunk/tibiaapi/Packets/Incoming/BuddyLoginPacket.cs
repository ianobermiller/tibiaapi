using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class BuddyLoginPacket : IncomingPacket
    {

        public uint PlayerId { get; set; }

        public BuddyLoginPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.BuddyLogin;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.BuddyLogin)
                return false;

            Destination = destination;
            Type = IncomingPacketType.BuddyLogin;

            PlayerId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(PlayerId);
        }

    }
}