using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Outgoing
{
    public class SayPacket : OutgoingPacket
    {

        public SpeakClasses_t SpeakType { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public ChatChannel_t ChannelId { get; set; }

        public SayPacket(Objects.Client c)
            : base(c)
        {
            Type = OutgoingPacketType_t.SAY;
            Destination = PacketDestination_t.SERVER;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)OutgoingPacketType_t.SAY)
                return false;

            Destination = destination;
            Type = OutgoingPacketType_t.SAY;

            SpeakType = (SpeakClasses_t)msg.GetByte();

            switch (SpeakType)
            {
                case SpeakClasses_t.SPEAK_PRIVATE:
                case SpeakClasses_t.SPEAK_PRIVATE_RED:
                case SpeakClasses_t.SPEAK_RVR_ANSWER:
                    Receiver = msg.GetString();
                    break;
                case SpeakClasses_t.SPEAK_CHANNEL_Y:
                case SpeakClasses_t.SPEAK_CHANNEL_R1:
                case SpeakClasses_t.SPEAK_CHANNEL_R2:
                case SpeakClasses_t.SPEAK_CHANNEL_W:
                    ChannelId = (ChatChannel_t)msg.GetUInt16();
                    break;
                default:
                    break;
            }

            Message = msg.GetString();

            return true;
        }

        public override byte[] ToByteArray()
        {
            NetworkMessage msg = new NetworkMessage(0);

            msg.AddByte((byte)Type);

            msg.AddByte((byte)SpeakType);

            switch (SpeakType)
            {
                case SpeakClasses_t.SPEAK_PRIVATE:
                case SpeakClasses_t.SPEAK_PRIVATE_RED:
                case SpeakClasses_t.SPEAK_RVR_ANSWER:
                    msg.AddString(Receiver);
                    break;
                case SpeakClasses_t.SPEAK_CHANNEL_Y:
                case SpeakClasses_t.SPEAK_CHANNEL_R1:
                case SpeakClasses_t.SPEAK_CHANNEL_R2:
                case SpeakClasses_t.SPEAK_CHANNEL_W:
                    msg.AddUInt16((ushort)ChannelId);
                    break;
                default:
                    break;
            }

            msg.AddString(Message);

            return msg.Packet;
        }

        public static bool Send(Objects.Client client, SpeakClasses_t type, string receiver, string message, ChatChannel_t channel)
        {
            SayPacket p = new SayPacket(client);

            p.SpeakType = type;
            p.Receiver = receiver;
            p.Message = message;
            p.ChannelId = channel;

            return p.Send();
        }


    }
}