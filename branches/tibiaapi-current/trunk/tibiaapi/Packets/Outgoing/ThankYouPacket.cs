using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ThankYouPacket : OutgoingPacket
    {
        public uint StatementId { get; set; }

        public ThankYouPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ThankYou;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ThankYou)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ThankYou;

            StatementId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(StatementId);
        }

        public static bool Send(Objects.Client client, uint statementId)
        {
            ThankYouPacket p = new ThankYouPacket(client);
            p.StatementId = statementId;
            return p.Send();
        }
    }
}