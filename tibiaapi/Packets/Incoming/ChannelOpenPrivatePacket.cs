using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ChannelOpenPrivatePacket : IncomingPacket
    {
        public string Name { get; set; }

        public ChannelOpenPrivatePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ChannelOpenPrivate;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ChannelOpenPrivate)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ChannelOpenPrivate;

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
            ChannelOpenPrivatePacket p = new ChannelOpenPrivatePacket(client);
            p.Name = name;
            return p.Send();
        }
    }
}