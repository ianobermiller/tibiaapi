using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class OpenOwnChannelPacket : IncomingPacket
    {

        public string Name { get; set; }
        public ushort ChannelId { get; set; }

        public OpenOwnChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.OpenOwnChannel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.OpenOwnChannel)
                return false;

            Destination = destination;
            Type = IncomingPacketType.OpenOwnChannel;

            ChannelId = msg.GetUInt16();
            Name = msg.GetString();

            //ushort JoinedCount = msg.GetUInt16();
            //for (int i = 0; i < JoinedCount; i++)
            //    msg.GetString(); //player names

            //ushort InvitedCount = msg.GetUInt16();
            //for (int x = 0; x < InvitedCount; x++)
            //    msg.GetString(); //player names

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16(ChannelId);
            msg.AddString(Name);
        }
    }
}