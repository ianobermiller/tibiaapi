using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class GetObjectInfoPacket : OutgoingPacket
    {
        public GetObjectInfoPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.GetObjectInfo;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.GetObjectInfo)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.GetObjectInfo;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            AddBuddyPacket p = new AddBuddyPacket(client);
            return p.Send();
        }
    }
}