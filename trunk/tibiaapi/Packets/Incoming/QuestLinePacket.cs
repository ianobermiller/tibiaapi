using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class QuestLinePacket : IncomingPacket
    {
        public QuestLinePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.QuestLine;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.QuestLine)
                return false;

            Destination = destination;
            Type = IncomingPacketType.QuestLine;

            msg.GetUInt16();
            byte Count = msg.GetByte();

            for (int i = 0; i < Count; i++)
            {
                msg.GetString();
                msg.GetString();
            }

            return true;
        }
    }
}