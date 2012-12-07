using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreaturePartyPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public PartyShield PartyShield { get; set; }

        public CreaturePartyPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureParty;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureParty)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureParty;

            CreatureId = msg.GetUInt32();
            PartyShield = (PartyShield)msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
            msg.AddByte((byte)PartyShield);
        }
    }
}