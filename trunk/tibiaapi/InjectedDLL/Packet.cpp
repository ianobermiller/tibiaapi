#include "stdafx.h"
#include <windows.h>
#include <string>
#include "Packet.h"

Packet::Packet(){
	m_Packet = new BYTE[MAX_PACKETSIZE];
	o_Packet = 0;
	m_PacketSize = 0;
	m_CurrentPos = 0;
}

Packet::Packet(int PacketSize){
	m_Packet = new BYTE[PacketSize];
	o_Packet = 0;
	m_PacketSize = 0;
	m_CurrentPos = 0;
}

Packet::Packet(BYTE* Packet, int PacketSize){
	m_Packet = Packet;
	m_PacketSize = PacketSize;
	m_CurrentPos = 0;
}

Packet::~Packet(){
	delete [] m_Packet;
	if (o_Packet != 0)
		delete [] o_Packet;
}

void Packet::AddByte(BYTE value){
	m_Packet[m_CurrentPos] = value;
	m_CurrentPos++;
	m_PacketSize++;
}

void Packet::AddShort(short value){
	*(short*)(m_Packet + m_CurrentPos) = value;
	m_CurrentPos += 2;
	m_PacketSize += 2;
}

void Packet::AddWord(WORD value){
	*(WORD*)(m_Packet + m_CurrentPos) = value;
	m_CurrentPos += 2;
	m_PacketSize += 2;
}

void Packet::AddDWord(DWORD value){
	*(DWORD*)(m_Packet + m_CurrentPos) = value;
	m_CurrentPos += 4;
	m_PacketSize += 4;
}

void Packet::AddString(std::string value){
	AddWord(value.length()); //Add string length to the packet
	strcpy((char*)(m_Packet + m_CurrentPos), value.c_str());
	m_CurrentPos += value.length();
	m_PacketSize += value.length();
}

BYTE* Packet::GetPacket(){
	o_Packet = new BYTE[m_PacketSize+2];
	memcpy(o_Packet + 2, m_Packet, m_PacketSize); //Copy packet to outgoing packet
	*(WORD*)o_Packet = m_PacketSize; //Copy packet size to the start
	return o_Packet;
}

int Packet::GetSize(){
	return m_PacketSize + 2;
}


BYTE Packet::ReadByte(BYTE *buffer, int *offset){
	return buffer[(*offset)++];
}

WORD Packet::ReadWord(BYTE *buffer, int *offset){
	WORD result;
	result = buffer[*offset]+(buffer[*offset+1]<<8);
	(*offset)+=2;
	return result;
}

short Packet::ReadShort(BYTE *buffer, int *offset){
	short result;
	result = buffer[*offset]+(buffer[*offset+1]<<8);
	(*offset)+=2;
	return result;
}

DWORD Packet::ReadDWord(BYTE *buffer, int *offset){
	DWORD result;
	result = buffer[*offset]+(buffer[*offset+1]<<8)+(buffer[*offset+2]<<0x10)+(buffer[*offset+3]<<0x18);
	(*offset)+=4;
	return result;
}

double Packet::ReadDouble(BYTE *buffer, int *offset){
	BYTE a[8];
	double *result;
	int i;
	for (i=0;i<sizeof(double);i++)
		a[i] = buffer[*offset+7-i];
	result = (double*)&a[0];
	(*offset)+=8;
	return *result;
}

std::string Packet::ReadString(BYTE *buffer, int *offset){
	WORD length = ReadWord(buffer, offset);
	std::string result = "";
	int i;
	for (i=0;i<length;i++)
		result += *(buffer+(*offset)++);
	return result;
}
