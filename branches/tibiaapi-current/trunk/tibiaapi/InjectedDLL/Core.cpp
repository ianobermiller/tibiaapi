#include "stdafx.h"
#include <windows.h>
#include <string>
#include <sstream>
#include <list>
#include <assert.h>
#include "Constants.h"
#include "Core.h"
#include "Packet.h"

#ifdef _MANAGED
#pragma managed(push, off)
#endif

using namespace std;

/* 
Thanks for the help of the following members of tpforums.org who contributed directly ou indirectly to this:
	Stiju & Zionz         : text display 
	Vitor                 : context menus, event trigger
	Stepler & yaboomaster : item and skin drawing
	DarkstaR			  : case_insensitive_compare
	TibiaAPI Team
*/



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
DWORD OldLookContextMenu = 0;
list<ContextMenu> ContextMenus;    //Used for storing the context menus that will be added on this call
//recv/send
DWORD OrigSendAddress = 0;
DWORD OrigRecvAddress = 0;
SOCKET sock = 0;
//icon
list<Icon> Icons;
//skin
list<Skin> Skins;

//Asynchronisation variables
//CHANDLE pipe;						//Holds the Pipe handle (CHandle is from ATL library)
HANDLE pipe;
OVERLAPPED overlapped = { 0 };		
DWORD errorStatus = ERROR_SUCCESS;
bool mustUnload = false;

DWORD CurrentPID;
BOOL fUnicode;
HWND hwndTibia=0;
WNDPROC oldWndProc;


void MyPrintNameWork( char* lpText, int nX, int nY)
{		
	char* someText;
	DWORD entityID=*(DWORD*)(lpText - 4);
	list<PlayerText>::iterator it;
	int x,y,font,r,g,b;

	//Displaying texts
	EnterCriticalSection(&CreatureTextCriticalSection);

	for(it = CreatureTexts.begin(); it != CreatureTexts.end(); ++it) 
	{
			//compare insensitive incase creature name isn't case sensitive (thanks DarkstaR)
		if(entityID == it->CreatureId || (it->CreatureId == 0 && case_insensitive_compare(lpText, it->CreatureName)))
		{
			someText=it->DisplayText;
			x=nX + it->RelativeX;
			y=nY + it->RelativeY;
			font=it->TextFont;
			r=it->cR;
			g=it->cG;
			b=it->cB;
			_asm
			{
				push 0
				push b
				push g
				push r
				push font
				push y
				push x
				push 1
				mov ecx, someText
				call PrintText
				add esp,32
			}
		}
	}
	LeaveCriticalSection(&CreatureTextCriticalSection);
	
}

void MyPrintFpsWork()
{
	char* someText;	

	int x, y, font, r, g, b, width, height, guiID, count, itemID, bSize;

	EnterCriticalSection(&NormalTextCriticalSection);
	list<NormalText>::iterator ntIT;	
	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
	{
		someText=ntIT->text;
		x=ntIT->x;
		y=ntIT->y;
		font=ntIT->font;
		r=ntIT->r;
		g=ntIT->g;
		b=ntIT->b;
		_asm
		{
			push 0
			push b
			push g
			push r
			push font
			push y
			push x
			push 1
			mov ecx, someText
			call PrintText
			add esp,32
		}
	}
	LeaveCriticalSection(&NormalTextCriticalSection);
	
	EnterCriticalSection(&DrawSkinCriticalSection);
	list<Skin>::iterator sIT;
	for(sIT = Skins.begin(); sIT != Skins.end(); ++sIT)
	{
		x=sIT->X;
		y=sIT->Y;
		width=sIT->Width;
		height=sIT->Height;
		guiID=sIT->GUIId;

		__asm
		{
			push 0
			push 0
			push height
			push width
			push y
			push x
			push 1
			mov ecx, guiID
			call DrawSkin
			add esp, 28
		}
	}
	LeaveCriticalSection(&DrawSkinCriticalSection);
	

	EnterCriticalSection(&DrawItemCriticalSection);
	list<Icon>::iterator iIT;
	for(iIT = Icons.begin(); iIT != Icons.end(); ++iIT)
	{
		x=iIT->X;
		y=iIT->Y;
		count=iIT->ItemCount;
		itemID=iIT->ItemId;
		bSize=iIT->BitmapSize;
		font=iIT->TextFont;
		r=iIT->cR;
		g=iIT->cG;
		b=iIT->cB;

		__asm
		{
			push 0
			push 2
			push b
			push g
			push r
			push font
			push bSize
			push bSize
			push y
			push x
			push 0
			push 0
			push 0
			push itemID
			push count
			push 0
			push y
			push x
			push 1
			mov ecx, [bSize]
			call DrawItem
			add esp, 76
		}
	}
	LeaveCriticalSection(&DrawItemCriticalSection);

}

