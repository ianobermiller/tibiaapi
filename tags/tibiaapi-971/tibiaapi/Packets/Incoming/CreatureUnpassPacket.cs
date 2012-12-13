using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class CreatureUnpassPacket : IncomingPacket
    {
        public uint CreatureId { get; set; }
        public bool IsBlocking { get; set; }
        public CreatureUnpassPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureUnpass;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.CreatureUnpass)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureUnpass;

            CreatureId = msg.GetUInt32();
            IsBlocking = System.Convert.ToBoolean(msg.GetByte());

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt32(CreatureId);
            msg.AddByte(System.Convert.ToByte(IsBlocking));
        }
    }
}