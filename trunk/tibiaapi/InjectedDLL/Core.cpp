#include "stdafx.h"
#include <windows.h>
#include <string>
#include <sstream>
#include <list>
//#include <atlbase.h>
#include <assert.h>
#include "Constants.h"
#include "Core.h"
#include "Packet.h"



#ifdef _MANAGED
#pragma managed(push, off)
#endif
/*
#ifdef _DEBUG
#undef _DEBUG
#endif*/

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

#define ADDR_RECV_STREAM		0x78CEE4 //8.52
typedef int TF_GETNEXTPACKET();
TF_GETNEXTPACKET *TfGetNextPacket = NULL;
TPacketStream * pRecvStream = (TPacketStream*)ADDR_RECV_STREAM;

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
					WriteFileEx(pipe, packet->GetPacket(), packet->GetSize(), &overlapped, NULL); 	
				}
			}
		}
		LeaveCriticalSection(&DrawItemCriticalSection);
	}
	return fUnicode ? CallWindowProcW(oldWndProc, hwnd, msg, wParam, lParam) : 
					CallWindowProcA(oldWndProc, hwnd, msg, wParam, lParam);
}

int OnGetNextPacket()
{
    int iCmd = TfGetNextPacket();
    if(iCmd != -1)
    {
        LPBYTE pBuffer = (LPBYTE)pRecvStream->pBuffer + pRecvStream->dwPos - 1;
		int pPos = 0;
		BYTE count = 0;
		BYTE itemCount = 0;
		BYTE cap = 0;
		BYTE speechType = 0;
		WORD strlen = 0;
		WORD lookType = 0;
		WORD thingId = 0;
		PacketCommandType type = (PacketCommandType)(pBuffer[pPos]);
		Packet* packet = new Packet();
		packet->AddByte(0x21);

		switch(type)
		{
		case SelfAppear:
			packet->AddByte(pBuffer[pPos]);
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			break;
		//case GMAction:
		//case ErrorMessage:
		};
		switch(type)
		{
		case FyiMessage:
			packet->AddByte(pBuffer[pPos]);
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		case WaitingList:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			packet->AddByte((pBuffer[pPos]));
			break;
		case Ping:
			packet->AddByte((pBuffer[pPos]));
			break;
		};
		switch(type)
		{
		case Death:
			packet->AddByte((pBuffer[pPos]));
			break;
		case CanReportBugs:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		//case MapDescription:
		};
		//switch(type)
		//{
		//case MoveNorth:
		//case MoveEast:
		//case MoveSouth:
		//};
		switch(type)
		{
		//case MoveWest:
		//case TileUpdate:
		case TileAddThing:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			thingId = ((WORD)pBuffer[pPos]);
			packet->AddWord(thingId);
			pPos += 2;
			if (thingId == 0x0061 || thingId == 0x0062)
			{
				if (thingId == 0x0062)
				{
					packet->AddDWord(((DWORD)pBuffer[pPos]));
					pPos += 4;
				}
				else if (thingId == 0x0061)
				{
					packet->AddDWord(((DWORD)pBuffer[pPos]));
					pPos += 4;
					packet->AddDWord(((DWORD)pBuffer[pPos]));
					pPos += 4;
					strlen = ((WORD)pBuffer[pPos]);
					packet->AddWord(strlen);
					pPos += 2;
					for (WORD i = 0; i < strlen; i++)
					{
						packet->AddByte((pBuffer[pPos]));
						pPos += 1;
					}
				}
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				lookType = ((WORD)pBuffer[pPos]);
				packet->AddWord(lookType);
				pPos += 2;
				if (lookType != 0)
				{
					packet->AddByte((pBuffer[pPos]));
					pPos += 1;
					packet->AddByte((pBuffer[pPos]));
					pPos += 1;
					packet->AddByte((pBuffer[pPos]));
					pPos += 1;
					packet->AddByte((pBuffer[pPos]));
					pPos += 1;
					packet->AddByte((pBuffer[pPos]));
					pPos += 1;
				}
				else
				{
					packet->AddWord(((WORD)pBuffer[pPos]));
					pPos += 2;
				}
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			else if (thingId == 0x0063)
			{
				packet->AddDWord(((DWORD)pBuffer[pPos]));
				pPos += 4;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			else
				packet->AddByte((pBuffer[pPos]));
		break;
		};
		switch(type)
		{
		case TileTransformThing:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			thingId = ((WORD)pBuffer[pPos]);
			packet->AddWord(thingId);
			pPos += 2;
			if (thingId == 0x0061 || thingId == 0x0062 || thingId == 0x0063)
			{
				packet->AddDWord(((DWORD)pBuffer[pPos]));
				pPos += 4;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			else
				packet->AddByte((pBuffer[pPos]));
			break;
		case TileRemoveThing:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		case CreatureMove:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			break;
		};
		switch(type)
		{
		case ContainerOpen:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			packet->AddByte(((pBuffer[pPos]) + strlen));
			pPos += 1;
			packet->AddByte(((pBuffer[pPos]) + strlen));
			pPos += 1;
			itemCount = ((pBuffer[pPos]) + strlen);
			packet->AddByte(itemCount);
			pPos += 1;
			for (int j = 0; j < itemCount; j++)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
			}
			packet->AddByte((pBuffer[pPos]));
			break;
		case ContainerClose:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		case ContainerAddItem:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			break;
		};
		switch(type)
		{
		case ContainerUpdateItem:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			break;
		case ContainerRemoveItem:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		case InventorySetSlot:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			break;
		};
		switch(type)
		{
		case InventoryResetSlot:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		case ShopWindowOpen:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			cap = (pBuffer[pPos]);
			packet->AddByte(cap);
			pPos += 1;
			for (int i = 0; i < cap; i++)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				strlen = ((WORD)pBuffer[pPos]);
				packet->AddWord(strlen);
				pPos += 2;
				for (WORD j = 0; j < strlen; j++)
				{
					packet->AddByte((pBuffer[pPos]));
					pPos += 1;
				}
				packet->AddDWord(((DWORD)pBuffer[pPos]));
				pPos += 4;
				packet->AddDWord(((DWORD)pBuffer[pPos]));
				pPos += 4;
				packet->AddDWord(((DWORD)pBuffer[pPos]));
			}
			break;
		case ShopSaleGoldCount:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			count = (pBuffer[pPos]);
			packet->AddByte(count);
			pPos += 1;
			for (int i = 0; i < count; i ++)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		};
		switch(type)
		{
		case ShopWindowClose:
			packet->AddByte((pBuffer[pPos]));
			break;
		case SafeTradeRequestAck:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			count = (pBuffer[pPos]);
			packet->AddByte(count);
			pPos += 1;
			for (int i = 0; i < count; i ++)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		case SafeTradeRequestNoAck:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			count = (pBuffer[pPos]);
			packet->AddByte(count);
			pPos += 1;
			for (int i = 0; i < count; i ++)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		};
		switch(type)
		{
		case SafeTradeClose:
			packet->AddByte((pBuffer[pPos]));
			break;
		case WorldLight:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		case MagicEffect:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			break;
		};
		switch(type)
		{
		case AnimatedText:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		case Projectile:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		case CreatureSquare:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddByte((pBuffer[pPos]));
			break;
		};
		switch(type)
		{
		case CreatureHealth:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddByte((pBuffer[pPos]));
			break;
		case CreatureLight:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		case CreatureOutfit:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			lookType = ((WORD)pBuffer[pPos]);
			packet->AddWord(lookType);
			pPos += 2;
			if (lookType != 0)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
			}
			break;
		};
		switch(type)
		{
		case CreatureSpeed:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddWord(((WORD)pBuffer[pPos]));
			break;
		case CreatureSkull:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddByte((pBuffer[pPos]));
			break;
		//case CreatureShield:
		};
		switch(type)
		{
		case ItemTextWindow:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD j = 0; j < strlen; j++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD x = 0; x < strlen; x++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		//case HouseTextWindow:
		case PlayerStatus:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			break;
		};
		switch(type)
		{
		case PlayerSkillsUpdate:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		case PlayerFlags:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			break;
		case CancelTarget:
			packet->AddByte((pBuffer[pPos]));
			break;
		};
		switch(type)
		{
		case CreatureSpeech:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			speechType = (pBuffer[pPos]);
			packet->AddByte(speechType);
			pPos += 1;
			if (speechType == 0x05)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			else if (speechType == 0x08)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
			}
			else if (speechType == 0x09)
			{
				packet->AddDWord(((DWORD)pBuffer[pPos]));
				pPos += 4;
			}
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD j = 0; j < strlen; j++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		case ChannelList:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			count = (pBuffer[pPos]);
			packet->AddByte(count);
			pPos += 1;
			for (int i = 0; i < count; i++)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				strlen = ((WORD)pBuffer[pPos]);
				packet->AddWord(strlen);
				pPos += 2;
				for (WORD i = 0; i < strlen; i++)
				{
					packet->AddByte((pBuffer[pPos]));
					pPos += 1;
				}
			}
			break;
		case ChannelOpen:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		};
		switch(type)
		{
		case ChannelOpenPrivate:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		case RuleViolationOpen:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer));
			break;
		case RuleViolationRemove:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		};
		switch(type)
		{
		case RuleViolationCancel:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		case RuleViolationLock:
			packet->AddByte((pBuffer[pPos]));
			break;
		case PrivateChannelCreate:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			pPos += 2;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		};
		switch(type)
		{
		case ChannelClosePrivate:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddWord(((WORD)pBuffer[pPos]));
			break;
		case TextMessage:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		case PlayerWalkCancel:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddByte((pBuffer[pPos]));
			break;
		};
		switch(type)
		{
		//case FloorChangeUp:
		//case FloorChangeDown:
		case OutfitWindow:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			lookType = ((WORD)pBuffer[pPos]);
			packet->AddWord(lookType);
			pPos += 2;
			if (lookType != 0)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
				packet->AddByte((pBuffer[pPos]));
			}
			count = (pBuffer[pPos]);
			packet->AddByte(count);
			pPos += 1;
			for (int i = 0; i < count; i++)
			{
				packet->AddWord(((WORD)pBuffer[pPos]));
				pPos += 2;
				strlen = ((WORD)pBuffer[pPos]);
				packet->AddWord(strlen);
				pPos += 2;
				for (WORD i = 0; i < strlen; i++)
				{
					packet->AddByte((pBuffer[pPos]));
					pPos += 1;
				}
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			break;
		};
		switch(type)
		{
		case VipState:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			pPos += 4;
			strlen = ((WORD)pBuffer[pPos]);
			packet->AddWord(strlen);
			pPos += 2;
			for (WORD i = 0; i < strlen; i++)
			{
				packet->AddByte((pBuffer[pPos]));
				pPos += 1;
			}
			packet->AddByte((pBuffer[pPos]));
			break;
		case VipLogin:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			break;
		case VipLogout:
			packet->AddByte((pBuffer[pPos]));
			pPos += 1;
			packet->AddDWord(((DWORD)pBuffer[pPos]));
			break;
		};
		//switch(type)
		//{
		//case QuestList:
		//case QuestPartList:
		//case ShowTutorial:
		//};
		//switch(type)
		//{
		//case AddMapMarker:
		//};

		if (packet->GetSize() > 1)
			WriteFileEx(pipe, packet->GetPacket(), packet->GetSize(), &overlapped, NULL); 
    }
    return iCmd;
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

	return OrigSend(s,buf,len,flags);
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
	
	
	EnterCriticalSection(&DrawItemCriticalSection);

	list<Icon>::iterator iIT;
	for(iIT = Icons.begin(); iIT != Icons.end(); ++iIT)		
		DrawItem(0x1,
		iIT->X,iIT->Y,
		iIT->BitmapSize,
		iIT->ItemId, iIT->ItemCount,
	    0, 0, 0, 0,
	    iIT->X,iIT->Y,
	    iIT->BitmapSize,iIT->BitmapSize,
	    iIT->TextFont, iIT->cR, iIT->cG, iIT->cB, 0x2,
	    0);
	LeaveCriticalSection(&DrawItemCriticalSection);



	EnterCriticalSection(&DrawSkinCriticalSection);

	list<Skin>::iterator sIT;
	for(sIT = Skins.begin(); sIT != Skins.end(); ++sIT)
		DrawSkin(0x1,
		sIT->X, sIT->Y,
		sIT->Width, sIT->Height,
		sIT->GUIId,
		0, 0);
	LeaveCriticalSection(&DrawSkinCriticalSection);
	

	
	EnterCriticalSection(&NormalTextCriticalSection);
	
	list<NormalText>::iterator ntIT;
	for(ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
		PrintText(0x01, ntIT->x, ntIT->y, ntIT->font, ntIT->r, ntIT->g, ntIT->b, ntIT->text, 0x00); //0x01 Surface, 0x00 Align

	LeaveCriticalSection(&NormalTextCriticalSection);
	
	
}