void MyContextMenuWork(BYTE myType,void* thisPointer)
{	
	list<ContextMenu>::iterator it;
	for(it = ContextMenus.begin(); it != ContextMenus.end(); ++it)
	{
		if(it->Type == myType || it->Type == 0x00)
		{			
			const char* custom = it->MenuText;
			int eventID = it->EventId;

			if(it->HasSeparator == 0x00)
			{
				__asm
				{
					mov edx, 0
					mov ecx, thisPointer
					push custom
					push eventID
					call Consts::ptrAddContextMenu
					add esp, 8
				}
			}
			else if(it->HasSeparator == 0x01)
			{
				__asm
				{
					mov edx, 0
					mov ecx, thisPointer
					mov byte ptr[ecx+0x30], 1
					push custom
					push eventID
					call Consts::ptrAddContextMenu
					add esp, 8
				}
			}
		}
	}
}

void MyOnClickContextMenuWork (int eventId)
{	
	Packet* packet = new Packet(5);
	packet->AddByte(0x0C);
	packet->AddDWord(eventId);
	PipeWrite(packet);
}

LRESULT WINAPI SubClassProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	if(msg == WM_LBUTTONDOWN)
	{
		POINT pos;
		pos.x=(WORD)lParam;
		pos.y=(WORD)(lParam>>16);
		
		
		EnterCriticalSection(&DrawItemCriticalSection);

		list<Icon>::iterator iIT;
		for(iIT = Icons.begin(); iIT != Icons.end(); ++iIT)	
		{
			if(pos.x >=iIT->X && pos.x <=iIT->X+iIT->BitmapSize)
			{
				if(pos.y >=iIT->Y && pos.y <=iIT->Y+iIT->BitmapSize)
				{
					Packet* packet = new Packet(5);
					packet->AddByte(0x15);
					packet->AddDWord(iIT->IconId);
					PipeWrite(packet);
				}
			}
		}
		LeaveCriticalSection(&DrawItemCriticalSection);
	}
	return fUnicode ? CallWindowProcW(oldWndProc, hwnd, msg, wParam, lParam) : 
					CallWindowProcA(oldWndProc, hwnd, msg, wParam, lParam);
}

void __declspec(naked) MyPrintName(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue,  int nAlign)
{	
	__asm
	{
		pop edx
		mov ebx, ecx
		call PrintText

		pushad
		pushfd		
		
		//nY
		mov eax, dword ptr ss:[ebp+0xFFFFAF7C]
		add eax, dword ptr ss:[ebp+0xFFFFAF9C]
		push eax
		//nX
		mov eax, dword ptr ss:[ebp+0xFFFFAEF0]
		add eax, dword ptr ss:[ebp+0xFFFFAF90]
		push eax
		//lpText
		push ebx
		call MyPrintNameWork
		add esp, 12

		popfd
		popad
		mov edx,[Consts::ptrPrintName]
		add edx,5
		jmp edx
	}
	
}

void __declspec(naked) MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue,  int nAlign)
{
	__asm
	{
			pop edx

			mov edx,[Consts::ptrShowFPS]
			cmp [edx],0
			je skipPrintText
			call PrintText

		skipPrintText:

			pushad
			pushfd

			call MyPrintFpsWork

			popfd
			popad

			mov edx, [Consts::ptrPrintFPS]
			add edx, 5
			jmp edx
	}
}

void __declspec(naked) MySetOutfitContextMenu (int eventId, const char* text)
{
	__asm
	{
			pop ebx
			call Consts::ptrAddContextMenu
					
			pushad
			pushfd
			
			push esi
			push 1
			call MyContextMenuWork
			add esp, 8
					
			popfd
			popad

			mov ebx, [Consts::ptrSetOutfitContextMenu]
			add ebx, 5
			jmp ebx			
	}
}

