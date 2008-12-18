using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets.Incoming
{
    public class CreatureSpeakPacket : IncomingPacket
    {
        public SpeechType SpeakType { get; set; }
        public uint UnknowSpeak { get; set; }
        public ChatChannel ChannelId { get; set; }
        public string SenderName { get; set; }
        public ushort SenderLevel { get; set; }
        public string Message { get; set; }
        public Objects.Location Position { get; set; }
        public uint Time { get; set; }

        public CreatureSpeakPacket(Objects.Client c)
            : base(c)
        {
            Type = IncomingPacketType.CreatureSpeak;
            Destination = PacketDestination.Client;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination, Objects.Location pos)
        {
            if (msg.GetByte() != (byte)IncomingPacketType.CreatureSpeak)
                return false;

            Destination = destination;
            Type = IncomingPacketType.CreatureSpeak;

            UnknowSpeak = msg.GetUInt32();
            SenderName = msg.GetString();
            SenderLevel = msg.GetUInt16();
            SpeakType = (SpeechType)msg.GetByte();

            switch (SpeakType)
            {
                case SpeechType.Say:
                case SpeechType.Whisper:
                case SpeechType.Yell:
                case SpeechType.MonsterSay:
                case SpeechType.MonsterYell:
                case SpeechType.PrivateNPCToPlayer:
                {
                    Position = msg.GetLocation();
                    break;
                }
                case SpeechType.ChannelRed:
                case SpeechType.ChannelRedAnonymous:
                case SpeechType.ChannelOrange:
                case SpeechType.ChannelYellow:
                case SpeechType.ChannelWhite:
                {
                    ChannelId = (ChatChannel)msg.GetUInt16();
                    break;
                }
                case SpeechType.RuleViolationReport:
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
                case SpeechType.Say:
                case SpeechType.Whisper:
                case SpeechType.Yell:
                case SpeechType.MonsterSay:
                case SpeechType.MonsterYell:
                case SpeechType.PrivateNPCToPlayer:
                {
                    msg.AddLocation(Position);
                    break;
                }
                case SpeechType.ChannelRed:
                case SpeechType.ChannelRedAnonymous:
                case SpeechType.ChannelOrange:
                case SpeechType.ChannelYellow:
                case SpeechType.ChannelWhite:
                {
                    msg.AddUInt16((ushort)ChannelId);
                    break;
                }
                case SpeechType.RuleViolationReport:
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