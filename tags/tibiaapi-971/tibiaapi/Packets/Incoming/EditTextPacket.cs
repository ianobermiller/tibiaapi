using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class EditTextPacket : IncomingPacket
    {
        public uint Id { get; set; }
        public ushort ItemId { get; set; }
        public ushort MaxLength { get; set; }
        public string Text { get; set; }
        public string Writer { get; set; }
        public string Date { get; set; }
        public EditTextPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.EditText;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.EditText)
                return false;

            Destination = destination;
            Type = IncomingPacketType.EditText;

            Id = msg.GetUInt32();//id
            ItemId = msg.GetUInt16();
            MaxLength = msg.GetUInt16();//max chars
            Text = msg.GetString();//text
            Writer = msg.GetString();//author
            Date = msg.GetString();//date

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddUInt32(Id);//id
            msg.AddUInt16(ItemId);
            msg.AddUInt16(MaxLength);//max chars
            msg.AddString(Text);//text
            msg.AddString(Writer);//author
            msg.AddString(Date);//date
        }
    }
}