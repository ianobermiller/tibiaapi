using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class EditTextPacket : IncomingPacket
    {
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

            msg.GetUInt32();//id
            msg.GetItem();
            msg.GetUInt16();//max chars
            msg.GetString();//text
            msg.GetString();//author
            msg.GetString();//date

            return true;
        }
    }
}