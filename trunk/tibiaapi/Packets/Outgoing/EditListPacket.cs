using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class EditListPacket : OutgoingPacket
    {
        public byte Edit { get; set; }
        public uint Id { get; set; }
        public string Text { get; set; }

        public EditListPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.EditList;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.EditList)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.EditList;

            Edit = msg.GetByte();
            Id = msg.GetUInt32();
            Text = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(Edit);
            msg.AddUInt32(Id);
            msg.AddString(Text);
        }

        public static bool Send(Objects.Client client, byte edit, uint id, string text)
        {
            EditListPacket p = new EditListPacket(client);
            p.Edit = edit;
            p.Id = id;
            p.Text = text;
            return p.Send();
        }
    }
}