using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class PrivateChannelPacket : OutgoingPacket
    {
        public string Receiver { get; set; }

        public PrivateChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.PrivateChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.PrivateChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.PrivateChannel;
            Receiver = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddString(Receiver);
        }

        public static bool Send(Objects.Client client, string receiver)
        {
            PrivateChannelPacket p = new PrivateChannelPacket(client);
            p.Receiver = receiver;
            return p.Send();
        }
    }
}