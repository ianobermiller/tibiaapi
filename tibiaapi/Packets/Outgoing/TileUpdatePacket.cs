using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class TileUpdatePacket : OutgoingPacket
    {
        public Objects.Location Location { get; set; }

        public TileUpdatePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.TileUpdate;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.TileUpdate)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.TileUpdate;

            Location = msg.GetLocation();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Location);
        }

        public static bool Send(Objects.Client client, Objects.Location location)
        {
            TileUpdatePacket p = new TileUpdatePacket(client);
            p.Location = location;
            return p.Send();
        }
    }
}
