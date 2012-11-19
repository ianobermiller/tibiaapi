using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class OpenChannelPacket : IncomingPacket
    {
        public ChatChannel ChannelId { get; set; }
        public string ChannelName { get; set; }
        public ushort NumberOfParticipants { get; set; }
        public string[] Participants { get; set; }
        public ushort NumberOfInvitees { get; set; }
        public string[] Invitees { get; set; }

        public OpenChannelPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.OpenChannel;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.OpenChannel)
                return false;

            Destination = destination;
            Type = IncomingPacketType.OpenChannel;

            ChannelId = (ChatChannel)msg.GetUInt16();
            ChannelName = msg.GetString();

            if (Client.VersionNumber >= 872)
            {
                NumberOfParticipants = msg.GetUInt16();
                for (ushort p = 0; p < NumberOfParticipants; p++)
                {
                    Participants[p] = msg.GetString();
                }
                NumberOfInvitees = msg.GetUInt16();
                for (ushort i = 0; i < NumberOfInvitees; i++)
                {
                    Invitees[i] = msg.GetString();
                }
            }

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddUInt16((ushort)ChannelId);
            msg.AddString(ChannelName);

            if (Client.VersionNumber >= 872)
            {
                msg.AddUInt16(NumberOfParticipants);
                for (ushort p = 0; p < NumberOfParticipants; p++)
                {
                    msg.AddString(Participants[p]);
                }
                msg.AddUInt16(NumberOfInvitees);
                for (ushort i = 0; i < NumberOfInvitees; i++)
                {
                    msg.AddString(Invitees[i]);
                }
            }
        }
    }
}