using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class EditListPacket : IncomingPacket
    {

        public byte DoorId { get; set; }
        public uint Id { get; set; }
        public string Text { get; set; }

        public EditListPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.EditList;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.EditList)
                return false;

            Destination = destination;
            Type = IncomingPacketType.EditList;

            DoorId = msg.GetByte();
            Id = msg.GetUInt32();
            Text = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte(DoorId);
            msg.AddUInt32(Id);
            msg.AddString(Text);
        }
    }
}