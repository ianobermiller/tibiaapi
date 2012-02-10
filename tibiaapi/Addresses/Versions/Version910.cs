using Tibia.Addresses;
using System.Diagnostics;
using System;

namespace Tibia
{
    public partial class Version
    {
        public static void SetVersion910(Process p)
        {
            uint BaseAddress = Convert.ToUInt32(p.MainModule.BaseAddress.ToInt32());

            BattleList.Start = 0x3E4F98 + BaseAddress;
            BattleList.StepCreatures = 0xB0;
            BattleList.MaxCreatures = 1300;
            BattleList.End = BattleList.Start + (BattleList.StepCreatures * BattleList.MaxCreatures);

            Client.StartTime = 0x5B3CD8;
            Client.XTeaKey = 0x3DAD4C + BaseAddress;
            Client.SocketStruct = 0x46C910 + BaseAddress;
            Client.RecvPointer = 0x2C4928 + BaseAddress;
            Client.SendPointer = 0x2C4958 + BaseAddress;
            Client.LastRcvPacket = 0x422800 + BaseAddress;
            Client.DecryptCall = 0x5D230 + BaseAddress;
            Client.ParserFunc = 0x5D1F0 + BaseAddress;
            Client.GetNextPacketCall = 0xFC0C0 + BaseAddress;
            Client.RecvStream = 0x5B2690 + BaseAddress;
            Client.FrameRatePointer = 0x46D448 + BaseAddress;
            Client.FrameRateCurrentOffset = 0x60;
            Client.FrameRateLimitOffset = 0x58;
            Client.MultiClient = 0x10F221 + BaseAddress;
            Client.MultiClientJMP = 0xEB;
            Client.MultiClientJNZ = 0x75;
            Client.Status = 0x4296EC + BaseAddress;
            Client.SafeMode = 0x429506 + BaseAddress;
            Client.FollowMode = 0x4270D8 + BaseAddress;
            Client.AttackMode = 0x42950C + BaseAddress;
            Client.ActionState = 0x429698 + BaseAddress;
            Client.ActionStateFreezer = 0x123E70 + BaseAddress;
            Client.ActionStateOriginal = new byte[] { 0xA3, 0x00, 0x00, 0x00, 0x00, 0xC3, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC };
            Client.ActionStateFreezed = new byte[] { 0xC7, 0x05, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0xC3 };
            Client.StatusbarText = 0x46B360 + BaseAddress;
            Client.StatusbarTime = 0x46B35C + BaseAddress;
            Client.ClickId = 0x5B34B4 + BaseAddress;
            Client.ClickCount = Client.ClickId + 4;
            Client.ClickZ = Client.ClickId - 0x68;
            Client.SeeId = 0x5B2A2C + BaseAddress;
            Client.SeeCount = Client.SeeId + 4;
            Client.SeeZ = Client.SeeId - 0x68;
            Client.ClickContextMenuItemId = Client.SeeId;
            Client.ClickContextMenuCreatureId = Client.ClickContextMenuItemId + 0xc;
            Client.LoginServerStart = 0x421F98 + BaseAddress;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x357280 + BaseAddress;
            Client.LoginCharList = 0x4296B8 + BaseAddress;
            Client.LoginCharListLength = 0x4296E8 + BaseAddress;
            Client.LoginSelectedChar = 0x429738 + BaseAddress;
            //Client.GameWindowRectPointer = 0x3AC5C4;
            //Client.GameWindowBar = 0xD1BDC;
            Client.DatPointer = 0x427030 + BaseAddress;
            Client.EventTriggerPointer = 0x123750 + BaseAddress;
            Client.DialogPointer = 0x41D45C + BaseAddress;
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

            Container.Start = 0x46F6B8 + BaseAddress;
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

            ContextMenus.AddContextMenuPtr = 0x538E0 + BaseAddress;
            ContextMenus.OnClickContextMenuPtr = 0x53FD0 + BaseAddress;
            ContextMenus.OnClickContextMenuVf = 0x35E684 + BaseAddress;
            ContextMenus.AddSetOutfitContextMenu = 0x54626 + BaseAddress;
            ContextMenus.AddPartyActionContextMenu = 0x5454C + BaseAddress;
            ContextMenus.AddCopyNameContextMenu = 0x546BD + BaseAddress;
            ContextMenus.AddTradeWithContextMenu = 0x542B6 + BaseAddress;
            ContextMenus.AddLookContextMenu = 0x5418F + BaseAddress;

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

            DrawItem.DrawItemFunc = 0xB5A30 + BaseAddress;

            DrawSkin.DrawSkinFunc = 0xB96F0 + BaseAddress;

            Hotkey.SendAutomaticallyStart = 0x4295B4 + BaseAddress;
            Hotkey.SendAutomaticallyStep = 0x01;
            Hotkey.TextStart = 0x4270E8 + BaseAddress;
            Hotkey.TextStep = 0x100;
            Hotkey.ObjectStart = 0x429600 + BaseAddress;
            Hotkey.ObjectStep = 0x04;
            Hotkey.ObjectUseTypeStart = 0x429518 + BaseAddress;
            Hotkey.ObjectUseTypeStep = 0x04;
            Hotkey.MaxHotkeys = 36;

            Map.MapPointer = 0x471FB8 + BaseAddress;
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
            Map.NameSpy1 = 0xF245B + BaseAddress;
            Map.NameSpy2 = 0xF2465 + BaseAddress;
            Map.NameSpy1Default = 0x4C75;
            Map.NameSpy2Default = 0x4275;
            Map.LevelSpy1 = 0xEEF7F + BaseAddress;
            Map.LevelSpy2 = 0xEF06B + BaseAddress;
            Map.LevelSpy3 = 0xEF0DB + BaseAddress;
            Map.LevelSpyPtr = Client.GameWindowRectPointer;
            Map.LevelSpyAdd1 = 28;
            Map.LevelSpyAdd2 = 0x5BC0;
            Map.FullLightNop = 0xF7229 + BaseAddress;
            Map.FullLightAdr = 0xF722E + BaseAddress;
            Map.FullLightNopDefault = new byte[] { 0x7E, 0x0A };
            Map.FullLightNopEdited = new byte[] { 0x90, 0x90 };
            Map.FullLightAdrDefault = 0x80;
            Map.FullLightAdrEdited = 0xFF;

            Player.Experience = 0x41CE10 + BaseAddress;
            Player.Flags = 0x3E2CDC + BaseAddress;
            Player.Id = 0x41CEAC + BaseAddress;
            Player.Health = 0x3E2CD4 + BaseAddress;
            Player.HealthMax = 0x41CEA0 + BaseAddress;
            Player.Level = 0x41CE44 + BaseAddress;
            Player.MagicLevel = 0x41CE4C + BaseAddress;
            Player.LevelPercent = 0x41CE9C + BaseAddress;
            Player.MagicLevelPercent = 0x41CE54 + BaseAddress;
            Player.Mana = 0x41CE5C + BaseAddress;
            Player.ManaMax = 0x3E2CDC + BaseAddress;
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
            Player.RedSquare = 0x41CE58 + BaseAddress;

            Player.SlotHead = 0x471F28 + BaseAddress;
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
            Player.GoToX = 0x41CEA8 + BaseAddress;
            Player.GoToY = 0x41CE90 + BaseAddress;
            Player.GoToZ = 0x3E2CE4 + BaseAddress;
            Player.TargetId = Player.RedSquare;
            Player.TargetBattlelistId = Player.TargetId - 8;
            Player.TargetBattlelistType = Player.TargetId - 5;
            Player.TargetType = Player.TargetId + 3;

            Player.Z = 0x41D484 + BaseAddress;
            Player.Y = 0x41D480 + BaseAddress;
            Player.X = 0x41D478 + BaseAddress;

            Player.AttackCount = 0x46C6E4 + BaseAddress;
            Player.FollowCount = 0x3E2CD8 + BaseAddress;

            TextDisplay.PrintName = 0xF010F + BaseAddress;
            TextDisplay.PrintFPS = 0x5A793 + BaseAddress;
            TextDisplay.ShowFPS = 0x46C6E2 + BaseAddress;
            TextDisplay.PrintTextFunc = 0xB6E20 + BaseAddress;
            TextDisplay.NopFPS = 0x5A6E9 + BaseAddress;

            Vip.Start = 0x3E2CEC + BaseAddress;
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