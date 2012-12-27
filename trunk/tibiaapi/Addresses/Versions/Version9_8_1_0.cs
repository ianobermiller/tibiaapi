using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;

namespace Tibia.Objects
{
    public partial class AddressesCollection
    {
        public void SetVersion9_8_1_0(uint baseAddress)
        {
            Type creatureStructure = typeof(Tibia.Version.v971.Creature);
            Type containerHeaderStructure = typeof(Tibia.Version.v971.ContainerHeader);
            Type simpleItemStructure = typeof(Tibia.Version.v971.SimpleItem);
            Type vipNodeStructure = typeof(Tibia.Version.v971.VipNode);
            Type playerSkillStructure = typeof(Tibia.Version.v971.PlayerSkills);
            Type playerProperties1Structure = typeof(Tibia.Version.v971.PlayerProperties1);
            Type playerProperties2Structure = typeof(Tibia.Version.v971.PlayerProperties2);
            Type playerSlotsStructure = typeof(Tibia.Version.v971.PlayerSlots);
            Type datMemoryStructure = typeof(Tibia.Version.v971.DatMemory);
            Type charListEntryStructure = typeof(Tibia.Version.v971.CharListEntry);
            Type loginInfoStructure = typeof(Tibia.Version.v971.LoginInfo);
            Type mapTileStructure = typeof(Tibia.Version.v971.MapTile);


            #region BattleList
            BattleList.Start = 0x54C008 + baseAddress;
            BattleList.StepCreatures = (uint)Marshal.SizeOf(creatureStructure);
            BattleList.MaxCreatures = 1300;
            BattleList.End = BattleList.Start + BattleList.MaxCreatures * BattleList.StepCreatures;
            #endregion

            #region Client
            Client.StartTime = 0;//    deprecated
            Client.XTeaKey = 0x3AEF6C + baseAddress;
            Client.SocketStruct = 0x59FF30 + baseAddress;
            Client.RecvPointer = 0x2F7940 + baseAddress;
            Client.SendPointer = 0x2F7970 + baseAddress;
            Client.ParserFunc = 0x68bd0 + baseAddress;
            Client.DecryptCall = 0x68c17 + baseAddress;
            Client.GetNextPacketCall = 0x68c17 + baseAddress;//0x11ACC0 /same as decrypt call
            Client.RecvStream = 0x5DA5F4 + baseAddress;
            Client.FrameRatePointer = 0x5960CC + baseAddress;
            Client.FrameRateCurrentOffset = 0x60;
            Client.FrameRateLimitOffset = 0x58;
            Client.MultiClient = 0x132347 + baseAddress;
            Client.MultiClientJMP = 0xEB;
            Client.MultiClientJNZ = 0x75;
            Client.Status = 0x3C0CF8 + baseAddress;
            Client.SafeMode =   0x3c0ab4 + baseAddress;
            Client.FollowMode = 0x3BE690 + baseAddress;
            Client.AttackMode = 0x3C0AC0 + baseAddress;
            Client.ActionState = 0x3C0CCC + baseAddress;
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
                                        }).Select(address => address - 0X11D0000 + baseAddress).ToArray();
            Client.StatusbarTime = 0x402944 + baseAddress;
            Client.StatusbarText = 0x402948 + baseAddress;
            Client.SeeId = 0x549498 + baseAddress;
            Client.SeeCount = Client.SeeId - 4;
            Client.ClickId = 0x549464 + baseAddress;
            Client.ClickCount = Client.ClickId - 4;
            Client.ClickContextMenuItemId = Client.SeeId;
            Client.ClickContextMenuCreatureId = 0x3C0D54 + baseAddress;
            Client.LoginServerStart = 0x3B7558 + baseAddress;
            Client.StepLoginServer = 112;
            Client.DistancePort = 100;
            Client.MaxLoginServers = 10;
            Client.RSA = 0x327FB8 + baseAddress;

