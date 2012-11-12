using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class InspectTradePacket : OutgoingPacket
    {
        public byte Side { get; set; }
        public byte Position { get; set; }

        public InspectTradePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.InspectTrade;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.InspectTrade)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.InspectTrade;

            Side = msg.GetByte();
            Position = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Side);
            msg.AddByte(Position);
        }

        public static bool Send(Objects.Client client, byte side, byte position)
        {
            InspectTradePacket p = new InspectTradePacket(client);
            p.Side = side;
            p.Position = position;
            return p.Send();
        }
    }
}