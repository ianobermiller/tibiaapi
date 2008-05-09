using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class ChatMessagePacket : Packet
    {
        private ChatType messageType;
        private ChatChannel channel;
        private short lenSenderName;
        private string senderName;
        private byte senderLevel;
        private short lenMessage;
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

        public byte SenderLevel
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
        public ChatMessagePacket()
        {
            type = PacketType.ChatMessage;
            destination = PacketDestination.Client;
        }

        public ChatMessagePacket(byte[] data):this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.ChatMessage) return false;
                int index = 7;
                lenSenderName = BitConverter.ToInt16(packet, index);
                index += 2;
                senderName = Encoding.GetEncoding("iso-8859-1").GetString(packet, index, lenSenderName);
                index += lenSenderName;
                senderLevel = packet[index];
                index += 2;
                messageType = (ChatType)packet[index];
                switch (messageType)
                {
                    case ChatType.Normal:
                    case ChatType.Whisper:
                    case ChatType.Yell:
                    case ChatType.Monster:
                    case ChatType.MonsterYell:
                        index += 1;
                        location = new Objects.Location();
                        location.X = BitConverter.ToInt16(packet, index);
                        index += 2;
                        location.Y = BitConverter.ToInt16(packet, index);
                        index += 2;
                        location.Z = packet[index];
                        index += 1;
                        lenMessage = packet[index];
                        index += 2;
                        message = Encoding.GetEncoding("iso-8859-1").GetString(packet, index, lenMessage);
                        break;
                
                    case ChatType.ChannelNormal:
                    case ChatType.ChannelTutor:
                    case ChatType.ChannelGM:
                        channel = (ChatChannel)BitConverter.ToInt16(packet,index);
                        index += 3;
                        lenMessage = packet[index];
                        index += 2;
                        message = Encoding.GetEncoding("iso-8859-1").GetString(packet, index, lenMessage);
                        break;

                    default:
                        index += 1;
                        lenMessage = packet[index];
                        index += 2;
                        message = Encoding.GetEncoding("iso-8859-1").GetString(packet, index, lenMessage);
                        break;
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ChatMessagePacket Create(ChatType messageType, string senderName, string message)
        {
            return Create(messageType, senderName, message, 0);
        }

        public static ChatMessagePacket Create(ChatType messageType, string senderName, string message, int level)
        {
            return Create(messageType, senderName, message, level, Objects.Location.GetInvalid(), ChatChannel.None);
        }

        public static ChatMessagePacket Create(ChatType messageType, string senderName, string message, ChatChannel chan)
        {
            return Create(messageType, senderName, message, 0, chan);
        }

        public static ChatMessagePacket Create(ChatType messageType, string senderName, string message, int level, ChatChannel chan)
        {
            return Create(messageType, senderName, message, level, Objects.Location.GetInvalid(), chan);
        }

        public static ChatMessagePacket Create(ChatType messageType, string senderName, string message, Objects.Location loc)
        {
            return Create(messageType, senderName, message, 0, loc);
        }

        public static ChatMessagePacket Create(ChatType messageType, string senderName, string message, int level, Objects.Location loc)
        {
            return Create(messageType, senderName, message, level, Objects.Location.GetInvalid(), ChatChannel.None);
        }

        public static ChatMessagePacket Create(ChatType messageType, string senderName, string message, int level, Objects.Location loc, ChatChannel chan)
        {
            try
            {
                if (level < 0) throw new ArgumentOutOfRangeException("level", "Level must be non-negative.");
                if (senderName.Length < 1) throw new ArgumentException("Sender name length but be at least 1.", "senderName");
                if (message.Length < 1) throw new ArgumentException("Message length must be at least 1.", "message");

                byte[] packet;
                switch (messageType)
                {
                    case ChatType.Normal:
                    case ChatType.Whisper:
                    case ChatType.Yell:
                    case ChatType.Monster:
                    case ChatType.MonsterYell:

                        if (!loc.IsValid()) throw new ArgumentException("You must supply a valid location for this message type.", "loc");

                        // Initialize packet
                        packet = new byte[message.Length + senderName.Length + 19];

                        // Packet length
                        Array.Copy(BitConverter.GetBytes((short)(senderName.Length + message.Length + 17)), packet, 2);

                        // Comment elements handled after the switch

                        // Location
                        Array.Copy(loc.ToBytes(), 0, packet, 9 + senderName.Length + 3, 5);

                        // Message length
                        Array.Copy(BitConverter.GetBytes((short)(message.Length)), 0, packet, 9 + senderName.Length + 8, 2);

                        // Message
                        Array.Copy(Encoding.GetEncoding("iso-8859-1").GetBytes(message), 0, packet, 9 + senderName.Length + 10, message.Length);
                        break;

                    case ChatType.ChannelNormal:
                    case ChatType.ChannelTutor:
                    case ChatType.ChannelGM:

                        if (chan == ChatChannel.None) throw new ArgumentException("You must supply a valid chat channel for this message type.", "chan");

                        // Initialize packet
                        packet = new byte[message.Length + senderName.Length + 16];

                        // Packet length
                        Array.Copy(BitConverter.GetBytes((short)(senderName.Length + message.Length + 14)), packet, 2);

                        // Comment elements handled after the switch

                        // Channel
                        Array.Copy(BitConverter.GetBytes((short)(chan)), 0, packet, 9 + senderName.Length + 3, 2);

                        // Message length
                        Array.Copy(BitConverter.GetBytes((short)(message.Length)), 0, packet, 9 + senderName.Length + 5, 2);

                        // Message
                        Array.Copy(Encoding.GetEncoding("iso-8859-1").GetBytes(message), 0, packet, 9 + senderName.Length + 7, message.Length);
                        break;

                    default:
                        // Initialize packet
                        packet = new byte[message.Length + senderName.Length + 14];

                        // Packet length
                        Array.Copy(BitConverter.GetBytes((short)(senderName.Length + message.Length + 12)), packet, 2);

                        // Comment elements handled after the switch

                        // Message length
                        Array.Copy(BitConverter.GetBytes((short)(message.Length)), 0, packet, 9 + senderName.Length + 3, 2);

                        // Message
                        Array.Copy(Encoding.GetEncoding("iso-8859-1").GetBytes(message), 0, packet, 9 + senderName.Length + 5, message.Length);
                        break;
                }

                // Packet type
                packet[2] = (byte)PacketType.ChatMessage;

                // Sender length
                Array.Copy(BitConverter.GetBytes((short)(senderName.Length)), 0, packet, 7, 2);

                // Sender name
                Array.Copy(Encoding.GetEncoding("iso-8859-1").GetBytes(senderName), 0, packet, 9, senderName.Length);

                // Sender level
                packet[9 + senderName.Length] = (byte)level;

                // Message type
                packet[9 + senderName.Length + 2] = (byte)messageType;

                ChatMessagePacket cmp = new ChatMessagePacket(packet);
                return cmp;
            }
            catch (Exception e)
            {
                Exceptions.Handler.Handle(e);
                return null;
            }
        }
    }
}
