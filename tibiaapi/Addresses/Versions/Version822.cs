using Tibia.Addresses;

namespace Tibia
{
    public partial class Version
    {
        public static void SetVersion822()
        {
            BattleList.StepCreatures = 0xA0;
            BattleList.MaxCreatures = 150;
            BattleList.Start = Player.Experience + 108;
            BattleList.End = BattleList.Start + (BattleList.StepCreatures * BattleList.MaxCreatures);

            Client.StartTime = 0x781258;
            Client.XTeaKey = 0x77BDB4;
            Client.SocketStruct = 0x77BD88;
            Client.SendPointer = 0x5A7600;
            Client.FrameRatePointer = 0x77FF3C;
            Client.FrameRateCurrentOffset = 0x0;
            Client.FrameRateLimitOffset = 0x58;
            Client.MultiClient = 0x502B94;//not verified
            Client.Status = 0x77F3F8;
            Client.SafeMode = 0x77C1D0;
            Client.FollowMode = Client.SafeMode + 4;
            Client.AttackMode = Client.FollowMode + 4;
            Client.ActionState = 0x77F458;
            Client.LastMSGText = 0x7814C0; //8.1, 8.0 = 0x7686A8
            Client.LastMSGAuthor = Client.LastMSGText - 0x28; //8.1, 8.0 = 0x768680
            Client.StatusbarText = 0x781270;
            Client.StatusbarTime = Client.StatusbarText - 4;
            Client.ClickId = 0x77F494;
            Client.ClickCount = Client.ClickId + 4;
            Client.ClickZ = Client.ClickId - 0x68;
            Client.SeeText = Client.ClickId + 12;
            Client.LoginServerStart = 0x776CF0;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x5A7610;
            Client.LoginCharList = 0x77F3BC;
            Client.LoginSelectedChar = 0x77F3B8;
            Client.GameWindowRectPointer = 0x62E754;
            Client.GameWindowBar = 0x631AC0;
            Client.DatPointer = 0x77BDD4;
            Client.DialogPointer = 0x631ABC;
            Client.DialogLeft = 0x14;
            Client.DialogTop = 0x18;
            Client.DialogWidth = 0x1C;
            Client.DialogHeight = 0x20;
            Client.DialogCaption = 0x50;
            Client.LoginPassword = 0x77F3E4;
            Client.LoginAccount = Client.LoginPassword + 32;
            Client.LoginAccountNum = Client.LoginAccount + 12;
            Client.Nop = 0x90;
            Client.LoginPatchOrig = new byte[] { 0xE8, 0x0D, 0x1D, 0x09, 0x00 };
            Client.LoginPatchOrig2 = new byte[] { 0xE8, 0xC8, 0x15, 0x09, 0x00 };

            Container.Start = 0x62F208;
            Container.End = Container.Start + (Container.MaxContainers * Container.StepContainer);
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

            DatItem.StepItems = 0x4C;
            DatItem.Width = 0;
            DatItem.Height = 4;
            DatItem.MaxSizeInPixels = 8;
            DatItem.Layers = 12;
            DatItem.PatternX = 16;
            DatItem.PatternY = 20;
            DatItem.PatternDepth = 24;
            DatItem.Phase = 28;
            DatItem.Sprite = 32;
            DatItem.Flags = 36;
            DatItem.CanLookAt = 0;
            DatItem.WalkSpeed = 40;
            DatItem.TextLimit = 44;
            DatItem.LightRadius = 48;
            DatItem.LightColor = 52;
            DatItem.ShiftX = 56;
            DatItem.ShiftY = 60;
            DatItem.WalkHeight = 64;
            DatItem.Automap = 68;
            DatItem.LensHelp = 72;

            Hotkey.SendAutomaticallyStart = 0x77C3C8;
            Hotkey.SendAutomaticallyStep = 0x1;
            Hotkey.TextStart = 0x77C3F0;
            Hotkey.TextStep = 0x100;
            Hotkey.ObjectStart = 0x77C2A8;
            Hotkey.ObjectStep = 0x4;
            Hotkey.ObjectUseTypeStart = 0x77C218;
            Hotkey.ObjectUseTypeStep = 0x4;
            Hotkey.MaxHotkeys = 36;

            Map.MapPointer = 0x636610;
            Map.StepTile = 172;
            Map.StepTileObject = 12;
            Map.DistanceTileObjectCount = 0;
            Map.DistanceTileObjects = 4;
            Map.DistanceObjectId = 0;
            Map.DistanceObjectData = 4;
            Map.DistanceObjectDataEx = 8;
            Map.MaxTileObjects = 13;
            Map.MaxX = 18;
            Map.MaxY = 14;
            Map.MaxZ = 8;
            Map.MaxTiles = 2016;
            Map.ZAxisDefault = 7;
            Map.PlayerTile = 0x3E3A08;
            Map.NameSpy1 = 0x4E95F9;
            Map.NameSpy2 = 0x4E9603;
            Map.NameSpy1Default = 19061;
            Map.NameSpy2Default = 16501;
            Map.LevelSpy1 = 0x4EB4AA;
            Map.LevelSpy2 = 0x4EB5AF;
            Map.LevelSpy3 = 0x4EB630;
            Map.LevelSpyPtr = 0x62E754;
            Map.LevelSpyAdd2 = 0x25D8;
            Map.LevelSpyDefault = new byte[] { 0x89, 0x86, 0xD8, 0x25, 0x00, 0x00 };
            Map.RevealInvisible1 = 0x45BF63;
            Map.RevealInvisible2 = 0x4E88C5;

            Player.Experience = 0x626C64;
            Player.GoToX = Player.Experience + 80;
            Player.GoToY = Player.Experience + 76;
            Player.GoToZ = Player.Experience + 72;
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
            Player.Fishing = Player.Experience - 52;
            Player.Shielding = Player.Experience - 56;
            Player.Distance = Player.Experience - 60;
            Player.Axe = Player.Experience - 64;
            Player.Sword = Player.Experience - 68;
            Player.Club = Player.Experience - 72;
            Player.Fist = Player.Experience - 76;
            Player.FishingPercent = Player.Experience - 80;
            Player.ShieldingPercent = Player.Experience - 84;
            Player.DistancePercent = Player.Experience - 88;
            Player.AxePercent = Player.Experience - 92;
            Player.SwordPercent = Player.Experience - 96;
            Player.ClubPercent = Player.Experience - 100;
            Player.FistPercent = Player.Experience - 104;
            Player.Flags = Player.Experience - 108;
            Player.MaxSlots = 10;
            Player.SlotHead = 0x62D190;
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
            Player.CurrentTileToGo = 0x626C78;
            Player.TilesToGo = 0x626C7C;
            Player.RedSquare = 0x626C3C;
            Player.GreenSquare = Player.RedSquare - 4;
            Player.WhiteSquare = Player.GreenSquare - 8;
            Player.AccessN = 0x766DF4;
            Player.AccessS = 0x766DC4;
            Player.TargetId = Player.RedSquare;
            Player.TargetBattlelistId = Player.TargetId - 8;
            Player.TargetBattlelistType = Player.TargetId - 5;
            Player.TargetType = Player.TargetId + 3;

            TextDisplay.PrintName = 0x4EC5E1;
            TextDisplay.PrintFPS = 0x4563C8;
            TextDisplay.ShowFPS = 0x624974;
            TextDisplay.PrintTextFunc = 0x4AC490;
            TextDisplay.NopFPS = 0x456304;

            Vip.Start = 0x624990;
            Vip.End = 0x625228;
            Vip.StepPlayers = 0x2C;
            Vip.MaxPlayers = 100;
            Vip.DistanceId = 0;
            Vip.DistanceName = 4;
            Vip.DistanceStatus = 34;
            Vip.DistanceIcon = 40;
        }
    }
}
