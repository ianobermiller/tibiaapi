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
            return Packets.Outgoing.PlayerSpeechPacket.Send(client, message.Type, message.Recipient, message.Text, message.Channel); 
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
        public Packets.SpeechType Type;

        /// <summary>
        /// Create a default message.
        /// </summary>
        /// <param name="text"></param>
        public ChatMessage(string text)
        {
            this.Text = text;
            this.Recipient = "";
            this.Channel = Packets.ChatChannel.None;
            this.Type = Packets.SpeechType.Say;
        }

        /// <summary>
        /// Create a private message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="recipient"></param>
        public static ChatMessage CreatePrivateMessage(string text, string recipient)
        {
            ChatMessage cm = new ChatMessage(text);
            cm.Recipient = recipient;
            cm.Channel = Packets.ChatChannel.None;
            cm.Type = Packets.SpeechType.Private;
            return cm;
        }

        /// <summary>
        /// Create a channel message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="channel"></param>
        public static ChatMessage CreateChannelMessage(string text, Packets.ChatChannel channel)
        {
            ChatMessage cm = new ChatMessage(text);
            cm.Recipient = "";
            cm.Channel = channel;
            cm.Type = Packets.SpeechType.ChannelYellow;
            return cm;
        }

        /// <summary>
        /// Create a yell or whisper message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public static ChatMessage CreateNormalMessage(string text, Packets.SpeechType type)
        {
            ChatMessage cm = new ChatMessage(text);
            cm.Recipient = "";
            cm.Channel = Packets.ChatChannel.None;
            cm.Type = type;
            return cm;
        }
    }
}