void __stdcall MySetOutfitContextMenu (int eventId, const char* text, const char* shortcut)
{
	/*#if _DEBUG	
		MessageBoxA(0, "MySetOutfitContextMenu", "", 0);
	#endif*/
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
	/*#if _DEBUG
		MessageBoxA(0, "MyPartyActionContextMenu", "", 0);
	#endif*/
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
	/*#if _DEBUG
		MessageBoxA(0, "MyCopyNameContextMenu", "",MB_ICONERROR);
	#endif*/
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
	/*#if _DEBUG
		MessageBoxA(0, "MyCopyNameContextMenu", "", 0);
	#endif*/
	AddContextMenu(eventId, text, shortcut);

	list<ContextMenu>::iterator it;
	for(it = ContextMenus.begin(); it != ContextMenus.end(); ++it)
	{
		//TradeWithContextMenu or AllMenus
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

void __stdcall MyLookContextMenu (int eventId, const char* text, const char* shortcut)
{	
	/*#if _DEBUG
		MessageBoxA(0, "MyLookContextMenu", "", 0);
	#endif*/
	AddContextMenu(eventId, text, shortcut);

	list<ContextMenu>::iterator it;
	for(it = ContextMenus.begin(); it != ContextMenus.end(); ++it)
	{
		//LookContextMenu or AllMenus
		if(it->Type == 0x05 || it->Type == 0x00)
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
		#if _DEBUG
			MessageBoxA(0, "The hook is already injected", "", MB_ICONINFORMATION);
		#endif
		return;
	}

	OldPrintName = HookCall(Consts::ptrPrintName, (DWORD)&MyPrintName);
	OldPrintFPS = HookCall(Consts::ptrPrintFPS, (DWORD)&MyPrintFps);

	OldSetOutfitContextMenu = HookCall(Consts::ptrSetOutfitContextMenu, (DWORD)&MySetOutfitContextMenu);
	OldPartyActionContextMenu = HookCall(Consts::ptrPartyActionContextMenu, (DWORD)&MyPartyActionContextMenu);
	OldCopyNameContextMenu = HookCall(Consts::ptrCopyNameContextMenu, (DWORD)&MyCopyNameContextMenu);
	OldTradeWithContextMenu = HookCall(Consts::ptrTradeWithContextMenu, (DWORD)&MyTradeWithContextMenu);
	OldLookContextMenu = HookCall(Consts::ptrLookContextMenu,(DWORD) &MyLookContextMenu);

	DWORD dwOldProtect, dwNewProtect, funcAddress;	

	//OnClickContextMenuEvent..
	funcAddress = (DWORD)&MyOnClickContextMenu;
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::prtOnClickContextMenuVf, &funcAddress, 4);
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, dwOldProtect, &dwNewProtect); //Restore access

	/*
	recv/send
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
	*/

	OldNopFPS = Nop(Consts::ptrNopFPS, 6); //Showing the FPS all the time..

	HookOnGetNextPacket(Consts::ptrOnGetNextPacket, (DWORD)&OnGetNextPacket, (LPDWORD)&TfGetNextPacket);

	//subclassing tibia's main window procedure
	oldWndProc = (WNDPROC) ((fUnicode) ? SetWindowLongPtrW(hwndTibia, GWLP_WNDPROC, (LONG_PTR)SubClassProc) : 
											SetWindowLongPtrA(hwndTibia, GWLP_WNDPROC, (LONG_PTR)SubClassProc));

	HooksEnabled = true;
}

