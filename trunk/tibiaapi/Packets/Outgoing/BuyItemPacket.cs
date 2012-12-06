using System;
using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class BuyItemPacket : OutgoingPacket
    {
        public ushort ItemId { get; set; }
        public byte SubType { get; set; }
        public byte Amount { get; set; }
        public bool IgnoreCapacity { get; set; }
        public bool WithBackpack { get; set; }


        public BuyItemPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.BuyItem;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.BuyItem)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.BuyItem;

            ItemId = msg.GetUInt16();
            SubType = msg.GetByte();
            Amount = msg.GetByte();
            IgnoreCapacity = Convert.ToBoolean(msg.GetByte());
            WithBackpack = Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt16(ItemId);
            msg.AddByte(SubType);
            msg.AddByte(Amount);
            msg.AddByte(Convert.ToByte(IgnoreCapacity));
            msg.AddByte(Convert.ToByte(WithBackpack));
        }

        public static bool Send(Objects.Client client, ushort itemId, byte subType, byte amount, bool ignoreCapacity, bool withBackpack)
        {
            return new BuyItemPacket(client) { ItemId = itemId, SubType = subType, Amount = amount, IgnoreCapacity = ignoreCapacity, WithBackpack = withBackpack }.Send();
        }
    }
}