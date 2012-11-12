using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class ShowModalDialogPacket : IncomingPacket
    {
        public ShowModalDialogPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.ShowModalDialog;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.ShowModalDialog)
                return false;

            Destination = destination;
            Type = IncomingPacketType.ShowModalDialog;

            msg.GetUInt32();
            msg.GetString();//title
            msg.GetString();//message

            byte Count = msg.GetByte();
            while (Count > 0)
            {
                msg.GetString();
                msg.GetByte();
            }

            msg.GetByte();
            msg.GetByte();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }
    }
}