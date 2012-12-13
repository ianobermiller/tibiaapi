using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class AddVipPacket : OutgoingPacket
    {
        public string Name { get; set; }

        public AddVipPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.AddVip;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.AddVip)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.AddVip;

            Name = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Name);
        }

        public static bool Send(Objects.Client client, string name)
        {
            return new AddVipPacket(client) { Name = name }.Send();
        }
    }
}