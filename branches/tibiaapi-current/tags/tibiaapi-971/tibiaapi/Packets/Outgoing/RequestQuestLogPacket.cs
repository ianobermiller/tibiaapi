using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RequestQuestLogPacket : OutgoingPacket
    {
        public RequestQuestLogPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RequestQuestLog;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RequestQuestLog)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RequestQuestLog;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            return new RequestQuestLogPacket(client).Send();
        }
    }
}