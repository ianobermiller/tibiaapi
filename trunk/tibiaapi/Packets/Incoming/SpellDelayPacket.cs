using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class SpellDelayPacket : IncomingPacket
    {
        public byte SpellId { get; set; }
        public uint Delay { get; set; }

        public SpellDelayPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SpellDelay;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.SpellDelay)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SpellDelay;

            SpellId = msg.GetByte();
            Delay = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(SpellId);
            msg.AddUInt32(Delay);
        }
    }
}