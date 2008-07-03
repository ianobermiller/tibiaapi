using System;
using Tibia.Objects;

namespace Tibia.Packets
{
    public class ChatMessagePacket : Packet
    {
        private ChatType messageType;
        private ChatChannel channel;
        private string senderName;
        private int senderLevel;
        private string message;
        private Tibia.Objects.Location location;

        public ChatType MessageType
        {
            get { return messageType; }
        }
        public ChatChannel Channel
        {
            get { return channel; }
        }
        public string SenderName
        {
            get { return senderName; }
        }

        public int SenderLevel
        {
            get { return senderLevel; }
        }
        public Objects.Location Location 
        {
            get { return location; }
        }
        public string Message
        {
            get { return message; }
        }
        public ChatMessagePacket(Client c) : base(c)
        {
            type = PacketType.ChatMessage;
            destination = PacketDestination.Client;
        }

        public ChatMessagePacket(Client c, byte[] data):this(c)
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ChatMessage) return false;
                PacketBuilder p = new PacketBuilder(client, packet, 3);

                p.Skip(4); // four 0's, reserved for future I guess
                senderName = p.GetString();
                senderLevel = p.GetInt();
                messageType = (ChatType)p.GetByte();

                switch (messageType)
                {
                    case ChatType.Normal:
                    case ChatType.Whisper:
                    case ChatType.Yell:
                    case ChatType.Monster:
                    case ChatType.MonsterYell:
                        location = p.GetLocation();
                        break;
                    case ChatType.ChannelNPC:
                    case ChatType.ChannelNormal:
                    case ChatType.ChannelTutor:
                    case ChatType.ChannelGM:
                    case ChatType.ChannelRedAnonymous:
                        channel = (ChatChannel)p.GetInt();
                        break;
                    default:
                        break;
                }
                message = p.GetString();
                index = p.Index;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Send a simple chat message with no level.
        /// Only valid ChatTypes are PrivateMessage and Broadcast.
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ChatMessagePacket Create(Client c, ChatType messageType, string senderName, string message)
        {
            return Create(c, messageType, senderName, message, 0);
        }

        /// <summary>
        /// Send a simple chat message with the specified level.
        /// Only valid ChatTypes are PrivateMessage and Broadcast.
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static ChatMessagePacket Create(Client c, ChatType messageType, string senderName, string message, int level)
        {
            return Create(c, messageType, senderName, message, level, Objects.Location.GetInvalid(), ChatChannel.None);
        }

        /// <summary>
        /// Send a channel message without level.
        /// Valid ChatTypes are ChannelNormal, ChannelTutor, ChannelGM, and ChannelRedAnonymous
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <param name="chan"></param>
        /// <returns></returns>
        public static ChatMessagePacket Create(Client c, ChatType messageType, string senderName, string message, ChatChannel chan)
        {
            return Create(c, messageType, senderName, message, 0, chan);
        }

        /// <summary>
        /// Send a channel message with the specified level.
        /// Valid ChatTypes are ChannelNormal, ChannelTutor, ChannelGM, and ChannelRedAnonymous
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <param name="chan"></param>
        /// <returns></returns>
        public static ChatMessagePacket Create(Client c, ChatType messageType, string senderName, string message, int level, ChatChannel chan)
        {
            return Create(c, messageType, senderName, message, level, Objects.Location.GetInvalid(), chan);
        }

        /// <summary>
        /// Send a normal speech message with no level.
        /// Valid ChatTypes are Normal, Whisper, Yell, Monster, and MonsterYell.
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <param name="loc"></param>
        /// <returns></returns>
        public static ChatMessagePacket Create(Client c, ChatType messageType, string senderName, string message, Objects.Location loc)
        {
            return Create(c, messageType, senderName, message, 0, loc);
        }

        /// <summary>
        /// Send a normal speech message with the specified level.
        /// Valid ChatTypes are Normal, Whisper, Yell, Monster, and MonsterYell.
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <param name="loc"></param>
        /// <returns></returns>
        public static ChatMessagePacket Create(Client c, ChatType messageType, string senderName, string message, int level, Objects.Location loc)
        {
            return Create(c, messageType, senderName, message, level, Objects.Location.GetInvalid(), ChatChannel.None);
        }

        /// <summary>
        /// Private because the various wrapper above should be used instead.
        /// Throws an ArgumentException if the arguments do not match.
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <param name="loc"></param>
        /// <param name="chan"></param>
        /// <returns></returns>
        private static ChatMessagePacket Create(Client c, ChatType messageType, string senderName, string message, int level, Objects.Location loc, ChatChannel chan)
        {
            if (level < 0) throw new ArgumentOutOfRangeException("level", "Level must be non-negative.");
            if (message.Length < 1) throw new ArgumentException("Message length must be at least 1.", "message");

            PacketBuilder p = new PacketBuilder(c, PacketType.ChatMessage);
            p.AddLong(0);
            p.AddString(senderName);
            p.AddInt(level);
            p.AddByte((byte)messageType);

            switch (messageType)
            {
                case ChatType.Normal:
                case ChatType.Whisper:
                case ChatType.Yell:
                case ChatType.Monster:
                case ChatType.MonsterYell:
                    if (!loc.IsValid()) throw new ArgumentException("You must supply a valid location for this message type.", "loc");
                    p.AddLocation(loc);
                    break;
                case ChatType.ChannelNPC:
                case ChatType.ChannelNormal:
                case ChatType.ChannelTutor:
                case ChatType.ChannelGM:
                case ChatType.ChannelRedAnonymous:
                    if (chan == ChatChannel.None) throw new ArgumentException("You must supply a valid chat channel for this message type.", "chan");
                    p.AddInt((int)chan);
                    break;
                default:
                    break;
            }

            p.AddString(message);

            return new ChatMessagePacket(c, p.GetPacket());
        }
    }
}
