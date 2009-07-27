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
	DWORD ptrTradeWithContextMenu = 0;
	DWORD ptrLookContextMenu = 0;
	DWORD prtOnClickContextMenuVf = 0;

	/* Socket Stuff */
	DWORD ptrRecv = 0;
	DWORD ptrSend = 0;

	/* Event Trigger Stuff */
	DWORD ptrEventTrigger = 0;
}

/* DLL Injection Related Stuff */
HINSTANCE hMod = 0;
bool HooksEnabled = false;

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
CRITICAL_SECTION EventTriggerCriticalSection;
CRITICAL_SECTION DrawItemCriticalSection;


