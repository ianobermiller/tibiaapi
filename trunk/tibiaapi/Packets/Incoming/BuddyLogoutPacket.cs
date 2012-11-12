using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class BuddyLogoutPacket : IncomingPacket
    {

        public uint PlayerId { get; set; }

        public BuddyLogoutPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.BuddyLogout;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.BuddyLogout)
                return false;

            Destination = destination;
            Type = IncomingPacketType.BuddyLogout;

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