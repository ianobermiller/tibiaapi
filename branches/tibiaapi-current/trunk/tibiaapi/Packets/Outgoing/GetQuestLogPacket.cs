using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class GetQuestLogPacket : OutgoingPacket
    {
        public GetQuestLogPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.GetQuestLog;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.GetQuestLog)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.GetQuestLog;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            GetQuestLogPacket p = new GetQuestLogPacket(client);
            return p.Send();
        }
    }
}