using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ObjectInfoPacket : IncomingPacket
    {
        public string Message { get; set; }
        public byte Time { get; set; }

        public ObjectInfoPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ObjectInfo;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.ObjectInfo)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ObjectInfo;

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

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}