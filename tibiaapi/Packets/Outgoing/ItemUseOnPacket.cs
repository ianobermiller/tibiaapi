using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ItemUseOnPacket : OutgoingPacket
    {
        public Objects.Location FromLocation { get; set; }
        public ushort FromSpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public Objects.Location ToLocation { get; set; }
        public ushort ToSpriteId { get; set; }
        public byte ToStackPosition { get; set; }

        public ItemUseOnPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemUseOn;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemUseOn)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemUseOn;

            FromLocation = msg.GetLocation();
            FromSpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            ToLocation = msg.GetLocation();
            ToSpriteId = msg.GetUInt16();
            ToStackPosition = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(FromLocation);
            msg.AddUInt16(FromSpriteId);
            msg.AddByte(FromStackPosition);
            msg.AddLocation(ToLocation);
            msg.AddUInt16(ToSpriteId);
            msg.AddByte(ToStackPosition);
        }

        public static bool Send(Objects.Client client, Objects.Location fromLocation, ushort fromSpriteId, byte fromStackPostion, Objects.Location toLocation, ushort toSpriteId, byte toStackPosition)
        {
            ItemUseOnPacket p = new ItemUseOnPacket(client);

            p.FromLocation = fromLocation;
            p.FromSpriteId = fromSpriteId;
            p.FromStackPosition = fromStackPostion;
            p.ToLocation = toLocation;
            p.ToSpriteId = toSpriteId;
            p.ToStackPosition = toStackPosition;

            return p.Send();
        }
    }
}