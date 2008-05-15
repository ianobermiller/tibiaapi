using System;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Packets
{
    public class PlayerSpeechPacket : Packet
    {
        private ChatType messageType = ChatType.Normal;
        private ChatChannel channel = ChatChannel.None;
        private string recipientName = String.Empty;
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
                PacketBuilder p = new PacketBuilder(packet, 3);

                messageType = (ChatType)p.GetByte();

                switch (messageType)
                {
                    case ChatType.ChannelNormal:
                    case ChatType.ChannelRedAnonymous:
                    case ChatType.ChannelTutor:
                    case ChatType.ChannelGM:
                        channel = (ChatChannel)p.GetInt();
                        break;
                    case ChatType.PrivateMessage:
                        recipientName = p.GetString();
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
            PacketBuilder p = new PacketBuilder(PacketType.PlayerSpeech);
            p.AddByte((byte)message.Type);
            switch (message.Type)
            {
                case ChatType.ChannelNormal:
                case ChatType.ChannelRedAnonymous:
                case ChatType.ChannelTutor:
                case ChatType.ChannelGM:
                    p.AddInt((int)message.Channel);
                    break;
                case Packets.ChatType.PrivateMessage:
                    p.AddString(message.Recipient);
                    break;
                default:
                    break;
            }
            p.AddString(message.Text);
            return new PlayerSpeechPacket(p.GetPacket());
        }
    }
}
