using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ItemMovePacket : OutgoingPacket
    {
        public Objects.Location FromLocation { get; set; }
        public ushort SpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public Objects.Location ToLocation { get; set; }
        public byte Count { get; set; }

        public ItemMovePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemMove;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemMove)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemMove;

            FromLocation = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            ToLocation = msg.GetLocation();
            Count = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddLocation(FromLocation);
            msg.AddUInt16(SpriteId);
            msg.AddByte(FromStackPosition);
            msg.AddLocation(ToLocation);
            msg.AddByte(Count);
        }

        public static bool Send(Objects.Client client, Objects.Location fromLocation, ushort spriteId, byte fromStackPostion, Objects.Location toLocation, byte count)
        {
            ItemMovePacket p = new ItemMovePacket(client);

            p.FromLocation = fromLocation;
            p.SpriteId = spriteId;
            p.FromStackPosition = fromStackPostion;
            p.ToLocation = toLocation;
            p.Count = count;

            return p.Send();
        }
    }
}