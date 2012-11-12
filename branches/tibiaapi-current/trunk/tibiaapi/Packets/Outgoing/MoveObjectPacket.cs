using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class MoveObjectPacket : OutgoingPacket
    {
        public Objects.Location FromLocation { get; set; }
        public ushort SpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public Objects.Location ToLocation { get; set; }
        public byte Count { get; set; }

        public MoveObjectPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.MoveObject;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.MoveObject)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.MoveObject;

            FromLocation = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            ToLocation = msg.GetLocation();
            Count = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
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
            MoveObjectPacket p = new MoveObjectPacket(client);

            p.FromLocation = fromLocation;
            p.SpriteId = spriteId;
            p.FromStackPosition = fromStackPostion;
            p.ToLocation = toLocation;
            p.Count = count;

            return p.Send();
        }
    }
}