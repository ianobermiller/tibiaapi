#include "stdafx.h"
#include <windows.h>
#include <string>
#include <sstream>
#include <list>
#include <atlbase.h>
#include <assert.h>
#include "Constants.h"
#include "Core.h"
#include "Packet.h"


#ifdef _MANAGED
#pragma managed(push, off)
#endif

#define AddContextMenu(eventId, text, shortcut)   \
	__asm push shortcut \
	__asm push text \
	__asm push eventId \
	__asm mov ecx, esi \
	__asm mov eax, Consts::ptrAddContextMenu \
	__asm call eax

#define AddContextMenuEx(eventId, text, shortcut)   \
	__asm mov byte ptr[esi+0x30], 1 \
	__asm push shortcut \
	__asm push text \
	__asm push eventId \
	__asm mov ecx, esi \
	__asm mov eax, Consts::ptrAddContextMenu \
	__asm call eax

using namespace std;

/* DisplayText. Credits for Displaying text goes to Stiju and Zionz. Thanks for the help!*/

list<NormalText> DisplayTexts;		//Used for normal text displyaing
list<PlayerText> CreatureTexts;		//Used for storing current text to display above creature
DWORD OldPrintName = 0;				//Used for restoring PrintText when uninjecting DLL
DWORD OldPrintFPS = 0;				//Used for restoring PrintFPS when uninjecting DLL
BYTE* OldNopFPS = 0;				//Used for restoring conditional jump (FPS)


//contextmenu
DWORD OldSetOutfitContextMenu = 0;  //Used for restoring SetOutfitContextMenu ~
DWORD OldPartyActionContextMenu = 0;//Used for restoring PartyActionContextMenu ~
DWORD OldCopyNameContextMenu = 0;   //Used for restoring CopyNameContextMenu ~
DWORD OldTradeWithContextMenu = 0;
list<ContextMenu> ContextMenus;    //Used for storing the context menus that will be added on this call
//recv/send
DWORD OrigSendAddress = 0;
DWORD OrigRecvAddress = 0;
SOCKET sock = 0;


//Asynchronisation variables
CHandle pipe;						//Holds the Pipe handle (CHandle is from ATL library)
OVERLAPPED overlapped = { 0 };		
DWORD errorStatus = ERROR_SUCCESS;


int WINAPI MyRecv(SOCKET s, char* buf, int len, int flags)
{	
	//sock=s;
	int bytesCount=OrigRecv(s,buf,len,flags);
	if(bytesCount>0)
	{		
		Packet* packet = new Packet(((WORD)buf)+1);
		packet->AddByte(0x0E);
		for(int i=0;i<bytesCount;i++)
			packet->AddByte((BYTE)buf[i]);
		WriteFileEx(pipe, packet->GetPacket(), packet->GetSize(), &overlapped, NULL); 
	}
	return bytesCount;
}

int WINAPI MySend(SOCKET s,char* buf, int len, int flags)
{
	sock=s;
	if(len>0)
	{
		Packet* packet = new Packet(((WORD)buf)+1);
		packet->AddByte(0x0F);
		for(int i=0;i<len;i++)
			packet->AddByte((BYTE)buf[i]);
		WriteFileEx(pipe, packet->GetPacket(), packet->GetSize(), &overlapped, NULL); 
	}

	return OrigSend(s,buf,len,flags);;
}

void MyPrintName(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign)
{
	list<PlayerText>::iterator it;

	//Displaying Original Text
	PrintText(nSurface, nX, nY, nFont, nRed, nGreen, nBlue, lpText, nAlign);

	/* Write own text */

	DWORD *EntityID = (DWORD*)(lpText - 4);

	//Displaying texts
	EnterCriticalSection(&CreatureTextCriticalSection);
	for(it = CreatureTexts.begin(); it != CreatureTexts.end(); ++it) {
		if(it->CreatureId == 0)
		{
			if(!strcmp(lpText, it->CreatureName)) {
				PrintText(0x01, nX + it->RelativeX, nY + it->RelativeY, it->TextFont, it->cR, it->cG, it->cB, it->DisplayText, 0x00);
			}
		}
		else if(*EntityID == it->CreatureId)
		{
			PrintText(0x01, nX + it->RelativeX, nY + it->RelativeY, it->TextFont, it->cR, it->cG, it->cB, it->DisplayText, 0x00);
		}
	}
	LeaveCriticalSection(&CreatureTextCriticalSection);
}

void MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign)
{
	bool *fps = (bool*)Consts::ptrShowFPS;
	if(*fps == true)
	{
		PrintText(nSurface, nX, nY, nFont, nRed, nGreen, nBlue, lpText, nAlign);
		//nY += 12; ??????
	}

	EnterCriticalSection(&NormalTextCriticalSection);

	list<NormalText>::iterator ntIT;
	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
		PrintText(0x01, ntIT->x, ntIT->y, ntIT->font, ntIT->r, ntIT->g, ntIT->b, ntIT->text, 0x00); //0x01 Surface, 0x00 Align

	LeaveCriticalSection(&NormalTextCriticalSection);
}


void __stdcall MySetOutfitContextMenu (int eventId, const char* text, const char* shortcut)
{
	//MessageBoxA(0, "MySetOutfitContextMenu", "Error!", MB_ICONERROR);
	AddContextMenu(eventId, text, shortcut);

	list<ContextMenu>::iterator it;
	for(it = ContextMenus.begin(); it != ContextMenus.end(); ++it)
	{
		//SetOutfitContextMenu or AllMenus
		if(it->Type == 0x01 || it->Type == 0x00)
		{
			const char* custom = it->MenuText;
			const char* shortcut_ = "";
			int eventid=it->EventId;

			if(it->HasSeparator == 0x00)
				AddContextMenu(eventid, custom, shortcut_);
			else if(it->HasSeparator == 0x01)
				AddContextMenuEx(eventid, custom, shortcut_);		
		}

	}	
}

void __stdcall MyPartyActionContextMenu (int eventId, const char* text, const char* shortcut)
{
	//MessageBoxA(0, "MyPartyActionContextMenu", "Error!", MB_ICONERROR);
	AddContextMenu(eventId, text, shortcut);

	list<ContextMenu>::iterator it;
	for(it = ContextMenus.begin(); it != ContextMenus.end(); ++it)
	{
		//PartyActionContextMenu or AllMenus
		if(it->Type == 0x02 || it->Type == 0x00)
		{
			const char* custom = it->MenuText;
			const char* shortcut_ = "";
			int eventid = it->EventId;

			if(it->HasSeparator == 0x00)
				AddContextMenu(eventid, custom, shortcut_);
			else if(it->HasSeparator == 0x01)
				AddContextMenuEx(eventid, custom, shortcut_);
		}
	}
}

void __stdcall MyCopyNameContextMenu (int eventId, const char* text, const char* shortcut)
{
	//MessageBoxA(0, "MyCopyNameContextMenu", "Error!", MB_ICONERROR);
	AddContextMenu(eventId, text, shortcut);

	list<ContextMenu>::iterator it;
	for(it = ContextMenus.begin(); it != ContextMenus.end(); ++it)
	{
		//CopyNameContextMenu or AllMenus
		if(it->Type == 0x03 || it->Type == 0x00)
		{
			const char* custom = it->MenuText;
			const char* shortcut_ = "";
			int eventid = it->EventId;

			if(it->HasSeparator == 0x00)
				AddContextMenu(eventid, custom, shortcut_);
			else if(it->HasSeparator == 0x01)
				AddContextMenuEx(eventid, custom, shortcut_);
		}
	}
}

void __stdcall MyTradeWithContextMenu (int eventId, const char* text, const char* shortcut)
{
	//MessageBoxA(0, "MyCopyNameContextMenu", "Error!", MB_ICONERROR);
	AddContextMenu(eventId, text, shortcut);

	list<ContextMenu>::iterator it;
	for(it = ContextMenus.begin(); it != ContextMenus.end(); ++it)
	{
		//CopyNameContextMenu or AllMenus
		if(it->Type == 0x04 || it->Type == 0x00)
		{
			const char* custom = it->MenuText;
			const char* shortcut_ = "";
			int eventid = it->EventId;

			if(it->HasSeparator == 0x00)
				AddContextMenu(eventid, custom, shortcut_);
			else if(it->HasSeparator == 0x01)
				AddContextMenuEx(eventid, custom, shortcut_);
		}
	}
}

