#include "stdafx.h"
#include <string>
#include <windows.h>
#include "Constants.h"

namespace Consts {

	/* Displaying Text Stuff */
	DWORD ptrPrintName = 0;
	DWORD ptrPrintFPS = 0;
	DWORD ptrShowFPS = 0;
	DWORD ptrNopFPS = 0;

	/* Context Menu Stuff */
	DWORD ptrAddContextMenu = 0;
	DWORD ptrOnClickContextMenu = 0;
	DWORD ptrSetOutfitContextMenu = 0;
	DWORD ptrPartyActionContextMenu = 0;
	DWORD ptrCopyNameContextMenu = 0;
	DWORD prtOnClickContextMenuVf = 0;
}

/* DLL Injection Related Stuff */
HINSTANCE hMod = 0;
bool HookInjected = false;

/* Pipes */
std::string PipeName;
bool PipeConnected = false;
HANDLE PipeThread = 0;
BYTE Buffer[1024] = {0};
CRITICAL_SECTION PipeReadCriticalSection;
CRITICAL_SECTION NormalTextCriticalSection;
CRITICAL_SECTION CreatureTextCriticalSection;
CRITICAL_SECTION ContextMenuCriticalSection;
CRITICAL_SECTION OnClickCriticalSection;


