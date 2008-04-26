using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Identifies the packet by its type (3rd byte)
    /// </summary>
    public enum PacketType : byte
    {
        CharListLoginData = 0x01,
        CharList = 0x14,
        Logout = 0x14,
        GameWorldLoginData = 0x0A,
        StatusMessage = 0xB4,
        ChatMessage = 0xAA,
        AnimatedText = 0x84,
        Projectile = 0x85,
        CreatureHealth = 0x8C,
        VipLogin = 0xD3
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
        None = -1,
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
    public enum ProjectileType : byte
    {
        Spear = 0x01,
        Bolt = 0x02,
        Arrow = 0x03,
        Fire = 0x04,
        Energy = 0x05,
        PoisonArrow = 0x06,
        BurstArrow = 0x07,
        ThrowingStar = 0x08,
        ThrowingKnife = 0x09,
        SmallStone = 0x0A,
        Skull = 0x0B,
        BigStone = 0x0C,
        SnowBall = 0x0D,
        PowerBolt = 0x0E,
        SmallPoison = 0x0F,
        InfernalBolt = 0x10,
        HuntingSpear = 0x11,
        EnchantedSpear = 0x12,
        AssassinStar = 0x13,
        ViperStar = 0x14,
        RoyalSpear = 0x15,
        SniperArrow = 0x16,
        OnyxArrow = 0x17,
        EarthArrow = 0x18,
        NormalSword = 0x19,
        NormalAxe = 0x1A,
        NormalClub = 0x1B,
        IceProjectile = 0x1C,
        IceProjectile2 = 0x1D,
        EarthProjectile = 0x1E,
        FireProjectile = 0x1F,
        DeathProjectile = 0x20,
        FlashArrow = 0x21,
        FlamingArrow = 0x22,
        ShiverArrow = 0x23,
        EnergyProjectile = 0x24,
        IceProjectile3 = 0x25,
        FireProjectile2 = 0x26,
        DeathProjectile2 = 0x27,
        EarthArrow2 = 0x28,
        SmallRedProjectile = 0x29,
        CakeProjectile = 0x2A
    }
}
