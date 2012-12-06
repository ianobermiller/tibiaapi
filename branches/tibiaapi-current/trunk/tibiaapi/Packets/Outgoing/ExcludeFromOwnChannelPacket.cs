using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ExcludeFromOwnChannelPacket : OutgoingPacket
    {
        public string Name { get; set; }

        public ExcludeFromOwnChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ExcludeFromOwnChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ExcludeFromOwnChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ExcludeFromOwnChannel;

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
            return new ExcludeFromOwnChannelPacket(client) { Name = name }.Send();
        }
    }
}