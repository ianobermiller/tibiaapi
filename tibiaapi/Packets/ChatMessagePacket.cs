using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class ChatMessagePacket : Packet
    {
        private ChatMessageType messageType;
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
                index += 1;
                if (messageType == ChatMessageType.Normal || messageType == ChatMessageType.Whisper || messageType == ChatMessageType.Yell)
                {
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
                else
                {
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

        public static ChatMessagePacket Create(ChatMessageType messagetype, string from, int level, string message, Objects.Location loc)
        {
            byte[] packet;
            if (messagetype == ChatMessageType.Normal || messagetype == ChatMessageType.Whisper || messagetype == ChatMessageType.Yell)
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
            return Create(messagetype,from,level,message,new Objects.Location(0,0,0));
        }
        public static ChatMessagePacket Create(ChatMessageType messagetype, string from, string message)
        {
            return Create(messagetype, from, 255, message);
        }
    }
}
