using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class ExcludeFromChannelPacket : OutgoingPacket
    {
        public string Name { get; set; }

        public ExcludeFromChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.ExcludeFromChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.ExcludeFromChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.ExcludeFromChannel;

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
            ExcludeFromChannelPacket p = new ExcludeFromChannelPacket(client);
            p.Name = name;
            return p.Send();
        }
    }
}