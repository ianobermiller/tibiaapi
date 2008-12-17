using System;

namespace Tibia.Packets
{
    /// <summary>
    /// Identifies the packet by its type (3rd byte)
    /// </summary>
    public enum PacketType : byte
    {
        // Pipe
        PipePacket          = 0xFF

    }

    public enum SpeakClasses_t : byte
    {
        SPEAK_SAY = 0x01,	//normal talk
        SPEAK_WHISPER = 0x02,	//whispering - #w text
        SPEAK_YELL = 0x03,	//yelling - #y text
        SPEAK_PRIVATE_PN = 0x04, //Player-to-NPC speaking(NPCs channel)
        SPEAK_PRIVATE_NP = 0x05, //NPC-to-Player speaking
        SPEAK_PRIVATE = 0x06, //Players speaking privately to players
        SPEAK_CHANNEL_Y = 0x07,	//Yellow message in chat
        SPEAK_CHANNEL_W = 0x08, //White message in chat
        SPEAK_RVR_CHANNEL = 0x09, //Reporting rule violation - Ctrl+R
        SPEAK_RVR_ANSWER = 0x0A, //Answering report
        SPEAK_RVR_CONTINUE = 0x0B, //Answering the answer of the report
        SPEAK_BROADCAST = 0x0C,	//Broadcast a message - #b
        SPEAK_CHANNEL_R1 = 0x0D,	//Talk red on chat - #c
        SPEAK_PRIVATE_RED = 0x0E,	//Red private - @name@ text
        SPEAK_CHANNEL_O = 0x0F,	//Talk orange on text
        //SPEAK_                = 0x10, //?
        SPEAK_CHANNEL_R2 = 0x11,	//Talk red anonymously on chat - #d
        SPEAK_MONSTER_SAY12 = 0x12, //?????
        SPEAK_MONSTER_SAY = 0x13,	//Talk orange
        SPEAK_MONSTER_YELL = 0x14,	//Yell orange
    }

    public enum MessageClasses_t : byte
    {
        MSG_STATUS_CONSOLE_RED = 0x11, //Red message in the console
        MSG_STATUS_CONSOLE_ORANGE2 = 0x13, //Orange message in the console
        MSG_STATUS_CONSOLE_ORANGE = 0x14, //Orange message in the console
        MSG_STATUS_WARNING = 0x15, //Red message in game window and in the console
        MSG_EVENT_ADVANCE = 0x16, //White message in game window and in the console
        MSG_EVENT_DEFAULT = 0x17, //White message at the bottom of the game window and in the console
        MSG_STATUS_DEFAULT = 0x18, //White message at the bottom of the game window and in the console
        MSG_INFO_DESCR = 0x19, //Green message in game window and in the console
        MSG_STATUS_SMALL = 0x1A, //White message at the bottom of the game window"
        MSG_STATUS_CONSOLE_BLUE = 0x1B, //Blue message in the console
    }

