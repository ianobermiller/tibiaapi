using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class LeavePartyPacket : OutgoingPacket
    {
        public LeavePartyPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.LeaveParty;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.LeaveParty)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.LeaveParty;

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client)
        {
            LeavePartyPacket p = new LeavePartyPacket(client);
            return p.Send();
        }
    }
}