using Tibia.Constants;

namespace Tibia.Packets.Outgoing
{
    public class PlayerSpeechPacket : OutgoingPacket
    {

        public SpeechType SpeechType { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public ChatChannel ChannelId { get; set; }

        public PlayerSpeechPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType.PlayerSpeech;
            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType.PlayerSpeech)
                return false;

            Destination = destination;
            Type = OutgoingPacketType.PlayerSpeech;

            SpeechType = (SpeechType)msg.GetByte();

            switch (SpeechType)
            {
                case SpeechType.Private:
                    Receiver = msg.GetString();
                    break;
                case SpeechType.ChannelYellow:
                case SpeechType.ChannelRed:
                case SpeechType.ChannelWhite:
                case SpeechType.ChannelOrange:
                    ChannelId = (ChatChannel)msg.GetUInt16();
                    break;
                default:
                    break;
            }

            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(ref NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
            msg.AddByte((byte)SpeechType);

            switch (SpeechType)
            {
                case SpeechType.Private:
                    msg.AddString(Receiver);
                    break;
                case SpeechType.ChannelYellow:
                case SpeechType.ChannelRed:
                case SpeechType.ChannelWhite:
                    msg.AddUInt16((ushort)ChannelId);
                    break;
                default:
                    break;
            }

            msg.AddString(Message);
        }

        public static bool Send(Objects.Client client, SpeechType type, string receiver, string message, ChatChannel channel)
        {
            PlayerSpeechPacket p = new PlayerSpeechPacket(client);

            p.SpeechType = type;
            p.Receiver = receiver;
            p.Message = message;
            p.ChannelId = channel;

            return p.Send();
        }


    }
}