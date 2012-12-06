using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RequestItemInfoPacket : OutgoingPacket
    {
        public byte SubType { get; set; }
        public ushort ItemId { get; set; }
        public byte Index { get; set; }
        public RequestItemInfoPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RequestItemInfo;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RequestItemInfo)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RequestItemInfo;

            SubType = msg.GetByte();
            ItemId = msg.GetUInt16();
            Index = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(SubType);
            msg.AddUInt16(ItemId);
            msg.AddByte(Index);
        }

        public static bool Send(Objects.Client client, byte subType, ushort itemId, byte index)
        {
            return new RequestItemInfoPacket(client) { SubType = subType, ItemId = itemId, Index = index }.Send();
        }
    }
}