void __stdcall MyOnClickContextMenu (int eventId)
{
	//function from http://www.tpforums.org/forum/showthread.php?t=2399 by Vitor
	__asm mov esi, ecx //; Compiler will ensure esi register is safe to use

		if (eventId >= 0x2000)
		{
			__asm
			{
				push eventId
					mov ecx, esi //; Ensure ecx carries the right value - you never know!
					mov eax, Consts::ptrOnClickContextMenu
					call eax
			}
			return;
		}

		/*WARNING:
		Again, as AddContextMenu, this function is a thiscall. But, unfortunately this time,
		the registers that carry the this are ecx and eax, registers commonly used to do random
		tasks at function's epilogue (that is, the code executed by the compiler when it enters
		a function). If, however, you can confirm that your compiler does not change ecx - or
		that it does not change eax, case in which you could move eax to ecx - you are ok to go on.
		If you can not confirm or you are not sure, we have to go deeper.
		*/

		/* Either switch event IDs if application is C++ or, if using TibiaAPI, send this information to the caller code using a pipe.
		* Here, we'll exemplify using a switch statement.
		*/

		Packet* packet = new Packet(5);
		packet->AddByte(0x0C);
		packet->AddDWord(eventId);
		WriteFileEx(pipe, packet->GetPacket(), packet->GetSize(), &overlapped, NULL); 
}

void EnableHooks()
{
	if(HooksEnabled)
	{
		MessageBoxA(0, "The hook is already injected", "Information", MB_ICONINFORMATION);
		return;
	}

	OldPrintName = HookCall(Consts::ptrPrintName, (DWORD)&MyPrintName);
	OldPrintFPS = HookCall(Consts::ptrPrintFPS, (DWORD)&MyPrintFps);

	OldSetOutfitContextMenu = HookCall(Consts::ptrSetOutfitContextMenu, (DWORD)&MySetOutfitContextMenu);
	OldPartyActionContextMenu = HookCall(Consts::ptrPartyActionContextMenu, (DWORD)&MyPartyActionContextMenu);
	OldCopyNameContextMenu = HookCall(Consts::ptrCopyNameContextMenu, (DWORD)&MyCopyNameContextMenu);
	OldTradeWithContextMenu = HookCall(Consts::ptrTradeWithContextMenu, (DWORD)&MyTradeWithContextMenu);

	DWORD dwOldProtect, dwNewProtect, funcAddress;	

	//OnClickContextMenuEvent..
	funcAddress = (DWORD)&MyOnClickContextMenu;
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::prtOnClickContextMenuVf, &funcAddress, 4);
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, dwOldProtect, &dwNewProtect); //Restore access

	//recv/send
	OrigSendAddress=(DWORD)GetProcAddress(GetModuleHandleA("WS2_32.dll"),"send");
	OrigSend=(PSEND)OrigSendAddress;
	funcAddress = (DWORD)&MySend;
	VirtualProtect((LPVOID)Consts::ptrSend, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::ptrSend, &funcAddress, 4);
	VirtualProtect((LPVOID)Consts::ptrSend, 4, dwOldProtect, &dwNewProtect);

	OrigRecvAddress=(DWORD)GetProcAddress(GetModuleHandleA("WS2_32.dll"),"recv");
	OrigRecv=(PRECV)OrigRecvAddress;
	funcAddress = (DWORD)&MyRecv;
	VirtualProtect((LPVOID)Consts::ptrRecv, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::ptrRecv, &funcAddress, 4);
	VirtualProtect((LPVOID)Consts::ptrRecv, 4, dwOldProtect, &dwNewProtect);

	OldNopFPS = Nop(Consts::ptrNopFPS, 6); //Showing the FPS all the time..

	EventTrigger = (_EventTrigger*)Consts::ptrEventTrigger;

	HooksEnabled = true;
}

void DisableHooks()
{
	//MessageBoxA(0, "Removing all hooks...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);
	if (OldPrintName)
		UnhookCall(Consts::ptrPrintName, OldPrintName);
	if (OldPrintFPS)
		UnhookCall(Consts::ptrPrintFPS, OldPrintFPS);
	if(OldSetOutfitContextMenu)
		UnhookCall(Consts::ptrSetOutfitContextMenu, OldSetOutfitContextMenu);
	if(OldPartyActionContextMenu)
		UnhookCall(Consts::ptrPartyActionContextMenu, OldPartyActionContextMenu);
	if(OldCopyNameContextMenu)
		UnhookCall(Consts::ptrCopyNameContextMenu, OldCopyNameContextMenu);
	if(OldTradeWithContextMenu)
		UnhookCall(Consts::ptrTradeWithContextMenu, OldTradeWithContextMenu);
	if (OldNopFPS)
		UnNop(Consts::ptrNopFPS, OldNopFPS, 6);

	//OnClickContextMenuEvent..
	//MessageBoxA(0, "Removing context menu click event...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);
	DWORD dwOldProtect, dwNewProtect, funcAddress;
	funcAddress = (DWORD)&MyOnClickContextMenu;
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::prtOnClickContextMenuVf, &Consts::ptrOnClickContextMenu, 4);
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, dwOldProtect, &dwNewProtect); //Restore access

	//recv/send
	//MessageBoxA(0, "Removing send/recv hooks...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);
	VirtualProtect((LPVOID)Consts::ptrSend, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::ptrSend, &OrigSendAddress, 4);
	VirtualProtect((LPVOID)Consts::ptrSend, 4, dwOldProtect, &dwNewProtect);

	VirtualProtect((LPVOID)Consts::ptrRecv, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::ptrRecv, &OrigRecvAddress, 4);
	VirtualProtect((LPVOID)Consts::ptrRecv, 4, dwOldProtect, &dwNewProtect);

	EventTrigger = 0;

	HooksEnabled = false;
}

