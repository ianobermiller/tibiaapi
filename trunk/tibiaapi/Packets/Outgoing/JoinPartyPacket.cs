using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class JoinPartyPacket : OutgoingPacket
    {
        public uint CreatureId { get; set; }

        public JoinPartyPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.JoinParty;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.JoinParty)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.JoinParty;

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
            return new JoinPartyPacket(client) { CreatureId = creatureId }.Send();
        }
    }
}