using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class SpellGroupDelayPacket : IncomingPacket
    {

        public byte GroupId { get; set; }
        public uint Delay { get; set; }

        public SpellGroupDelayPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.SpellGroupDelay;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.SpellGroupDelay)
                return false;

            Destination = destination;
            Type = IncomingPacketType.SpellGroupDelay;

            GroupId = msg.GetByte();
            Delay = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(GroupId);
            msg.AddUInt32(Delay);
        }
    }
}