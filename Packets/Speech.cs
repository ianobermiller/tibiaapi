using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Create packets to send messages.
    /// </summary>
    public static class Speech
    {
        /// <summary>
        /// Say something in the default channel (wrapper for Say)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] Default(string text)
        {
            return Say(new Message(text));
        }

        /// <summary>
        /// Say the spell words (wrapper for Say)
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static byte[] Spell(string words)
        {
           return Default(words);
        }

        /// <summary>
        /// Say the words of a spell (wrapper for Say)
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        public static byte[] Spell(Objects.Spell spell)
        {
            return Spell(spell.Words);
        }
        
        /// <summary>
        /// Send a message (generic).
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <returns>message packet</returns>
        public static byte[] Say(Message message)
        {
            byte[] packet = { };
            int packetLength, payloadLength;
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            switch (message.type)
            {
                case Type.Normal:
                case Type.Whisper:
                case Type.Yell:
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
                case Type.Channel:
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
                case Type.PrivateMessage:
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
            }

            return packet;
        }

        /// <summary>
        /// Send a private message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="recipient"></param>
        /// <returns>message packet</returns>
        public static byte[] Send(string message, string recipient)
        {
            return null;
        }

        /// <summary>
        /// A message in Tibia.
        /// </summary>
        public struct Message
        {
            public string text;
            public string recipient;
            public Channel channel;
            public Type type;

            /// <summary>
            /// Create a default message.
            /// </summary>
            /// <param name="text"></param>
            public Message(string text)
            {
                this.text = text;
                this.recipient = "";
                this.channel = Channel.None;
                this.type = Type.Normal;
            }

            /// <summary>
            /// Create a private message.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="recipient"></param>
            public Message(string text, string recipient)
            {
                this.text = text;
                this.recipient = recipient;
                this.channel = Channel.None;
                this.type = Type.PrivateMessage;
            }

            /// <summary>
            /// Create a channel message.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="channel"></param>
            public Message(string text, Channel channel)
            {
                this.text = text;
                this.recipient = "";
                this.channel = channel;
                this.type = Type.Channel;
            }

            /// <summary>
            /// Create a yell or whisper message.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="type"></param>
            public Message(string text, Type type)
            {
                this.text = text;
                this.recipient = "";
                this.channel = Channel.None;
                this.type = type;
            }
        }

        public enum Channel
        {
            None = -1,
            Guild = 0,
            Game = 4,
            Trade = 5,
            RealLife = 6,
            Help = 7,
            OwnPrivate = 14,
            Private1 = 17
        }

        public enum Type
        {
            Normal = 1,
            Whisper = 2,
            Yell = 3,
            PrivateMessage = 4,
            Channel = 5
        }
    }
}
