using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureSpeakPacket : IncomingPacket
    {
        public SpeakClasses_t SpeakType { get; set; }
        public uint UnknowSpeak { get; set; }
        public ChatChannel_t ChannelId { get; set; }
        public string SenderName { get; set; }
        public ushort SenderLevel { get; set; }
        public string Message { get; set; }
        public Objects.Location Position { get; set; }
        public uint Time { get; set; }

        public CreatureSpeakPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType_t.CREATURE_SPEAK;
            Destination = PacketDestination_t.CLIENT;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination_t destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType_t.CREATURE_SPEAK)
                return false;

            Destination = destination;
            Type = IncomingPacketType_t.CREATURE_SPEAK;

            UnknowSpeak = msg.GetUInt32();
            SenderName = msg.GetString();
            SenderLevel = msg.GetUInt16();
            SpeakType = (SpeakClasses_t)msg.GetByte();

            switch (SpeakType)
            {
                case SpeakClasses_t.SPEAK_SAY:
                case SpeakClasses_t.SPEAK_WHISPER:
                case SpeakClasses_t.SPEAK_YELL:
                case SpeakClasses_t.SPEAK_MONSTER_SAY:
                case SpeakClasses_t.SPEAK_MONSTER_YELL:
                case SpeakClasses_t.SPEAK_PRIVATE_NP:
                    {
                        Position = msg.GetLocation();
                        break;
                    }
                case SpeakClasses_t.SPEAK_CHANNEL_R1:
                case SpeakClasses_t.SPEAK_CHANNEL_R2:
                case SpeakClasses_t.SPEAK_CHANNEL_O:
                case SpeakClasses_t.SPEAK_CHANNEL_Y:
                case SpeakClasses_t.SPEAK_CHANNEL_W:
                    {
                        ChannelId = (ChatChannel_t)msg.GetUInt16();
                        break;
                    }
                case SpeakClasses_t.SPEAK_RVR_CHANNEL:
                    {
                        Time = msg.GetUInt32();
                        break; 
                    }
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

            msg.AddUInt32(UnknowSpeak);
            msg.AddString(SenderName);
            msg.AddUInt16(SenderLevel);
            msg.AddByte((byte)SpeakType);

            switch (SpeakType)
            {
                case SpeakClasses_t.SPEAK_SAY:
                case SpeakClasses_t.SPEAK_WHISPER:
                case SpeakClasses_t.SPEAK_YELL:
                case SpeakClasses_t.SPEAK_MONSTER_SAY:
                case SpeakClasses_t.SPEAK_MONSTER_YELL:
                case SpeakClasses_t.SPEAK_PRIVATE_NP:
                    {
                        msg.AddLocation(Position);
                        break;
                    }
                case SpeakClasses_t.SPEAK_CHANNEL_R1:
                case SpeakClasses_t.SPEAK_CHANNEL_R2:
                case SpeakClasses_t.SPEAK_CHANNEL_O:
                case SpeakClasses_t.SPEAK_CHANNEL_Y:
                case SpeakClasses_t.SPEAK_CHANNEL_W:
                    {
                        msg.AddUInt16((ushort)ChannelId);
                        break;
                    }
                case SpeakClasses_t.SPEAK_RVR_CHANNEL:
                    {
                        msg.AddUInt32(Time);
                        break;
                    }
                default:
                    break;

            }

            msg.AddString(Message);

            return msg.Packet;
        }
    }
}