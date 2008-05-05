using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class PlayerSpeechPacket : Packet
    {
        private ChatType messageType = ChatType.Normal;
        private ChatChannel channel = ChatChannel.None;
        private short lenRecipientName = 0;
        private string recipientName = String.Empty;
        private short lenMessage = 0;
        private string message = String.Empty;

        public ChatType MessageType
        {
            get { return messageType; }
        }
        public ChatChannel Channel
        {
            get { return channel; }
        }
        public string RecipientName
        {
            get { return recipientName; }
        }
        public string Message
        {
            get { return message; }
        }
        public PlayerSpeechPacket()
        {
            type = PacketType.PlayerSpeech;
            destination = PacketDestination.Server;
        }

        public PlayerSpeechPacket(byte[] data)
            : this()
        {
            ParseData(data);
        }

        public new bool ParseData(byte[] packet)
        {
            if (base.ParseData(packet))
            {
                if (type != PacketType.PlayerSpeech) return false;
                int index = 3;
                messageType = (ChatType)packet[index];
                index++;

                switch (messageType)
                {
                    case ChatType.Normal:
                    case ChatType.Whisper:
                    case ChatType.Yell:
                        lenMessage = BitConverter.ToInt16(packet, 4);
                        message = Encoding.ASCII.GetString(packet, 6, lenMessage);
                        break;

                    case ChatType.ChannelNormal:
                        channel = (ChatChannel)BitConverter.ToInt16(packet, 4);
                        lenMessage = BitConverter.ToInt16(packet, 6);
                        message = Encoding.ASCII.GetString(packet, 8, lenMessage);
                        break;
                    case ChatType.PrivateMessage:
                        lenRecipientName = BitConverter.ToInt16(packet, index);
                        index += 2;
                        recipientName = Encoding.ASCII.GetString(packet, index, lenRecipientName);
                        index += lenRecipientName;
                        lenMessage = BitConverter.ToInt16(packet, index);
                        index += 2;
                        message = Encoding.ASCII.GetString(packet, index, lenMessage);
                        break;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static PlayerSpeechPacket Create(string message)
        {
            return Create(new Objects.ChatMessage(message));
        }

        public static PlayerSpeechPacket Create(string message, string recipient)
        {
            return Create(new Objects.ChatMessage(message, recipient));
        }

        public static PlayerSpeechPacket Create(Objects.ChatMessage message)
        {
            byte[] packet = { };
            int packetLength, payloadLength;
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            switch (message.type)
            {
                case Packets.ChatType.Normal:
                case Packets.ChatType.Whisper:
                case Packets.ChatType.Yell:
                    packetLength = 6 + message.text.Length;
                    payloadLength = packetLength - 2;
                    packet = new byte[packetLength];

                    packet[00] = Packet.Lo(payloadLength);
                    packet[01] = Packet.Hi(payloadLength);
                    packet[02] = 0x96;
                    packet[03] = (byte)message.type;
                    packet[04] = Packet.Lo(message.text.Length);
                    packet[05] = Packet.Hi(message.text.Length);

                    // Copy the message to the rest of the bytes
                    Array.Copy(enc.GetBytes(message.text), 0, packet, 6, message.text.Length);
                    break;
                case Packets.ChatType.ChannelNormal:
                    packetLength = 8 + message.text.Length;
                    payloadLength = packetLength - 2;
                    packet = new byte[packetLength];

                    packet[00] = Packet.Lo(payloadLength);
                    packet[01] = Packet.Hi(payloadLength);
                    packet[02] = 0x96;
                    packet[03] = (byte)message.type;
                    packet[04] = (byte)message.channel;
                    packet[05] = 0x0;
                    packet[06] = Packet.Lo(message.text.Length);
                    packet[07] = Packet.Hi(message.text.Length);

                    // Copy the message to the rest of the bytes
                    Array.Copy(enc.GetBytes(message.text), 0, packet, 8, message.text.Length);
                    break;
                case Packets.ChatType.PrivateMessage:
                    packetLength = 8 + message.text.Length + message.recipient.Length;
                    payloadLength = packetLength - 2;
                    packet = new byte[packetLength];

                    packet[00] = Packet.Lo(payloadLength);
                    packet[01] = Packet.Hi(payloadLength);
                    packet[02] = 0x96;
                    packet[03] = (byte)message.type;
                    packet[04] = Packet.Lo(message.recipient.Length);
                    packet[05] = Packet.Hi(message.recipient.Length);

                    // Insert the recipient's name
                    Array.Copy(enc.GetBytes(message.recipient), 0, packet, 6, message.recipient.Length);

                    // Skip the index ahead, past the recipient's name
                    int i = 6 + message.recipient.Length;

                    packet[i] = Packet.Lo(message.text.Length);
                    packet[i + 1] = Packet.Hi(message.text.Length);

                    // Copy the message to the rest of the bytes
                    Array.Copy(enc.GetBytes(message.text), 0, packet, i + 2, message.text.Length);
                    break;
                default:
                    throw new ArgumentException("This type is not valid for sending a message.", "message.type");
            }
            return new PlayerSpeechPacket(packet);
        }
    }
}
