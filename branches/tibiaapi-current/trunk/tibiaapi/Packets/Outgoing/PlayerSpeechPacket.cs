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

            SpeechTypeInfo info = Enums.GetSpeechTypeInfo(Client.VersionNumber, msg.GetByte());
            SpeechType = info.SpeechType;

            if (SpeechType == SpeechType.Private)
            {
                Receiver = msg.GetString();
            }
            else if (info.AdditionalSpeechData == AdditionalSpeechData.ChannelId)
            {
                ChannelId = (ChatChannel)msg.GetUInt16();
            }

            Message = msg.GetString();

            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            SpeechTypeInfo info = Enums.GetSpeechTypeInfo(Client.VersionNumber, SpeechType);

            msg.AddByte(info.Value);

            if (SpeechType == SpeechType.Private)
            {
                msg.AddString(Receiver);
            }
            else if (info.AdditionalSpeechData == AdditionalSpeechData.ChannelId)
            {
                msg.AddUInt16((ushort)ChannelId);
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