using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class InviteToChannelPacket : OutgoingPacket
    {
        public string Name { get; set; }

        public InviteToChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.InviteToChannel;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.InviteToChannel)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.InviteToChannel;

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
            AddBuddyPacket p = new AddBuddyPacket(client);
            p.Name = name;
            return p.Send();
        }
    }
}