void DisableHooks()
{	
	#if _DEBUG
		MessageBoxA(0, "Removing all hooks...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif
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
	if(OldLookContextMenu)
		UnhookCall(Consts::ptrLookContextMenu, OldLookContextMenu);
	if (OldNopFPS)
		UnNop(Consts::ptrNopFPS, OldNopFPS, 6);

	//OnClickContextMenuEvent..
	#if _DEBUG
		MessageBoxA(0, "Removing context menu click event...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif
	DWORD dwOldProtect, dwNewProtect, funcAddress;
	funcAddress = (DWORD)&MyOnClickContextMenu;
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::prtOnClickContextMenuVf, &Consts::ptrOnClickContextMenu, 4);
	VirtualProtect((LPVOID)Consts::prtOnClickContextMenuVf, 4, dwOldProtect, &dwNewProtect); //Restore access

	/*
	recv/send
	#if _DEBUG
		MessageBoxA(0, "Removing send/recv hooks...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif
	VirtualProtect((LPVOID)Consts::ptrSend, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::ptrSend, &OrigSendAddress, 4);
	VirtualProtect((LPVOID)Consts::ptrSend, 4, dwOldProtect, &dwNewProtect);

	VirtualProtect((LPVOID)Consts::ptrRecv, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::ptrRecv, &OrigRecvAddress, 4);
	VirtualProtect((LPVOID)Consts::ptrRecv, 4, dwOldProtect, &dwNewProtect);
	*/

	UnhookOnGetNextPacket(Consts::ptrOnGetNextPacket, (LPDWORD)TfGetNextPacket);

	fUnicode ? SetWindowLongPtrW(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc) : 
			SetWindowLongPtrA(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc);

	HooksEnabled = false;
}

void HookOnGetNextPacket(DWORD dwCallAddress, DWORD dwNewAddress, LPDWORD pOldAddress)
{
	DWORD dwOldProtect, dwNewProtect, dwOldCall, dwNewCall;
	BYTE call[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	dwNewCall = dwNewAddress - dwCallAddress - 5;
	memcpy(&call[1], &dwNewCall, 4);

	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwCallAddress), 5, PAGE_READWRITE, &dwOldProtect);
	if(pOldAddress)
	{
		memcpy(&dwOldCall, (LPVOID)(dwCallAddress+1), 4);
		*pOldAddress = dwCallAddress + dwOldCall + 5;
	}
	memcpy((LPVOID)(dwCallAddress), &call, 5);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwCallAddress), 5, dwOldProtect, &dwNewProtect);
}

