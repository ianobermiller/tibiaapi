using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class EquipItemPacket : OutgoingPacket
    {
        public ushort ItemId { get; set; }
        public byte Data { get; set; }

        public EquipItemPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.EquipItem;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.EquipItem)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.EquipItem;

            ItemId = msg.GetUInt16();
            Data = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(ItemId);
            msg.AddByte(Data);
        }

        public static bool Send(Objects.Client client, ushort itemId, byte data)
        {
            return new EquipItemPacket(client) { ItemId = itemId, Data = data }.Send();
        }
    }
}