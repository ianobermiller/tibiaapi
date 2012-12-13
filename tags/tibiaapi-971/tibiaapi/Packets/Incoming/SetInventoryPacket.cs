using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class SetInventoryPacket : IncomingPacket
    {
        public byte Slot { get; set; }
        public Objects.Item Item { get; set; }

        public SetInventoryPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SetInventory;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.SetInventory)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SetInventory;

            Slot = msg.GetByte();

            Item = msg.GetItem();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Slot);

            msg.AddItem(Item);
        }

    }
}