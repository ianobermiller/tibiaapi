using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class InviteToOwnChannelPacket : OutgoingPacket
    {
        public string Name { get; set; }

        public InviteToOwnChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.InviteToOwnChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.InviteToOwnChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.InviteToOwnChannel;

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
            return new InviteToOwnChannelPacket(client) { Name = name }.Send();
        }
    }
}