void __declspec(naked) MyCopyNameContextMenu (int eventId, const char* text)
{
	__asm
	{
			pop ebx
			call Consts::ptrAddContextMenu
					
			pushad
			pushfd
			
			push esi
			push 3
			call MyContextMenuWork
			add esp, 8
					
			popfd
			popad

			mov ebx, [Consts::ptrCopyNameContextMenu]
			add ebx, 5
			jmp ebx			
	}
}

void __declspec(naked) MyTradeWithContextMenu (int eventId, const char* text)
{
	__asm
	{
			pop ebx
			call Consts::ptrAddContextMenu
					
			pushad
			pushfd
			
			push esi
			push 4
			call MyContextMenuWork
			add esp, 8
					
			popfd
			popad

			mov ebx, [Consts::ptrTradeWithContextMenu]
			add ebx, 5
			jmp ebx			
	}
}

void __declspec(naked) MyLookContextMenu (int eventId, const char* text)
{
	__asm
	{
			pop ebx
			call Consts::ptrAddContextMenu
					
			pushad
			pushfd
			
			push esi
			push 5
			call MyContextMenuWork
			add esp, 8
					
			popfd
			popad

			mov ebx, [Consts::ptrLookContextMenu]
			add ebx, 5
			jmp ebx			
	}
}

void __declspec(naked) MyOnClickContextMenu (int eventId)
{
	__asm
	{
			pop ebx
			mov esi, [esp]
			cmp esi, 0x2000
			jl customEvents

			call Consts::ptrOnClickContextMenu
			jmp _leave

		customEvents:
			pushad
			pushfd

			push esi
			call MyOnClickContextMenuWork
			add esp, 4

			popfd
			popad
		_leave:
			jmp ebx
	}
}

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
		PipeWrite(packet);
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
		PipeWrite(packet);
	}

	return OrigSend(s,buf,len,flags);
}

bool case_insensitive_compare( string a, string b)
{ 
	char char1, char2, blank = ' ' ;   
	int len1 = a.length() ; 
	int len2 = b.length() ;  

	if (len1 != len2) return false ; 
	
	for (int i = 0 ; i < len1 ; ++i )
	{ 
		// get a single character from the current position in the string
		char1 = *(a.substr(i,1).data());
		char2 = *(b.substr(i,1).data()); 
		// make lowercase for compare 
		char1 |= blank;
		char2 |= blank; 
		//Test
		if (char1 == char2) continue; 
		return false; 
	} 
	//Everything matched up, return true
	return true; 
} 

void EnableHooks()
{
	if(HooksEnabled)
	{
		return;
	}
	
	DWORD dwOldProtect, dwNewProtect, funcAddress;	
	


	OldPrintName = HookCall(Consts::ptrPrintName, (DWORD)&MyPrintName);
	OldPrintFPS = HookCall(Consts::ptrPrintFPS, (DWORD)&MyPrintFps);	
	OldNopFPS = Nop(Consts::ptrNopFPS, 6);

	
	OldSetOutfitContextMenu = HookCall(Consts::ptrSetOutfitContextMenu, (DWORD)&MySetOutfitContextMenu);
	OldCopyNameContextMenu = HookCall(Consts::ptrCopyNameContextMenu, (DWORD)&MyCopyNameContextMenu);
	OldTradeWithContextMenu = HookCall(Consts::ptrTradeWithContextMenu, (DWORD)&MyTradeWithContextMenu);
	OldLookContextMenu = HookCall(Consts::ptrLookContextMenu,(DWORD) &MyLookContextMenu);

	funcAddress = (DWORD)&MyOnClickContextMenu;
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::prtOnClickContextMenuVf, &funcAddress, 4);
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, dwOldProtect, &dwNewProtect);
				
	
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
	

	oldWndProc = (WNDPROC) ((fUnicode) ? SetWindowLongPtrW(hwndTibia, GWLP_WNDPROC, (LONG_PTR)SubClassProc) : 
											SetWindowLongPtrA(hwndTibia, GWLP_WNDPROC, (LONG_PTR)SubClassProc));

	HooksEnabled = true;
}