void UnhookOnGetNextPacket(DWORD dwCallAddress, LPDWORD dwOldCall)
{
    DWORD dwOldProtect, dwNewProtect;
    BYTE call[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

    memcpy(&call[1], &dwOldCall, 4);

    VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwCallAddress), 5, PAGE_READWRITE, &dwOldProtect);
    memcpy((LPVOID)(dwCallAddress), &call, 5);
    VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwCallAddress), 5, dwOldProtect, &dwNewProtect);
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
		#if _DEBUG
			MessageBoxA(0, "Removing all text...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
		#endif
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
		#if _DEBUG
			MessageBoxA(0, "Removing all context menus...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
		#endif
		list<ContextMenu>::iterator cmIT;
		EnterCriticalSection(&ContextMenuCriticalSection);
		for(cmIT = ContextMenus.begin(); cmIT != ContextMenus.end(); ++cmIT)
			delete [] cmIT->MenuText;
		ContextMenus.clear();
		LeaveCriticalSection(&ContextMenuCriticalSection);

		#if _DEBUG
			MessageBoxA(0, "Removing all icons...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
		#endif
		EnterCriticalSection(&DrawItemCriticalSection);
		Icons.clear();
		LeaveCriticalSection(&DrawItemCriticalSection);

		DisableHooks();
	}

	#if _DEBUG
		MessageBoxA(0, "Detaching pipe...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif

	//pipe.Detach();
	CloseHandle(pipe);
	//::DeleteFileA(PipeName.c_str());
	//TerminateThread(PipeThread, EXIT_SUCCESS);
	#if _DEBUG
		MessageBoxA(0, "Unhooking winproc...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif



	#if _DEBUG
		MessageBoxA(0, "Deleting critical sections...", "TibiaAPI Injected DLL - Cleaning up", MB_ICONINFORMATION);
	#endif

	DeleteCriticalSection(&PipeReadCriticalSection);
	DeleteCriticalSection(&NormalTextCriticalSection);
	DeleteCriticalSection(&CreatureTextCriticalSection);
	DeleteCriticalSection(&ContextMenuCriticalSection);
	DeleteCriticalSection(&OnClickCriticalSection);
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
		!Consts::ptrCopyNameContextMenu || !Consts::ptrPartyActionContextMenu || !Consts::ptrSetOutfitContextMenu
		|| !Consts::prtOnClickContextMenuVf || !Consts::ptrTradeWithContextMenu ||
		/*!Consts::ptrRecv || !Consts::ptrSend || */!Consts::ptrEventTrigger || !Consts::ptrLookContextMenu || 
		!Consts::ptrOnGetNextPacket/* || !Consts::ptrRecvStream ||
		!PrintText || !DrawItem || !DrawSkin || !EventTrigger || !OrigSend || !OrigRecv*/) 
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
	case LookContextMenu:
		Consts::ptrLookContextMenu = value;
		break;
	case DrawItemFunc:		
		DrawItem = (_DrawItem*)value;
		break;
	case DrawSkinFunc:
		DrawSkin = (_DrawSkin*)value;
		break;
	case OnGetNextPacketFunc:
		Consts::ptrOnGetNextPacket = value;
		break;
	case RecvStream:
		//pRecvStream = (TPacketStream*)value;
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
		if(miIT->IconId == icon.ItemId)
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
			InitializeCriticalSection(&NormalTextCriticalSection);
			InitializeCriticalSection(&CreatureTextCriticalSection);
			InitializeCriticalSection(&ContextMenuCriticalSection);
			InitializeCriticalSection(&OnClickCriticalSection);
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
			DeleteCriticalSection(&NormalTextCriticalSection);
			DeleteCriticalSection(&CreatureTextCriticalSection);
			DeleteCriticalSection(&ContextMenuCriticalSection);
			DeleteCriticalSection(&OnClickCriticalSection);
			DeleteCriticalSection(&EventTriggerCriticalSection);
			DeleteCriticalSection(&DrawItemCriticalSection);
			DeleteCriticalSection(&DrawSkinCriticalSection);
			
			fUnicode ? SetWindowLongPtrW(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc) : 
						SetWindowLongPtrA(hwndTibia, GWLP_WNDPROC, (LONG_PTR)oldWndProc);

			break;
		}
	}

	return true;
}
