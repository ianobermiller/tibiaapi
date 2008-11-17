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

using namespace std;

/* DisplayText. Credits for Displaying text goes to Stiju and Zionz. Thanks for the help!*/

list<NormalText> DisplayTexts;		//Used for normal text displyaing
list<PlayerText> CreatureTexts;		//Used for storing current text to display above creature
list<ContextMenu> ContextMenus;    //Used for storing the context menus that will be added on this call
DWORD OldPrintName = 0;				//Used for restoring PrintText when uninjecting DLL
DWORD OldPrintFPS = 0;				//Used for restroring PrintFPS when uninjecting DLL
BYTE* OldNopFPS = 0;				//Used for restoring conditional jump (FPS)

//Asynchronisation variables
CHandle pipe;						//Holds the Pipe handle (CHandle is from ATL library)
OVERLAPPED overlapped = { 0 };		
DWORD errorStatus = ERROR_SUCCESS;

/*Addresses are loaded from Constants.xml file */

void MyPrintName(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign)
{
	list<PlayerText>::iterator it;

	//Displaying Original Text
	PrintText(nSurface, nX, nY, nFont, nRed, nGreen, nBlue, lpText, nAlign);

	/* Write own text */

	DWORD *EntityID = (DWORD*)(lpText - 4);

	//Displaying texts
	EnterCriticalSection(&CreatureTextCriticalSection);
	for(it=CreatureTexts.begin(); it!=CreatureTexts.end(); ++it) {
		if (it->CreatureId == 0)
		{
			if(!strcmp(lpText, it->CreatureName)) {
				PrintText(0x01, nX + it->RelativeX, nY + it->RelativeY, it->TextFont, it->cR, it->cG, it->cB, it->DisplayText, 0x00);
			}
		}
		else if (*EntityID == it->CreatureId)
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
		nY += 12;
	}

	list<NormalText>::iterator ntIT;
	
	EnterCriticalSection(&NormalTextCriticalSection);
	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
	{
		PrintText(0x01, ntIT->x, ntIT->y, ntIT->font, ntIT->r, ntIT->g, ntIT->b, ntIT->text, 0x00); //0x01 Surface, 0x00 Align
	}
	LeaveCriticalSection(&NormalTextCriticalSection);
}

//TODO:Context menus functions hooking and onclick event

DWORD HookCall(DWORD dwAddress, DWORD dwFunction)
{   
	DWORD dwOldProtect, dwNewProtect, dwOldCall, dwNewCall;
	//CALL opcode = 0xE8 <4 byte for distance>
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	//Calculate the distance
	dwNewCall = dwFunction - dwAddress - 5;
	memcpy(&callByte[1], &dwNewCall, 4);
	
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect); //Gain access to read/write
	memcpy(&dwOldCall, (LPVOID)(dwAddress+1), 4); //Get the old function address for unhooking
	memcpy((LPVOID)(dwAddress), &callByte, 5); //Hook the function
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect); //Restore access
	return dwOldCall; //Return old funtion address for unhooking
}

void UnhookCall(DWORD dwAddress, DWORD dwOldCall)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	memcpy(&callByte[1], &dwOldCall, 4);
	
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), &callByte, 5);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect);
}

BYTE* Nop(DWORD dwAddress, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE* OldBytes;
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	OldBytes = new BYTE[size];
	memcpy(OldBytes, (LPVOID)(dwAddress), size);
	memset((LPVOID)(dwAddress), 0x90, size);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);
	return OldBytes;
}

void UnNop(DWORD dwAddress, BYTE* OldBytes, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), OldBytes, size);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);

	delete [] OldBytes;
	OldBytes = 0;
}

void __declspec(noreturn) UninjectSelf(HMODULE Module)
{
   __asm
   {
      push -2
      push 0
      push Module
      mov eax, TerminateThread
      push eax
      mov eax, FreeLibrary
      jmp eax
   }
}

