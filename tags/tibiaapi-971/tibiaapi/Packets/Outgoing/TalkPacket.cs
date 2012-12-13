using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class TalkPacket : OutgoingPacket
    {

        public SpeechType SpeechType { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public ChatChannel ChannelId { get; set; }

        public TalkPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.Talk;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.Talk)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.Talk;

            byte Mode = msg.GetByte();
            var speechType = Constants.Enums.GetSpeechTypeInfo(Client.VersionNumber, Mode);

            switch (speechType.SpeechType)
            {
                case SpeechType.PrivateTo:
                case SpeechType.GamemasterPrivateTo:
                    Receiver = msg.GetString();
                    break;
                case SpeechType.Channel:
                case SpeechType.ChannelHighlight:
                case SpeechType.ChannelManagement:
                case SpeechType.GamemasterChannel:
                    msg.GetUInt16(); //channelid
                    break;
                default:
                    break;
            }

            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            SpeechTypeInfo info = Enums.GetSpeechTypeInfo(971, SpeechType);

            msg.AddByte(info.Value);

            switch (SpeechType)
            {
                case SpeechType.PrivateTo:
                case SpeechType.GamemasterPrivateTo:
                    msg.AddString(Receiver);
                    break;
                case SpeechType.Channel:
                case SpeechType.ChannelHighlight:
                case SpeechType.ChannelManagement:
                case SpeechType.GamemasterChannel:
                    msg.AddUInt16((ushort)ChannelId);
                    break;
                default:
                    break;
            }

            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, SpeechType type, string receiver, string message, ChatChannel channel)
        {
            return new TalkPacket(client) { SpeechType = type, Receiver = receiver, Message = message, ChannelId = channel }.Send();
        }


    }
}