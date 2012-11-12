using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class MarketCreatePacket : OutgoingPacket
    {
        public byte Kind { get; set; }
        public ushort TypeId { get; set; }
        public ushort Amount { get; set; }
        public uint PiecePrice { get; set; }
        public bool IsAnonymous { get; set; }

        public MarketCreatePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.MarketCreate;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.MarketCreate)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.MarketCreate;

            Kind = msg.GetByte();
            TypeId = msg.GetUInt16();
            Amount = msg.GetUInt16();
            PiecePrice = msg.GetUInt32();
            IsAnonymous = System.Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Kind);
            msg.AddUInt16(TypeId);
            msg.AddUInt16(Amount);
            msg.AddUInt32(PiecePrice);
            msg.AddByte(System.Convert.ToByte(IsAnonymous));
        }

        public static bool Send(Objects.Client client, byte kind, ushort typeId, ushort amount, uint piecePrice, bool isAnonymous)
        {
            MarketCreatePacket p = new MarketCreatePacket(client);
            p.Kind = kind;
            p.TypeId = typeId;
            p.Amount = amount;
            p.PiecePrice = piecePrice;
            p.IsAnonymous = isAnonymous;
            return p.Send();
        }
    }
}