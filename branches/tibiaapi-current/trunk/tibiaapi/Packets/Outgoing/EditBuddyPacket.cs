using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class EditBuddyPacket : OutgoingPacket
    {
        public uint Id { get; set; }
        public string Description { get; set; }
        public uint Icon { get; set; }
        public bool Notify { get; set; }

        public EditBuddyPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.EditBuddy;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.EditBuddy)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.EditBuddy;

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
            EditBuddyPacket p = new EditBuddyPacket(client);
            p.Id = id;
            p.Description = description;
            p.Icon = icon;
            p.Notify = notify;
            return p.Send();
        }
    }
}