            Client.LoginStruct = 0x549CE8 + baseAddress;
            Client.LoginCharListBegin = (uint)(Client.LoginStruct + Marshal.OffsetOf(loginInfoStructure, "charListBegin").ToInt32());
            Client.LoginCharListEnd = (uint)(Client.LoginStruct + Marshal.OffsetOf(loginInfoStructure, "charListEnd").ToInt32());
            Client.LoginAccount = (uint)(Client.LoginStruct + Marshal.OffsetOf(loginInfoStructure, "accountTextField").ToInt32());
            Client.LoginPassword = (uint)(Client.LoginStruct + Marshal.OffsetOf(loginInfoStructure, "passwordTextField").ToInt32());
            Client.LoginSelectedChar = (uint)(Client.LoginStruct + Marshal.OffsetOf(loginInfoStructure, "selectedChar").ToInt32());
            Client.LoginCharListDistanceCharName = (uint)Marshal.OffsetOf(charListEntryStructure, "charNameTextField");
            Client.LoginCharListDistanceWorldName = (uint)Marshal.OffsetOf(charListEntryStructure, "worldNameTextField");
            Client.LoginCharListDistanceIsPreview = (uint)Marshal.OffsetOf(charListEntryStructure, "isPreview");
            Client.LoginCharListDistanceWorldIP = (uint)Marshal.OffsetOf(charListEntryStructure, "worldIP");
            Client.LoginCharListDistanceWorldPort = (uint)Marshal.OffsetOf(charListEntryStructure, "worldPort");
            Client.LoginCharListStepCharacter = (uint)Marshal.SizeOf(charListEntryStructure);

            Client.DatPointer = 0x3BE5EC + baseAddress;
            Client.EventTriggerPointer = 0x117A70 + baseAddress;
            Client.DialogPointer = 0x3B752C + baseAddress;
            Client.DialogLeft = 0x14;
            Client.DialogTop = 0x18;
            Client.DialogWidth = 0x1C;
            Client.DialogHeight = 0x20;
            Client.DialogCaption = 0x54;
            Client.GameWindowRectPointer = 0x5DA500 + baseAddress;
            Client.GameWindowBar = 0x402940 + baseAddress;
            #endregion

            #region Container
            Container.Start = 0x4063E0 + baseAddress;
            Container.MaxSlots = 36;
            Container.MaxContainers = 16;
            Container.MaxStack = 100;

            Container.StepSlot = (uint)Marshal.SizeOf(simpleItemStructure);
            Container.StepContainer = (uint)Marshal.SizeOf(containerHeaderStructure) +
                                       Container.MaxSlots * Container.StepSlot;
            Container.End = Container.Start + (Container.MaxContainers * Container.StepContainer);

            Container.DistanceHasParent = (uint)Marshal.OffsetOf(containerHeaderStructure, "hasParent");
            Container.DistanceId = (uint)Marshal.OffsetOf(containerHeaderStructure, "id");
            Container.DistanceName = (uint)Marshal.OffsetOf(containerHeaderStructure, "name");
            Container.DistanceAmount = (uint)Marshal.OffsetOf(containerHeaderStructure, "ammount");
            Container.DistanceIsOpen = (uint)Marshal.OffsetOf(containerHeaderStructure, "isOpen");
            Container.DistanceVolume = (uint)Marshal.OffsetOf(containerHeaderStructure, "volume");

            Container.DistanceContainerSlotsBegin = (uint)Marshal.SizeOf(containerHeaderStructure);
            Container.DistanceContainerSlotItemCount = (uint)Marshal.OffsetOf(simpleItemStructure, "data");
            Container.DistanceContainerSlotItemId = (uint)Marshal.OffsetOf(simpleItemStructure, "id");
            #endregion

            #region ContextMenus
            ContextMenus.OnClickContextMenuVf = 0x32a390 + baseAddress;
            ContextMenus.AddContextMenuPtr = 0x5e2b0 + baseAddress;
            ContextMenus.OnClickContextMenuPtr = 0x5f310 + baseAddress;
            ContextMenus.AddSetOutfitContextMenu = 0x5f0c1 + baseAddress;
            ContextMenus.AddPartyActionContextMenu = 0xFFFFFFFF;
            ContextMenus.AddCopyNameContextMenu = 0x5f158 + baseAddress;
            ContextMenus.AddTradeWithContextMenu = 0x5ed46 + baseAddress;
            ContextMenus.AddLookContextMenu = 0x5ec1f + baseAddress;
            #endregion

