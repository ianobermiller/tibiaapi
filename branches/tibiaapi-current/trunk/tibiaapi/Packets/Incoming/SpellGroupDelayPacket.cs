using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class SpellGroupDelayPacket : IncomingPacket
    {

        public byte Unknown1 { get; set; }
        public uint Unknown2 { get; set; }

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

            Unknown1 = msg.GetByte();
            Unknown2 = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Unknown1);
            msg.AddUInt32(Unknown2);
        }
    }
}