using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class InitGamePacket : IncomingPacket
    {
        public uint YourId { get; set; }
        public ushort BeatDuration { get; set; }
        public byte CanReportBug { get; set; }

        public InitGamePacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.InitGame;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.InitGame)
                return false;

            Destination = destination;
            Type = IncomingPacketType.InitGame;

            YourId = msg.GetUInt32();
            BeatDuration = msg.GetUInt16(); // Related to client-side drawing speed
            CanReportBug = msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(YourId);
            msg.AddUInt16(0x0032); // Related to client-side drawing speed
            msg.AddByte(CanReportBug);
        }
    }
}