inline void PipeOnRead(){
	int position=0;
	WORD len  = 0;
	len = Packet::ReadWord(Buffer, &position);
	BYTE PacketID = Packet::ReadByte(Buffer, &position);
	switch (PacketID){
		case 0x1: // Set Constant
			{	
				string ConstantName = Packet::ReadString(Buffer, &position);
				if (ConstantName == "ptrPrintName") {
					Consts::ptrPrintName = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrPrintFPS") {
					Consts::ptrPrintFPS = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrShowFPS") {
					Consts::ptrShowFPS = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrPrintTextFunc") {
					PrintText = (_PrintText*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrNopFPS") {
					Consts::ptrNopFPS = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrAddContextMenuFunc") {
					AddContextMenu = (_AddContextMenu*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrOnClickContextMenu") {
					Consts::ptrOnClickContextMenu = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrSetOutfitContextMenu") {
					Consts::ptrSetOutfitContextMenu = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrPartyActionContextMenu") {
					Consts::ptrPartyActionContextMenu = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrCopyNameContextMenu") {
					Consts::ptrCopyNameContextMenu = (DWORD)Packet::ReadDWord(Buffer, &position);
				}				
			}
			break;
		case 0x2: // DisplayText
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
			break;
		case 0x3: //RemoveText
			{
				string RemovalTextName = Packet::ReadString(Buffer, &position);
				list<NormalText>::iterator ntIT;
				EnterCriticalSection(&NormalTextCriticalSection);
				for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ) {
					if (ntIT->TextName == RemovalTextName)
					{
						delete [] ntIT->TextName;
						delete [] ntIT->text;
						ntIT = DisplayTexts.erase(ntIT);
					} else {
						++ntIT;
					}
				}
				LeaveCriticalSection(&NormalTextCriticalSection);
						
			}
			break;
		case 0x4: //Remove All
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
			break;
		case 0x5: //Inject Display
			{
				BYTE Inject = Packet::ReadByte(Buffer, &position);
				/* Testing that every constant contains a value */
				if(!Consts::ptrPrintFPS || !Consts::ptrPrintName || !Consts::ptrShowFPS || !Consts::ptrNopFPS ) {
					MessageBoxA(0, "Error. All the constant doesn't contain a value", "Error", MB_ICONERROR);
					break;
				}
				if(Inject) {
					if(HookInjected) {
						MessageBoxA(0, "The Hook is already injected", "Information", MB_ICONINFORMATION);
						break;
					}
					OldPrintName = HookCall(Consts::ptrPrintName, (DWORD)&MyPrintName);
					OldPrintFPS = HookCall(Consts::ptrPrintFPS, (DWORD)&MyPrintFps);
					//TODO: Add Bytes nop to Constants
					OldNopFPS = Nop(Consts::ptrNopFPS, 6); //Showing the FPS all the time..
					HookInjected = true;
				} else {
					if(!HookInjected) {
						MessageBoxA(0, "Please inject the hook before uninjecting", "Information", MB_ICONINFORMATION);
						break;
					}
					if (OldPrintName)
						UnhookCall(Consts::ptrPrintName, OldPrintName);
					if (OldPrintFPS)
						UnhookCall(Consts::ptrPrintFPS, OldPrintFPS);
					if (OldNopFPS)
						UnNop(Consts::ptrNopFPS, OldNopFPS, 6);
					HookInjected = false;
				}
			}
			break;
		case 0x6: //Set Text Above Creature
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
			break;
		case 0x7: //Remove Text Above Creature
			{
				int Id = Packet::ReadDWord(Buffer, &position);
				string Name = Packet::ReadString(Buffer, &position);
				
				list<PlayerText>::iterator ptIT;
				EnterCriticalSection(&CreatureTextCriticalSection);
				for(ptIT = CreatureTexts.begin(); ptIT != CreatureTexts.end(); ) {
					if (ptIT->CreatureId == 0) {
						if (ptIT->CreatureName == Name) {
							free(ptIT->DisplayText);
							free(ptIT->CreatureName);
							ptIT->DisplayText = 0; //Just to make sure I won't try to free this twice
							ptIT->CreatureName = 0;
							ptIT = CreatureTexts.erase(ptIT);
						} else {
							++ptIT;
						}
					} else if (ptIT->CreatureId == Id) {
						free(ptIT->DisplayText);
						free(ptIT->CreatureName);
						ptIT->DisplayText = 0; //Just to make sure I won't try to free this twice
						ptIT->CreatureName = 0;
						ptIT = CreatureTexts.erase(ptIT);
					} else {
						++ptIT;
					}
				}
				LeaveCriticalSection(&CreatureTextCriticalSection);
				
			}
			break;
		case 0x8: //Update Text Above Creature
			{
				int ID = Packet::ReadDWord(Buffer, &position);
				string CName = Packet::ReadString(Buffer, &position);
				int PosX = Packet::ReadShort(Buffer, &position);
				int PosY = Packet::ReadShort(Buffer, &position);
				string NewText = Packet::ReadString(Buffer, &position);
				char *lpNewText = (char*)calloc(NewText.size() + 1, sizeof(char));
				char *OldText;
				strcpy(lpNewText, NewText.c_str());
				list<PlayerText>::iterator newit;
				EnterCriticalSection(&CreatureTextCriticalSection);
				for(newit = CreatureTexts.begin(); newit != CreatureTexts.end(); ++newit) {
					if (newit->CreatureId == 0) {
						if (newit->CreatureName == CName && newit->RelativeX == PosX && newit->RelativeY == PosY) {
							OldText = newit->DisplayText;
							strcpy(OldText, "");
							newit->DisplayText = lpNewText;
							free(OldText);
							OldText = 0;
							break;
						}
					}
					else if (newit->CreatureId == ID && newit->RelativeX == PosX && newit->RelativeY == PosY) {
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
			break;
		case 0x9:
			//TODO:add item to ContextMenus
			break;
		case 0xA:
			//TODO:remove item from ContextMenus
			break;
		case 0xB:
			//TODO:clear items from ContextMenus
			break;
		case 0xC:
			//TODO:Nothing here?the injected dll should send this packet to tibiaapi containing the eventid
			//and the matching contextmenu eventid would raise its the event
			break;
		default:
			{
				MessageBoxA(0, "Unknown PacketType!", "Error!", MB_ICONERROR);
			}
			break;

	}
}

void PipeThreadProc(HMODULE Module){
	//Connect to Pipe
	if (WaitNamedPipeA(PipeName.c_str(), NMPWAIT_WAIT_FOREVER)) {
		pipe.Attach(::CreateFileA(PipeName.c_str(), GENERIC_READ | GENERIC_WRITE , 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL));
		if (pipe == INVALID_HANDLE_VALUE){
			errorStatus = ::GetLastError();
			MessageBoxA(0, "Pipe connection failed!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
			return;
		} else {
			//Pipe is ready. Let's start listening for incoming packets
			PipeConnected = true;
			if(!::ReadFileEx(pipe, Buffer, sizeof(Buffer), &overlapped, ReadFileCompleted))
			{
				errorStatus = ::GetLastError();
				MessageBoxA(0, "Pipe read error!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
				return;
			} else {
				while (errorStatus == ERROR_SUCCESS)
				{
					const DWORD sleepResult = ::SleepEx(INFINITE, TRUE);
					assert(WAIT_IO_COMPLETION == sleepResult);
				}
			}
		}
	} else {
		MessageBoxA(0, "Failed waiting for pipe, maybe pipe is not ready?.", "TibiaAPI Injected DLL - Fatal Error", 0);
	}
}

void CALLBACK ReadFileCompleted(DWORD errorCode, DWORD bytesCopied, OVERLAPPED* overlapped)
{
	errorStatus = errorCode;;

	if (errorStatus == ERROR_SUCCESS)
	{
		PipeOnRead();

		if (!::ReadFileEx(pipe, Buffer, sizeof(Buffer), overlapped, ReadFileCompleted))
		{
			errorStatus = ::GetLastError();
			MessageBoxA(0, "Pipe read error!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
		}
	}
}




extern "C" bool APIENTRY DllMain (HMODULE hModule, DWORD reason, LPVOID reserved){
	switch (reason){
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
			PipeConnected=false;
			//Start new thread for Pipe
			PipeThread = CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)PipeThreadProc, hMod, NULL, NULL);
		}
        break;
		case DLL_PROCESS_DETACH: //DLL was uninjected
		{
			TerminateThread(PipeThread, EXIT_SUCCESS);
			DeleteCriticalSection(&PipeReadCriticalSection);
			DeleteCriticalSection(&NormalTextCriticalSection);
			DeleteCriticalSection(&CreatureTextCriticalSection);
			DeleteCriticalSection(&ContextMenuCriticalSection);
		}
		break;
    }
    return true;
}
