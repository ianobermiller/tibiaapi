using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class RevokeInvitationPacket : OutgoingPacket
    {
        public uint CreatureId { get; set; }

        public RevokeInvitationPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.RevokeInvitation;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.RevokeInvitation)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.RevokeInvitation;

            CreatureId = msg.GetUInt32();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
        }

        public static bool Send(Objects.Client client, uint creatureId)
        {
            return new RevokeInvitationPacket(client) { CreatureId = creatureId }.Send();
        }
    }
}