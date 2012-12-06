using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ItemInfoPacket : IncomingPacket
    {
        public string Message { get; set; }
        public byte Time { get; set; }

        public ItemInfoPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ItemInfo;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.ItemInfo)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ItemInfo;

            byte Count = msg.GetByte();
            Count -= 1;
            while (Count >= 0)
            {
                msg.GetUInt16();
                msg.GetByte();
                msg.GetString();
                Count -= 1;
            }

            return true;
        }
    }
}