using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class MarketCancelPacket : OutgoingPacket
    {
        public uint Timestamp { get; set; }
        public ushort Counter { get; set; }

        public MarketCancelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.MarketCancel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.MarketCancel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.MarketCancel;

            Timestamp = msg.GetUInt32();
            Counter = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(Timestamp);
            msg.AddUInt16(Counter);
        }

        public static bool Send(Objects.Client client, uint timestamp, ushort counter)
        {
            MarketCancelPacket p = new MarketCancelPacket(client);
            p.Timestamp = timestamp;
            p.Counter = counter;
            return p.Send();
        }
    }
}