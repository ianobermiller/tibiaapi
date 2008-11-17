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

	extern DWORD ptrOnClickContextMenu;
	extern DWORD ptrSetOutfitContextMenu;
	extern DWORD ptrPartyActionContextMenu;
	extern DWORD ptrCopyNameContextMenu;
}

/* DLL Injection Related Stuff */
extern HINSTANCE hMod;
extern bool HookInjected;

/* Pipes */
extern std::string PipeName;
extern bool PipeConnected;
extern HANDLE PipeThread;
extern BYTE Buffer[1024];
extern CRITICAL_SECTION PipeReadCriticalSection;
extern CRITICAL_SECTION NormalTextCriticalSection;
extern CRITICAL_SECTION CreatureTextCriticalSection;
extern CRITICAL_SECTION ContextMenuCriticalSection;

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

//Add Context Menu structure
struct ContextMenu
{
	int EventId;
	char *MenuText;
	int Type;
	char hasSeparator;
}
#endif