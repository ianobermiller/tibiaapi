using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TutorialHintPacket : IncomingPacket
    {
        public TutorialHintPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.TutorialHint;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.TutorialHint)
                return false;

            Destination = destination;
            Type = IncomingPacketType.TutorialHint;

            msg.GetByte();

            return true;
        }
    }
}