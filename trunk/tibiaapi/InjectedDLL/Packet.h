#pragma once

class Packet {

public:
	static BYTE ReadByte(BYTE *buffer, int *offset);
	static WORD ReadWord(BYTE *buffer, int *offset);
	static DWORD ReadDWord(BYTE *buffer, int *offset);
	static double ReadDouble(BYTE *buffer, int *offset);
	static std::string ReadString(BYTE *buffer, int *offset);
};
