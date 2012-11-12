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
extern CRITICAL_SECTION EventTriggerCriticalSection;
extern CRITICAL_SECTION DrawItemCriticalSection;
extern CRITICAL_SECTION DrawSkinCriticalSection;

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
		DrawSkinFunc = 0x12
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
