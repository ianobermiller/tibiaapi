using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Identifies the packet by its type (3rd byte)
    /// </summary>
    public enum PacketType : byte
    {
        // Incoming
        DefaultTemplate     = 0x00,
        CharListLoginData   = 0x01,
        AddCreature         = 0x0A,
        BadLogin            = 0x0A,
        CharList            = 0x14,
        InformationBox      = 0x15,
        Ping                = 0x1E,
        MapItemAdd          = 0x6A,
        MapItemUpdate       = 0x6B,
        MapItemRemove       = 0x6C,
        CreatureMove        = 0x6D,
        ContainerOpened     = 0x6E,
        ContainerClosed     = 0x6F,
        ContainerItemAdd    = 0x70,
        ContainerItemUpdate = 0x71,
        ContainerItemRemove = 0x72,
        EqItemAdd           = 0x78,
        EqItemRemove        = 0x79,
        TradeBoxOpen        = 0x7A,
        WorldLight          = 0x82,
        TileAnimation       = 0x83,
        AnimatedText        = 0x84,
        Projectile          = 0x85,
        CreatureSquare      = 0x86,
        CreatureHealth      = 0x8C,
        CreatureLight       = 0x8D,
        CreatureOutfit      = 0x8E,
        CreatureSpeed       = 0x8F,
        CreatureSkull       = 0x90,
        PartyInvite         = 0x91,
        BookOpen            = 0x96,
        StatusUpdate        = 0xA0,
        SkillUpdate         = 0xA1,
        FlagUpdate          = 0xA2,
        CancelTarget        = 0xA3,
        ChatMessage         = 0xAA,
        ChannelList         = 0xAB,
        ChannelOpen         = 0xAC,
        PrivateChannelOpen  = 0xAD, 
        StatusMessage       = 0xB4,
        CancelAutoWalk      = 0xB5,
        VipAdd              = 0xD2, 
        VipLogin            = 0xD3,
        VipLogout           = 0xD4,

        // Outgoing
        Logout              = 0x14,
        ItemMove            = 0x78,
        ItemUse             = 0x82,
        ItemUseOn           = 0x83,
        ItemUseBattlelist   = 0x84,
        ContainerClose      = 0x87,
        ContainerOpenParent = 0x88,
        LookAt              = 0x8C,
        PlayerSpeech        = 0x96,
        ClientLoggedIn      = 0xA0,
        Attack              = 0xA1,
        CancelMove          = 0xBE,

        // Pipe
        PipePacket          = 0xFF

    }

    /// <summary>
    /// Identifies the PipePacket by its type (3rd byte)
    /// </summary>
    public enum PipePacketType : byte
    {
        DefaultTemplate     = 0x00,
        SetConstant         = 0x01,
        DisplayText         = 0x02,
        RemoveText          = 0x03,
        RemoveAllText       = 0x04,
        InjectDisplayText   = 0x05,
        DisplayCreatureText = 0x06,
        RemoveCreatureText  = 0x07,
        UpdateCreatureText  = 0x08
    }


    /// <summary>
    /// Describes the packets destination
    /// </summary>
    public enum PacketDestination : byte
    {
        Client,
        Server,
        Pipe,
        None
    }

    public enum SquareColor : byte
    {
        Black = 0
    }

    public enum MapItemAddType
    {
        Item,
        CreatureReturning,
        CreatureKnown,
        CreatureNew
    }

    #region Speech
    public enum ChatType : byte
    {
        Normal = 0x01,
        Whisper = 0x02,
        Yell = 0x03,
        PrivatePlayerToNpc = 0x04,
        PrivateNpcToPlayer = 0x05,
        PrivateMessage = 0x06,
        ChannelNormal = 0x07,
        ChannelGM = 0x0A,
        Broadcast = 0x0B,
        ChannelRed = 0x0C,
        PrivateRed = 0x0D,
        ChannelTutor = 0x0E,
        ChannelRedAnonymous = 0x10,
        Monster = 0x12,
        MonsterYell = 0x13, 
    }

    public enum ChatChannel : ushort
    {
        Guild = 0,
        Game = 4,
        Trade = 5,
        RL = 6,
        Help = 7,
        OwnPrivate = 14,
        Private1 = 17,
        Private = 0xFFFF,
        Custom = 0xA0,
        Custom1 = 0xA1,
        Custom2 = 0xA2,
        Custom3 = 0xA3,
        Custom4 = 0xA4,
        Custom5 = 0xA5,
        Custom6 = 0xA6,
        Custom7 = 0xA7,
        Custom8 = 0xA8,
        Custom9 = 0xA9,
        None = 0xAAAA
    }

    #endregion

    public enum StatusMessageType : byte
    {
        ConsoleRed          = 0x11, //Red, console
        ConsoleOrange       = 0x13, //Orange, console
	    Warning			    = 0x14, //Red, center, console
	    EventAdvance		= 0x15, //White, center, console
	    EventDefault		= 0x16, //White, bottom, console
	    Default			    = 0x17, //White, bottom, console
	    Description			= 0x18, //Green, center, console
	    SmallStatus			= 0x19, //White, bottom
	    ConsoleBlue		    = 0x1A, //Blue, console
    }

    public enum TextColor : byte
    {
        Blue = 5,
        Green = 30,
        LightBlue = 35,
        Crystal = 65,
        Purple = 83,
        Platinum = 89,
        LightGrey = 129,
        DarkRed = 144,
        Red = 180,
        Orange = 198,
        Gold = 210,
        White = 215,
        None = 255
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

    public enum TileAnimationType
    {
        DrawBlood = 0x00,
        LoseEnergy = 0x01,
        Puff = 0x02,
        BlockHit = 0x03,
        ExplosionArea = 0x04,
        ExplosionDamage = 0x05,
        FireArea = 0x06,
        YellowRings = 0x07,
        PoisonRings = 0x08,
        HitArea = 0x09,
        Teleport = 0x0a,
        EnergyDamage = 0x0b,
        MagicEnergy = 0x0c,
        MagicBlood = 0x0d,
        MagicPoison = 0x0e,
        HitByFire = 0x0f,
        Poison = 0x10,
        MortArea = 0x11,
        SoundGreen = 0x12,
        SoundRed = 0x13,
        PoisonArea = 0x14,
        SoundYellow = 0x15,
        SoundPurple = 0x16,
        SoundBlue = 0x17,
        SoundWhite = 0x18,
        Bubbles = 0x19,
        Craps = 0x1a,
        GiftWraps = 0x1b,
        FireworkYellow = 0x1c,
        FireworkRed = 0x1d,
        FireworkBlue = 0x1e,
        Stun = 0x1f,
        Sleep = 0x20,
        WaterCreature = 0x21,
        Groundshaker = 0x22,
        Hearts = 0x23,
        FireAttack = 0x24,
        EnergyArea = 0x25,
        SmallClouds = 0x26,
        HolyDamage = 0x27,
        BigClouds = 0x28,
        IceArea = 0x29,
        IceTornado = 0x2a,
        IceAttack = 0x2b,
        Stones = 0x2c,
        SmallPlants = 0x2d,
        Carniphila = 0x2e,
        PurpleEnergy = 0x2f,
        YellowEnergy = 0x30,
        HolyArea = 0x31,
        BigPlants = 0x32,
        Cake = 0x33,
        GiantIce = 0x34,
        WaterSplash = 0x35,
        PlantAttack = 0x36,
    }
    public enum PartyType
    {
        Inviter = 1,
        Invitee = 2,
        Member  = 3,
        Leader  = 4,
        MemberShareXP = 5,
        LeaderShareXP = 6,
        MemberShareXPInactive = 7,
        LeaderShareXPInactive = 8,
        MemberShareXPAlone = 9,
        LeaderShareXPAlone = 10
    }
}