    public enum IncomingPacketType_t : byte
    {
        // GameServer
        SELF_APPEAR = 0x0A,
        GM_ACTION = 0x0B,
        ERROR_MESSAGE = 0x14,
        FYI_MESSAGE = 0x15,
        WAITING_LIST = 0x16,
        PING = 0x1E,
        DEATH = 0x28,
        CAN_REPORT_BUGS = 0x32,
        MAP_DESCRIPTION = 0x64,
        MOVE_NORTH = 0x65,
        MOVE_EAST = 0x66,
        MOVE_SOUTH = 0x67,
        MOVE_WEST = 0x68,
        UPDATE_TILE = 0x69,
        TILE_ADD_THING = 0x6A,
        TILE_TRANSFORM_THING = 0x6B,
        TILE_REMOVE_THING = 0x6C,
        CREATURE_MOVE = 0x6D,
        OPEN_CONTAINER = 0x6E,
        CLOSE_CONTAINER = 0x6F,
        CONTAINER_ADD_ITEM = 0x70,
        CONTAINER_UPDATE_ITEM = 0x71,
        CONTAINER_REMOVE_ITEM = 0x72,
        INVENTORY_SET_SLOT = 0x78,
        INVENTORY_RESET_SLOT = 0x79,
        SAFE_TRADE_REQUEST_ACK = 0x7D,
        SAFE_TRADE_REQUEST_NO_ACK = 0x7E,
        SAFE_TRADE_CLOSE = 0x7F,
        WORLD_LIGHT = 0x82,
        MAGIC_EFFECT = 0x83,
        ANIMATED_TEXT = 0x84,
        DISTANCE_SHOT = 0x85,
        CREATURE_SQUARE = 0x86,
        CREATURE_HEALTH = 0x8C,
        CREATURE_LIGHT = 0x8D,
        CREATURE_OUTFIT = 0x8E,
        CREATURE_SPEED = 0x8F,
        CREATURE_SKULLS = 0x90,
        CREATURE_SHIELDS = 0x91,
        ITEM_TEXT_WINDOW = 0x96,
        HOUSE_TEXT_WINDOW = 0x97,
        PLAYER_STATS = 0xA0,
        PLAYER_SKILLS = 0xA1,
        PLAYER_ICONS = 0xA2,
        PLAYER_CANCEL_ATTACK = 0xA3,
        CREATURE_SPEAK = 0xAA,
        CHANNEL_LIST = 0xAB,
        OPEN_CHANNEL = 0xAC,
        OPEN_PRIVATE_PLAYER_CHAT = 0xAD,
        //OPEN_RULE_VIOLATION = 0xAE,
        //RuleViolationAF = 0xAF,
        //RuleViolationB0 = 0xB0,
        //RuleViolationB1 = 0xB1,
        CREATE_PRIVATE_CHANNEL = 0xB2,
        CLOSE_PRIVATE_CHANNEL = 0xB3,
        TEXT_MESSAGE = 0xB4,
        PLAYER_CANCEL_WALK = 0xB5,
        FLOOR_CHANGE_UP = 0xBE,
        FLOOR_CHANGE_DOWN = 0xBF,
        OUTFIT_WINDOW = 0xC8,
        VIP_STATE = 0xD2,
        VIP_LOGIN = 0xD3,
        VIP_LOGOUT = 0xD4,
        QUEST_LIST = 0xF0,
        QUEST_PART_LIST = 0xF1,
        OPEN_SHOP_WINDOW = 0x7A,
        SHOP_SALE_ITEM_LIST = 0x7B,
        CLOSE_SHOP_WINDOW = 0x7C,
        SHOW_TUTORIAL = 0xDC,
        ADD_MAP_MARKER = 0xDD,
    }

    public enum OutgoingPacketType_t : byte
    {
        SAY = 0x96,
        OPEN_CHANNEL = 0x98,
        CLOSE_CHANNEL = 0x99,
        ATTACK = 0xA1,
        THROW = 0x78,
        LOOK_AT = 0x8C,
        FOLLOW = 0xA2,
        USE_ITEM = 0x82,
        USE_ITEM_EX = 0x83,
        CANCEL_MOVE = 0xBE,
        BATTLE_WINDOW = 0x84,
        LOGOUT = 0x14,
        CLOSE_CONTAINER = 0x87,
        UP_ARROW_CONTAINER = 0x88,
    }

    public enum PacketDestination_t : byte
    {
        CLIENT,
        SERVER,
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
        UpdateCreatureText  = 0x08,
        AddContextMenu      = 0x09,
        RemoveContextMenu   = 0x0A,
        RemoveAllContextMenus=0x0B,
        OnClickContextMenu  = 0x0C
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

