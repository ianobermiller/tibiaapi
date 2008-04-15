using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Identifies the packet by its type (3rd byte)
    /// </summary>
    public enum PacketType : byte
    {
        CharList = 0x14,
        StatusMessage = 0xB4,
        ChatMessage = 0xAA,
        AnimatedText = 0x84
    }

    /// <summary>
    /// Describes the packets destination
    /// </summary>
    public enum PacketDestination : byte
    {
        Client,
        Server,
        None
    }

    #region Speech

    public enum SpeechChannel
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

    public enum SpeechType
    {
        Normal = 1,
        Whisper = 2,
        Yell = 3,
        PrivateMessage = 4,
        Channel = 5
    }

    #endregion

    public enum StatusMessageType : byte
    {
        YellowDefault = 0x01,
        BlueReceivePM = 0x04,
        Red           = 0x09,
        OrangeCM      = 0x10,
        RedOnScreen   = 0x12,
        WhiteAdvance  = 0x13,
        WhiteStatus   = 0x15,
        GreenYouSee   = 0x16,
        BlueSentPM    = 0x18
    }

    public enum ChatMessageType : byte
    {
        Normal = 0x01,
        Whisper = 0x02,
        Yell = 0x03,
        PM = 0x04,
        GM = 0x09,
        Monster = 0x10,
        MonsterYell = 0x11,
        ChannelNormal = 0x05,
        ChannelGM = 0x0A,
        ChannelTutor = 0x0C
    }

    public enum ChatChannel : int
    {
        Guild = 0,
        Game = 4,
        Trade = 5,
        RL = 6,
        Help = 7,
        Private = 0xFFFF
    }

    public enum TextColor : byte
    {
        Blue = 5,
        Green = 30,
        LightBlue = 35,
        Crystal = 65,
        Platinum = 89,
        LightGrey = 129,
        Red = 180,
        Orange = 198,
        Gold = 210,
        White = 215,
        Purple = 255
    }
}
