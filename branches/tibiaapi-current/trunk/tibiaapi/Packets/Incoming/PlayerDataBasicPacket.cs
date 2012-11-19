using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class PlayerDataBasicPacket : IncomingPacket
    {
        public PlayerDataBasicPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.PlayerDataBasic;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.PlayerDataBasic)
                return false;

            Destination = destination;
            Type = IncomingPacketType.PlayerDataBasic;

            msg.GetByte();//ispremium
            msg.GetByte();//profession

            ushort KnownSpellCount = msg.GetUInt16();
            for (int i = 0; i < KnownSpellCount; i++)
                msg.GetByte();

            return true;
        }
    }
}