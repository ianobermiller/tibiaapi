using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Identifies the packet by its type (3rd byte)
    /// </summary>
    public enum PacketType : byte
    {
        CharList = 0x14,
        StatusMessage = 0xB4
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
        WhiteAdvance  = 0x13,
        WhiteStatus   = 0x15,
        GreenYouSee   = 0x16,
        BlueSentPM    = 0x18
    }
}
