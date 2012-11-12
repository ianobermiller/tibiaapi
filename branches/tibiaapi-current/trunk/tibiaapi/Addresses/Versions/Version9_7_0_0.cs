using Tibia.Addresses;
using System.Diagnostics;
using System;
using System.Linq;

namespace Tibia
{
    public partial class Version
    {
        public static void SetVersion9_7_0_0(Process p)
        {
            uint BaseAddress = Convert.ToUInt32(p.MainModule.BaseAddress.ToInt32());

            BattleList.Start = 0x548008 + BaseAddress;
            BattleList.StepCreatures = 0xB0;
            BattleList.MaxCreatures = 1300;
            BattleList.End = BattleList.Start + (BattleList.StepCreatures * BattleList.MaxCreatures);

            Client.StartTime = 0;//    deprecated
            Client.XTeaKey = 0x3AAC60 + BaseAddress;
            Client.SocketStruct = 0x59BE1C + BaseAddress;
            Client.RecvPointer = 0x2F4940 + BaseAddress;
            Client.SendPointer = 0x2F4970 + BaseAddress;
            Client.LastRcvPacket = 0x4B3D60;
            Client.DecryptCall = 0x66330 + BaseAddress;
            Client.GetNextPacketCall = 0x118D20 + BaseAddress;
            Client.RecvStream = 0x5D45C4 + BaseAddress;
            Client.FrameRatePointer = 0x592008 + BaseAddress;
            Client.FrameRateCurrentOffset = 0x60;
            Client.FrameRateLimitOffset = 0x58;
            Client.MultiClient = 0x12EC57 + BaseAddress;
            Client.MultiClientJMP = 0xEB;
            Client.MultiClientJNZ = 0x75;
            Client.Status = 0x3BCCC4 + BaseAddress;
            Client.SafeMode = 0x3BCA57 + BaseAddress;
            Client.FollowMode = 0x3BA630 + BaseAddress;
            Client.AttackMode = 0x3BCA60 + BaseAddress;
            Client.ActionState = 0x3BCC70 + BaseAddress;
            Client.ActionStateFreezer = (new uint[] { 
                                        0X1270F63,//CMP DWORD PTR DS:[15FCC70],0C
                                        0X127E565,//CMP DWORD PTR DS:[15FCC70],6
                                        0X127E7D7,//CMP DWORD PTR DS:[15FCC70],6
                                        0X127F54E,//CMP DWORD PTR DS:[15FCC70],6
                                        0X127FA05,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X1282BA9,//CMP DWORD PTR DS:[15FCC70],6
                                        0X12839F4,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X128919D,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X128B690,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X128BA60,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X12A27F1,//CMP DWORD PTR DS:[15FCC70],0C
                                        0X12A2825,//CMP DWORD PTR DS:[15FCC70],0B
                                        0X12A285B,//CMP DWORD PTR DS:[15FCC70],0A
                                        0X12A42C1,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X12A4387,//MOV EDX,DWORD PTR DS:[15FCC70]
                                        0X12A4507,//MOV EDX,DWORD PTR DS:[15FCC70]
                                        0X12A45BF,//MOV EDX,DWORD PTR DS:[15FCC70]
                                        0X12A4640,//MOV ECX,DWORD PTR DS:[15FCC70]
                                        0X12A4971,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X12A4AB5,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X12A4D1E,//MOV ECX,DWORD PTR DS:[15FCC70]
                                        0X12A4E5F,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X12A5089,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X12A526E,//MOV ECX,DWORD PTR DS:[15FCC70]
                                        0X12A53B1,//CMP DWORD PTR DS:[15FCC70],ESI
                                        0X12A55F0,//MOV ECX,DWORD PTR DS:[15FCC70]
                                        0X12A58E6,//CMP DWORD PTR DS:[15FCC70],0A
                                        0X12A5923,//CMP DWORD PTR DS:[15FCC70],EBX
                                        0X12A7408,//CMP DWORD PTR DS:[15FCC70],EDI
                                        0X12C1B02,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X1330CD5,//CMP DWORD PTR DS:[15FCC70],EBX
                                        0X1330D5F,//MOV EDX,DWORD PTR DS:[15FCC70]
                                        0X1348D7F,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X136F7C8,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X1389351,//MOV EAX,DWORD PTR DS:[15FCC70]
                                        0X13893EA,//CMP DWORD PTR DS:[15FCC70],ECX
                                        }).Select(address => address - 0X1240000  + BaseAddress).ToArray();
            Client.StatusbarText = 0x3FE938 + BaseAddress;
            Client.StatusbarTime = 0x3FE934 + BaseAddress;
            Client.ClickId = 0x545468 + BaseAddress;
            Client.ClickCount = Client.ClickId - 4;
            Client.SeeId = 0x545434 + BaseAddress;
            Client.SeeCount = Client.SeeId - 4;
            Client.ClickContextMenuItemId = Client.SeeId;
            Client.ClickContextMenuCreatureId = Client.ClickContextMenuItemId + 0xc;
            Client.LoginServerStart = 0x3B34F8 + BaseAddress;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x324EC0 + BaseAddress;
            Client.LoginCharList = 0x3BCC90 + BaseAddress;
            Client.LoginAccount = Client.LoginCharList + 0xC;
            Client.LoginCharListLength = Client.LoginCharList + 0x30;
            Client.LoginSelectedChar = Client.LoginCharList + 0x80;
            Client.LoginPassword = Client.LoginCharList + 0xA4;

            Client.DatPointer = 0x3BA58C + BaseAddress;
            Client.EventTriggerPointer = 0x115A50 + BaseAddress;
            Client.DialogPointer = 0x3B34CC + BaseAddress;
            Client.DialogLeft = 0x14;
            Client.DialogTop = 0x18;
            Client.DialogWidth = 0x1C;
            Client.DialogHeight = 0x20;
            Client.DialogCaption = 0x54;
            Client.GameWindowRectPointer = 0x5D44D0 + BaseAddress;
            Client.GameWindowBar = 0x3FE930 + BaseAddress;//7FE92C
            Client.XORKey = 0x3B2E90 + BaseAddress;

            Container.Start = 0x4023C0 + BaseAddress;
            Container.StepContainer = 492;
            Container.MaxContainers = 16;
            Container.End = Container.Start + (Container.MaxContainers * Container.StepContainer);
            Container.MaxStack = 100;
            Container.DistanceHasParent = 0;
            Container.DistanceId = 0 + 12;
            Container.DistanceName = 4 + 12;
            Container.DistanceAmount = 36 + 12;
            Container.DistanceIsOpen = 40 + 12;
            Container.DistanceVolume = 44 + 12;
            Container.DistanceItemCount = 52 + 12;
            Container.DistanceItemId = 56 + 12;
            Container.StepSlot = 12;

            ContextMenus.AddContextMenuPtr = 0x5BDA0 + BaseAddress;
            ContextMenus.OnClickContextMenuPtr = 0x5CE60 + BaseAddress;
            ContextMenus.OnClickContextMenuVf = 0x326FA0 + BaseAddress;
            ContextMenus.AddSetOutfitContextMenu = 0x5CBB1 + BaseAddress;
            ContextMenus.AddPartyActionContextMenu = 0x5CA3D + BaseAddress;
            ContextMenus.AddCopyNameContextMenu = 0x5DB47 + BaseAddress;
            ContextMenus.AddTradeWithContextMenu = 0x5C836 + BaseAddress;
            ContextMenus.AddLookContextMenu = 0x5C70F + BaseAddress;


            Creature.DistanceId = 0;
            Creature.DistanceType = 3;
            Creature.DistanceName = 4;
            Creature.DistanceZ = 36;
            Creature.DistanceY = 40;
            Creature.DistanceX = 44;
            Creature.DistanceScreenOffsetVert = 48;
            Creature.DistanceScreenOffsetHoriz = 52;
            Creature.DistanceFaceDirection = 56;

            Creature.DistanceIsWalking = 80;
            Creature.DistanceWalkDirection = 84;
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
            Creature.DistanceIsBlocking = 148;
            Creature.DistanceSkull = 152;
            Creature.DistanceParty = 156;
            Creature.DistanceWarIcon = 168;
            Creature.DistanceIsVisible = 172;


            DatItem.StepItems = 136;
            DatItem.MarketName = 0;
            DatItem.Width = 32;
            DatItem.Height = 36;
            DatItem.MaxSizeInPixels = 40;
            DatItem.Layers = 44;
            DatItem.PatternX = 48;
            DatItem.PatternY = 52;
            DatItem.PatternDepth = 56;
            DatItem.Phase = 60;
            DatItem.Sprite = 64;
            DatItem.Flags = 68;
            DatItem.WalkSpeed = 76;
            DatItem.TextLimit = 80;
            DatItem.LightRadius = 84;
            DatItem.LightColor = 88;
            DatItem.ShiftX = 92;
            DatItem.ShiftY = 96;
            DatItem.WalkHeight = 100;
            DatItem.Automap = 104;
            DatItem.LensHelp = 108;
            DatItem.ClothSlot = 112;
            DatItem.MarketCategory = 116;
            DatItem.MarketTradeAs = 120;
            DatItem.MarketShowAs = 124;
            DatItem.MarketRestrictProfession = 128;
            DatItem.MarketRestrictLevel = 132;

            DrawItem.DrawItemFunc = 0xC8C40 + BaseAddress;

            DrawSkin.DrawSkinFunc = 0xCFD10 + BaseAddress;

            Hotkey.SendAutomaticallyStart = 0x3BCB08 + BaseAddress;
            Hotkey.SendAutomaticallyStep = 0x01;
            Hotkey.TextStart = 0x3BA638 + BaseAddress;
            Hotkey.TextStep = 0x100;
            Hotkey.ObjectStart = 0x3BCBD8 + BaseAddress;
            Hotkey.ObjectStep = 0x04;
            Hotkey.ObjectUseTypeStart = 0x3BCA68 + BaseAddress;
            Hotkey.ObjectUseTypeStep = 0x04;
            Hotkey.MaxHotkeys = 36;


            Map.MapPointer = 0x5D4594 + BaseAddress;
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
            Map.NameSpy1 = 0x10E58C + BaseAddress;
            Map.NameSpy2 = 0x10E599 + BaseAddress;
            Map.NameSpy1Default = 0x5075;
            Map.NameSpy2Default = 0x4375;
            Map.LevelSpy1 = 0x10A3DF + BaseAddress;
            Map.LevelSpy2 = 0x10A4D7 + BaseAddress;
            Map.LevelSpy3 = 0x10A553 + BaseAddress;
            Map.LevelSpyDefault = new byte[] { 0x89, 0x86, 0xC0, 0x5B, 0x00, 0x00 };
            Map.LevelSpyPtr = Client.GameWindowRectPointer;
            Map.LevelSpyAdd1 = 28;
            Map.LevelSpyAdd2 = 0x5BC0;
            Map.FullLightNop = 0x1129A9 + BaseAddress;
            Map.FullLightAdr = 0x1129AE + BaseAddress;
            Map.FullLightNopDefault = new byte[] { 0x7E, 0x0A };
            Map.FullLightNopEdited = new byte[] { 0x90, 0x90 };
            Map.FullLightAdrDefault = 0x80;
            Map.FullLightAdrEdited = 0xFF;

            Player.Experience = 0x3B2EA0 + BaseAddress;
            Player.Flags = 0x3B2E54 + BaseAddress;
            Player.Id = 0x57FEA4 + BaseAddress;
            Player.Health = 0x548000 + BaseAddress;
            Player.HealthMax = 0x57FE9C + BaseAddress;
            Player.Level = 0x3B2ECC + BaseAddress;
            Player.MagicLevel = 0x3B2ED4 + BaseAddress;
            Player.LevelPercent = 0x3B2F14 + BaseAddress;
            Player.MagicLevelPercent = 0x3B2EDC + BaseAddress;
            Player.Mana = 0x3B2EE4 + BaseAddress;
            Player.ManaMax = 0x3B2E94 + BaseAddress;
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

            Player.WhiteSquare = 0;
            Player.GreenSquare = 0x3B2E88 + BaseAddress;
            Player.RedSquare = 0x3B2EE0 + BaseAddress;

            Player.SlotAmmo = 0x5D450C + BaseAddress;
            Player.SlotRing = Player.SlotHead + 12;
            Player.SlotFeet = Player.SlotRing + 12;
            Player.SlotLegs = Player.SlotFeet + 12;
            Player.SlotRight = Player.SlotLegs + 12;
            Player.SlotLeft = Player.SlotRight + 12;
            Player.SlotArmor = Player.SlotLeft + 12;
            Player.SlotBackpack = Player.SlotArmor + 12;
            Player.SlotNeck = Player.SlotBackpack + 12;
            Player.MaxSlots = 10;
            Player.DistanceSlotCount = 4;
            Player.CurrentTileToGo = Player.Flags + 132;
            Player.TilesToGo = Player.CurrentTileToGo + 4;
            Player.GoToX = 0x57FEA0 + BaseAddress;
            Player.GoToY = 0x57FE98 + BaseAddress;
            Player.GoToZ = 0x548004 + BaseAddress;
            Player.TargetId = Player.RedSquare;
            Player.TargetBattlelistId = Player.TargetId - 8;
            Player.TargetBattlelistType = Player.TargetId - 5;
            Player.TargetType = Player.TargetId + 3;

            Player.X = 0x57FEA8 + BaseAddress;
            Player.Y = 0x57FEAC + BaseAddress;
            Player.Z = 0x57FEB0 + BaseAddress;

            Player.AttackCount = 0x5CE2F4 + BaseAddress;
            Player.FollowCount = 0x4B2E50 + BaseAddress;

            TextDisplay.PrintName = 0x10B5C1 + BaseAddress;
            TextDisplay.PrintFPS = 0x63696 + BaseAddress;
            TextDisplay.ShowFPS = 0x595937 + BaseAddress;
            TextDisplay.PrintTextFunc = 0xCB8B0 + BaseAddress;
            TextDisplay.NopFPS = 0x63501 + BaseAddress;

            Vip.MarkerNodePtr = 0x545CC8 + BaseAddress;
            Vip.Count = Vip.MarkerNodePtr + 4;
            Vip.DistancePreviousNode = 0;
            Vip.DistanceNextNode = 4;
            Vip.DistanceId = 16;
            Vip.DistanceIcon = 20;
            Vip.DistanceNotify = 21;
            Vip.DistanceNameField = 24;
            Vip.DistanceDescriptionField = 52;
            Vip.DistanceStatus = 80;
            Vip.DistanceText = 0;
            Vip.DistanceTextLength = 16;
            Vip.DistanceFieldType = 20;
        }
    }
}