void DisableHooks()
{	
	DWORD dwOldProtect, dwNewProtect, funcAddress;

	funcAddress = (DWORD)&MyOnClickContextMenu;
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::prtOnClickContextMenuVf, &Consts::ptrOnClickContextMenu, 4);
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, dwOldProtect, &dwNewProtect);
	

	VirtualProtect((LPVOID)Consts::ptrSend, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::ptrSend, &OrigSendAddress, 4);
	VirtualProtect((LPVOID)Consts::ptrSend, 4, dwOldProtect, &dwNewProtect);

	VirtualProtect((LPVOID)Consts::ptrRecv, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::ptrRecv, &OrigRecvAddress, 4);
	VirtualProtect((LPVOID)Consts::ptrRecv, 4, dwOldProtect, &dwNewProtect);


	if (OldPrintName)
		UnhookCall(Consts::ptrPrintName, OldPrintName);
	if (OldPrintFPS)
		UnhookCall(Consts::ptrPrintFPS, OldPrintFPS);	
	if(OldSetOutfitContextMenu)
		UnhookCall(Consts::ptrSetOutfitContextMenu, OldSetOutfitContextMenu);
	if(OldCopyNameContextMenu)
		UnhookCall(Consts::ptrCopyNameContextMenu, OldCopyNameContextMenu);
	if(OldTradeWithContextMenu)
		UnhookCall(Consts::ptrTradeWithContextMenu, OldTradeWithContextMenu);
	if(OldLookContextMenu)
		UnhookCall(Consts::ptrLookContextMenu, OldLookContextMenu);
	if (OldNopFPS)
		UnNop(Consts::ptrNopFPS, OldNopFPS, 6);
		
	
	fUnicode ? SetWindowLongPtrW(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc) : 
			SetWindowLongPtrA(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc);

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
		push hMod;
		push ExitCode;
		jmp dword ptr [FreeLibraryAndExitThread] ;
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
		#if _DEBUG
			MessageBoxA(0,"StartUninjectSelf -> Unable to uninject from process." ,"TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR | MB_TOPMOST );
		#endif
	}
}

