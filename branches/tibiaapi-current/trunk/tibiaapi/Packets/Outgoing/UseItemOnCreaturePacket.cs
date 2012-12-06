using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class UseItemOnCreaturePacket : OutgoingPacket
    {
        public Objects.Location FromLocation { get; set; }
        public ushort ItemId { get; set; }
        public byte FromStackPosition { get; set; }
        public uint CreatureId { get; set; }

        public UseItemOnCreaturePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.UseOnCreature;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.UseOnCreature)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.UseOnCreature;

            FromLocation = msg.GetLocation();
            ItemId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            CreatureId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(FromLocation);
            msg.AddUInt16(ItemId);
            msg.AddByte(FromStackPosition);
            msg.AddUInt32(CreatureId);
        }

        public static bool Send(Objects.Client client, Objects.Location fromLocation, ushort itemId, byte fromStack, uint creatureId)
        {
            return new UseItemOnCreaturePacket(client) { FromLocation = fromLocation, ItemId = itemId, FromStackPosition = fromStack, CreatureId = creatureId }.Send();
        }
    }
}