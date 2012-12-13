using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RequestQuestLinePacket : OutgoingPacket
    {
        public ushort QuestId { get; set; }

        public RequestQuestLinePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RequestQuestLine;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RequestQuestLine)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RequestQuestLine;

            QuestId = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(QuestId);
        }

        public static bool Send(Objects.Client client, ushort questId)
        {
            return new RequestQuestLinePacket(client) { QuestId = questId }.Send();
        }
    }
}