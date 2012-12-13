#if MSC_VER > 100
#pragma once
#endif

#ifndef _CONSTANTS_H_
#define _CONSTANTS_H_

namespace Consts {

	extern DWORD clientVersion;

	/* Displaying Text Stuff */
	extern DWORD ptrPrintName;
	extern DWORD ptrPrintFPS;
	extern DWORD ptrShowFPS;
	extern DWORD ptrNopFPS;
	
	extern DWORD prtOnClickContextMenuVf;
	extern DWORD ptrAddContextMenu;
	extern DWORD ptrOnClickContextMenu;
	extern DWORD ptrSetOutfitContextMenu;
	extern DWORD ptrPartyActionContextMenu;
	extern DWORD ptrCopyNameContextMenu;
	extern DWORD ptrTradeWithContextMenu;
	extern DWORD ptrLookContextMenu;

	extern DWORD ptrRecv;
	extern DWORD ptrSend;
	extern DWORD ptrSocket;

	extern DWORD ptrEventTrigger;
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
extern CRITICAL_SECTION PipeWriteCriticalSection;
extern CRITICAL_SECTION NormalTextCriticalSection;
extern CRITICAL_SECTION CreatureTextCriticalSection;
extern CRITICAL_SECTION ContextMenuCriticalSection;
extern CRITICAL_SECTION EventTriggerCriticalSection;
extern CRITICAL_SECTION DrawItemCriticalSection;
extern CRITICAL_SECTION DrawSkinCriticalSection;

enum PipeConstantType : BYTE
{
        ClientVersion = 1,

        PrintName = 2,
        PrintFPS = 3,
        ShowFPS = 4,
        PrintTextFunc = 5,
        NopFPS = 6,
        
        OnClickContextMenuVf = 7,
        AddContextMenuFunc = 8,
        OnClickContextMenu = 9,
        SetOutfitContextMenu = 10,
        PartyActionContextMenu = 11,
        CopyNameContextMenu = 12,
        TradeWithContextMenu = 13,
        LookContextMenu = 14,

        Recv = 15,
        Send = 16,
        Socket = 17,

        EventTrigger = 18,
        DrawItemFunc = 19,
        DrawSkinFunc = 20
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
#endif
