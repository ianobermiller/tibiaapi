using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class BuddyDataPacket : IncomingPacket
    {

        public uint Id { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public string Description { get; set; }
        public uint Icon { get; set; }
        public bool NotifyAtLogin { get; set; }

        public BuddyDataPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.BuddyData;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.BuddyData)
                return false;

            Destination = destination;
            Type = IncomingPacketType.BuddyData;

            Id = msg.GetUInt32();
            Name = msg.GetString();

            if (Client.VersionNumber >= 9620)
            {
                Description = msg.GetString();
                Icon = msg.GetUInt32();
                NotifyAtLogin = msg.GetByte().Equals(0x01);
            }

            IsOnline = msg.GetByte().Equals(0x01);

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(Id);
            msg.AddString(Name);

            if (Client.VersionNumber >= 9620)
            {
                msg.AddString(Description);
                msg.AddUInt32(Icon);
                msg.AddByte(System.Convert.ToByte(NotifyAtLogin));
            }

            msg.AddByte(System.Convert.ToByte(IsOnline));
        }

        public static bool Send(Objects.Client client, uint playerId, string playerName, string description, uint icon, bool notifyAtLogin, bool isOnline)
        {
            BuddyDataPacket p = new BuddyDataPacket(client);
            p.Id = playerId;
            p.Name = playerName;
            p.Description = description;
            p.Icon = icon;
            p.NotifyAtLogin = notifyAtLogin;
            p.IsOnline = isOnline;
            return p.Send();
        }
    }
}