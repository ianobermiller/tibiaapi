using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class SellItemPacket : OutgoingPacket
    {
        public ushort ItemId { get; set; }
        public byte SubType { get; set; }
        public byte Amount { get; set; }
        public bool KeepEquipped { get; set; }

        public SellItemPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.SellItem;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.SellItem)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.SellItem;

            ItemId = msg.GetUInt16();
            SubType = msg.GetByte();
            Amount = msg.GetByte();
            KeepEquipped = System.Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt16(ItemId);
            msg.AddByte(SubType);
            msg.AddByte(Amount);
            msg.AddByte(System.Convert.ToByte(KeepEquipped));
        }

        public static bool Send(Objects.Client client, ushort itemId, byte subType, byte amount, bool keepEquipped)
        {
            return new SellItemPacket(client) { ItemId = itemId, SubType = subType, Amount = amount, KeepEquipped = keepEquipped }.Send();
        }
    }
}