            #region Creature
            Creature.DistanceId = (uint)Marshal.OffsetOf(creatureStructure, "id");
            Creature.DistanceName = (uint)Marshal.OffsetOf(creatureStructure, "name");
            Creature.DistanceZ = (uint)Marshal.OffsetOf(creatureStructure, "z");
            Creature.DistanceY = (uint)Marshal.OffsetOf(creatureStructure, "y");
            Creature.DistanceX = (uint)Marshal.OffsetOf(creatureStructure, "x");
            Creature.DistanceScreenOffsetVert = (uint)Marshal.OffsetOf(creatureStructure, "screenOffsetVertical");
            Creature.DistanceScreenOffsetHoriz = (uint)Marshal.OffsetOf(creatureStructure, "screenOffsetHorizontal");
            Creature.DistanceFaceDirection = (uint)Marshal.OffsetOf(creatureStructure, "faceDirection");

            Creature.DistanceIsWalking = (uint)Marshal.OffsetOf(creatureStructure, "isWalking"); ;
            Creature.DistanceWalkDirection = (uint)Marshal.OffsetOf(creatureStructure, "walkDirection");
            Creature.DistanceOutfit = (uint)Marshal.OffsetOf(creatureStructure, "outfit");
            Creature.DistanceColorHead = (uint)Marshal.OffsetOf(creatureStructure, "colorHead");
            Creature.DistanceColorBody = (uint)Marshal.OffsetOf(creatureStructure, "colorBody");
            Creature.DistanceColorLegs = (uint)Marshal.OffsetOf(creatureStructure, "colorLegs");
            Creature.DistanceColorFeet = (uint)Marshal.OffsetOf(creatureStructure, "colorFeet");
            Creature.DistanceAddon = (uint)Marshal.OffsetOf(creatureStructure, "addon");
            Creature.DistanceMountId = (uint)Marshal.OffsetOf(creatureStructure, "mountId");
            Creature.DistanceLight = (uint)Marshal.OffsetOf(creatureStructure, "light"); ;
            Creature.DistanceLightColor = (uint)Marshal.OffsetOf(creatureStructure, "lightColor");
            Creature.DistanceBlackSquare = (uint)Marshal.OffsetOf(creatureStructure, "blackSquare");
            Creature.DistanceHPBar = (uint)Marshal.OffsetOf(creatureStructure, "hpBar");
            Creature.DistanceWalkSpeed = (uint)Marshal.OffsetOf(creatureStructure, "walkSpeed");
            Creature.DistanceIsBlocking = (uint)Marshal.OffsetOf(creatureStructure, "isBlocking");
            Creature.DistanceSkull = (uint)Marshal.OffsetOf(creatureStructure, "skull");
            Creature.DistanceParty = (uint)Marshal.OffsetOf(creatureStructure, "party");
            Creature.DistanceWarIcon = (uint)Marshal.OffsetOf(creatureStructure, "warIcon");
            Creature.DistanceIsVisible = (uint)Marshal.OffsetOf(creatureStructure, "isVisible");
            #endregion

