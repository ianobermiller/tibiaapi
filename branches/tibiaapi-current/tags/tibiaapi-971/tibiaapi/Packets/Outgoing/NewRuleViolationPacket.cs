using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class NewRuleViolationPacket : OutgoingPacket
    {
        public byte ReportType { get; set; }
        public byte Reason { get; set; }
        public string CharacterName { get; set; }
        public string Comment { get; set; }
        public string Translation { get; set; }
        public uint StatementId { get; set; }

        public NewRuleViolationPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.NewRuleViolation;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.NewRuleViolation)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.NewRuleViolation;

            /// <summary>
            /// Statement Report
            /// </summary>
            ReportType = msg.GetByte();
            Reason = msg.GetByte();
            CharacterName = msg.GetString();
            Comment = msg.GetString();
            Translation = msg.GetString();
            StatementId = msg.GetUInt32();

            /// <summary>
            /// Bot Report
            /// </summary>
            ReportType = msg.GetByte();
            Reason = msg.GetByte();
            CharacterName = msg.GetString();
            Comment = msg.GetString();

            /// <summary>
            /// Name Report
            /// </summary>
            ReportType = msg.GetByte();
            Reason = msg.GetByte();
            CharacterName = msg.GetString();
            Comment = msg.GetString();
            Translation = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddByte(ReportType);
            msg.AddByte(Reason);
            msg.AddString(CharacterName);
            msg.AddString(Comment);
            msg.AddString(Translation);
            msg.AddUInt32(StatementId);
        }

        public static bool Send(Objects.Client client, byte reportType, byte reason, string characterName, string comment, string translation, uint statementId)
        {
            NewRuleViolationPacket p = new NewRuleViolationPacket(client);
            p.ReportType = reportType;
            p.Reason = reason;
            p.CharacterName = characterName;
            p.Comment = comment;
            p.Translation = translation;
            p.StatementId = statementId;
            return p.Send();
        }
    }
}