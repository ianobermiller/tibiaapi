using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class BugReportPacket : OutgoingPacket
    {
        public string Message { get; set; }

        public BugReportPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.BugReport;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.BugReport)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.BugReport;

            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, string message)
        {
            return new BugReportPacket(client) { Message = message }.Send();
        }
    }
}