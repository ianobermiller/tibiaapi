using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreatureUnpassPacket : IncomingPacket
    {
        public CreatureUnpassPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureUnpass;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureUnpass)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureUnpass;

            msg.GetUInt32();//creatureid
            msg.GetByte();//isunpassable

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}