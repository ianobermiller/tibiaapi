using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RemoveBuddyPacket : OutgoingPacket
    {
        public uint Id { get; set; }

        public RemoveBuddyPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RemoveBuddy;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RemoveBuddy)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RemoveBuddy;

            Id = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt32(Id);
        }

        public static bool Send(Objects.Client client, uint id)
        {
            RemoveBuddyPacket p = new RemoveBuddyPacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}