using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class EditTextPacket : OutgoingPacket
    {
        public uint Id { get; set; }
        public string Text { get; set; }

        public EditTextPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.EditText;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.EditText)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.EditText;

            Id = msg.GetUInt32();
            Text = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(Id);
            msg.AddString(Text);
        }

        public static bool Send(Objects.Client client, uint id, string text)
        {
            EditTextPacket p = new EditTextPacket(client);
            p.Id = id;
            p.Text = text;
            return p.Send();
        }
    }
}