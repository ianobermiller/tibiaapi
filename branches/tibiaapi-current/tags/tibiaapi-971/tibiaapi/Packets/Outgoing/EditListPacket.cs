using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class EditListPacket : OutgoingPacket
    {
        public byte DoorId { get; set; }
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

        public static bool Send(Objects.Client client, byte doorId, uint id, string text)
        {
            return new EditListPacket(client) { DoorId = doorId, Id = id, Text = text }.Send();
        }
    }
}