using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class InviteToPartyPacket : OutgoingPacket
    {
        public uint CreatureId { get; set; }

        public InviteToPartyPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.InviteToParty;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.InviteToParty)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.InviteToParty;

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
            InviteToPartyPacket p = new InviteToPartyPacket(client);
            p.CreatureId = creatureId;
            return p.Send();
        }
    }
}