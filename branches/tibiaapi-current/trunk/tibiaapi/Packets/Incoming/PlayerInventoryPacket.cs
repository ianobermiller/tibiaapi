using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerInventoryPacket : IncomingPacket
    {
        public PlayerInventoryPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerInventory;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerInventory)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerInventory;

            ushort Count = msg.GetUInt16();
            Count -= 1;

            while (Count >= 0)
            {
                msg.GetUInt16();
                msg.GetByte();
                msg.GetUInt16();
                Count -= 1;
            }

            return true;
        }
    }
}