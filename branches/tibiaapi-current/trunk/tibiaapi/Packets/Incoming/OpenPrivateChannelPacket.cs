using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class OpenPrivateChannelPacket : IncomingPacket
    {
        public string Name { get; set; }

        public OpenPrivateChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.OpenPrivateChannel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.OpenPrivateChannel)
                return false;

            Destination = destination;
            Type = IncomingPacketType.OpenPrivateChannel;

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