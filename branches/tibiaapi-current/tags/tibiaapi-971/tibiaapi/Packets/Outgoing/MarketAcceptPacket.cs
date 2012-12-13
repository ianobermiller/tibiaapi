using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class MarketAcceptPacket : OutgoingPacket
    {
        public uint Timestamp { get; set; }
        public ushort Counter { get; set; }
        public ushort Amount { get; set; }

        public MarketAcceptPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.MarketAccept;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.MarketAccept)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.MarketAccept;

            Timestamp = msg.GetUInt32();
            Counter = msg.GetUInt16();
            Amount = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(Timestamp);
            msg.AddUInt16(Counter);
            msg.AddUInt16(Amount);
        }

        public static bool Send(Objects.Client client, uint timestamp, ushort counter, ushort amount)
        {
            MarketAcceptPacket p = new MarketAcceptPacket(client);
            p.Timestamp = timestamp;
            p.Counter = counter;
            p.Amount = amount;
            return p.Send();
        }
    }
}