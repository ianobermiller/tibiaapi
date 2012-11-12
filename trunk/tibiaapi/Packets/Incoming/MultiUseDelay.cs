using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class MultiUseDelayPacket : IncomingPacket
    {
        public uint Delay { get; set; }

        public MultiUseDelayPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.MultiUseDelay;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.MultiUseDelay)
                return false;

            Destination = destination;
            Type = IncomingPacketType.MultiUseDelay;

            Delay = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(Delay);
        }
    }
}