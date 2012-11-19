using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class AutomapFlagPacket : IncomingPacket
    {

        public Objects.Location Location { get; set; }
        public byte Mark { get; set; }
        public string Description { get; set; }

        public AutomapFlagPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.AutomapFlag;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.AutomapFlag)
                return false;

            Destination = destination;
            Type = IncomingPacketType.AutomapFlag;

            Location = msg.GetLocation();
            Mark = msg.GetByte();
            Description = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddLocation(Location);
            msg.AddByte(Mark);
            msg.AddString(Description);
        }

    }
}