    public enum ShootType_t : byte
    {
        NM_SHOOT_SPEAR = 1,
        NM_SHOOT_BOLT = 2,
        NM_SHOOT_ARROW = 3,
        NM_SHOOT_FIRE = 4,
        NM_SHOOT_ENERGY = 5,
        NM_SHOOT_POISONARROW = 6,
        NM_SHOOT_BURSTARROW = 7,
        NM_SHOOT_THROWINGSTAR = 8,
        NM_SHOOT_THROWINGKNIFE = 9,
        NM_SHOOT_SMALLSTONE = 10,
        NM_SHOOT_DEATH = 11,
        NM_SHOOT_LARGEROCK = 12,
        NM_SHOOT_SNOWBALL = 13,
        NM_SHOOT_POWERBOLT = 14,
        NM_SHOOT_POISONFIELD = 15,
        NM_SHOOT_INFERNALBOLT = 16,
        NM_SHOOT_HUNTINGSPEAR = 17,
        NM_SHOOT_ENCHANTEDSPEAR = 18,
        NM_SHOOT_REDSTAR = 19,
        NM_SHOOT_GREENSTAR = 20,
        NM_SHOOT_ROYALSPEAR = 21,
        NM_SHOOT_SNIPERARROW = 22,
        NM_SHOOT_ONYXARROW = 23,
        NM_SHOOT_PIERCINGBOLT = 24,
        NM_SHOOT_WHIRLWINDSWORD = 25,
        NM_SHOOT_WHIRLWINDAXE = 26,
        NM_SHOOT_WHIRLWINDCLUB = 27,
        NM_SHOOT_ETHEREALSPEAR = 28,
        NM_SHOOT_ICE = 29,
        NM_SHOOT_EARTH = 30,
        NM_SHOOT_HOLY = 31,
        NM_SHOOT_SUDDENDEATH = 32,
        NM_SHOOT_FLASHARROW = 33,
        NM_SHOOT_FLAMMINGARROW = 34,
        NM_SHOOT_SHIVERARROW = 35,
        NM_SHOOT_ENERGYBALL = 36,
        NM_SHOOT_SMALLICE = 37,
        NM_SHOOT_SMALLHOLY = 38,
        NM_SHOOT_SMALLEARTH = 39,
        NM_SHOOT_EARTHARROW = 40,
        NM_SHOOT_EXPLOSION = 41,
        NM_SHOOT_CAKE = 42,
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
        BlueArrow = 0x38,
        FlashSquare = 0x39  
    }

    public enum PartyShields_t
    {
        SHIELD_NONE = 0,
        SHIELD_WHITEYELLOW = 1,
        SHIELD_WHITEBLUE = 2,
        SHIELD_BLUE = 3,
        SHIELD_YELLOW = 4,
        SHIELD_BLUE_SHAREDEXP = 5,
        SHIELD_YELLOW_SHAREDEXP = 6,
        SHIELD_BLUE_NOSHAREDEXP_BLINK = 7,
        SHIELD_YELLOW_NOSHAREDEXP_BLINK = 8,
        SHIELD_BLUE_NOSHAREDEXP = 9,
        SHIELD_YELLOW_NOSHAREDEXP = 10
    }

    public class AvalibleOutfit_t
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public byte Addons { get; set; }

        public AvalibleOutfit_t() { }
    }

    public class ShopInfo
    {
        public ushort ItemId { get; set; }
        public byte SubType { get; set; }
        public uint BuyPrice { get; set; }
        public uint SellPrice { get; set; }
        public string ItemName { get; set; }
        public uint Weight { get; set; }

        public ShopInfo() { }

        public ShopInfo(ushort _itemId, byte _subType,
            uint _buyPrice, uint _sellPrice)
        {
            ItemId = _itemId;
            SubType = _subType;
            BuyPrice = _buyPrice;
            SellPrice = _sellPrice;
        }
    }

    public enum PacketCreatureType_t : byte
    {
        KNOW,
        UNKNOW,
        TURN,
    }

    public class PacketCreature
    {
        public PacketCreatureType_t Type { get; set; }
        public uint Id { get; set; }
        public string Name { get; set; }
        public byte Health { get; set; }
        public byte Direction { get; set; }
        public Objects.Outfit Outfit { get; set; }
        public byte LightLevel { get; set; }
        public byte LightColor { get; set; }
        public ushort Speed { get; set; }
        public Constants.Skulls_t Skull { get; set; }
        public PartyShields_t PartyShield { get; set; }
        public uint RemoveId { get; set; }
        public Objects.Client Client { get; set; }

        public PacketCreature(Objects.Client client)
        {
            Client = client;
        }
    }


}
