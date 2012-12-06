using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class UseItemPacket : OutgoingPacket
    {
        public Objects.Location FromLocation { get; set; }
        public ushort ItemId { get; set; }
        public byte FromStackPosition { get; set; }
        public byte Index { get; set; }

        public UseItemPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.UseItem;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.UseItem)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.UseItem;

            FromLocation = msg.GetLocation();
            ItemId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            Index = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddLocation(FromLocation);
            msg.AddUInt16(ItemId);
            msg.AddByte(FromStackPosition);
            msg.AddByte(Index);
        }

        public static bool Send(Objects.Client client, Objects.Location fromPosition, ushort itemId, byte fromStack, byte index)
        {
            return new UseItemPacket(client) { FromLocation = fromPosition, ItemId = itemId, FromStackPosition = fromStack, Index = index }.Send();
        }
    }
}