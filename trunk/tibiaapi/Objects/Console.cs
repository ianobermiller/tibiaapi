using System;
using Tibia.Packets;

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
            return Say(new ChatMessage(text));
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
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Spell(Objects.Spell s)
        {
            return Spell(s.Words);
        }
        
        /// <summary>
        /// Send a message (generic).
        /// </summary>
        /// <param name="message"></param>
        /// <returns>message packet</returns>
        public bool Say(ChatMessage message)
        {
            return Packets.Outgoing.SayPacket.Send(client, (SpeakClasses_t)message.Type, message.Recipient, message.Text, message.Channel); 
        }

        /// <summary>
        /// Send a private message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="recipient"></param>
        /// <returns>message packet</returns>
        public bool Say(string message, string recipient)
        {
            return Say(new ChatMessage(message, recipient));
        }
    }

    /// <summary>
    /// A message in Tibia.
    /// </summary>
    public struct ChatMessage
    {
        public string Text;
        public string Recipient;
        public Packets.ChatChannel Channel;
        public Packets.ChatType Type;

        /// <summary>
        /// Create a default message.
        /// </summary>
        /// <param name="text"></param>
        public ChatMessage(string text)
        {
            this.Text = text;
            this.Recipient = "";
            this.Channel = Packets.ChatChannel.None;
            this.Type = Packets.ChatType.Normal;
        }

        /// <summary>
        /// Create a private message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="recipient"></param>
        public ChatMessage(string text, string recipient)
        {
            this.Text = text;
            this.Recipient = recipient;
            this.Channel = Packets.ChatChannel.None;
            this.Type = Packets.ChatType.PrivateMessage;
        }

        /// <summary>
        /// Create a channel message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="channel"></param>
        public ChatMessage(string text, Packets.ChatChannel channel)
        {
            this.Text = text;
            this.Recipient = "";
            this.Channel = channel;
            this.Type = Packets.ChatType.ChannelNormal;
        }

        /// <summary>
        /// Create a yell or whisper message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public ChatMessage(string text, Packets.ChatType type)
        {
            this.Text = text;
            this.Recipient = "";
            this.Channel = Packets.ChatChannel.None;
            this.Type = type;
        }
    }
}