DWORD HookCall(DWORD dwAddress, DWORD dwFunction)
{   
	DWORD dwOldProtect, dwNewProtect, dwOldCall, dwNewCall;
	//CALL opcode = 0xE8 <4 byte for distance>
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	//Calculate the distance
	dwNewCall = dwFunction - dwAddress - 5;
	memcpy(&callByte[1], &dwNewCall, 4);

	VirtualProtect((LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect); //Gain access to read/write
	memcpy(&dwOldCall, (LPVOID)(dwAddress+1), 4); //Get the old function address for unhooking
	memcpy((LPVOID)(dwAddress), &callByte, 5); //Hook the function
	VirtualProtect((LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect); //Restore access

	return dwOldCall; //Return old funtion address for unhooking
}

void UnhookCall(DWORD dwAddress, DWORD dwOldCall)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	memcpy(&callByte[1], &dwOldCall, 4);

	VirtualProtect((LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), &callByte, 5);
	VirtualProtect((LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect);
}

BYTE* Nop(DWORD dwAddress, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE* OldBytes;
	VirtualProtect((LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	OldBytes = new BYTE[size];
	memcpy(OldBytes, (LPVOID)(dwAddress), size);
	memset((LPVOID)(dwAddress), 0x90, size);
	VirtualProtect((LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);

	return OldBytes;
}

void UnNop(DWORD dwAddress, BYTE* OldBytes, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	VirtualProtect((LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), OldBytes, size);
	VirtualProtect((LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);

	delete [] OldBytes;
	OldBytes = 0;
}

void __declspec(noreturn) UninjectSelf()
{	
	DWORD ExitCode=0;
	__asm
	{
		push hMod
			push ExitCode
			jmp dword ptr [FreeLibraryAndExitThread] 
	}
}

void StartUninjectSelf()
{
	try
	{
		CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)UninjectSelf, hMod, NULL, NULL);
	}
	catch (...)
	{
		MessageBoxA(0,"StartUninjectSelf -> Unable to uninject from process." ,"TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
	}
}

void UnloadSelf()
{
	if(HooksEnabled) 
	{
		//remove all text
		//MessageBoxA(0, "Removing all text...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);
		list<NormalText>::iterator ntIT;
		EnterCriticalSection(&NormalTextCriticalSection);
		for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
		{
			delete [] ntIT->text;
			delete [] ntIT->TextName;
		}
		DisplayTexts.clear();
		LeaveCriticalSection(&NormalTextCriticalSection);


		//remove all contextmenus
		//MessageBoxA(0, "Removing all context menus...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);
		list<ContextMenu>::iterator cmIT;
		EnterCriticalSection(&ContextMenuCriticalSection);
		for(cmIT = ContextMenus.begin(); cmIT != ContextMenus.end(); ++cmIT)
			delete [] cmIT->MenuText;
		ContextMenus.clear();
		LeaveCriticalSection(&ContextMenuCriticalSection);

		DisableHooks();
	}

	//MessageBoxA(0, "Detaching pipe...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);

	pipe.Detach();
	//::DeleteFileA(PipeName.c_str());
	//TerminateThread(PipeThread, EXIT_SUCCESS);

	//MessageBoxA(0, "Deleting critical sections...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);

	DeleteCriticalSection(&PipeReadCriticalSection);
	DeleteCriticalSection(&NormalTextCriticalSection);
	DeleteCriticalSection(&CreatureTextCriticalSection);
	DeleteCriticalSection(&ContextMenuCriticalSection);
	DeleteCriticalSection(&OnClickCriticalSection);
	DeleteCriticalSection(&EventTriggerCriticalSection);

	//MessageBoxA(0, "Uninjecting self...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);

	StartUninjectSelf();

	//MessageBoxA(0, "Done.", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);
}

inline void PipeOnRead()
{
	int position = 0;
	WORD len = Packet::ReadWord(Buffer, &position);
	BYTE PacketID = Packet::ReadByte(Buffer, &position);

	switch (PacketID){

		case PipePacketType_HooksEnableDisable:
			ParseHooksEnableDisable(Buffer, position);
			break;
		case PipePacketType_SetConstant:
			ParseSetConstant(Buffer, position);
			break;
		case PipePacketType_DisplayText:
			ParseDisplayText(Buffer, position);
			break;
		case PipePacketType_RemoveText:
			ParseRemoveText(Buffer, position);
			break;
		case PipePacketType_RemoveAllText:
			RemoveAllText();
			break;
		case PipePacketType_DisplayCreatureText:
			ParseDisplayCreatureText(Buffer, position);
			break;
		case PipePacketType_RemoveCreatureText:
			ParseRemoveCreatureText(Buffer, position);
			break;
		case PipePacketType_UpdateCreatureText:
			ParseUpdateCreatureText(Buffer, position);
			break;
		case PipePacketType_AddContextMenu:
			ParseAddContextMenu(Buffer, position);
			break;
		case PipePacketType_RemoveContextMenu:
			ParseRemoveContextMenu(Buffer, position);
			break;
		case PipePacketType_RemoveAllContextMenus:
			RemoveAllContextMenus();
			break;
		case PipePacketType_UnloadDll:
			UnloadSelf();
			break;
		case PipePacketType_HookSendToServer:		
			ParseHookSendToServer(Buffer, position);
			break;
		case PipePacketType_OnClickContextMenu:
		case PipePacketType_HookReceivedPacket:
		case PipePacketType_HookSentPacket:
			//OUTGOING PACKETS
			break;
		case PipePacketType_EventTrigger:
			ParseEventTrigger(Buffer, position);
			break;
		default:
			MessageBoxA(0, "Unknown PacketType!", "Error!", MB_ICONERROR);
			break;
	}
}

void ParseHooksEnableDisable(BYTE *Buffer, int position)
{
	BYTE Enable = Packet::ReadByte(Buffer, &position);
	/* Testing that every constant contains a value */
	if(!Consts::ptrPrintFPS || !Consts::ptrPrintName || !Consts::ptrShowFPS || !Consts::ptrNopFPS || 
		!Consts::ptrCopyNameContextMenu || !Consts::ptrPartyActionContextMenu || !Consts::ptrSetOutfitContextMenu
		|| !Consts::prtOnClickContextMenuVf || !Consts::ptrTradeWithContextMenu ||
		!Consts::ptrRecv || !Consts::ptrSend || !Consts::ptrEventTrigger) 
	{
		MessageBoxA(0, "Every constant must contain a value before injecting.", "Error", MB_ICONERROR);
		return;
	}

	if(Enable) 
	{
		EnableHooks();
	} 
	else 
	{
		if(!HooksEnabled) 
		{
			MessageBoxA(0, "Please enable the function hooks before uninjecting", "Information", MB_ICONINFORMATION);
			return;
		}

		DisableHooks();
	}
}

void ParseSetConstant(BYTE *Buffer, int position)
{
	PipeConstantType type = (PipeConstantType)Packet::ReadByte(Buffer, &position);
	DWORD value = Packet::ReadDWord(Buffer, &position);

	switch(type)
	{
	case PrintName:
		Consts::ptrPrintName = value;
		break;
	case PrintFPS:
		Consts::ptrPrintFPS = value;
		break;
	case ShowFPS:
		Consts::ptrShowFPS = value;
		break;
	case PrintTextFunc:
		PrintText = (_PrintText*)value;
		break;
	case NopFPS:
		Consts::ptrNopFPS = value;
		break;
	case AddContextMenuFunc:
		Consts::ptrAddContextMenu = value;
		break;
	case OnClickContextMenu:
		Consts::ptrOnClickContextMenu = value;
		break;
	case SetOutfitContextMenu:
		Consts::ptrSetOutfitContextMenu = value;
		break;
	case PartyActionContextMenu:
		Consts::ptrPartyActionContextMenu = value;
		break;
	case CopyNameContextMenu:
		Consts::ptrCopyNameContextMenu = value;
		break;
	case OnClickContextMenuVf:
		Consts::prtOnClickContextMenuVf = value;
		break;
	case TradeWithContextMenu:
		Consts::ptrTradeWithContextMenu = value;
		break;
	case Recv:
		Consts::ptrRecv = value;
		break;
	case Send:
		Consts::ptrSend = value;
		break;
	case EventTriggered:
		Consts::ptrEventTrigger = value;
		break;
	default:
		break;
	};
}

void ParseDisplayText(BYTE *Buffer, int position)
{
	string TextName = Packet::ReadString(Buffer, &position);
	int PosX = Packet::ReadWord(Buffer, &position);
	int PosY = Packet::ReadWord(Buffer, &position);
	int ColorRed = Packet::ReadWord(Buffer, &position);
	int ColorGreen = Packet::ReadWord(Buffer, &position);
	int ColorBlue = Packet::ReadWord(Buffer, &position);
	int Font = Packet::ReadWord(Buffer, &position);
	string Text = Packet::ReadString(Buffer, &position);

	NormalText NewText;
	NewText.b = ColorBlue;
	NewText.g = ColorGreen;
	NewText.r = ColorRed;
	NewText.x = PosX;
	NewText.y = PosY;
	NewText.font = Font;

	NewText.TextName = new char[TextName.size() + 1];
	NewText.text = new char[Text.size() + 1];

	memcpy(NewText.TextName, TextName.c_str(), TextName.size() + 1);
	memcpy(NewText.text, Text.c_str(), Text.size() + 1);

	EnterCriticalSection(&NormalTextCriticalSection);

	DisplayTexts.push_back(NewText);

	LeaveCriticalSection(&NormalTextCriticalSection);
}

void ParseRemoveText(BYTE *Buffer, int position)
{
	string RemovalTextName = Packet::ReadString(Buffer, &position);
	list<NormalText>::iterator ntIT;
	EnterCriticalSection(&NormalTextCriticalSection);

	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ) 
	{
		if (ntIT->TextName == RemovalTextName)
		{
			delete [] ntIT->TextName;
			delete [] ntIT->text;
			ntIT = DisplayTexts.erase(ntIT);
		} 
		else
			++ntIT;
	}

	LeaveCriticalSection(&NormalTextCriticalSection);

}

void RemoveAllText()
{
	list<NormalText>::iterator ntIT;
	EnterCriticalSection(&NormalTextCriticalSection);

	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
	{
		delete [] ntIT->text;
		delete [] ntIT->TextName;
	}

	DisplayTexts.clear();
	LeaveCriticalSection(&NormalTextCriticalSection);
}

void ParseDisplayCreatureText(BYTE *Buffer, int position)
{
	int Id = Packet::ReadDWord(Buffer, &position);
	string CName = Packet::ReadString(Buffer, &position);
	int nX = Packet::ReadShort(Buffer, &position);
	int nY = Packet::ReadShort(Buffer, &position);
	int ColorR = Packet::ReadWord(Buffer, &position);
	int ColorG = Packet::ReadWord(Buffer, &position);
	int ColorB = Packet::ReadWord(Buffer, &position);
	int TxtFont = Packet::ReadWord(Buffer, &position);
	string Text = Packet::ReadString(Buffer, &position);
	char *lpText = (char*)calloc(Text.size() + 1, sizeof(char));
	char *cText = (char*)calloc(CName.size() + 1, sizeof(char));
	strcpy(lpText, Text.c_str());
	strcpy(cText, CName.c_str());
	PlayerText Creature = {0};
	Creature.cB = ColorB;
	Creature.cG = ColorG;
	Creature.cR = ColorR;
	Creature.CreatureId = Id;
	Creature.DisplayText = lpText;
	Creature.CreatureName = cText;
	Creature.RelativeX = nX;
	Creature.RelativeY = nY;
	Creature.TextFont = TxtFont;

	EnterCriticalSection(&CreatureTextCriticalSection);
	CreatureTexts.push_back(Creature);
	LeaveCriticalSection(&CreatureTextCriticalSection);
}

void ParseRemoveCreatureText(BYTE *Buffer, int position)
{	
	int Id = Packet::ReadDWord(Buffer, &position);
	string Name = Packet::ReadString(Buffer, &position);

	list<PlayerText>::iterator ptIT;
	EnterCriticalSection(&CreatureTextCriticalSection);
	for(ptIT = CreatureTexts.begin(); ptIT != CreatureTexts.end(); ) 
	{
		if (ptIT->CreatureId == 0) 
		{
			if (ptIT->CreatureName == Name) 
			{
				free(ptIT->DisplayText);
				free(ptIT->CreatureName);
				ptIT->DisplayText = 0; //Just to make sure I won't try to free this twice
				ptIT->CreatureName = 0;
				ptIT = CreatureTexts.erase(ptIT);
			} 
			else
				++ptIT;
		} 
		else if (ptIT->CreatureId == Id) 
		{
			free(ptIT->DisplayText);
			free(ptIT->CreatureName);
			ptIT->DisplayText = 0; //Just to make sure I won't try to free this twice
			ptIT->CreatureName = 0;
			ptIT = CreatureTexts.erase(ptIT);
		} 
		else 
			++ptIT;
	}
	LeaveCriticalSection(&CreatureTextCriticalSection);
}

void ParseUpdateCreatureText(BYTE *Buffer, int position)
{
	int ID = Packet::ReadDWord(Buffer, &position);
	string CName = Packet::ReadString(Buffer, &position);
	int PosX = Packet::ReadShort(Buffer, &position);
	int PosY = Packet::ReadShort(Buffer, &position);
	string NewText = Packet::ReadString(Buffer, &position);
	char *lpNewText = (char*)calloc(NewText.size() + 1, sizeof(char));
	char *OldText;
	strcpy(lpNewText, NewText.c_str());

	EnterCriticalSection(&CreatureTextCriticalSection);

	list<PlayerText>::iterator newit;
	for(newit = CreatureTexts.begin(); newit != CreatureTexts.end(); ++newit) 
	{
		if (newit->CreatureId == 0) 
		{
			if (newit->CreatureName == CName && newit->RelativeX == PosX && newit->RelativeY == PosY) 
			{
				OldText = newit->DisplayText;
				strcpy(OldText, "");
				newit->DisplayText = lpNewText;
				free(OldText);
				OldText = 0;
				break;
			}
		}
		else if (newit->CreatureId == ID && newit->RelativeX == PosX && newit->RelativeY == PosY) 
		{
			OldText = newit->DisplayText;
			strcpy(OldText, "");
			newit->DisplayText = lpNewText;
			free(OldText);
			OldText = 0;
			break;
		}
	}

	LeaveCriticalSection(&CreatureTextCriticalSection);
}

void ParseAddContextMenu(BYTE *Buffer, int position)
{
	int id = Packet::ReadDWord(Buffer, &position);
	string text=Packet::ReadString(Buffer, &position);
	BYTE type = Packet::ReadByte(Buffer,&position);
	BYTE hasSeparator=Packet::ReadByte(Buffer,&position);

	ContextMenu ctxt;
	ctxt.EventId = id;
	ctxt.Type = type;
	ctxt.HasSeparator = hasSeparator;		
	ctxt.MenuText = new char[text.size()+1];

	memcpy(ctxt.MenuText, text.c_str(), text.size() + 1);

	EnterCriticalSection(&ContextMenuCriticalSection);
	ContextMenus.push_back(ctxt);
	LeaveCriticalSection(&ContextMenuCriticalSection);
}

void ParseRemoveContextMenu(BYTE *Buffer, int position)
{
	int id = Packet::ReadDWord(Buffer, &position);
	string text = Packet::ReadString(Buffer, &position);
	BYTE type = Packet::ReadByte(Buffer,&position);
	BYTE hasSeparator = Packet::ReadByte(Buffer,&position);

	EnterCriticalSection(&ContextMenuCriticalSection);

	list<ContextMenu>::iterator cmIT;
	for(cmIT = ContextMenus.begin(); cmIT != ContextMenus.end(); ) 
	{
		if (cmIT->EventId == id && cmIT->MenuText == text
			&& cmIT->Type == type && cmIT->HasSeparator == hasSeparator)
		{
			delete [] cmIT->MenuText;
			cmIT = ContextMenus.erase(cmIT);
		} 
		else 
			++cmIT;
	}

	LeaveCriticalSection(&ContextMenuCriticalSection);
}

void RemoveAllContextMenus()
{
	list<ContextMenu>::iterator cmIT;

	EnterCriticalSection(&ContextMenuCriticalSection);

	for(cmIT = ContextMenus.begin(); cmIT != ContextMenus.end(); ++cmIT)
		delete [] cmIT->MenuText;

	ContextMenus.clear();

	LeaveCriticalSection(&ContextMenuCriticalSection);
}

void ParseHookSendToServer(BYTE *Buffer, int position)
{			
	int packetLen = Packet::ReadWord(Buffer,&position);
	char* buf =new char[packetLen+2];
	memcpy((LPVOID)buf,(LPVOID)(Buffer+position-2),packetLen+2);

	int ret=OrigSend(sock,buf,packetLen+2,0);	
	delete buf;
}

void ParseEventTrigger(BYTE *Buffer, int position)
{
	int type = Packet::ReadDWord(Buffer, &position);

	EnterCriticalSection(&EventTriggerCriticalSection);
	EventTrigger(type, NULL, NULL);
	LeaveCriticalSection(&EventTriggerCriticalSection);
}

void PipeThreadProc(HMODULE Module)
{
	//Connect to Pipe
	if (WaitNamedPipeA(PipeName.c_str(), NMPWAIT_WAIT_FOREVER)) 
	{
		pipe.Attach(::CreateFileA(PipeName.c_str(), GENERIC_READ | GENERIC_WRITE , 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL));

		if (pipe == INVALID_HANDLE_VALUE)
		{
			errorStatus = ::GetLastError();
			MessageBoxA(0, "Pipe connection failed!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
			UnloadSelf();
			return;
		} 
		else 
		{
			//Pipe is ready. Let's start listening for incoming packets
			PipeConnected = true;
			if(!::ReadFileEx(pipe, Buffer, sizeof(Buffer), &overlapped, ReadFileCompleted))
			{
				errorStatus = ::GetLastError();
				MessageBoxA(0, "Pipe read error!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
				UnloadSelf();
				return;
			} 
			else 
			{
				while (errorStatus == ERROR_SUCCESS)
				{
					const DWORD sleepResult = ::SleepEx(INFINITE, TRUE);
					assert(WAIT_IO_COMPLETION == sleepResult);
				}
			}
		}
	} 
	else 
		MessageBoxA(0, "Failed waiting for pipe, maybe pipe is not ready?", "TibiaAPI Injected DLL - Fatal Error", 0);
}

void CALLBACK ReadFileCompleted(DWORD errorCode, DWORD bytesCopied, OVERLAPPED* overlapped)
{
	errorStatus = errorCode;

	if (errorStatus == ERROR_SUCCESS)
	{
		PipeOnRead();

		if (!::ReadFileEx(pipe, Buffer, sizeof(Buffer), overlapped, ReadFileCompleted))
		{
			errorStatus = ::GetLastError();
			MessageBoxA(0, "Pipe read error!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
			UnloadSelf();
		}
	}
	else
	{
		// pipe disconnected clean everything and remove the hook
		//MessageBoxA(0, "Pipe disconnected, cleaning up.", "TibiaAPI Injected DLL - Cleaning up", MB_ICONERROR);

		UnloadSelf();
	}
}

extern "C" bool APIENTRY DllMain (HMODULE hModule, DWORD reason, LPVOID reserved)
{
	switch (reason)
	{
		case DLL_PROCESS_ATTACH: //DLL was injected
		{
			hMod = hModule;
			/* Get Current Process ID and use it as Pipename (Pipe is named as TibiaAPI<processID> */
			DWORD CurrentPID = GetCurrentProcessId();
			std::stringstream sout;
			sout << "\\\\.\\pipe\\TibiaAPI" << CurrentPID;
			PipeName =  sout.str();

			InitializeCriticalSection(&PipeReadCriticalSection);
			InitializeCriticalSection(&NormalTextCriticalSection);
			InitializeCriticalSection(&CreatureTextCriticalSection);
			InitializeCriticalSection(&ContextMenuCriticalSection);
			InitializeCriticalSection(&OnClickCriticalSection);
			InitializeCriticalSection(&EventTriggerCriticalSection);

			PipeConnected = false;
			//Start new thread for Pipe
			PipeThread = CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)PipeThreadProc, hMod, NULL, NULL);
			break;
		}
		case DLL_PROCESS_DETACH: //DLL was uninjected
		{
			TerminateThread(PipeThread, EXIT_SUCCESS);
			DeleteCriticalSection(&PipeReadCriticalSection);
			DeleteCriticalSection(&NormalTextCriticalSection);
			DeleteCriticalSection(&CreatureTextCriticalSection);
			DeleteCriticalSection(&ContextMenuCriticalSection);
			DeleteCriticalSection(&OnClickCriticalSection);
			DeleteCriticalSection(&EventTriggerCriticalSection);
			break;
		}
	}

	return true;
}
