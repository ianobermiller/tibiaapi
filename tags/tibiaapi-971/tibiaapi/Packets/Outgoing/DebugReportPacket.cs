using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class DebugReportPacket : OutgoingPacket
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }

        public DebugReportPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.DebugReport;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.DebugReport)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.DebugReport;

            A = msg.GetString();
            B = msg.GetString();
            C = msg.GetString();
            D = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(A);
            msg.AddString(B);
            msg.AddString(C);
            msg.AddString(D);
        }

        public static bool Send(Objects.Client client, string a, string b,string c,string d)
        {
            return new DebugReportPacket(client) { A = a, B = b, C = c, D = d }.Send();
        }
    }
}
