using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class InspectNPCTradePacket : OutgoingPacket
    {
        public ushort TradeType { get; set; }
        public byte Data { get; set; }

        public InspectNPCTradePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.InspectNPCTrade;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.InspectNPCTrade)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.InspectNPCTrade;

            TradeType = msg.GetUInt16();
            Data = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(TradeType);
            msg.AddByte(Data);
        }

        public static bool Send(Objects.Client client, ushort tradeType, byte data)
        {
            InspectNPCTradePacket p = new InspectNPCTradePacket(client);
            p.TradeType = tradeType;
            p.Data = data;
            return p.Send();
        }
    }
}