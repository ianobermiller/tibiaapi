using Tibia.Addresses;
using System.Diagnostics;
using System;
using System.Linq;

namespace Tibia
{
    public partial class Version
    {
        public static void SetVersion9_7_1_0(Process p)
        {
            uint BaseAddress = Convert.ToUInt32(p.MainModule.BaseAddress.ToInt32());

            BattleList.Start = 0x549008 + BaseAddress;
            BattleList.StepCreatures = 0xB0;
            BattleList.MaxCreatures = 1300;
            BattleList.End = BattleList.Start + (BattleList.StepCreatures * BattleList.MaxCreatures);

            Client.StartTime = 0;//    deprecated
            Client.XTeaKey = 0x3ABF6C + BaseAddress;
            Client.SocketStruct = 0x59C9AC + BaseAddress;
            Client.RecvPointer = 0x2F5940 + BaseAddress;
            Client.SendPointer = 0x2F5970 + BaseAddress;
            Client.LastRcvPacket = 0;//    deprecated
            Client.DecryptCall = 0x67FD7 + BaseAddress;
            Client.GetNextPacketCall = 0x11ACC0 + BaseAddress;
            Client.RecvStream = 0x5D5394 + BaseAddress;
            Client.FrameRatePointer = 0x593008 + BaseAddress;
            Client.FrameRateCurrentOffset = 0x60;
            Client.FrameRateLimitOffset = 0x58;
            Client.MultiClient = 0x1309C7 + BaseAddress;
            Client.MultiClientJMP = 0xEB;
            Client.MultiClientJNZ = 0x75;
            Client.Status = 0x3BDCDC + BaseAddress;
            Client.SafeMode = 0x3BDA97 + BaseAddress;
            Client.FollowMode = 0x3BB670 + BaseAddress;
            Client.AttackMode = 0x3BDAA0 + BaseAddress;
            Client.ActionState = 0x3BDCB0 + BaseAddress;
            Client.ActionStateFreezer = (new uint[] { 
                                        0X1202C03,//CMP DWORD PTR DS:[158DCB0],0C             DS:[0158DCB0]=00000000
                                        0X12101A5,//CMP DWORD PTR DS:[158DCB0],6              DS:[0158DCB0]=00000000
                                        0X1210417,//CMP DWORD PTR DS:[158DCB0],6              DS:[0158DCB0]=00000000
                                        0X121118E,//CMP DWORD PTR DS:[158DCB0],6              DS:[0158DCB0]=00000000
                                        0X1211645,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X12147E9,//CMP DWORD PTR DS:[158DCB0],6              DS:[0158DCB0]=00000000
                                        0X1215634,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X121ADDD,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X121D2D0,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X121D6A0,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X1234421,//CMP DWORD PTR DS:[158DCB0],0C             DS:[0158DCB0]=00000000
                                        0X1234455,//CMP DWORD PTR DS:[158DCB0],0B             DS:[0158DCB0]=00000000
                                        0X123448B,//CMP DWORD PTR DS:[158DCB0],0A             DS:[0158DCB0]=00000000
                                        0X1235F11,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X1235FD7,//MOV EDX,DWORD PTR DS:[158DCB0]            DS:[0158DCB0]=00000000
                                        0X1236157,//MOV EDX,DWORD PTR DS:[158DCB0]            DS:[0158DCB0]=00000000
                                        0X123620F,//MOV EDX,DWORD PTR DS:[158DCB0]            DS:[0158DCB0]=00000000
                                        0X1236290,//MOV ECX,DWORD PTR DS:[158DCB0]            DS:[0158DCB0]=00000000
                                        0X12365C1,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X1236705,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X123696E,//MOV ECX,DWORD PTR DS:[158DCB0]            DS:[0158DCB0]=00000000
                                        0X1236AAF,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X1236CD9,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X1236EBE,//MOV ECX,DWORD PTR DS:[158DCB0]            DS:[0158DCB0]=00000000
                                        0X1237001,//CMP DWORD PTR DS:[158DCB0],ESI
                                        0X1237240,//MOV ECX,DWORD PTR DS:[158DCB0]            DS:[0158DCB0]=00000000
                                        0X1237536,//CMP DWORD PTR DS:[158DCB0],0A             DS:[0158DCB0]=00000000
                                        0X1237573,//CMP DWORD PTR DS:[158DCB0],EBX
                                        0X1239138,//CMP DWORD PTR DS:[158DCB0],EDI
                                        0X12536B2,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X12C2C35,//CMP DWORD PTR DS:[158DCB0],EBX
                                        0X12C2CBF,//MOV EDX,DWORD PTR DS:[158DCB0]            DS:[0158DCB0]=00000000
                                        0X12DAD9F,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X1301538,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X131B0D1,//MOV EAX,DWORD PTR DS:[158DCB0]            [0158DCB0]=00000000
                                        0X131B16A,//CMP DWORD PTR DS:[158DCB0],ECX
                                        }).Select(address => address - 0X11D0000  + BaseAddress).ToArray();
            Client.StatusbarText = 0x3FF930 + BaseAddress;
            Client.StatusbarTime = 0x3FF928 + BaseAddress;
            Client.ClickId = 0x546460 + BaseAddress;
            Client.ClickCount = Client.ClickId - 4;
            Client.SeeId = 0x54642C + BaseAddress;
            Client.SeeCount = Client.SeeId - 4;
            Client.ClickContextMenuItemId = Client.SeeId;
            Client.ClickContextMenuCreatureId = 0x3BDD38 + BaseAddress;
            Client.LoginServerStart = 0x3B4538 + BaseAddress;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x325EA8 + BaseAddress;

            Client.LoginStruct = 0x546CB0 + BaseAddress;
            Client.LoginCharListBegin = Client.LoginStruct;
            Client.LoginCharListEnd = Client.LoginStruct + 4;
            Client.LoginAccount = Client.LoginStruct + 20;
            Client.LoginPassword = Client.LoginStruct + 48;
            Client.LoginSelectedChar = Client.LoginStruct + 76;
            Client.LoginCharListDistanceCharName = 4;
            Client.LoginCharListDistanceWorldName = 32;
            Client.LoginCharListDistanceIsPreview = 60;
            Client.LoginCharListDistanceWorldIP = 64;
            Client.LoginCharListDistanceWorldPort = 68;
            Client.LoginCharListStepCharacter = 72;

            Client.DatPointer = 0x3BB5CC + BaseAddress;
            Client.EventTriggerPointer = 0x117A70 + BaseAddress;
            Client.DialogPointer = 0x3B450C + BaseAddress;
            Client.DialogLeft = 0x14;
            Client.DialogTop = 0x18;
            Client.DialogWidth = 0x1C;
            Client.DialogHeight = 0x20;
            Client.DialogCaption = 0x54;
            Client.GameWindowRectPointer = 0x5D52A0 + BaseAddress;
            Client.GameWindowBar = 0x3FF924 + BaseAddress;

            Container.Start = 0x4033B8 + BaseAddress;
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

            ContextMenus.AddContextMenuPtr = 0x5DA90 + BaseAddress;
            ContextMenus.OnClickContextMenuPtr = 0x5EAF0 + BaseAddress;
            ContextMenus.OnClickContextMenuVf = 0x328268 + BaseAddress;
            ContextMenus.AddSetOutfitContextMenu = 0x5E8A1 + BaseAddress;
            ContextMenus.AddCopyNameContextMenu = 0x5E938 + BaseAddress;
            ContextMenus.AddTradeWithContextMenu = 0x5E526 + BaseAddress;
            ContextMenus.AddLookContextMenu = 0x5E3FF + BaseAddress;


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


            DrawItem.DrawItemFunc = 0xCAAE0 + BaseAddress;
            DrawSkin.DrawSkinFunc = 0xD1BD0 + BaseAddress;


            Hotkey.SendAutomaticallyStart = 0x3BDB48 + BaseAddress;
            Hotkey.SendAutomaticallyStep = 0x01;
            Hotkey.TextStart = 0x3BB678 + BaseAddress;
            Hotkey.TextStep = 0x100;
            Hotkey.ObjectStart = 0x3BDC18 + BaseAddress;
            Hotkey.ObjectStep = 0x04;
            Hotkey.ObjectUseTypeStart = 0x3BDAA8 + BaseAddress;
            Hotkey.ObjectUseTypeStep = 0x04;
            Hotkey.MaxHotkeys = 36;


            Map.MapPointer = 0x5D5364 + BaseAddress;
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
            Map.NameSpy1 = 0x1105AC + BaseAddress;
            Map.NameSpy2 = 0x1105B9 + BaseAddress;
            Map.NameSpy1Default = 0x5075;
            Map.NameSpy2Default = 0x4375;
            Map.LevelSpy1 = 0x10C3FF + BaseAddress;
            Map.LevelSpy2 = 0x10C4F7 + BaseAddress;
            Map.LevelSpy3 = 0x10C573 + BaseAddress;
            Map.LevelSpyDefault = new byte[] { 0x89, 0x86, 0xC0, 0x5B, 0x00, 0x00 };
            Map.LevelSpyPtr = Client.GameWindowRectPointer;
            Map.LevelSpyAdd1 = 28;
            Map.LevelSpyAdd2 = 0x5BC0;
            Map.FullLightNop = 0x1149C9 + BaseAddress;
            Map.FullLightAdr = 0x1149CE + BaseAddress;
            Map.FullLightNopDefault = new byte[] { 0x7E, 0x0A };
            Map.FullLightNopEdited = new byte[] { 0x90, 0x90 };
            Map.FullLightAdrDefault = 0x80;
            Map.FullLightAdrEdited = 0xFF;

            Player.Experience = 0x3B3EE0 + BaseAddress;
            Player.Flags = 0x3B3E94 + BaseAddress;
            Player.Id = 0x580EA4 + BaseAddress;
            Player.Health = 0x549000 + BaseAddress;
            Player.HealthMax = 0x580E9C + BaseAddress;
            Player.Level = 0x3B3F0C + BaseAddress;
            Player.MagicLevel = 0x3B3F14 + BaseAddress;
            Player.LevelPercent = 0x3B3F54 + BaseAddress;
            Player.MagicLevelPercent = 0x3B3F1C + BaseAddress;
            Player.Mana = 0x3B3F24 + BaseAddress;
            Player.ManaMax = 0x3B3ED4 + BaseAddress;
            Player.Soul = 0x3B3F10 + BaseAddress;
            Player.OfflineTraining = 0x3B3EC4 + BaseAddress;
            Player.Stamina = 0x3B3F58 + BaseAddress;
            Player.Capacity = 0x580E94 + BaseAddress;
            Player.XORKey = 0x3B3ED0 + BaseAddress;

            Player.FistPercent = 0x3B3F2C + BaseAddress;
            Player.ClubPercent = Player.FistPercent + 4;
            Player.SwordPercent = Player.FistPercent + 8;
            Player.AxePercent = Player.FistPercent + 12;
            Player.DistancePercent = Player.FistPercent + 16;
            Player.ShieldingPercent = Player.FistPercent + 20;
            Player.FishingPercent = Player.FistPercent + 24;
            Player.Fist = 0x580E78 + BaseAddress;
            Player.Club = Player.Fist + 4;
            Player.Sword = Player.Club + 4;
            Player.Axe = Player.Sword + 4;
            Player.Distance = Player.Axe + 4;
            Player.Shielding = Player.Distance + 4;
            Player.Fishing = Player.Shielding + 4;

            Player.WhiteSquare = 0;
            Player.GreenSquare = 0x3B3EC8 + BaseAddress;
            Player.RedSquare = 0x3B3F20 + BaseAddress;

            Player.SlotAmmo = 0x5D52DC + BaseAddress;
            Player.SlotRing = Player.SlotAmmo + Player.SlotStep;
            Player.SlotFeet = Player.SlotRing + Player.SlotStep;
            Player.SlotLegs = Player.SlotFeet + Player.SlotStep;
            Player.SlotLeft = Player.SlotLegs + Player.SlotStep;
            Player.SlotRight = Player.SlotLeft + Player.SlotStep;
            Player.SlotArmor = Player.SlotLeft + Player.SlotStep;
            Player.SlotBackpack = Player.SlotArmor + Player.SlotStep;
            Player.SlotNeck = Player.SlotBackpack + Player.SlotStep;
            Player.SlotHead = Player.SlotNeck + Player.SlotStep;
            Player.SlotBegin = Player.SlotAmmo;
            Player.SlotStep = 12;
            Player.MaxSlots = 10;
            Player.DistanceSlotCount = 4;
            Player.CurrentTileToGo = Player.Flags + 132;
            Player.TilesToGo = Player.CurrentTileToGo + 4;
            Player.GoToX = 0x580EA0 + BaseAddress;
            Player.GoToY = 0x580E98 + BaseAddress;
            Player.GoToZ = 0x549004 + BaseAddress;
            Player.TargetId = Player.RedSquare;

            Player.X = 0x580EA8 + BaseAddress;
            Player.Y = 0x580EAC + BaseAddress;
            Player.Z = 0x580EB0 + BaseAddress;

            Player.AttackCount = 0x5CEEE4 + BaseAddress;
            Player.FollowCount = Player.AttackCount;

            TextDisplay.PrintName = 0x10D5E1 + BaseAddress;
            TextDisplay.PrintFPS = 0x652D6 + BaseAddress;
            TextDisplay.PrintTextFunc = 0xCD750 + BaseAddress;
            TextDisplay.ShowFPS = 0x59689F + BaseAddress;
            TextDisplay.NopFPS = 0x65141 + BaseAddress;

            Vip.MarkerNodePtr = 0x546D04 + BaseAddress;
            Vip.Count = Vip.MarkerNodePtr + 4;
            Vip.DistancePreviousNode = 0;
            Vip.DistanceNextNode = 4;
            Vip.DistanceId = 16;
            Vip.DistanceIcon = 20;
            Vip.DistanceNotify = 21;
            Vip.DistanceNameField = 24;
            Vip.DistanceDescriptionField = 52;
            Vip.DistanceStatus = 80;
        }
    }
}