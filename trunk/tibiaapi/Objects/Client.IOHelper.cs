using System;
using System.Collections.Generic;
using Tibia.Packets;
using Tibia.Util;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class IOHelper
        {
            private Client client;
            private Proxy proxy;
            private bool usingProxy = false;

            private bool sendToServerCodeWritten = false;
            private IntPtr pSendToServer;

            private bool sendToClientCodeWritten = false;
            private IntPtr pSendToClient;
            private IntPtr pMyGetNextPacket;
            private IntPtr pStreamHolderAddress;
            private IntPtr pMyStreamAddress;
            private IntPtr pFlagAddress;
            private byte[] oldCallBytes;

            internal IOHelper(Client client) { this.client = client; }

            ~IOHelper()
            {
                CleanUpToClient();
                CleanUpToServer();
            }

            #region Encryption
            public uint[] XteaKey
            {
                get
                {
                    //if we are using proxy the xteakey is parsed from the first login msg
                    //so we dont have to read it from the clients memory.
                    if (UsingProxy)
                        return Proxy.XteaKey;
                    else
                        return client.Memory.ReadBytes(client.Addresses.Client.XTeaKey, 16).ToUInt32Array();
                }
            }
            #endregion

            #region Proxy wrappers

            /// <summary>
            /// Whether or not the client is connected using a proxy.
            /// </summary>
            public bool UsingProxy
            {
                get { return usingProxy; }
                set { usingProxy = value; }
            }

            /// <summary>
            /// Start the proxy associated with this client.
            /// </summary>
            /// <returns>True if the proxy initialized correctly.</returns>
            public bool StartProxy()
            {
                proxy = new Proxy(client);
                return UsingProxy;
            }

            /// <summary>
            /// Start the proxy associated with this client specifying if it's for OT or official servers.
            /// </summary>
            /// <returns>True if the proxy initialized correctly.</returns>
            public bool StartProxy(bool isOtServer)
            {
                proxy = new Proxy(client, isOtServer);
                return UsingProxy;
            }

            /// <summary>
            /// Get the proxy object associated with this client. 
            /// Will ruturn null unless StartProxy() is called first
            /// </summary>
            public Proxy Proxy
            {
                get { return proxy; }
            }

            #endregion

            #region SendToServer stuff
            /// <summary>
            /// Get the base address of our send (to server) function
            /// </summary>
            public IntPtr SendToServerAddress
            {
                get { return pSendToServer; }
            }

            /// <summary>
            /// Checks if the code to call send functions has already been written to memory
            /// </summary>
            public bool IsSendToServerCodeWritten
            {
                get
                {
                    return sendToServerCodeWritten;
                }
            }


            byte[] sendToServerOpcodes = new byte[]{                
        		0x6A, 0x00,                             //PUSH 0                            ;_flag                
                0xFF, 0x33,                             //PUSH DWORD PTR [EBX]			    ;_length
                0x83, 0xC3, 0x04,                       //ADD EBX,4                
                0x53,                                   //PUSH EBX						    ;_buffer                
                0xA1, 0xFF, 0xFF, 0xFF, 0xFF,           //MOV EAX,DWORD PTR [SocketStruct]	;_socketstruct
                0xFF, 0x70, 0x04,                       //PUSH DWORD PTR [EAX]      		;_socket                
                0xFF, 0x15, 0xFF, 0xFF, 0xFF, 0xFF,     //CALL DWORD PTR [Send]		;call send
                0xC3                                    //RETN
	        };

            public bool WriteSendToServerCode()
            {
                CleanUpToServer();
                sendToServerCodeWritten = false;


                bool result = true;
                sendToServerOpcodes = this.sendToServerOpcodes;

                List<uint> indeces = new List<uint>();
                for (int ii = 0; ii < sendToServerOpcodes.Length - 4; ii++)
                {
                    if (BitConverter.ToUInt32(sendToServerOpcodes,ii) == 0xFFFFFFFF)
                        indeces.Add((uint)ii);
                }

                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.SocketStruct), 0, sendToServerOpcodes, indeces[0], 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.SendPointer), 0, sendToServerOpcodes, indeces[2], 4);

                pSendToServer = Tibia.Util.WinApi.VirtualAllocEx(
                        client.ProcessHandle,
                        IntPtr.Zero,
                        (uint)sendToServerOpcodes.Length,
                        WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                        WinApi.MemoryProtection.ExecuteReadWrite);

                if (pSendToServer == IntPtr.Zero)
                    return false;


                result &= client.Memory.WriteBytes((uint)pSendToServer, sendToServerOpcodes, (uint)sendToServerOpcodes.Length);

                sendToServerCodeWritten = result;
                return result;
            }

            void CleanUpToServer()
            {
                if (pSendToServer != IntPtr.Zero)
                {
                    WinApi.VirtualFreeEx(client.ProcessHandle, pSendToServer, (uint)this.sendToServerOpcodes.Length, WinApi.AllocationType.Release);
                    pSendToServer = IntPtr.Zero;
                } 
            }

            #endregion

            #region SendToClient stuff
            /// <summary>
            /// Get the base address of our send function
            /// </summary>
            public IntPtr SendToClientAddress
            {
                get { return pSendToClient; }
            }
            public IntPtr MyStreamAddress
            {
                get { return pMyStreamAddress; }
            }

            /// <summary>
            /// Checks if the code to use the parser functions has already been written to memory
            /// </summary>
            public bool IsSendToClientCodeWritten
            {
                get
                {
                    return sendToClientCodeWritten;
                }
            }


            #region SendToClient
            byte[] sendToClientOpCodes = new byte[]{
                                                        //SendToClient:
                0xC6,0x05,0xFF,0xFF,0xFF,0xFF,0x01,     //MOV BYTE PTR [ADDR_FLAG],1                    <--const 0
                
                0x60,                                   //PUSHAD
                0x9C,                                   //PUSHFD
                0xB8,0xFF,0xFF,0xFF,0xFF,    			//MOV EAX, ADDR_RECV_STREAM						<--const 1
                0xBB,0xFF,0xFF,0xFF,0xFF,    			//MOV EBX, ADDR_MY_STREAM						<--const 2
                0xBA,0xFF,0xFF,0xFF,0xFF,               //MOV EDX, ADDR_STREAM_HOLDER                   <--const 3
                                                        //save and replace ADDR_RECV_STREAM
                0x8B,0x08,                              //MOV ECX,DWORD PTR DS:[EAX]
                0x89,0x0A,                    			//MOV DWORD PTR DS:[EDX],ECX		
                0x8B,0x0B,                              //MOV ECX,DWORD PTR DS:[EBX]
                0x89,0x08,           					//MOV DWORD PTR DS:[EAX],ecx
                0x83,0xC0,0x04,        					//ADD EAX,4
                0x83,0xC3,0x04,        					//ADD EBX,4
                0x83,0xC2,0x04,        					//ADD EDX,4                                    
                                                        //save and replace ADDR_RECV_STREAM+4
                0x8B,0x08,                              //MOV ECX,DWORD PTR DS:[EAX]
                0x89,0x0A,                    			//MOV DWORD PTR DS:[EDX],ECX		
                0x8B,0x0B,                              //MOV ECX,DWORD PTR DS:[EBX]
                0x89,0x08,           					//MOV DWORD PTR DS:[EAX],ecx                
                0x83,0xC0,0x04,        					//ADD EAX,4
                0x83,0xC3,0x04,        					//ADD EBX,4
                0x83,0xC2,0x04,        					//ADD EDX,4
                                                        //save and replace ADDR_RECV_STREAM+8
                0x8B,0x08,                              //MOV ECX,DWORD PTR DS:[EAX]
                0x89,0x0A,                    			//MOV DWORD PTR DS:[EDX],ECX		
                0x8B,0x0B,                              //MOV ECX,DWORD PTR DS:[EBX]
                0x89,0x08,           					//MOV DWORD PTR DS:[EAX],ecx
                0x9D,                                   //POPFD
                0x61,                                   //POPAD
 
                0xB8,0xFF,0xFF,0xFF,0xFF,    			//MOV EAX,FUNC_PARSER							<--const 4
                0x90,                                   //NOP
                0xFF,0xD0,           					//CALL EAX
                
                0x60,                                   //PUSHAD
                0x9C,                                   //PUSHFD                    
                0xB8,0xFF,0xFF,0xFF,0xFF,    			//MOV EAX, ADDR_RECV_STREAM						<--const 5
                0xBA,0xFF,0xFF,0xFF,0xFF,               //MOV EDX, ADDR_STREAM_HOLDER                   <--const 6                
                                                        //restore ADDR_RECV_STREAM
                0x8B,0x0A,                              //MOV ECX, DWORD PTR DS:[EDX]
                0x89,0x08,           					//MOV DWORD PTR DS:[EAX],ecx	                
                0x83,0xC0,0x04,        					//ADD EAX,4
                0x83,0xC2,0x04,        					//ADD EDX,4                
                                                        //restore ADDR_RECV_STREAM+4
                0x8B,0x0A,                              //MOV ECX, DWORD PTR DS:[EDX]
                0x89,0x08,           					//MOV DWORD PTR DS:[EAX],ecx	                
                0x83,0xC0,0x04,        					//ADD EAX,4
                0x83,0xC2,0x04,        					//ADD EDX,4                
                                                        //restore ADDR_RECV_STREAM+8
                0x8B,0x0A,                              //MOV ECX, DWORD PTR DS:[EDX]
                0x89,0x08,           					//MOV DWORD PTR DS:[EAX],ecx	                

                0x9D,                                   //POPFD
                0x61,                                   //POPAD
                
                0xC6,0x05,0xFF,0xFF,0xFF,0xFF,0x00,     //MOV BYTE PTR [ADDR_FLAG],0                    <--const 7
                0xC3,                                   //RETN
            };
            #endregion

            #region MyGetNextPacket
            byte[] myGetNextPacketOpCodes = new byte[]{
                                                        //MyGetNextPacket:
                0X80,0x3D,0xFF,0xFF,0xFF,0xFF,0x00,		//CMP BYTE PTR [ADDR_FLAG],1                    <--const 0
                0x7D,43,              				    //JGE SHORT ...	<-----jump to line of const 6	
                                                        
                0x8B,0x15,0xFF,0xFF,0xFF,0xFF,          //MOV EDX, DWORD PTR [ ADDR_RECV_STREAM+8]      <--const 1 
                0x3B,0x15,0xFF,0xFF,0xFF,0xFF,          //CMP EDX, DWORD PTR [ ADDR_RECV_STREAM+4]      <--const 2
                0x7C,23,                                //JL SHORT ...	<-----jump to line of const 5

                0x8B,0x1D,0xFF,0xFF,0xFF,0xFF,          //MOV EBX, [ADDR_RECV_STREAM]                   <--const 3
                0x03,0xDA,                              //ADD EBX, EDX
                0xB8,0x00,0x00,0x00,0x00,               //MOV EAX, 0
                0x8A,0x03,                              //MOV AL, BYTE PTR [EBX]
                                                        
                0x42,                                   //INC EDX
                0x89,0x15,0xFF,0xFF,0xFF,0xFF,          //MOV DWORD PTR [ ADDR_RECV_STREAM+8], EDX      <--const 4
                0xC3,                                   //RETN

                0xB8,0xFF,0xFF,0xFF,0xFF,               //MOV EAX, -1                                   <--const 5
                0xC3,                                   //RETN
                                                        
                0xB8,0xFF,0xFF,0xFF,0xFF,    			//MOV EAX,oldAddress	<----------jump here	<--const 6
                0x90,                                   //NOP
                0xFF,0xD0,           					//CALL EAX
                0xC3             						//RETN
                };
            #endregion


            public bool WriteSendToClientCode()
            {
                CleanUpToClient();
                sendToClientCodeWritten = false;

                oldCallBytes = client.Memory.ReadBytes(
                            client.Addresses.Client.GetNextPacketCall,
                            5);

                var sendToClientOpCodes = this.sendToClientOpCodes;
                var myGetNextPacketOpCodes = this.myGetNextPacketOpCodes;
                List<uint> consts;

                #region pStreamHolderAddress
                pStreamHolderAddress =  WinApi.VirtualAllocEx(
                    client.ProcessHandle,
                    IntPtr.Zero,
                    (uint)12,
                    WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                    WinApi.MemoryProtection.ExecuteReadWrite);
                if (pStreamHolderAddress == IntPtr.Zero)
                {
                    CleanUpToClient();
                    return false;
                }
                #endregion

                #region pMyStreamAddress
                pMyStreamAddress = WinApi.VirtualAllocEx(
                    client.ProcessHandle,
                    IntPtr.Zero,
                    (uint)12,
                    WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                    WinApi.MemoryProtection.ExecuteReadWrite);
                if (pMyStreamAddress == IntPtr.Zero)
                {
                    CleanUpToClient();
                    return false;
                }
                #endregion

                #region pFlagAddress
                pFlagAddress = WinApi.VirtualAllocEx(
                            client.ProcessHandle,
                            IntPtr.Zero,
                            (uint)1,
                            WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                            WinApi.MemoryProtection.ExecuteReadWrite);
                if (pFlagAddress == IntPtr.Zero || !client.Memory.WriteByte((uint)pFlagAddress, 0))
                {
                    CleanUpToClient();
                    return false;
                }
                #endregion

                #region pSendToClient
                pSendToClient = WinApi.VirtualAllocEx(
                            client.ProcessHandle,
                            IntPtr.Zero,
                            (uint)sendToClientOpCodes.Length,
                            WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                            WinApi.MemoryProtection.ExecuteReadWrite);
                if (pSendToClient == IntPtr.Zero) return false;

                consts = new List<uint>();
                for (int ii = 0; ii < sendToClientOpCodes.Length - 4; ii++)
                {
                    if (BitConverter.ToUInt32(sendToClientOpCodes, ii) == 0xFFFFFFFF)
                        consts.Add((uint)ii);
                }


                Array.Copy(BitConverter.GetBytes((uint)pFlagAddress), 0, sendToClientOpCodes, consts[0], 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.RecvStream), 0, sendToClientOpCodes, consts[1], 4);
                Array.Copy(BitConverter.GetBytes((uint)pMyStreamAddress), 0, sendToClientOpCodes, consts[2], 4);
                Array.Copy(BitConverter.GetBytes((uint)pStreamHolderAddress), 0, sendToClientOpCodes, consts[3], 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.ParserFunc), 0, sendToClientOpCodes, consts[4], 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.RecvStream), 0, sendToClientOpCodes, consts[5], 4);
                Array.Copy(BitConverter.GetBytes((uint)pStreamHolderAddress), 0, sendToClientOpCodes, consts[6], 4);
                Array.Copy(BitConverter.GetBytes((uint)pFlagAddress), 0, sendToClientOpCodes, consts[7], 4);

                if (!client.Memory.WriteBytes((uint)pSendToClient, sendToClientOpCodes, (uint)sendToClientOpCodes.Length))
                {
                    CleanUpToClient();
                    return false;
                }
                #endregion
                
                
                #region pMyGetNextPacket
                pMyGetNextPacket = WinApi.VirtualAllocEx(
                            client.ProcessHandle,
                            IntPtr.Zero,
                            (uint)myGetNextPacketOpCodes.Length,
                            WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                            WinApi.MemoryProtection.ExecuteReadWrite);
                if (pMyGetNextPacket == IntPtr.Zero) return false;


                #region calls
                uint newCall, oldCall;

                byte[] call = new byte[] { 0xE8, 0x00, 0x00, 0x00, 0x00 };
                newCall = (uint)pMyGetNextPacket - client.Addresses.Client.GetNextPacketCall - 5;
                Array.Copy(BitConverter.GetBytes(newCall), 0, call, 1, 4);

                oldCall = BitConverter.ToUInt32(
                    client.Memory.ReadBytes(client.Addresses.Client.GetNextPacketCall + 1, 4),
                    0);
                uint oldAddress = (uint)(client.Addresses.Client.GetNextPacketCall + oldCall + 5);
                #endregion


                consts = new List<uint>();
                for (int ii = 0; ii < myGetNextPacketOpCodes.Length - 4; ii++)
                {
                    if (BitConverter.ToUInt32(myGetNextPacketOpCodes,ii)==0xFFFFFFFF)
                        consts.Add((uint)ii);
                }


                Array.Copy(BitConverter.GetBytes((uint)pFlagAddress), 0, myGetNextPacketOpCodes, consts[0], 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.RecvStream + 8), 0, myGetNextPacketOpCodes, consts[1], 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.RecvStream + 4), 0, myGetNextPacketOpCodes, consts[2], 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.RecvStream), 0, myGetNextPacketOpCodes, consts[3], 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.RecvStream + 8), 0, myGetNextPacketOpCodes, consts[4], 4);
                Array.Copy(BitConverter.GetBytes(oldAddress ), 0, myGetNextPacketOpCodes, consts[6], 4);

                if (!client.Memory.WriteBytes((uint)pMyGetNextPacket, myGetNextPacketOpCodes, (uint)myGetNextPacketOpCodes.Length))
                {
                    CleanUpToClient();
                    return false;
                }
                #endregion

                #region hook GetNextPacket
                //Begin HookCall
                WinApi.MemoryProtection oldProtect = WinApi.MemoryProtection.NoAccess;
                WinApi.MemoryProtection newProtect = WinApi.MemoryProtection.NoAccess;

                if (!WinApi.VirtualProtectEx(client.ProcessHandle,
                    new IntPtr(client.Addresses.Client.GetNextPacketCall),
                    new IntPtr(5),
                    WinApi.MemoryProtection.ExecuteReadWrite,
                    ref oldProtect)
                    ||
                    !client.Memory.WriteBytes(client.Addresses.Client.GetNextPacketCall, call, 5)
                    )
                {
                    CleanUpToClient();
                    return false;
                }

                WinApi.VirtualProtectEx(client.ProcessHandle,
                    new IntPtr(client.Addresses.Client.GetNextPacketCall),
                    new IntPtr(5),
                    oldProtect,
                    ref newProtect);
                #endregion
                
                sendToClientCodeWritten = true;
                return true;
            }

            private void CleanUpToClient()
            {
                if (pStreamHolderAddress != IntPtr.Zero)
                {
                    WinApi.VirtualFreeEx(client.ProcessHandle, pStreamHolderAddress, 12, WinApi.AllocationType.Release);
                    pStreamHolderAddress = IntPtr.Zero;
                }
                if (pMyStreamAddress != IntPtr.Zero)
                {
                    WinApi.VirtualFreeEx(client.ProcessHandle, pMyStreamAddress, 12, WinApi.AllocationType.Release);
                    pMyStreamAddress = IntPtr.Zero;
                }
                if (pFlagAddress != IntPtr.Zero)
                {
                    WinApi.VirtualFreeEx(client.ProcessHandle, pFlagAddress, 1, WinApi.AllocationType.Release);
                    pFlagAddress = IntPtr.Zero;
                }
                if (pSendToClient != IntPtr.Zero)
                {
                    WinApi.VirtualFreeEx(client.ProcessHandle, pSendToClient, (uint)sendToClientOpCodes.Length, WinApi.AllocationType.Release);
                    pSendToClient = IntPtr.Zero;
                }
                if (pMyGetNextPacket != IntPtr.Zero)
                {
                    WinApi.VirtualFreeEx(client.ProcessHandle, pSendToClient, (uint)myGetNextPacketOpCodes.Length, WinApi.AllocationType.Release);
                    pMyGetNextPacket = IntPtr.Zero;
                }
            }

            public bool RestoreOldGetNextPacketCall()
            {
                bool result = false;
                if (oldCallBytes != null && sendToClientCodeWritten)
                {
                    WinApi.MemoryProtection oldProtect = WinApi.MemoryProtection.NoAccess;
                    WinApi.MemoryProtection newProtect = WinApi.MemoryProtection.NoAccess;

                    if (WinApi.VirtualProtectEx(client.ProcessHandle,
                                        new IntPtr(client.Addresses.Client.GetNextPacketCall),
                                        new IntPtr(5),
                                        WinApi.MemoryProtection.ExecuteReadWrite,
                                        ref oldProtect))
                    {

                        if (client.Memory.WriteBytes(client.Addresses.Client.GetNextPacketCall, oldCallBytes, 5))
                        {
                            CleanUpToClient();
                            sendToClientCodeWritten = false;
                            result = true;
                        }

                        WinApi.VirtualProtectEx(client.ProcessHandle,
                                        new IntPtr(client.Addresses.Client.GetNextPacketCall),
                                        new IntPtr(5),
                                        oldProtect,
                                        ref newProtect);
                    }
                }
                return result;

            }
            #endregion
        }
    }
}