void UnloadSelf()
{
	if(HooksEnabled) 
	{
		//remove all text
		list<NormalText>::iterator ntIT;
		EnterCriticalSection(&NormalTextCriticalSection);
		for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
		{
			delete [] ntIT->text;
			delete [] ntIT->TextName;
		}
		DisplayTexts.clear();
		LeaveCriticalSection(&NormalTextCriticalSection);
		

		list<ContextMenu>::iterator cmIT;
		EnterCriticalSection(&ContextMenuCriticalSection);
		for(cmIT = ContextMenus.begin(); cmIT != ContextMenus.end(); ++cmIT)
			delete [] cmIT->MenuText;
		ContextMenus.clear();
		LeaveCriticalSection(&ContextMenuCriticalSection);

		
		EnterCriticalSection(&DrawItemCriticalSection);
		Icons.clear();
		LeaveCriticalSection(&DrawItemCriticalSection);

		DisableHooks();
	}

	#if _DEBUG
		MessageBoxA(0, "Detaching pipe...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif


	CloseHandle(pipe);
	
	DeleteCriticalSection(&PipeReadCriticalSection);
	DeleteCriticalSection(&PipeWriteCriticalSection);
	DeleteCriticalSection(&NormalTextCriticalSection);
	DeleteCriticalSection(&CreatureTextCriticalSection);
	DeleteCriticalSection(&ContextMenuCriticalSection);
	DeleteCriticalSection(&EventTriggerCriticalSection);
	DeleteCriticalSection(&DrawItemCriticalSection);
	DeleteCriticalSection(&DrawSkinCriticalSection);

	#if _DEBUG
		MessageBoxA(0, "Uninjecting self...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif

	StartUninjectSelf();

	#if _DEBUG
		MessageBoxA(0, "Done.", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif
}

void PipeWriteProc(LPVOID lpParameter)
{
	EnterCriticalSection(&PipeWriteCriticalSection);
	Packet* packet=(Packet*)lpParameter;
	WriteFileEx(pipe, packet->GetPacket(), packet->GetSize(), &overlapped, NULL); 	
	LeaveCriticalSection(&PipeWriteCriticalSection);
}

inline void PipeWrite(Packet* p)
{
	CreateThread(NULL, NULL,(LPTHREAD_START_ROUTINE) PipeWriteProc ,(LPVOID)p,0,NULL);
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
			mustUnload=true;
			break;
		case PipePacketType_HookSendToServer:		
			ParseHookSendToServer(Buffer, position);
			break;
		case PipePacketType_EventTrigger:
			ParseEventTrigger(Buffer, position);
			break;
		case PipePacketType_OnClickContextMenu:
		case PipePacketType_HookReceivedPacket:
		case PipePacketType_HookSentPacket:
		case PipePacketType_OnClickIcon:
			//OUTGOING PACKETS
			break;
		case PipePacketType_AddIcon:
			ParseAddIcon(Buffer, position);
			break;
		case PipePacketType_UpdateIcon:
			ParseUpdateIcon(Buffer, position);
			break;
		case PipePacketType_RemoveIcon:
			ParseRemoveIcon(Buffer, position);
			break;
		case PipePacketType_RemoveAllIcons:
			ParseRemoveAllIcons();
			break;
		case PipePacketType_AddSkin:
			ParseAddSkin(Buffer, position);
			break;
		case PipePacketType_RemoveSkin:
			ParseRemoveSkin(Buffer, position);
			break;
		case PipePacketType_UpdateSkin:
			ParseUpdateSkin(Buffer, position);
			break;
		case PipePacketType_RemoveAllSkins:
			ParseRemoveAllSkins();
			break;
		default:
			#if _DEBUG
				MessageBoxA(0, "Unknown PacketType!", "Error!", MB_ICONERROR);
			#endif
			break;
	}
}

void ParseHooksEnableDisable(BYTE *Buffer, int position)
{
	BYTE Enable = Packet::ReadByte(Buffer, &position);
	/* Testing that every constant contains a value */
	if(!Consts::ptrPrintFPS || !Consts::ptrPrintName || !Consts::ptrShowFPS || !Consts::ptrNopFPS || 
		!Consts::ptrCopyNameContextMenu || !Consts::ptrSetOutfitContextMenu || 
		!Consts::prtOnClickContextMenuVf || !Consts::ptrTradeWithContextMenu || !Consts::ptrRecv || !Consts::ptrSend || 
		!Consts::ptrEventTrigger || !Consts::ptrLookContextMenu || !Consts::ptrAddContextMenu) 
	{
		#if _DEBUG
			MessageBoxA(0, "Every constant must contain a value before injecting.", "Error", MB_ICONERROR);
		#endif
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
			#if _DEBUG
				MessageBoxA(0, "Enable the function hooks before uninjecting", "Information", MB_ICONINFORMATION);
			#endif
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
		Consts::ptrShowFPS =value;
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
	case LookContextMenu:
		Consts::ptrLookContextMenu = value;
		break;
	case DrawItemFunc:		
		DrawItem = (_DrawItem*)value;
		break;
	case DrawSkinFunc:
		DrawSkin = (_DrawSkin*)value;
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

void ParseAddIcon(BYTE *Buffer, int position)
{
	Icon icon;
	icon.IconId=Packet::ReadDWord(Buffer, &position);
	icon.X=Packet::ReadWord(Buffer, &position);
	icon.Y=Packet::ReadWord(Buffer, &position);
	icon.BitmapSize=Packet::ReadWord(Buffer, &position);
	icon.ItemId=Packet::ReadWord(Buffer, &position);
	icon.ItemCount=Packet::ReadWord(Buffer, &position);
	icon.TextFont=Packet::ReadByte(Buffer, &position);
	icon.cR=Packet::ReadByte(Buffer, &position);
	icon.cG=Packet::ReadByte(Buffer, &position);
	icon.cB=Packet::ReadByte(Buffer, &position);
	
	EnterCriticalSection(&DrawItemCriticalSection);
	Icons.push_back(icon);
	LeaveCriticalSection(&DrawItemCriticalSection);
}

void ParseUpdateIcon(BYTE *Buffer, int position)
{
	Icon icon;
	icon.IconId=Packet::ReadDWord(Buffer, &position);
	icon.X=Packet::ReadWord(Buffer, &position);
	icon.Y=Packet::ReadWord(Buffer, &position);
	icon.BitmapSize=Packet::ReadWord(Buffer, &position);
	icon.ItemId=Packet::ReadWord(Buffer, &position);
	icon.ItemCount=Packet::ReadWord(Buffer, &position);
	icon.TextFont=Packet::ReadByte(Buffer, &position);
	icon.cR=Packet::ReadByte(Buffer, &position);
	icon.cG=Packet::ReadByte(Buffer, &position);
	icon.cB=Packet::ReadByte(Buffer, &position);

	EnterCriticalSection(&DrawItemCriticalSection);
	list<Icon>::iterator miIT;
	for(miIT = Icons.begin(); miIT != Icons.end(); ++miIT)		
		if(miIT->IconId == icon.IconId)
		{			
			miIT->IconId=icon.IconId;
			miIT->X=icon.X;
			miIT->Y=icon.Y;
			miIT->BitmapSize=icon.BitmapSize;
			miIT->ItemId=icon.ItemId;
			miIT->ItemCount=icon.ItemCount;
			miIT->TextFont=icon.TextFont;
			miIT->cR=icon.cR;
			miIT->cG=icon.cG;
			miIT->cB=icon.cB;
			break;
		}
	LeaveCriticalSection(&DrawItemCriticalSection);

}

void ParseRemoveIcon(BYTE *Buffer, int position)
{
	int iconId = Packet::ReadDWord(Buffer, &position);
	EnterCriticalSection(&DrawItemCriticalSection);
	list<Icon>::iterator oiIT;
	for(oiIT = Icons.begin(); oiIT != Icons.end(); ++oiIT)		
		if(oiIT->IconId == iconId)
		{			
			oiIT=Icons.erase(oiIT);
			break;
		}
	LeaveCriticalSection(&DrawItemCriticalSection);
}

void ParseRemoveAllIcons()
{
	EnterCriticalSection(&DrawItemCriticalSection);

	Icons.clear();

	LeaveCriticalSection(&DrawItemCriticalSection);

}

void ParseAddSkin(BYTE *Buffer, int position)
{
	Skin skin;
	skin.SkinId = Packet::ReadDWord(Buffer, &position);
	skin.X = Packet::ReadWord(Buffer, &position);
	skin.Y = Packet::ReadWord(Buffer, &position);
	skin.Width = Packet::ReadWord(Buffer, &position);
	skin.Height = Packet::ReadWord(Buffer, &position);
	skin.GUIId = Packet::ReadWord(Buffer, &position);

	EnterCriticalSection(&DrawSkinCriticalSection);
	Skins.push_back(skin);
	LeaveCriticalSection(&DrawSkinCriticalSection);
}

void ParseRemoveSkin(BYTE *Buffer, int position)
{
	int skinId = Packet::ReadDWord(Buffer, &position);

	EnterCriticalSection(&DrawSkinCriticalSection);
	list<Skin>::iterator osIT;
	for(osIT = Skins.begin(); osIT != Skins.end(); ++osIT)
		if(osIT->SkinId == skinId)
		{
			osIT = Skins.erase(osIT);
			break;
		}
	LeaveCriticalSection(&DrawSkinCriticalSection);
}

void ParseUpdateSkin(BYTE *Buffer, int position)
{
	Skin skin;
	skin.SkinId = Packet::ReadDWord(Buffer, &position);
	skin.X = Packet::ReadWord(Buffer, &position);
	skin.Y = Packet::ReadWord(Buffer, &position);
	skin.Width = Packet::ReadWord(Buffer, &position);
	skin.Height = Packet::ReadWord(Buffer, &position);
	skin.GUIId = Packet::ReadWord(Buffer, &position);

	EnterCriticalSection(&DrawSkinCriticalSection);
	list<Skin>::iterator msIT;
	for(msIT = Skins.begin(); msIT != Skins.end(); ++msIT)
		if(msIT->SkinId == skin.SkinId)
		{
			msIT->SkinId = skin.SkinId;
			msIT->X = skin.X;
			msIT->Y = skin.Y;
			msIT->Width = skin.Width;
			msIT->Height = skin.Height;
			msIT->GUIId = skin.GUIId;
			break;
		}
	LeaveCriticalSection(&DrawSkinCriticalSection);
}

void ParseRemoveAllSkins()
{
	EnterCriticalSection(&DrawSkinCriticalSection);

	Skins.clear();

	LeaveCriticalSection(&DrawSkinCriticalSection);
}

void PipeThreadProc(HMODULE Module)
{
	//Connect to Pipe
	if (WaitNamedPipeA(PipeName.c_str(), NMPWAIT_WAIT_FOREVER)) 
	{
		//pipe.Attach(::CreateFileA(PipeName.c_str(), GENERIC_READ | GENERIC_WRITE , 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL));
		pipe=CreateFileA(PipeName.c_str(), GENERIC_READ | GENERIC_WRITE , 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL);

		if (pipe == INVALID_HANDLE_VALUE)
		{
			errorStatus = ::GetLastError();
			#if _DEBUG
				MessageBoxA(0, "Pipe connection failed!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
			#endif
			UnloadSelf();
			return;
		} 
		else 
		{
			//Pipe is ready. Let's start listening for incoming packets
			PipeConnected = true;
			
			if (!mustUnload)
			{
				if(!::ReadFileEx(pipe, Buffer, sizeof(Buffer), &overlapped, ReadFileCompleted))
				{
					errorStatus = ::GetLastError();					
					#if _DEBUG
						MessageBoxA(0, "Pipe read error!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
					#endif
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
			else
			{
				UnloadSelf();
			}
		}
	} 
	else 
	{
		#if _DEBUG
			MessageBoxA(0, "Failed waiting for pipe, maybe pipe is not ready?", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
		#endif
	}
}

void CALLBACK ReadFileCompleted(DWORD errorCode, DWORD bytesCopied, OVERLAPPED* overlapped)
{
	errorStatus = errorCode;

	if (errorStatus == ERROR_SUCCESS)
	{
		PipeOnRead();

		if (!mustUnload)
		{
			if (!::ReadFileEx(pipe, Buffer, sizeof(Buffer), overlapped, ReadFileCompleted))
			{
				errorStatus = ::GetLastError();				
				#if _DEBUG
					MessageBoxA(0, "Pipe read error!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
				#endif
				UnloadSelf();
			}
		}
		else
		{
			UnloadSelf();
		}
	}
	else
	{
		// pipe disconnected clean everything and remove the hook
		#if _DEBUG
			MessageBoxA(0, "Pipe disconnected, cleaning up.", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
		#endif

		UnloadSelf();
	}
}

BOOL CALLBACK EnumWindowsProc(HWND hwnd,LPARAM lParam)
{
	DWORD PID ;
	DWORD threadID;
	threadID=GetWindowThreadProcessId(hwnd,&PID);
	if(PID==CurrentPID)
	{
		hwndTibia=hwnd;
		//::MessageBoxA(0,"found","",0);
	}
	return hwndTibia ?0:1;
}

extern "C" bool APIENTRY DllMain (HMODULE hModule, DWORD reason, LPVOID reserved)
{
	switch (reason)
	{
		case DLL_PROCESS_ATTACH: //DLL was injected
		{
					
			hMod = hModule;
			/* Get Current Process ID and use it as Pipename (Pipe is named as TibiaAPI<processID> */
			CurrentPID = GetCurrentProcessId();

			EnumWindows(EnumWindowsProc,0);
			fUnicode=IsWindowUnicode(hwndTibia);

			std::stringstream sout;
			sout << "\\\\.\\pipe\\TibiaAPI" << CurrentPID;
			PipeName =  sout.str();
	
			InitializeCriticalSection(&PipeReadCriticalSection);
			InitializeCriticalSection(&PipeWriteCriticalSection);
			InitializeCriticalSection(&NormalTextCriticalSection);
			InitializeCriticalSection(&CreatureTextCriticalSection);
			InitializeCriticalSection(&ContextMenuCriticalSection);
			InitializeCriticalSection(&EventTriggerCriticalSection);
			InitializeCriticalSection(&DrawItemCriticalSection);
			InitializeCriticalSection(&DrawSkinCriticalSection);

			PipeConnected = false;
			//Start new thread for Pipe
			PipeThread = CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)PipeThreadProc, hMod, NULL, NULL);
			break;
		}
		case DLL_PROCESS_DETACH: //DLL was uninjected
		{
			TerminateThread(PipeThread, EXIT_SUCCESS);
			DeleteCriticalSection(&PipeReadCriticalSection);
			DeleteCriticalSection(&PipeWriteCriticalSection);
			DeleteCriticalSection(&NormalTextCriticalSection);
			DeleteCriticalSection(&CreatureTextCriticalSection);
			DeleteCriticalSection(&ContextMenuCriticalSection);
			DeleteCriticalSection(&EventTriggerCriticalSection);
			DeleteCriticalSection(&DrawItemCriticalSection);
			DeleteCriticalSection(&DrawSkinCriticalSection);

			break;
		}
	}

	return true;
}
