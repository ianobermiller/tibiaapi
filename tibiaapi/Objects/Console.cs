using System;

namespace Tibia.Objects
{
    public class Console
    {
        private Client client;

        /// <summary>
        /// Create a new inventory object with the specified client.
        /// </summary>
        /// <param name="c"></param>
        public Console(Client c)
        {
            client = c;
        }
        
        /// <summary>
        /// Say something in the default channel (wrapper for Say)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool Say(string text)
        {
            return Say(new Message(text));
        }

        /// <summary>
        /// Say the spell words (wrapper for Say)
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public bool Spell(string words)
        {
           return Say(words);
        }

        /// <summary>
        /// Say the words of a spell (wrapper for Say)
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        public bool Spell(Objects.Spell s)
        {
            return Spell(s.Words);
        }
        
        /// <summary>
        /// Send a message (generic).
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <returns>message packet</returns>
        public bool Say(Message message)
        {
            byte[] packet = { };
            int packetLength, payloadLength;
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            switch (message.type)
            {
                case Constants.SpeechType.Normal:
                case Constants.SpeechType.Whisper:
                case Constants.SpeechType.Yell:
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
                case Constants.SpeechType.Channel:
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
                case Constants.SpeechType.PrivateMessage:
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

            return client.Send(packet);
        }

        /// <summary>
        /// Send a private message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="recipient"></param>
        /// <returns>message packet</returns>
        public bool send(string message, string recipient)
        {
            return Say(new Message(message, recipient));
        }
    }

    /// <summary>
    /// A message in Tibia.
    /// </summary>
    public struct Message
    {
        public string text;
        public string recipient;
        public Constants.SpeechChannel channel;
        public Constants.SpeechType type;

        /// <summary>
        /// Create a default message.
        /// </summary>
        /// <param name="text"></param>
        public Message(string text)
        {
            this.text = text;
            this.recipient = "";
            this.channel = Constants.SpeechChannel.None;
            this.type = Constants.SpeechType.Normal;
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
            this.channel = Constants.SpeechChannel.None;
            this.type = Constants.SpeechType.PrivateMessage;
        }

        /// <summary>
        /// Create a channel message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="channel"></param>
        public Message(string text, Constants.SpeechChannel channel)
        {
            this.text = text;
            this.recipient = "";
            this.channel = channel;
            this.type = Constants.SpeechType.Channel;
        }

        /// <summary>
        /// Create a yell or whisper message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public Message(string text, Constants.SpeechType type)
        {
            this.text = text;
            this.recipient = "";
            this.channel = Constants.SpeechChannel.None;
            this.type = type;
        }
    }
}
