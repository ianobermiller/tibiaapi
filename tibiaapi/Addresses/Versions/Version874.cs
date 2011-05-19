using Tibia.Addresses;

namespace Tibia
{
    public partial class Version
    {
        public static void SetVersion874()
        {
            BattleList.Start = 0x640E18;
            BattleList.StepCreatures = 0xAC;
            BattleList.MaxCreatures = 1300;
            BattleList.End = BattleList.Start + (BattleList.StepCreatures * BattleList.MaxCreatures);

            Client.StartTime = 0x80DB00;
            Client.XTeaKey = 0x7C6CFC; //RecvStream + 0x10
            Client.SocketStruct = 0x7C6CD0;
            Client.RecvPointer = 0x5B95E8;
            Client.SendPointer = 0x5B9614;
            Client.LastRcvPacket = 0x7C24B0;
            Client.DecryptCall = 0x45D565;
            Client.ParserFunc = 0x45D530;
            Client.GetNextPacketCall = 0x45D565; // Same as Client.DecryptCall = ParserFunc + 0x35
            Client.RecvStream = 0x7C6CEC;
            Client.FrameRatePointer = 0x7CADE4;
            Client.FrameRateCurrentOffset = 0x60;
            Client.FrameRateLimitOffset = 0x58;
            Client.MultiClient = 0x50BC94;
            Client.MultiClientJMP = 0xEB;
            Client.MultiClientJNZ = 0x75;
            Client.Status = 0x7CA29C;
            Client.SafeMode = 0x7C7124;
            Client.FollowMode = Client.SafeMode + 4;
            Client.AttackMode = Client.FollowMode + 4;
            Client.ActionState = 0x7CA2FC;
            Client.ActionStateFreezer = 0x51E7C0;
            Client.ActionStateOriginal = new byte[] { 0xA3, 0x00, 0x00, 0x00, 0x00, 0xC3, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC };
            Client.ActionStateFreezed = new byte[] { 0xC7, 0x05, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0xC3 };
            //Client.LastMSGText = 0x80CD60;
            Client.LastMSGAuthor = Client.LastMSGText - 0x28;
            Client.StatusbarText = Client.StartTime + 0x20;
            Client.StatusbarTime = Client.StatusbarText - 2;
            Client.ClickId = 0x7CA33C;
            Client.ClickCount = Client.ClickId + 4;
            Client.ClickZ = Client.ClickId - 0x68;
            Client.SeeId = Client.ClickId + 12;
            Client.SeeCount = Client.SeeId + 4;
            Client.SeeZ = Client.SeeId - 0x68;
            Client.ClickContextMenuItemId = Client.SeeId;
            //Client.ClickContextMenuItemGroundId = ?
            Client.ClickContextMenuCreatureId = Client.ClickContextMenuItemId + 0x0C;
            Client.LoginServerStart = 0x7C1C38;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x5B9980;
            Client.LoginCharList = 0x7CA250;
            Client.LoginCharListLength = Client.LoginCharList + 4;
            Client.LoginSelectedChar = Client.LoginCharList - 4;
            Client.GameWindowRectPointer = 0x67969C;
            Client.GameWindowBar = 0x80DB10;
            Client.DatPointer = 0x7C6D1C;
            Client.EventTriggerPointer = 0x5202B0;
            Client.DialogPointer = 0x67CA04;
            Client.DialogLeft = 0x14;
            Client.DialogTop = 0x18;
            Client.DialogWidth = 0x1C;
            Client.DialogHeight = 0x20;
            Client.DialogCaption = 0x50;
            Client.LoginAccountNum = 0;
            Client.LoginPassword = Client.LoginCharList + 8;
            Client.LoginAccount = Client.LoginPassword + 32;
            Client.LoginPatch = 0;
            Client.LoginPatch2 = 0;
            Client.LoginPatchOrig = new byte[] { 0xE8, 0x0D, 0x1D, 0x09, 0x00 };
            Client.LoginPatchOrig2 = new byte[] { 0xE8, 0xC8, 0x15, 0x09, 0x00 };

            Container.Start = 0x67A150;
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
            Container.End = Container.Start + (Container.MaxContainers * Container.StepContainer);

            ContextMenus.AddContextMenuPtr = 0x453880;
            ContextMenus.OnClickContextMenuPtr = 0x450460;
            ContextMenus.OnClickContextMenuVf = 0x5BED28;
            ContextMenus.AddSetOutfitContextMenu = 0x4547AF;
            ContextMenus.AddPartyActionContextMenu = 0x4546C4;
            ContextMenus.AddCopyNameContextMenu = 0x454841;
            ContextMenus.AddTradeWithContextMenu = 0x454429;
            ContextMenus.AddLookContextMenu = 0x4542DF;

            Creature.DistanceId = 0;
            Creature.DistanceType = 3;
            Creature.DistanceName = 4;
            Creature.DistanceX = 36;
            Creature.DistanceY = 40;
            Creature.DistanceZ = 44;
            Creature.DistanceScreenOffsetHoriz = 48;
            Creature.DistanceScreenOffsetVert = 52;
            Creature.DistanceIsWalking = 76;
            Creature.DistanceDirection = 80;
            Creature.DistanceOutfit = 96;
            Creature.DistanceColorHead = 100;
            Creature.DistanceColorBody = 104;
            Creature.DistanceColorLegs = 108;
            Creature.DistanceColorFeet = 112;
            Creature.DistanceAddon = 116;
            Creature.DistanceMountId = 120;
            Creature.DistanceLight = 124;
            Creature.DistanceLightColor = 128;
            Creature.DistanceBlackSquare = 136;
            Creature.DistanceHPBar = 140;
            Creature.DistanceWalkSpeed = 144;
            Creature.DistanceIsVisible = 148;
            Creature.DistanceSkull = 152;
            Creature.DistanceParty = 156;
            Creature.DistanceWarIcon = 164;
            Creature.DistanceIsBlocking = 168;

            DatItem.StepItems = 0x50;
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
            DatItem.CanEquip = 40;
            DatItem.CanLookAt = 0;
            DatItem.WalkSpeed = 44;
            DatItem.TextLimit = 48;
            DatItem.LightRadius = 52;
            DatItem.LightColor = 56;
            DatItem.ShiftX = 60;
            DatItem.ShiftY = 64;
            DatItem.WalkHeight = 68;
            DatItem.Automap = 72;
            DatItem.LensHelp = 76;

            DrawItem.DrawItemFunc = 0x4B56E0;

            DrawSkin.DrawSkinFunc = 0x4B9390;

            Hotkey.SendAutomaticallyStart = 0x7C7320;
            Hotkey.SendAutomaticallyStep = 0x01;
            Hotkey.TextStart = 0x7C7348;
            Hotkey.TextStep = 0x100;
            Hotkey.ObjectStart = 0x7C7290;
            Hotkey.ObjectStep = 0x04;
            Hotkey.ObjectUseTypeStart = 0x7C7170;
            Hotkey.ObjectUseTypeStep = 0x04;
            Hotkey.MaxHotkeys = 36;

            Map.MapPointer = 0x681558;
            Map.StepTile = 168;
            Map.StepTileObject = 12;
            Map.DistanceTileObjectCount = 0;
            Map.DistanceTileObjects = 4;
            Map.DistanceObjectId = 0;
            Map.DistanceObjectData = 4;
            Map.DistanceObjectDataEx = 8;
            Map.MaxTileObjects = 10;
            Map.MaxX = 18;
            Map.MaxY = 14;
            Map.MaxZ = 8;
            Map.MaxTiles = 2016;
            Map.ZAxisDefault = 7;
            Map.NameSpy1 = 0x4F245B;
            Map.NameSpy2 = 0x4F2465;
            Map.NameSpy1Default = 0x4C75;
            Map.NameSpy2Default = 0x4275;
            Map.LevelSpy1 = 0x4F432A;
            Map.LevelSpy2 = 0x4F442F;
            Map.LevelSpy3 = 0x4F44B0;
            Map.LevelSpyPtr = Client.GameWindowRectPointer;
            Map.LevelSpyAdd1 = 28;
            Map.LevelSpyAdd2 = 0x5BC0;
            Map.FullLightNop = 0x4EAA09;
            Map.FullLightAdr = 0x4EAA0C;
            Map.FullLightNopDefault = new byte[] { 0x7E, 0x05 };
            Map.FullLightNopEdited = new byte[] { 0x90, 0x90 };
            Map.FullLightAdrDefault = 0x80;
            Map.FullLightAdrEdited = 0xFF;

            Player.Experience = 0x640D80;
            Player.Flags = Player.Experience - 112;
            Player.Id = Player.Experience + 16;
            Player.Health = Player.Experience + 12;
            Player.HealthMax = Player.Experience + 8;
            Player.Level = Player.Experience - 8;
            Player.MagicLevel = Player.Experience - 12;
            Player.LevelPercent = Player.Experience - 16;
            Player.MagicLevelPercent = Player.Experience - 20;
            Player.Mana = Player.Experience - 24;
            Player.ManaMax = Player.Experience - 28;
            Player.Soul = Player.Experience - 32;
            Player.Stamina = Player.Experience - 36;
            Player.Capacity = Player.Experience - 40;

            Player.FistPercent = Player.Flags + 4;
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

            Player.WhiteSquare = Player.Flags + 60;
            Player.GreenSquare = Player.Flags + 64;
            Player.RedSquare = Player.Flags + 68;

            Player.SlotHead = 0x67A0D8;
            Player.SlotNeck = Player.SlotHead + 12;
            Player.SlotBackpack = Player.SlotHead + 24;
            Player.SlotArmor = Player.SlotHead + 36;
            Player.SlotRight = Player.SlotHead + 48;
            Player.SlotLeft = Player.SlotHead + 60;
            Player.SlotLegs = Player.SlotHead + 72;
            Player.SlotFeet = Player.SlotHead + 84;
            Player.SlotRing = Player.SlotHead + 96;
            Player.SlotAmmo = Player.SlotHead + 108;
            Player.MaxSlots = 10;
            Player.DistanceSlotCount = 4;
            Player.CurrentTileToGo = Player.Flags + 132;
            Player.TilesToGo = Player.CurrentTileToGo + 4;
            Player.GoToX = Player.Experience + 84;
            Player.GoToY = Player.GoToX - 4;
            Player.GoToZ = Player.GoToX - 8;
            //Player.AccessN = 0;
            //Player.AccessS = 0;
            Player.TargetId = Player.RedSquare;
            Player.TargetBattlelistId = Player.TargetId - 8;
            Player.TargetBattlelistType = Player.TargetId - 5;
            Player.TargetType = Player.TargetId + 3;

            Player.Z = 0x67CA40;
            Player.Y = Player.Z + 4;
            Player.X = Player.Z + 8;

            Player.AttackCount = 0x63E920;
            Player.FollowCount = Player.AttackCount + 0x20;

            TextDisplay.PrintName = 0x4F5493;
            TextDisplay.PrintFPS = 0x45B568;
            TextDisplay.ShowFPS = 0x63EA1C;
            TextDisplay.PrintTextFunc = 0x4B4B20;
            TextDisplay.NopFPS = 0x45B4A4;

            Vip.Start = 0x63EAA8;
            Vip.StepPlayers = 0x2C;
            Vip.MaxPlayers = 200;
            Vip.DistanceId = 0;
            Vip.DistanceName = 4;
            Vip.DistanceStatus = 34;
            Vip.DistanceIcon = 40;
            Vip.End = Vip.Start + (Vip.StepPlayers * Vip.MaxPlayers);
        }
    }
}