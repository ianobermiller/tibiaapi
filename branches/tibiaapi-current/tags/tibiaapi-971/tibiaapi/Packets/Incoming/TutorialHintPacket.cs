using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TutorialHintPacket : IncomingPacket
    {
        public uint TutorialId { get; set; }
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

            TutorialId = msg.GetByte();

            return true;
        }
        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(TutorialId);
        }
    }
}