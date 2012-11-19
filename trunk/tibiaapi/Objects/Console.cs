using System;
using Tibia.Packets;
using Tibia.Constants;

namespace Tibia.Objects
{
    public class Console
    {
        private Client client;

        /// <summary>
        /// Create a new console object with the specified client.
        /// </summary>
        /// <param name="c"></param>
        public Console(Client client)
        {
            this.client = client;
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
        /// Send a message (generic).
        /// </summary>
        /// <param name="message"></param>
        public bool Say(ChatMessage message)
        {
            return Packets.Outgoing.TalkPacket.Send(client, message.Type, message.Recipient, message.Text, message.Channel); 
        } 
    }

    /// <summary>
    /// A message in Tibia.
    /// </summary>
    public struct ChatMessage
    {
        public string Text;
        public string Recipient;
        public ChatChannel Channel;
        public SpeechType Type;

        /// <summary>
        /// Create a default message.
        /// </summary>
        /// <param name="text"></param>
        public ChatMessage(string text)
        {
            this.Text = text;
            this.Recipient = "";
            this.Channel = ChatChannel.None;
            this.Type = SpeechType.Say;
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
            this.Channel = ChatChannel.None;
            this.Type = SpeechType.PrivateTo;
        }

        /// <summary>
        /// Create a channel message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="channel"></param>
        public ChatMessage(string text, ChatChannel channel)
        {
            this.Text=text;
            this.Recipient = "";
            this.Channel = channel;
            this.Type = SpeechType.Channel;
        }

        /// <summary>
        /// Create a yell or whisper message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public ChatMessage(string text, SpeechType type)
        {
            this.Text = text;
            this.Recipient = "";
            this.Channel = ChatChannel.None;
            this.Type = type;
        }

        public ChatMessage(string text, string recipient, ChatChannel channel, SpeechType type)
        {
            this.Text = text;
            this.Recipient = recipient;
            this.Channel = channel;
            this.Type = type;
        }
    }
}
