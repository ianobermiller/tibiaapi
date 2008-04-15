using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class ChatMessagePacket : Packet
    {
        private ChatMessageType messageType;
        private ChatChannel channel;
        private short lenFrom;
        private string from;
        private byte fromLvl;
        private short lenMessage;
        private string message;
        private Tibia.Objects.Location location;

        public ChatMessageType MessageType
        {
            get { return messageType; }
        }
        public ChatChannel Channel
        {
            get { return channel; }
        }
        public string From
        {
            get { return from; }
        }

        public byte FromLevel
        {
            get { return fromLvl; }
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
                lenFrom = BitConverter.ToInt16(packet, index);
                index += 2;
                from = Encoding.ASCII.GetString(packet, index, lenFrom);
                index += lenFrom;
                fromLvl = packet[index];
                index += 2;
                messageType = (ChatMessageType)packet[index];
                if (messageType == ChatMessageType.Normal || messageType == ChatMessageType.Whisper || messageType == ChatMessageType.Yell || messageType == ChatMessageType.Monster || messageType == ChatMessageType.MonsterYell)
                {
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
                    message = Encoding.ASCII.GetString(packet, index, lenMessage);
                }
                else if (messageType == ChatMessageType.ChannelNormal || messageType == ChatMessageType.ChannelTutor || messageType == ChatMessageType.ChannelGM)
                {
                    channel = (ChatChannel)BitConverter.ToInt16(packet,index);
                    index += 3;
                    lenMessage = packet[index];
                    index += 2;
                    message = Encoding.ASCII.GetString(packet, index, lenMessage);
                }
                else
                {
                    index += 1;
                    lenMessage = packet[index];
                    index += 2;
                    message = Encoding.ASCII.GetString(packet, index, lenMessage);
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ChatMessagePacket Create(ChatMessageType messagetype, string from, int level, string message, Objects.Location loc, ChatChannel chan)
        {
            byte[] packet;
            if (messagetype == ChatMessageType.Normal || messagetype == ChatMessageType.Whisper || messagetype == ChatMessageType.Yell || messagetype == ChatMessageType.Monster || messagetype == ChatMessageType.MonsterYell)
            {
                packet = new byte[message.Length + from.Length + 19];
                Array.Copy(BitConverter.GetBytes((short)(from.Length + message.Length + 17)), packet, 2);
                packet[2] = (byte)PacketType.ChatMessage;
                Array.Copy(BitConverter.GetBytes((short)(from.Length)), 0, packet, 7, 2);
                Array.Copy(Encoding.ASCII.GetBytes(from), 0, packet, 9, from.Length);
                packet[9 + from.Length] = (byte)level;
                packet[9 + from.Length + 2] = (byte)messagetype;
                Array.Copy(BitConverter.GetBytes((short)(loc.X)), 0, packet, 9 + from.Length + 3, 2);
                Array.Copy(BitConverter.GetBytes((short)(loc.Y)), 0, packet, 9 + from.Length + 5, 2);
                packet[9 + from.Length + 7] = (byte)loc.Z;
                Array.Copy(BitConverter.GetBytes((short)(message.Length)), 0, packet, 9 + from.Length + 8, 2);
                Array.Copy(Encoding.ASCII.GetBytes(message), 0, packet, 9 + from.Length + 10, message.Length);
            }
            else if (messagetype == ChatMessageType.ChannelNormal || messagetype == ChatMessageType.ChannelTutor || messagetype == ChatMessageType.ChannelGM)
            {
                packet = new byte[message.Length + from.Length + 16];
                Array.Copy(BitConverter.GetBytes((short)(from.Length + message.Length + 14)), packet, 2);
                packet[2] = (byte)PacketType.ChatMessage;
                Array.Copy(BitConverter.GetBytes((short)(from.Length)), 0, packet, 7, 2);
                Array.Copy(Encoding.ASCII.GetBytes(from), 0, packet, 9, from.Length);
                packet[9 + from.Length] = (byte)level;
                packet[9 + from.Length + 2] = (byte)messagetype;
                Array.Copy(BitConverter.GetBytes((short)(chan)),0,packet,9+from.Length+3,2);
                Array.Copy(BitConverter.GetBytes((short)(message.Length)), 0, packet, 9 + from.Length + 5, 2);
                Array.Copy(Encoding.ASCII.GetBytes(message), 0, packet, 9 + from.Length + 7, message.Length);
            }
            else
            {
                packet = new byte[message.Length + from.Length + 14];
                Array.Copy(BitConverter.GetBytes((short)(from.Length + message.Length + 12)), packet, 2);
                packet[2] = (byte)PacketType.ChatMessage;
                Array.Copy(BitConverter.GetBytes((short)(from.Length)), 0, packet, 7, 2);
                Array.Copy(Encoding.ASCII.GetBytes(from), 0, packet, 9, from.Length);
                packet[9 + from.Length] = (byte)level;
                packet[9 + from.Length + 2] = (byte)messagetype;
                Array.Copy(BitConverter.GetBytes((short)(message.Length)), 0, packet, 9 + from.Length + 3, 2);
                Array.Copy(Encoding.ASCII.GetBytes(message), 0, packet, 9 + from.Length + 5, message.Length);
            }
            ChatMessagePacket cmp = new ChatMessagePacket(packet);
            return cmp;
        }
        public static ChatMessagePacket Create(ChatMessageType messagetype, string from, int level, string message)
        {
            return Create(messagetype,from,level,message,new Objects.Location(0,0,0),ChatChannel.Game);
        }
        public static ChatMessagePacket Create(ChatMessageType messagetype, string from, int level, string message, ChatChannel chan)
        {
            return Create(messagetype, from, level, message, new Objects.Location(0, 0, 0), chan);
        }
        public static ChatMessagePacket Create(ChatMessageType messagetype, string from, string message)
        {
            return Create(messagetype, from, 255, message);
        }
    }
}
