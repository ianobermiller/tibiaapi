using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class UseItemWithPacket : OutgoingPacket
    {
        public Objects.Location FromLocation { get; set; }
        public ushort FromItemId { get; set; }
        public byte FromStackPosition { get; set; }
        public Objects.Location ToLocation { get; set; }
        public ushort ToItemId { get; set; }
        public byte ToStackPosition { get; set; }

        public UseItemWithPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.UseItemWith;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.UseItemWith)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.UseItemWith;

            FromLocation = msg.GetLocation();
            FromItemId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            ToLocation = msg.GetLocation();
            ToItemId = msg.GetUInt16();
            ToStackPosition = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(FromLocation);
            msg.AddUInt16(FromItemId);
            msg.AddByte(FromStackPosition);
            msg.AddLocation(ToLocation);
            msg.AddUInt16(ToItemId);
            msg.AddByte(ToStackPosition);
        }

        public static bool Send(Objects.Client client,
                                Objects.Location fromLocation, ushort fromItemId, byte fromStackPostion,
                                Objects.Location toLocation, ushort toItemId, byte toStackPosition)
        {
            UseItemWithPacket p = new UseItemWithPacket(client);

            p.FromLocation = fromLocation;
            p.FromItemId = fromItemId;
            p.FromStackPosition = fromStackPostion;
            p.ToLocation = toLocation;
            p.ToItemId = toItemId;
            p.ToStackPosition = toStackPosition;

            return p.Send();
        }
    }
}