using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RuleViolationPacket : OutgoingPacket
    {
        public string Target { get; set; }
        public byte Reason { get; set; }
        public byte Action { get; set; }
        public string Comment { get; set; }
        public string Statement { get; set; }
        public ushort StatementId { get; set; }
        public byte IpBanishment { get; set; }

        public RuleViolationPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RuleViolation;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RuleViolation)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RuleViolation;

            Target = msg.GetString();
            Reason = msg.GetByte();
            Action = msg.GetByte();
            Comment = msg.GetString();
            Statement = msg.GetString();
            StatementId = msg.GetUInt16();
            IpBanishment = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Target);
            msg.AddByte(Reason);
            msg.AddByte(Action);
            msg.AddString(Comment);
            msg.AddString(Statement);
            msg.AddUInt16(StatementId);
            msg.AddByte(IpBanishment);
        }

        public static bool Send(Objects.Client client, string target, byte reason, byte action, string comment, string statement, ushort statementId, byte ipBanishment)
        {
            RuleViolationPacket p = new RuleViolationPacket(client);
            p.Target = target;
            p.Reason = reason;
            p.Action = action;
            p.Comment = comment;
            p.Statement = statement;
            p.StatementId = statementId;
            p.IpBanishment = ipBanishment;
            return p.Send();
        }
    }
}