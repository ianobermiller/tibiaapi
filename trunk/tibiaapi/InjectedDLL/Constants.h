#if MSC_VER > 100
#pragma once
#endif

#ifndef _CONSTANTS_H_
#define _CONSTANTS_H_

namespace Consts {

	/* Displaying Text Stuff */
	extern DWORD ptrPrintName;
	extern DWORD ptrPrintFPS;
	extern DWORD ptrShowFPS;
	extern DWORD ptrNopFPS;

	extern DWORD ptrAddContextMenu;
	extern DWORD ptrOnClickContextMenu;
	extern DWORD ptrSetOutfitContextMenu;
	extern DWORD ptrPartyActionContextMenu;
	extern DWORD ptrCopyNameContextMenu;
	extern DWORD ptrTradeWithContextMenu;
	extern DWORD ptrLookContextMenu;
	extern DWORD prtOnClickContextMenuVf;

	extern DWORD ptrRecv;
	extern DWORD ptrSend;

	extern DWORD ptrEventTrigger;
	
	extern DWORD ptrOnGetNextPacket;
	extern DWORD ptrRecvStream;
}

/* DLL Injection Related Stuff */
extern HINSTANCE hMod;
extern bool HooksEnabled;

/* Pipes */
extern std::string PipeName;
extern bool PipeConnected;
extern HANDLE PipeThread;
extern BYTE Buffer[1024];
extern CRITICAL_SECTION PipeReadCriticalSection;
extern CRITICAL_SECTION NormalTextCriticalSection;
extern CRITICAL_SECTION CreatureTextCriticalSection;
extern CRITICAL_SECTION ContextMenuCriticalSection;
extern CRITICAL_SECTION OnClickCriticalSection;
extern CRITICAL_SECTION EventTriggerCriticalSection;
extern CRITICAL_SECTION DrawItemCriticalSection;
extern CRITICAL_SECTION DrawSkinCriticalSection;

enum PacketCommandType : BYTE
{
	    SelfAppear = 0x0A,
        GMAction = 0x0B,
        ErrorMessage = 0x14,
        FyiMessage = 0x15,
        WaitingList = 0x16,
        Ping = 0x1E,
        Death = 0x28,
        CanReportBugs = 0x32,
        MapDescription = 0x64,
        MoveNorth = 0x65,
        MoveEast = 0x66,
        MoveSouth = 0x67,
        MoveWest = 0x68,
        TileUpdate = 0x69,
        TileAddThing = 0x6A,
        TileTransformThing = 0x6B,
        TileRemoveThing = 0x6C,
        CreatureMove = 0x6D,
        ContainerOpen = 0x6E,
        ContainerClose = 0x6F,
        ContainerAddItem = 0x70,
        ContainerUpdateItem = 0x71,
        ContainerRemoveItem = 0x72,
        InventorySetSlot = 0x78,
        InventoryResetSlot = 0x79,
        ShopWindowOpen = 0x7A,
        ShopSaleGoldCount = 0x7B,
        ShopWindowClose = 0x7C,
        SafeTradeRequestAck = 0x7D,
        SafeTradeRequestNoAck = 0x7E,
        SafeTradeClose = 0x7F,
        WorldLight = 0x82,
        MagicEffect = 0x83,
        AnimatedText = 0x84,
        Projectile = 0x85,
        CreatureSquare = 0x86,
        CreatureHealth = 0x8C,
        CreatureLight = 0x8D,
        CreatureOutfit = 0x8E,
        CreatureSpeed = 0x8F,
        CreatureSkull = 0x90,
        CreatureShield = 0x91,
        ItemTextWindow = 0x96,
        HouseTextWindow = 0x97,
        PlayerStatus = 0xA0,
        PlayerSkillsUpdate = 0xA1,
        PlayerFlags = 0xA2,
        CancelTarget = 0xA3,
        CreatureSpeech = 0xAA,
        ChannelList = 0xAB,
        ChannelOpen = 0xAC,
        ChannelOpenPrivate = 0xAD,
        RuleViolationOpen = 0xAE,
        RuleViolationRemove = 0xAF,
        RuleViolationCancel = 0xB0,
        RuleViolationLock = 0xB1,
        PrivateChannelCreate = 0xB2,
        ChannelClosePrivate = 0xB3,
        TextMessage = 0xB4,
        PlayerWalkCancel = 0xB5,
        FloorChangeUp = 0xBE,
        FloorChangeDown = 0xBF,
        OutfitWindow = 0xC8,
        VipState = 0xD2,
        VipLogin = 0xD3,
        VipLogout = 0xD4,
        QuestList = 0xF0,
        QuestPartList = 0xF1,
        ShowTutorial = 0xDC,
        AddMapMarker = 0xDD
};

enum PipeConstantType : BYTE
{
        PrintName = 0x01,
        PrintFPS = 0x02,
        ShowFPS = 0x03,
        PrintTextFunc = 0x04,
        NopFPS = 0x05,
        AddContextMenuFunc = 0x06,
        OnClickContextMenu = 0x07,
        SetOutfitContextMenu = 0x08,
        PartyActionContextMenu = 0x09,
        CopyNameContextMenu = 0x0A,
		OnClickContextMenuVf = 0x0B,
		TradeWithContextMenu = 0x0C,
		Recv=0x0D,
		Send=0x0E,
		EventTriggered = 0x0F,
		LookContextMenu = 0x10,
		DrawItemFunc = 0x11,
		DrawSkinFunc = 0x12,
		OnGetNextPacketFunc = 0x13,
		RecvStream = 0x14
};

/* Structures */
//Display Normal Text Strcture
struct NormalText
{
	char* text;
	int r,g,b;
	int x,y;
	int font;
	char *TextName;
}; 

//Display Creature Text Structure
struct PlayerText
{
	char *DisplayText;
	char *CreatureName;
	int CreatureId;
	int cR;
	int cG;
	int cB;
	int TextFont;
	int RelativeX;
	int RelativeY;

};

//Context Menu structure
struct ContextMenu
{
	int EventId;
	char *MenuText;
	BYTE Type;
	BYTE HasSeparator;
};

struct Icon
{
	int IconId;
	int X;
	int Y;
	int BitmapSize;
	int ItemId;
	int ItemCount;
	int TextFont;
	int cR;
	int cG;
	int cB;
};

struct Skin
{
	int SkinId;
	int X;
	int Y;
	int Width;
	int Height;
	int GUIId;
};

struct TPacketStream
{
	LPVOID pBuffer;
	DWORD dwSize;
	DWORD dwPos;
};
#endif
