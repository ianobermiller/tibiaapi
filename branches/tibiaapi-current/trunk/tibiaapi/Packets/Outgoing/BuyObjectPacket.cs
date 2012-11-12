using System;
using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class BuyObjectPacket : OutgoingPacket
    {
        public ushort ItemId { get; set; }
        public byte Count { get; set; }
        public byte Amount { get; set; }
        public bool IgnoreCapacity { get; set; }
        public bool WithBackpack { get; set; }


        public BuyObjectPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.BuyObject;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.BuyObject)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.BuyObject;

            ItemId = msg.GetUInt16();
            Count = msg.GetByte();
            Amount = msg.GetByte();
            IgnoreCapacity = Convert.ToBoolean(msg.GetByte());
            WithBackpack = Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt16(ItemId);
            msg.AddByte(Count);
            msg.AddByte(Amount);
            msg.AddByte(Convert.ToByte(IgnoreCapacity));
            msg.AddByte(Convert.ToByte(WithBackpack));
        }

        public static bool Send(Objects.Client client, ushort itemId, byte count, byte amount, bool ignoreCapacity, bool withBackpack)
        {
            BuyObjectPacket p = new BuyObjectPacket(client);

            p.ItemId = itemId;
            p.Count = count;
            p.Amount = amount;
            p.IgnoreCapacity = ignoreCapacity;
            p.WithBackpack = withBackpack;

            return p.Send();
        }
    }
}