using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class AnswerModalDialogPacket : OutgoingPacket
    {
        public uint Id { get; set; }
        public byte Answer { get; set; }

        public AnswerModalDialogPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.AnswerModalDialog;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.AnswerModalDialog)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.AnswerModalDialog;

            Id = msg.GetUInt32();
            Answer = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(Id);
            msg.AddByte(Answer);
        }

        public static bool Send(Objects.Client client, uint id, byte answer)
        {
            AnswerModalDialogPacket p = new AnswerModalDialogPacket(client);
            p.Id = id;
            p.Answer = answer;
            return p.Send();
        }
    }
}