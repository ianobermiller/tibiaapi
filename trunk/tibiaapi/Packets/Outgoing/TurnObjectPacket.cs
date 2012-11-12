using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class TurnObjectPacket : OutgoingPacket
    {
        public Objects.Location Location { get; set; }
        public ushort ItemId { get; set; }
        public byte StackPosition { get; set; }

        public TurnObjectPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.TurnObject;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.TurnObject)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.TurnObject;

            Location = msg.GetLocation();
            ItemId = msg.GetUInt16();
            StackPosition = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {

            msg.AddByte((byte)Type);

            msg.AddLocation(Location);
            msg.AddUInt16(ItemId);
            msg.AddByte(StackPosition);
        }

        public static bool Send(Objects.Client client, Objects.Location location, ushort itemId, byte stackPosition)
        {
            TurnObjectPacket p = new TurnObjectPacket(client);

            p.Location = location;
            p.ItemId = itemId;
            p.StackPosition = stackPosition;

            return p.Send();
        }
    }
}