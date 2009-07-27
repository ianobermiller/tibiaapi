#if MSC_VER > 100
#pragma once
#endif
#ifndef _CORE_H_
#define _CORE_H_

#include <string>

enum PipePacketType : unsigned char
{
	PipePacketType_DefaultTemplate = 0x00,
	PipePacketType_HooksEnableDisable = 0x01,
	PipePacketType_SetConstant = 0x02,
	PipePacketType_DisplayText = 0x03,
	PipePacketType_RemoveText = 0x04,
	PipePacketType_RemoveAllText = 0x05,
	PipePacketType_DisplayCreatureText = 0x06,
	PipePacketType_RemoveCreatureText = 0x07,
	PipePacketType_UpdateCreatureText = 0x08,
	PipePacketType_AddContextMenu = 0x09,
	PipePacketType_RemoveContextMenu = 0x0A,
	PipePacketType_RemoveAllContextMenus= 0x0B,
	PipePacketType_OnClickContextMenu = 0x0C,
	PipePacketType_UnloadDll = 0x0D,
	PipePacketType_HookReceivedPacket = 0x0E,
	PipePacketType_HookSentPacket = 0x0F,
	PipePacketType_HookSendToServer = 0x10,
	PipePacketType_EventTrigger = 0x11,
	PipePacketType_AddIcon = 0x12,
	PipePacketType_UpdateIcon = 0x13,
	PipePacketType_RemoveIcon = 0x14,
	PipePacketType_OnClickIcon = 0x15,
	PipePacketType_RemoveAllIcons = 0x16

};

typedef void _EventTrigger(int type, void* maw, void* mow);
static _EventTrigger *EventTrigger = 0;

typedef void _PrintText(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);
static _PrintText *PrintText = 0;

typedef int (WINAPI *PRECV)(SOCKET s, char* buf, int len, int flags);
static PRECV OrigRecv = 0;
typedef int (WINAPI *PSEND)(SOCKET s, char* buf, int len, int flags);
static PSEND OrigSend = 0;

//Credits to Stepler (http://www.tpforums.org/forum/showthread.php?t=2981) and yaboomaster
typedef void _DrawItem(int surface,
					   int x, int y,
					   int size,
					   int itemId, int itemData1,
					   int itemData2, int edgeR, int edgeG, int edgeB ,
					   int clipX,int clipY, int clipW, int clipH,
					   //text
					   int textFont,int textRed,int textGreen,int textBlue,int textAlign,
					   int textForce);
static _DrawItem *DrawItem = 0;

void MyPrintName(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);
void MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);
void __stdcall MySetOutfitContextMenu (int eventId, const char* text, const char* shortcut);
void __stdcall MyPartyActionContextMenu (int eventId, const char* text, const char* shortcut);
void __stdcall MyCopyNameContextMenu (int eventId, const char* text, const char* shortcut);
void __stdcall MyTradeWithContextMenu (int eventId, const char* text, const char* shortcut);
void __stdcall MyLookContextMenu (int eventId, const char* text, const char* shortcut);
void __stdcall MyOnClickContextMenu (int eventId);
int WINAPI MyRecv(SOCKET s, char* buf, int len, int flags);
int WINAPI MySend(SOCKET s,char* buf, int len, int flags);

void DisableHooks();
DWORD HookCall(DWORD dwAddress, DWORD dwFunction);
void UnhookCall(DWORD dwAddress, DWORD dwOldCall);
BYTE* Nop(DWORD dwAddress, int size);
void UnNop(DWORD dwAddress, BYTE* OldBytes, int size);

void StartUninjectSelf();
void __declspec(noreturn) UninjectSelf();
void UnloadSelf();

inline void PipeOnRead();
void PipeThreadProc(HMODULE Module);
void CALLBACK ReadFileCompleted(DWORD errorCode, DWORD bytesCopied, OVERLAPPED* overlapped);

void ParseHooksEnableDisable(BYTE *Buffer, int position);
void ParseSetConstant(BYTE *Buffer, int position);
void ParseDisplayText(BYTE *Buffer, int position);
void ParseRemoveText(BYTE *Buffer, int position);
void RemoveAllText();
void ParseDisplayCreatureText(BYTE *Buffer, int position);
void ParseRemoveCreatureText(BYTE *Buffer, int position);
void ParseUpdateCreatureText(BYTE *Buffer, int position);
void ParseAddContextMenu(BYTE *Buffer, int position);
void ParseRemoveContextMenu(BYTE *Buffer, int position);
void RemoveAllContextMenus();
void ParseHookSendToServer(BYTE *Buffer, int position);
void ParseEventTrigger(BYTE *Buffer, int position);
void ParseAddIcon(BYTE *Buffer, int position);
void ParseUpdateIcon(BYTE *Buffer, int position);
void ParseRemoveIcon(BYTE *Buffer, int position);
void ParseRemoveAllIcons();

BOOL CALLBACK EnumWindowsProc(HWND hwnd,LPARAM lParam);
LRESULT WINAPI SubClassProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
//LRESULT CALLBACK MouseHookProc(int nCode,WPARAM wParam, LPARAM lParam);

#endif