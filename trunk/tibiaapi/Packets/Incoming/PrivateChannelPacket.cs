using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PrivateChannelPacket : IncomingPacket
    {
        public string Name { get; set; }

        public PrivateChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PrivateChannel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PrivateChannel)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PrivateChannel;

            Name = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Name);
        }
    }
}