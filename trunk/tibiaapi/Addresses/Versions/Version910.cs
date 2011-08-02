using Tibia.Addresses;
using System.Diagnostics;
using System;
namespace Tibia
{
    public partial class Version
    {
        public static void SetVersion910(Process p)
                    {
                        uint  baseAdress = Convert.ToUInt32(p.MainModule.BaseAddress.ToInt32());
          
                        BattleList.Start = 0x7e4f98 - 0x400000 + baseAdress;
                        BattleList.StepCreatures = 0xb0;
                        BattleList.MaxCreatures = 1300;
                        BattleList.End = BattleList.Start + (BattleList.StepCreatures * BattleList.MaxCreatures);

                        //Client.StartTime = 0x80fb20;
                        Client.SocketStruct = 0x86c910 - 0x400000 + baseAdress;
                        Client.RecvPointer = 0x6c4928 - 0x400000 + baseAdress;
                        Client.SendPointer = 0x6c4958 - 0x400000 + baseAdress;
                        Client.LastRcvPacket = 0x822800 - 0x400000 + baseAdress;
                        Client.DecryptCall = 0x45d230 - 0x400000 + baseAdress;
                        Client.ParserFunc = 0x45d1f0 - 0x400000 + baseAdress;
                        Client.XTeaKey = 0x7dad4c - 0x400000 + baseAdress;
                        Client.GetNextPacketCall = 0x4fc0c0 - 0x400000 + baseAdress;
                        // Same as Client.DecryptCall = ParserFunc + 0x35
                        Client.RecvStream = 0x9b2690 - 0x400000 + baseAdress;
                        Client.FrameRatePointer = 0x86d448 - 0x400000 + baseAdress;
                        Client.FrameRateCurrentOffset = 0x60;
                        Client.FrameRateLimitOffset = 0x58;
                        Client.MultiClient = 0x50f221 - 0x400000 + baseAdress;
                        Client.MultiClientJMP = 0xeb;
                        Client.MultiClientJNZ = 0x75;
                        Client.Status = 0x8296ec - 0x400000 + baseAdress;
                        Client.SafeMode = 0x829506 - 0x400000 + baseAdress;
                        Client.FollowMode = 0x8270d8 - 0x400000 + baseAdress;
                        Client.AttackMode = 0x82950c - 0x400000 + baseAdress;
                        Client.ActionState = 0x829698 - 0x400000 + baseAdress;
                        // Client.ActionStateFreezer = 0x51fa70;
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
                        //	Client.LastMSGAuthor = Client.LastMSGText - &H28
                        Client.StatusbarText = 0x86b360 - 0x400000 + baseAdress;
                        Client.StatusbarTime = 0x86b35c - 0x400000 + baseAdress;
                        Client.ClickId = 0x9b34b4 - 0x400000 + baseAdress;
                        Client.ClickCount = Client.ClickId + 4;
                        Client.ClickZ = Client.ClickId - 0x68;
                        Client.SeeId = 0x9b2a2c - 0x400000 + baseAdress;
                        Client.SeeCount = Client.SeeId + 4;
                        Client.SeeZ = Client.SeeId - 0x68;
                        Client.ClickContextMenuItemId = Client.SeeId;
                        //Client.ClickContextMenuItemGroundId = ?
                        Client.ClickContextMenuCreatureId = Client.ClickContextMenuItemId + 0xc;
                        Client.LoginServerStart = 0x821f98 - 0x400000 + baseAdress;
                        Client.StepLoginServer = 112;
                        Client.DistancePort = 100;
                        Client.MaxLoginServers = 10;
                        Client.RSA = 0x757280 - 0x400000 + baseAdress;
                        Client.LoginCharList = 0x8296b8 - 0x400000 + baseAdress;
                        Client.LoginCharListLength = 0x8296e8 - 0x400000 + baseAdress;
                        Client.LoginSelectedChar = 0x829738 - 0x400000 + baseAdress;
                        // Client.GameWindowRectPointer = &H67969C
                        // Client.GameWindowBar = &H80DB10
                        Client.DatPointer = 0x827030 - 0x400000 + baseAdress;
                        Client.EventTriggerPointer = 0x523750 - 0x400000 + baseAdress;
                        Client.DialogPointer = 0x81d45c - 0x400000 + baseAdress;
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

                        Container.Start = 0x86f6b8 - 0x400000 + baseAdress;
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

                        ContextMenus.AddContextMenuPtr = 0x4538e0 - 0x400000 + baseAdress;
                        ContextMenus.OnClickContextMenuPtr = 0x453fd0 - 0x400000 + baseAdress;
                        //ContextMenus.OnClickContextMenuVf = 0x5BFD48;
                        ContextMenus.AddSetOutfitContextMenu = 0x454626 - 0x400000 + baseAdress;
                        ContextMenus.AddPartyActionContextMenu = 0x45454c - 0x400000 + baseAdress;
                        ContextMenus.AddCopyNameContextMenu = 0x4546bd - 0x400000 + baseAdress;
                        ContextMenus.AddTradeWithContextMenu = 0x4542b6 - 0x400000 + baseAdress;
                        ContextMenus.AddLookContextMenu = 0x45418f - 0x400000 + baseAdress;

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

                        DrawItem.DrawItemFunc = 0x4b5a30 - 0x400000 + baseAdress;

                        DrawSkin.DrawSkinFunc = 0x4b96f0 - 0x400000 + baseAdress;

                        Hotkey.SendAutomaticallyStart = 0x8295b4 - 0x400000 + baseAdress;
                        Hotkey.SendAutomaticallyStep = 0x1;
                        Hotkey.TextStart = 0x8270e8 - 0x400000 + baseAdress;
                        Hotkey.TextStep = 0x100;
                        Hotkey.ObjectStart = 0x829600 - 0x400000 + baseAdress;
                        Hotkey.ObjectStep = 0x4;
                        Hotkey.ObjectUseTypeStart = 0x829518 - 0x400000 + baseAdress;
                        Hotkey.ObjectUseTypeStep = 0x4;
                        Hotkey.MaxHotkeys = 36;

                        Map.MapPointer = 0x871fb8 - 0x400000 + baseAdress;
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
                        Map.NameSpy1 = 0x4f245b - 0x400000 + baseAdress;
                        Map.NameSpy2 = 0x4f2465 - 0x400000 + baseAdress;
                        Map.NameSpy1Default = 0x4c75;
                        Map.NameSpy2Default = 0x4275;
                        Map.LevelSpy1 = 0x4eef7f - 0x400000 + baseAdress;
                        Map.LevelSpy2 = 0x4ef06b - 0x400000 + baseAdress;
                        Map.LevelSpy3 = 0x4ef0db - 0x400000 + baseAdress;
                        Map.LevelSpyPtr = Client.GameWindowRectPointer;
                        Map.LevelSpyAdd1 = 28;
                        Map.LevelSpyAdd2 = 0x5bc0;
                        Map.FullLightNop = 0x4F7229 - 0x400000 + baseAdress;
                        Map.FullLightAdr = 0x4F722E - 0x400000 + baseAdress;
                        Map.FullLightNopDefault = new byte[] {
	0x7e,
	0xA
};
                        Map.FullLightNopEdited = new byte[] {
	0x90,
	0x90
};
                        Map.FullLightAdrDefault = 0x80;
                        Map.FullLightAdrEdited = 0xff;

                        Player.Experience = 0x81ce10 - 0x400000 + baseAdress;
                        Player.Flags = 0x7e2cdc - 0x400000 + baseAdress;
                        Player.Id = 0x81ceac - 0x400000 + baseAdress;
                        Player.Health = 0x7e2cd4 - 0x400000 + baseAdress;
                        Player.HealthMax = 0x81cea0 - 0x400000 + baseAdress;
                        Player.Level = 0x81ce44 - 0x400000 + baseAdress;
                        Player.MagicLevel = 0x81ce4c - 0x400000 + baseAdress;
                        Player.LevelPercent = 0x81ce9c - 0x400000 + baseAdress;
                        Player.MagicLevelPercent = 0x81ce54 - 0x400000 + baseAdress;
                        Player.Mana = 0x81ce5c - 0x400000 + baseAdress;
                        Player.ManaMax = 0x7e2cdc - 0x400000 + baseAdress;
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
                        Player.RedSquare = 0x81ce58 - 0x400000 + baseAdress;

                        Player.SlotHead = 0x871f28 - 0x400000 + baseAdress;
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
                        Player.GoToX = 0x81cea8 - 0x400000 + baseAdress;
                        Player.GoToY = 0x81ce90 - 0x400000 + baseAdress;
                        Player.GoToZ = 0x7e2ce4 - 0x400000 + baseAdress;
                        //Player.AccessN = 0;
                        //Player.AccessS = 0;
                        Player.TargetId = Player.RedSquare;
                        Player.TargetBattlelistId = Player.TargetId - 8;
                        Player.TargetBattlelistType = Player.TargetId - 5;
                        Player.TargetType = Player.TargetId + 3;

                        Player.Z = 0x81d484 - 0x400000 + baseAdress;
                        Player.Y = 0x81d480 - 0x400000 + baseAdress;
                        Player.X = 0x81d478 - 0x400000 + baseAdress;

                        Player.AttackCount = 0x86c6e4 - 0x400000 + baseAdress;
                        Player.FollowCount = 0x7e2cd8 - 0x400000 + baseAdress;

                        TextDisplay.PrintName = 0x4f010f - 0x400000 + baseAdress;
                        TextDisplay.PrintFPS = 0x45a793 - 0x400000 + baseAdress;
                        TextDisplay.ShowFPS = 0x86c6e2 - 0x400000 + baseAdress;
                        TextDisplay.PrintTextFunc = 0x4b6e20 - 0x400000 + baseAdress;
                        TextDisplay.NopFPS = 0x45a6e9 - 0x400000 + baseAdress;

                        Vip.Start = 0x7e2cec - 0x400000 + baseAdress;
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