            #region DatItem
            DatItem.StepItems = (uint)Marshal.SizeOf(datMemoryStructure);
            DatItem.MarketName = (uint)Marshal.OffsetOf(datMemoryStructure, "marketName");
            DatItem.Width = (uint)Marshal.OffsetOf(datMemoryStructure, "width");
            DatItem.Height = (uint)Marshal.OffsetOf(datMemoryStructure, "height");
            DatItem.MaxSizeInPixels = (uint)Marshal.OffsetOf(datMemoryStructure, "maxSizeInPixels");
            DatItem.Layers = (uint)Marshal.OffsetOf(datMemoryStructure, "layers");
            DatItem.PatternX = (uint)Marshal.OffsetOf(datMemoryStructure, "patternX");
            DatItem.PatternY = (uint)Marshal.OffsetOf(datMemoryStructure, "patternY");
            DatItem.PatternDepth = (uint)Marshal.OffsetOf(datMemoryStructure, "patternDepth");
            DatItem.Phase = (uint)Marshal.OffsetOf(datMemoryStructure, "phase");
            DatItem.Sprite = (uint)Marshal.OffsetOf(datMemoryStructure, "sprite");
            DatItem.Flags = (uint)Marshal.OffsetOf(datMemoryStructure, "flags");
            DatItem.WalkSpeed = (uint)Marshal.OffsetOf(datMemoryStructure, "walkSpeed");
            DatItem.TextLimit = (uint)Marshal.OffsetOf(datMemoryStructure, "textLimit");
            DatItem.LightRadius = (uint)Marshal.OffsetOf(datMemoryStructure, "lightRadius");
            DatItem.LightColor = (uint)Marshal.OffsetOf(datMemoryStructure, "lightColor");
            DatItem.ShiftX = (uint)Marshal.OffsetOf(datMemoryStructure, "shiftX");
            DatItem.ShiftY = (uint)Marshal.OffsetOf(datMemoryStructure, "shiftY");
            DatItem.WalkHeight = (uint)Marshal.OffsetOf(datMemoryStructure, "walkHeight");
            DatItem.Automap = (uint)Marshal.OffsetOf(datMemoryStructure, "automap");
            DatItem.LensHelp = (uint)Marshal.OffsetOf(datMemoryStructure, "lensHelp");
            DatItem.ClothSlot = (uint)Marshal.OffsetOf(datMemoryStructure, "clothSlot");
            DatItem.MarketCategory = (uint)Marshal.OffsetOf(datMemoryStructure, "marketCategory");
            DatItem.MarketTradeAs = (uint)Marshal.OffsetOf(datMemoryStructure, "marketTradeAs");
            DatItem.MarketShowAs = (uint)Marshal.OffsetOf(datMemoryStructure, "marketShowAs");
            DatItem.MarketRestrictProfession = (uint)Marshal.OffsetOf(datMemoryStructure, "marketRestrictProfession");
            DatItem.MarketRestrictLevel = (uint)Marshal.OffsetOf(datMemoryStructure, "marketRestrictLevel");
            #endregion

            #region DrawFunctions
            DrawItem.DrawItemFunc = 0xcbb90 + baseAddress;
            DrawSkin.DrawSkinFunc = 0xd2c70 + baseAddress;
            #endregion

            #region Hotkey
            Hotkey.TextStart = 0x3BE698 + baseAddress;
            Hotkey.TextStep = 0x100;
            Hotkey.ObjectUseTypeStart = 0x3C0AC8 + baseAddress;
            Hotkey.ObjectUseTypeStep = 0x04;
            Hotkey.SendAutomaticallyStart = 0x3C0B68 + baseAddress;
            Hotkey.SendAutomaticallyStep = 0x01;
            Hotkey.ObjectStart = 0x3C0C30 + baseAddress;
            Hotkey.ObjectStep = 0x04;
            Hotkey.MaxHotkeys = 36;
            #endregion

            #region Map
            Map.MapPointer = 0x5DA5C4 + baseAddress;
            Map.StepTile = (uint)Marshal.SizeOf(mapTileStructure);
            Map.StepTileObject = (uint)Marshal.SizeOf(simpleItemStructure);
            Map.DistanceTileItemsCount = (uint)Marshal.OffsetOf(mapTileStructure, "count");
            Map.DistanceTileItemOrder = (uint)Marshal.OffsetOf(mapTileStructure, "stackOrder");
            Map.DistanceTileItems = (uint)Marshal.OffsetOf(mapTileStructure, "items");
            Map.DistanceTileEffect = (uint)Marshal.OffsetOf(mapTileStructure, "effect");
            Map.DistanceItemDataEx = (uint)Marshal.OffsetOf(simpleItemStructure, "dataEx");
            Map.DistanceItemData = (uint)Marshal.OffsetOf(simpleItemStructure, "data");
            Map.DistanceItemId = (uint)Marshal.OffsetOf(simpleItemStructure, "id");
            Map.MaxTileItems = 10;
            Map.MaxX = 18;
            Map.MaxY = 14;
            Map.MaxZ = 8;
            Map.MaxTiles = 2016;
            Map.ZAxisDefault = 7;
            Map.NameSpy1 = 0x1117cc + baseAddress;
            Map.NameSpy2 = 0x1117d9 + baseAddress;
            Map.NameSpy1Default = 0x5075;
            Map.NameSpy2Default = 0x4375;
            Map.LevelSpy1 = 0x10d60f + baseAddress;
            Map.LevelSpy2 = 0x10d707 + baseAddress;
            Map.LevelSpy3 = 0x10d783 + baseAddress;
            Map.LevelSpyDefault = new byte[] { 0x89, 0x86, 0xC0, 0x5B, 0x00, 0x00 };
            Map.LevelSpyPtr = Client.GameWindowRectPointer;
            Map.LevelSpyAdd1 = 28;
            Map.LevelSpyAdd2 = 0x5BC0;
            Map.FullLightNop = 0x115be9 + baseAddress;
            Map.FullLightAdr = Map.FullLightNop + 0x5;
            Map.FullLightNopDefault = new byte[] { 0x7E, 0x0A };
            Map.FullLightNopEdited = new byte[] { 0x90, 0x90 };
            Map.FullLightAdrDefault = 0x80;
            Map.FullLightAdrEdited = 0xFF;
            #endregion

