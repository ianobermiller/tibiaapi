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

            switch (Mode)
            {
                case 0x05:
                case 0x0F:
                    Receiver = msg.GetString();
                    break;
                case 0x07:
                case 0x0D:
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

            if (info.AdditionalSpeechData == AdditionalSpeechData.Receiver)
                msg.AddString(Receiver);
            else if (info.AdditionalSpeechData == AdditionalSpeechData.ChannelId)
                msg.AddUInt16((ushort)ChannelId);

            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, SpeechType type, string receiver, string message, ChatChannel channel)
        {
            TalkPacket p = new TalkPacket(client);

            p.SpeechType = type;
            p.Receiver = receiver;
            p.Message = message;
            p.ChannelId = channel;

            return p.Send();
        }


    }
}