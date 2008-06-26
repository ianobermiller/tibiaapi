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
}

/* DLL Injection Related Stuff */
extern HINSTANCE hMod;
extern bool HookInjected;

/* Pipes */
extern std::string PipeName;
extern bool PipeConnected;
extern HANDLE PipeHandle;
extern HANDLE PipeThread;
extern BYTE Buffer[1024];
extern CRITICAL_SECTION PipeReadCriticalSection;
extern CRITICAL_SECTION NormalTextCriticalSection;
extern CRITICAL_SECTION CreatureTextCriticalSection;

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
#endif
