using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class GetQuestLinePacket : OutgoingPacket
    {
        public ushort QuestLine { get; set; }

        public GetQuestLinePacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.GetQuestLine;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.GetQuestLine)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.GetQuestLine;

            QuestLine = msg.GetUInt16();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(QuestLine);
        }

        public static bool Send(Objects.Client client, ushort questLine)
        {
            GetQuestLinePacket p = new GetQuestLinePacket(client);
            p.QuestLine = questLine;
            return p.Send();
        }
    }
}