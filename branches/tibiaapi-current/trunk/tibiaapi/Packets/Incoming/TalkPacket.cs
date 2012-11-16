using Tibia.Constants;

namespace Tibia.Packets.Incoming
{
    public class TalkPacket : IncomingPacket
    {
        public SpeechType SpeechType { get; set; }
        public uint StatementId { get; set; }
        public ChatChannel ChannelId { get; set; }
        public string SenderName { get; set; }
        public ushort SenderLevel { get; set; }
        public string Message { get; set; }
        public Objects.Location Position { get; set; }
        public uint Time { get; set; }

        public TalkPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.Talk;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            int position = msg.Position;

            if (msg.GetByte() != (byte)IncomingPacketType.Talk)
                return false;

            Destination = destination;
            Type = IncomingPacketType.Talk;

            StatementId = msg.GetUInt32();
            SenderName = msg.GetString();
            SenderLevel = msg.GetUInt16();


            byte Mode = msg.GetByte();

            SpeechTypeInfo info = Enums.GetSpeechTypeInfo(971, Mode);
            SpeechType = info.SpeechType;


            if (info.AdditionalSpeechData == AdditionalSpeechData.Location)
                Position = msg.GetLocation();
            else if (info.AdditionalSpeechData == AdditionalSpeechData.ChannelId)
                ChannelId = (Tibia.Constants.ChatChannel)msg.GetUInt16();

            Message = msg.GetString();

            return true;
        }

        /// <summary>
        /// Send a channel message.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="senderName"></param>
        /// <param name="senderLevel"></param>
        /// <param name="speechType"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static bool Send(Objects.Client client, string senderName, ushort senderLevel, string message, SpeechType speechType, ChatChannel channelId)
        {
            return Send(client, senderName, senderLevel, message, speechType, channelId, Objects.Location.Invalid, 0);
        }

        /// <summary>
        /// Send a private or normal message.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="senderName"></param>
        /// <param name="senderLevel"></param>
        /// <param name="speechType"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool Send(Objects.Client client, string senderName, ushort senderLevel, string message, SpeechType speechType, Objects.Location position)
        {
            return Send(client, senderName, senderLevel, message, speechType, ChatChannel.None, position, 0);
        }

        /// <summary>
        /// Send a rule violation message.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="senderName"></param>
        /// <param name="senderLevel"></param>
        /// <param name="speechType"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool Send(Objects.Client client, string senderName, ushort senderLevel, string message, SpeechType speechType, uint time)
        {
            return Send(client, senderName, senderLevel, message, speechType, ChatChannel.None, Objects.Location.Invalid, time);
        }

        /// <summary>
        /// Send a generic message.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="senderName"></param>
        /// <param name="senderLevel"></param>
        /// <param name="speechType"></param>
        /// <param name="channelId"></param>
        /// <param name="position"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool Send(Objects.Client client, string senderName, ushort senderLevel, string message, SpeechType speechType, ChatChannel channelId, Objects.Location position, uint time)
        {
            TalkPacket p = new TalkPacket(client);
            p.SenderName = senderName;
            p.SenderLevel = senderLevel;
            p.Message = message;
            p.SpeechType = speechType;
            p.ChannelId = channelId;
            p.Position = position;
            p.Time = time;
            return p.Send();
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);

            msg.AddUInt32(StatementId);
            msg.AddString(SenderName);
            msg.AddUInt16(SenderLevel);

            SpeechTypeInfo info = Enums.GetSpeechTypeInfo(Client.VersionNumber, SpeechType);

            msg.AddByte(info.Value);

            switch (info.AdditionalSpeechData)
            {
                case AdditionalSpeechData.Location:
                    msg.AddLocation(Position);
                    break;
                case AdditionalSpeechData.ChannelId:
                    msg.AddUInt16((ushort)ChannelId);
                    break;
                case AdditionalSpeechData.Time:
                    msg.AddUInt32(Time);
                    break;
                default:
                    break;
            }

            msg.AddString(Message);
        }
    }
}