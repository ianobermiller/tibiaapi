#pragma once
#define MAX_PACKETSIZE 1024

class Packet {

public:

	/* Constructors and Destructors */
	Packet();
	Packet(BYTE* Packet, int PacketSize);
	~Packet();

	/* Methods */
	void AddByte(BYTE value);
	void AddShort(short value);
	void AddWord(WORD value);
	void AddDWord(DWORD value);
	void AddString(std::string value);
	BYTE* GetPacket();
	int GetSize();


	/* Static Functions */
	static BYTE ReadByte(BYTE *buffer, int *offset);
	static WORD ReadWord(BYTE *buffer, int *offset);
	static short ReadShort(BYTE *buffer, int *offset);
	static DWORD ReadDWord(BYTE *buffer, int *offset);
	static double ReadDouble(BYTE *buffer, int *offset);
	static std::string ReadString(BYTE *buffer, int *offset);

private:
	BYTE* m_Packet; //Packet body
	BYTE* o_Packet; //Outgoing packet
	int m_PacketSize;
	int m_CurrentPos;
};
