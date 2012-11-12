using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class MarketBrowsePacket : OutgoingPacket
    {
        public ushort Id { get; set; }

        public MarketBrowsePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.MarketBrowse;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.MarketBrowse)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.MarketBrowse;

            Id = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(Id);
        }

        public static bool Send(Objects.Client client, ushort id)
        {
            MarketBrowsePacket p = new MarketBrowsePacket(client);
            p.Id = id;
            return p.Send();
        }
    }
}