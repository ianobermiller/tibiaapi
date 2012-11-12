using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChangeOnMapPacket : IncomingPacket
    {
        public ushort ThingId { get; set; }
        public byte StackPosition { get; set; }
        public Objects.Location Position { get; set; }
        public Objects.Item Item { get; set; }
        public uint CreatureId { get; set; }
        public byte CreatureDirection { get; set; }
        public bool IsUnpassable { get; set; }

        public ChangeOnMapPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChangeOnMap;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ChangeOnMap)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ChangeOnMap;

            Position = msg.GetLocation();
            StackPosition = msg.GetByte();
            ThingId = msg.GetUInt16();
            CreatureId = msg.GetUInt32();
            CreatureDirection = msg.GetByte();
            IsUnpassable = System.Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}