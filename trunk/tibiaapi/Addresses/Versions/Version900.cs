using Tibia.Addresses;

namespace Tibia
{
    public partial class Version
    {
        public static void SetVersion900()
        {
            BattleList.Start = 0x642e28;
            BattleList.StepCreatures = 0xac;
            BattleList.MaxCreatures = 1300;
            BattleList.End = BattleList.Start + (BattleList.StepCreatures * BattleList.MaxCreatures);

            Client.StartTime = 0x80fb20;

            //RecvStream + 0x10
            Client.SocketStruct = 0x7c8cf0;
            Client.RecvPointer = 0x5ba5f8;
            Client.SendPointer = 0x5ba624;
            Client.LastRcvPacket = 0x7c44d0;
            Client.DecryptCall = 0x45d775;
            Client.ParserFunc = 0x45d740;
            Client.XTeaKey = Addresses.Client.SocketStruct + 0x2c;
            Client.GetNextPacketCall = 0x45d775;
            // Same as Client.DecryptCall = ParserFunc + 0x35
            Client.RecvStream = 0x7c8d0c;
            Client.FrameRatePointer = 0x7cce04;
            Client.FrameRateCurrentOffset = 0x60;
            Client.FrameRateLimitOffset = 0x58;
            Client.MultiClient = 0x50cf44;
            Client.MultiClientJMP = 0xeb;
            Client.MultiClientJNZ = 0x75;
            Client.Status = 0x7cc2bc;
            Client.SafeMode = 0x7c9144;
            Client.FollowMode = Client.SafeMode + 4;
            Client.AttackMode = Client.FollowMode + 4;
            Client.ActionState = 0x7cc31c;
            Client.ActionStateFreezer = 0x51fa70;
            Client.ActionStateOriginal = new byte[] {
	0xa3,
	0x0,
	0x0,
	0x0,
	0x0,
	0xc3,
	0xcc,
	0xcc,
	0xcc,
	0xcc,
	0xcc
};
            Client.ActionStateFreezed = new byte[] {
	0xc7,
	0x5,
	0x0,
	0x0,
	0x0,
	0x0,
	0x7,
	0x0,
	0x0,
	0x0,
	0xc3
};
            //Client.LastMSGText = 0x80CD60;
            Client.LastMSGAuthor = Client.LastMSGText - 0x28;
            Client.StatusbarText = 0x80fb40;
            Client.StatusbarTime = Client.StatusbarText - 8;
            Client.ClickId = 0x7cc35c;
            Client.ClickCount = Client.ClickId + 4;
            Client.ClickZ = Client.ClickId - 0x68;
            Client.SeeId = Client.ClickId + 12;
            Client.SeeCount = Client.SeeId + 4;
            Client.SeeZ = Client.SeeId - 0x68;
            Client.ClickContextMenuItemId = Client.SeeId;
            //Client.ClickContextMenuItemGroundId = ?
            Client.ClickContextMenuCreatureId = Client.ClickContextMenuItemId + 0xc;
            Client.LoginServerStart = 0x7c3c58;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x5ba990;
            Client.LoginCharList = 0x7cc270;
            Client.LoginCharListLength = Client.LoginCharList + 4;
            Client.LoginSelectedChar = Client.LoginCharList - 4;
            // Client.GameWindowRectPointer = &H67969C
            // Client.GameWindowBar = &H80DB10
            Client.DatPointer = 0x7c8d3c;
            Client.EventTriggerPointer = 0x521580;
            Client.DialogPointer = 0x67ea24;
            Client.DialogLeft = 0x14;
            Client.DialogTop = 0x18;
            Client.DialogWidth = 0x1c;
            Client.DialogHeight = 0x20;
            Client.DialogCaption = 0x50;
            Client.LoginAccountNum = 0;
            Client.LoginPassword = Client.LoginCharList + 8;
            Client.LoginAccount = Client.LoginPassword + 32;
            Client.LoginPatch = 0;
            Client.LoginPatch2 = 0;
            Client.LoginPatchOrig = new byte[] {
	0xe8,
	0xd,
	0x1d,
	0x9,
	0x0
};
            Client.LoginPatchOrig2 = new byte[] {
	0xe8,
	0xc8,
	0x15,
	0x9,
	0x0
};

            Container.Start = 0x67c170;
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

            ContextMenus.AddContextMenuPtr = 0x4539e0;
            ContextMenus.OnClickContextMenuPtr = 0x450560;
            //  ContextMenus.OnClickContextMenuVf = &H5BED28
            ContextMenus.AddSetOutfitContextMenu = 0x45490f;
            ContextMenus.AddPartyActionContextMenu = 0x454824;
            ContextMenus.AddCopyNameContextMenu = 0x4549a1;
            ContextMenus.AddTradeWithContextMenu = 0x454589;
            ContextMenus.AddLookContextMenu = 0x45443f;

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

            DrawItem.DrawItemFunc = 0x4b5a30;

            DrawSkin.DrawSkinFunc = 0x4b96f0;

            Hotkey.SendAutomaticallyStart = 0x7c9340;
            Hotkey.SendAutomaticallyStep = 0x1;
            Hotkey.TextStart = 0x7c9368;
            Hotkey.TextStep = 0x100;
            Hotkey.ObjectStart = 0x7c92b0;
            Hotkey.ObjectStep = 0x4;
            Hotkey.ObjectUseTypeStart = 0x7c9190;
            Hotkey.ObjectUseTypeStep = 0x4;
            Hotkey.MaxHotkeys = 36;

            Map.MapPointer = 0x683578;
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
            Map.NameSpy1 = 0x4f245b;
            Map.NameSpy2 = 0x4f2465;
            Map.NameSpy1Default = 0x4c75;
            Map.NameSpy2Default = 0x4275;
            Map.LevelSpy1 = 0x4f4eaa;
            Map.LevelSpy2 = 0x4f4faf;
            Map.LevelSpy3 = 0x4f5030;
            Map.LevelSpyPtr = Client.GameWindowRectPointer;
            Map.LevelSpyAdd1 = 28;
            Map.LevelSpyAdd2 = 0x5bc0;
            Map.FullLightNop = 0x4eb589;
            Map.FullLightAdr = 0x4eb58b;
            Map.FullLightNopDefault = new byte[] {
	0x7e,
	0x5
};
            Map.FullLightNopEdited = new byte[] {
	0x90,
	0x90
};
            Map.FullLightAdrDefault = 0x80;
            Map.FullLightAdrEdited = 0xff;

            Player.Experience = 0x642d90;
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

            Player.SlotHead = 0x67c0f8;
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

            Player.Z = 0x67ea60;
            Player.Y = Player.Z + 4;
            Player.X = Player.Z + 8;

            Player.AttackCount = 0x640940;
            Player.FollowCount = Player.AttackCount + 0x20;

            TextDisplay.PrintName = 0x4f6013;
            TextDisplay.PrintFPS = 0x45b778;
            TextDisplay.ShowFPS = 0x640a3c;
            TextDisplay.PrintTextFunc = 0x4b4e70;
            TextDisplay.NopFPS = 0x45b6b4;

            Vip.Start = 0x640ab8;
            Vip.StepPlayers = 0x2c;
            Vip.MaxPlayers = 200;
            Vip.DistanceId = 0;
            Vip.DistanceName = 4;
            Vip.DistanceStatus = 34;
            Vip.DistanceIcon = 40;
            Vip.End = Vip.Start + (Vip.StepPlayers * Vip.MaxPlayers);
        }
    }
}