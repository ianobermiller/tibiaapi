using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class UseObjectPacket : OutgoingPacket
    {
        public Objects.Location FromLocation { get; set; }
        public ushort SpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public byte Index { get; set; }

        public UseObjectPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.UseObject;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.UseObject)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.UseObject;

            FromLocation = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            Index = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddLocation(FromLocation);
            msg.AddUInt16(SpriteId);
            msg.AddByte(FromStackPosition);
            msg.AddByte(Index);
        }

        public static bool Send(Objects.Client client, Objects.Location fromPosition, ushort spriteId, byte fromStack, byte index)
        {
            UseObjectPacket p = new UseObjectPacket(client);
            p.FromLocation = fromPosition;
            p.SpriteId = spriteId;
            p.FromStackPosition = fromStack;
            p.Index = index;

            return p.Send();
        }
    }
}