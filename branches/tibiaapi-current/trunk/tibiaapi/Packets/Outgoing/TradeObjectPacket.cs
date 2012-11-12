using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class TradeObjectPacket : OutgoingPacket
    {
        public Objects.Location Location { get; set; }
        public ushort ObjectType { get; set; }
        public byte StackPosition { get; set; }
        public uint TradePartner { get; set; }

        public TradeObjectPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.TradeObject;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.TradeObject)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.TradeObject;

            Location = msg.GetLocation();
            ObjectType = msg.GetUInt16();
            StackPosition = msg.GetByte();
            TradePartner = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Location);
            msg.AddUInt16(ObjectType);
            msg.AddByte(StackPosition);
            msg.AddUInt32(TradePartner);
        }

        public static bool Send(Objects.Client client, Objects.Location location, ushort objectType, byte stackPosition, uint tradePartner)
        {
            TradeObjectPacket p = new TradeObjectPacket(client);
            p.Location = location;
            p.ObjectType = objectType;
            p.StackPosition = stackPosition;
            p.TradePartner = tradePartner;
            return p.Send();
        }
    }
}