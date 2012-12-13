using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class DeleteInventoryPacket : IncomingPacket
    {
        public byte Slot { get; set; }

        public DeleteInventoryPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.DeleteInventory;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.DeleteInventory)
                return false;

            Destination = destination;
            Type = IncomingPacketType.DeleteInventory;

            Slot = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Slot);
        }
    }
}