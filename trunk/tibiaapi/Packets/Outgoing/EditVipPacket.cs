using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class EditVipPacket : OutgoingPacket
    {
        public uint Id { get; set; }
        public string Description { get; set; }
        public uint Icon { get; set; }
        public bool Notify { get; set; }

        public EditVipPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.EditVip;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.EditVip)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.EditVip;

            Id = msg.GetUInt32();
            Description = msg.GetString();
            Icon = msg.GetUInt32();
            Notify = msg.GetByte().Equals(0x01);

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(Id);
            msg.AddString(Description);
            msg.AddUInt32(Icon);
            msg.AddByte(System.Convert.ToByte(Notify));
        }

        public static bool Send(Objects.Client client, uint id, string description, uint icon, bool notify)
        {
            return new EditVipPacket(client) { Id = id, Description = description, Icon = icon, Notify = notify }.Send();
        }
    }
}