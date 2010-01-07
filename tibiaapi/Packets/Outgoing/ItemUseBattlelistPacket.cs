using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ItemUseBattlelistPacket : OutgoingPacket
    {
        public Objects.Location FromLocation { get; set; }
        public ushort SpriteId { get; set; }
        public byte FromStackPosition { get; set; }
        public uint CreatureId { get; set; }

        public ItemUseBattlelistPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ItemUseBattlelist;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ItemUseBattlelist)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ItemUseBattlelist;

            FromLocation = msg.GetLocation();
            SpriteId = msg.GetUInt16();
            FromStackPosition = msg.GetByte();
            CreatureId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(FromLocation);
            msg.AddUInt16(SpriteId);
            msg.AddByte(FromStackPosition);
            msg.AddUInt32(CreatureId);
        }

        public static bool Send(Objects.Client client, Objects.Location fromLocation, ushort spriteId, byte fromStack,uint creatureId)
        {
            ItemUseBattlelistPacket p = new ItemUseBattlelistPacket(client);

            p.FromLocation = fromLocation;
            p.SpriteId = spriteId;
            p.FromStackPosition = fromStack;
            p.CreatureId = creatureId;

            return p.Send();
        }
    }
}