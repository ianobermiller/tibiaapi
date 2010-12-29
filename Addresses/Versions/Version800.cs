using Tibia.Addresses;

namespace Tibia
{
    public partial class Version
    {
        public static void SetVersion800()
        {
            BattleList.Start = 0x60EB30;
            BattleList.End = 0x6148F0;
            BattleList.StepCreatures = 0xA0;
            BattleList.MaxCreatures = 100;

            Client.XTeaKey = 0x7637AC;
            Client.SocketStruct = 0x763780;
            Client.SendPointer = 0x593600;
            //Client.FrameRate = 0x7661F4; 
            Client.MultiClient = 0x4EFB71; //not verified
            Client.Status = 0x766DF8;
            Client.FollowMode = 0x763BD0;
            Client.AttackMode = 0x763BD4;
            Client.SafeMode = 0x763BCC;
            Client.ActionState = 0x751BD8;
            //Client.CurrentWindow = 0x6198B4;
            Client.LastMSGAuthor = 0x768680;
            Client.LastMSGText = 0x7686A8;
            Client.StatusbarText = 0x00768458;
            Client.StatusbarTime = 0x00768454;
            Client.ClickId = 0x766E94;
            Client.ClickCount = 0x766E98;
            Client.ClickZ = 0x766E2C;
            Client.SeeId = 0x766EA0;
            Client.SeeCount = 0x766EA4;
            Client.SeeZ = 0x766E00;
            Client.LoginServerStart = 0x75EAE8;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x593610;
            Client.LoginCharList = 0x766DBC;
            Client.LoginSelectedChar = 0x766DB8;
            Client.DialogPointer = 0x6198B4;
            Client.DatPointer = 0x7637CC;

            Container.Start = 0x617000;
            Container.End = 0x618EC0;
            Container.StepContainer = 492;
            Container.StepSlot = 12;
            Container.MaxContainers = 16;
            Container.MaxStack = 100;
            Container.DistanceIsOpen = 0;
            Container.DistanceId = 4;
            Container.DistanceName = 16;
            Container.DistanceVolume = 48;
            Container.DistanceAmount = 56;
            Container.DistanceItemId = 60;
            Container.DistanceItemCount = 64;

            Creature.DistanceId = 0;
            Creature.DistanceType = 3;
            Creature.DistanceName = 4;
            Creature.DistanceX = 36;
            Creature.DistanceY = 40;
            Creature.DistanceZ = 44;
            Creature.DistanceScreenOffsetHoriz = 48;
            Creature.DistanceScreenOffsetVert = 52;
            Creature.DistanceIsWalking = 76;
            Creature.DistanceWalkSpeed = 140;
            Creature.DistanceDirection = 80;
            Creature.DistanceIsVisible = 144;
            Creature.DistanceBlackSquare = 128;
            Creature.DistanceLight = 120;
            Creature.DistanceLightColor = 124;
            Creature.DistanceHPBar = 136;
            Creature.DistanceSkull = 148;
            Creature.DistanceParty = 152;
            Creature.DistanceOutfit = 96;
            Creature.DistanceColorHead = 100;
            Creature.DistanceColorBody = 104;
            Creature.DistanceColorLegs = 108;
            Creature.DistanceColorFeet = 112;
            Creature.DistanceAddon = 116;

            Map.MapPointer = 0x61E408;
            Map.StepTile = 172;
            Map.StepTileObject = 12;
            Map.DistanceTileObjectCount = 0;
            Map.DistanceTileObjects = 4;
            Map.DistanceObjectId = 0;
            Map.DistanceObjectData = 4;
            Map.DistanceObjectDataEx = 8;
            Map.MaxTiles = 2016;
            Map.MaxTileObjects = 13;
            Map.ZAxisDefault = 7;

            Player.Flags = 0x60EA58;
            Player.Experience = 0x60EAC4;
            Player.Id = Player.Experience + 12;
            Player.Health = Player.Experience + 8;
            Player.HealthMax = Player.Experience + 4;
            Player.Level = Player.Experience - 4;
            Player.MagicLevel = Player.Experience - 8;
            Player.LevelPercent = Player.Experience - 12;
            Player.MagicLevelPercent = Player.Experience - 16;
            Player.Mana = Player.Experience - 20;
            Player.ManaMax = Player.Experience - 24;
            Player.Soul = Player.Experience - 28;
            Player.Stamina = Player.Experience - 32;
            Player.Capacity = Player.Experience - 36;
            Player.FistPercent = 0x60EA5C;
            Player.ClubPercent = Player.FistPercent + 4;
            Player.SwordPercent = Player.FistPercent + 8;
            Player.AxePercent = Player.FistPercent + 12;
            Player.DistancePercent = Player.FistPercent + 16;
            Player.ShieldingPercent = Player.FistPercent + 20;
            Player.FishingPercent = Player.FistPercent + 24;
            Player.Fist = Player.FistPercent + 28;
            Player.Club = Player.FistPercent + 32;
            Player.Sword = Player.FistPercent + 36;
            Player.Axe = Player.FistPercent + 40;
            Player.Distance = Player.FistPercent + 44;
            Player.Shielding = Player.FistPercent + 48;
            Player.Fishing = Player.FistPercent + 52;
            Player.SlotHead = 0x616F88;
            Player.SlotNeck = Player.SlotHead + 12;
            Player.SlotBackpack = Player.SlotHead + 24;
            Player.SlotArmor = Player.SlotHead + 36;
            Player.SlotRight = Player.SlotHead + 48;
            Player.SlotLeft = Player.SlotHead + 60;
            Player.SlotLegs = Player.SlotHead + 72;
            Player.SlotFeet = Player.SlotHead + 84;
            Player.SlotRing = Player.SlotHead + 96;
            Player.SlotAmmo = Player.SlotHead + 108;
            Player.DistanceSlotCount = 4;
            Player.GoToX = 0x60EB10;
            Player.GoToY = Player.GoToX - 4;
            Player.GoToZ = Player.GoToX - 8;
            Player.RedSquare = 0x60EA9C;
            Player.GreenSquare = Player.RedSquare - 4;
            Player.WhiteSquare = Player.GreenSquare - 8;
            Player.AccessN = 0x766DF4;
            Player.AccessS = 0x766DC4;
            Player.TargetId = 0x60EA9C;
            Player.TargetType = 0x60EA9F;
            Player.TargetBattlelistId = 0x60EA94;
            Player.TargetBattlelistType = 0x60EA97;

            Vip.Start = 0x60C7F0;
            Vip.End = 0x60C840;
            Vip.StepPlayers = 0x2C;
            Vip.MaxPlayers = 100;
            Vip.DistanceId = 0;
            Vip.DistanceName = 4;
            Vip.DistanceStatus = 34;
            Vip.DistanceIcon = 40;
        }
    }
}