            #region Player
            var properties1Begin = 0x3B6EB4 + baseAddress;
            var playerSkillsPercentageBegin = properties1Begin + Marshal.SizeOf(playerProperties1Structure) + 4;
            var afterSkillsPercentage = (uint)(playerSkillsPercentageBegin + Marshal.SizeOf(playerSkillStructure) + 12);
            var playerSkillsBegin = 0x583E78 + baseAddress;
            var properties2Begin = playerSkillsBegin + Marshal.SizeOf(playerSkillStructure);

            Player.Health = 0x54C000 + baseAddress;
            Player.GoToZ = 0x54C004 + baseAddress;
            Player.AttackCount = 0x5D3FC8 + baseAddress;
            Player.SlotBegin = 0x5DA534 + baseAddress; 

            Player.Flags = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "flags").ToInt32());
            Player.OfflineTraining = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "offlineTraining").ToInt32());
            Player.GreenSquare = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "greenSquare").ToInt32());
            Player.XORKey = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "xorKey").ToInt32());
            Player.ManaMax = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "manaMax").ToInt32());
            Player.Experience = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "experience").ToInt32());
            Player.Level = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "level").ToInt32());
            Player.Soul = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "soul").ToInt32());
            Player.MagicLevel = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "magicLevel").ToInt32());
            Player.MagicLevelPercent = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "magicLevelPercent").ToInt32());
            Player.RedSquare = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "redSquare").ToInt32());
            Player.Mana = (uint)(properties1Begin + Marshal.OffsetOf(playerProperties1Structure, "mana").ToInt32());

            Player.FistPercent = (uint)(playerSkillsPercentageBegin + Marshal.OffsetOf(playerSkillStructure, "fist").ToInt32());
            Player.ClubPercent = (uint)(playerSkillsPercentageBegin + Marshal.OffsetOf(playerSkillStructure, "club").ToInt32());
            Player.SwordPercent = (uint)(playerSkillsPercentageBegin + Marshal.OffsetOf(playerSkillStructure, "sword").ToInt32());
            Player.AxePercent = (uint)(playerSkillsPercentageBegin + Marshal.OffsetOf(playerSkillStructure, "axe").ToInt32());
            Player.DistancePercent = (uint)(playerSkillsPercentageBegin + Marshal.OffsetOf(playerSkillStructure, "distance").ToInt32());
            Player.ShieldingPercent = (uint)(playerSkillsPercentageBegin + Marshal.OffsetOf(playerSkillStructure, "shielding").ToInt32());
            Player.FishingPercent = (uint)(playerSkillsPercentageBegin + Marshal.OffsetOf(playerSkillStructure, "fishing").ToInt32());

            Player.LevelPercent = afterSkillsPercentage;
            Player.Stamina = afterSkillsPercentage + 4;


            Player.TargetId = Player.RedSquare;
            Player.FollowCount = Player.AttackCount;


            Player.Fist = (uint)(playerSkillsBegin + Marshal.OffsetOf(playerSkillStructure, "fist").ToInt32());
            Player.Club = (uint)(playerSkillsBegin + Marshal.OffsetOf(playerSkillStructure, "club").ToInt32());
            Player.Sword = (uint)(playerSkillsBegin + Marshal.OffsetOf(playerSkillStructure, "sword").ToInt32());
            Player.Axe = (uint)(playerSkillsBegin + Marshal.OffsetOf(playerSkillStructure, "axe").ToInt32());
            Player.Distance = (uint)(playerSkillsBegin + Marshal.OffsetOf(playerSkillStructure, "distance").ToInt32());
            Player.Shielding = (uint)(playerSkillsBegin + Marshal.OffsetOf(playerSkillStructure, "shielding").ToInt32());
            Player.Fishing = (uint)(playerSkillsBegin + Marshal.OffsetOf(playerSkillStructure, "fishing").ToInt32());

            Player.Capacity = (uint)(properties2Begin + Marshal.OffsetOf(playerProperties2Structure, "capacity").ToInt32());
            Player.GoToY = (uint)(properties2Begin + Marshal.OffsetOf(playerProperties2Structure, "gotoY").ToInt32());
            Player.HealthMax = (uint)(properties2Begin + Marshal.OffsetOf(playerProperties2Structure, "healthMax").ToInt32());
            Player.GoToX = (uint)(properties2Begin + Marshal.OffsetOf(playerProperties2Structure, "gotoX").ToInt32());
            Player.Id = (uint)(properties2Begin + Marshal.OffsetOf(playerProperties2Structure, "id").ToInt32());
            Player.X = (uint)(properties2Begin + Marshal.OffsetOf(playerProperties2Structure, "x").ToInt32());
            Player.Y = (uint)(properties2Begin + Marshal.OffsetOf(playerProperties2Structure, "y").ToInt32());
            Player.Z = (uint)(properties2Begin + Marshal.OffsetOf(playerProperties2Structure, "z").ToInt32());


            Player.MaxSlots = 10;
            Player.SlotAmmo = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "ammo");
            Player.SlotRing = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "ring");
            Player.SlotFeet = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "feet");
            Player.SlotLegs = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "legs");
            Player.SlotLeft = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "left");
            Player.SlotRight = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "right");
            Player.SlotArmor = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "armor");
            Player.SlotBackpack = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "backpack");
            Player.SlotNeck = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "neck");
            Player.SlotHead = Player.SlotBegin + (uint)Marshal.OffsetOf(playerSlotsStructure, "head");
            Player.SlotStep = (uint)Marshal.SizeOf(simpleItemStructure);
            Player.DistanceSlotCount = (uint)Marshal.OffsetOf(simpleItemStructure, "data");
            Player.DistanceSlotId = (uint)Marshal.OffsetOf(simpleItemStructure, "id");



            #endregion

            #region TextDisplay
            TextDisplay.PrintName = 0x10e7f1 + baseAddress;
            TextDisplay.PrintFPS = 0x65ae6 + baseAddress;
            TextDisplay.PrintTextFunc = 0xce800 + baseAddress;
            TextDisplay.ShowFPS = 0x59999B + baseAddress;
            TextDisplay.NopFPS = 0x65951 + baseAddress;
            #endregion

            #region Vip
            Vip.MarkerNodePtr = 0x549D3C + baseAddress;
            Vip.Count = Vip.MarkerNodePtr + 4;
            Vip.DistancePreviousNode = (uint)Marshal.OffsetOf(vipNodeStructure, "previousNode");
            Vip.DistanceNextNode = (uint)Marshal.OffsetOf(vipNodeStructure, "nextNode");
            Vip.DistanceId = (uint)Marshal.OffsetOf(vipNodeStructure, "id");
            Vip.DistanceIcon = (uint)Marshal.OffsetOf(vipNodeStructure, "icon");
            Vip.DistanceNotify = (uint)Marshal.OffsetOf(vipNodeStructure, "notify");
            Vip.DistanceNameField = (uint)Marshal.OffsetOf(vipNodeStructure, "nameTextField");
            Vip.DistanceDescriptionField = (uint)Marshal.OffsetOf(vipNodeStructure, "descriptionTextField");
            Vip.DistanceStatus = (uint)Marshal.OffsetOf(vipNodeStructure, "status");
            #endregion
        }
    }
}