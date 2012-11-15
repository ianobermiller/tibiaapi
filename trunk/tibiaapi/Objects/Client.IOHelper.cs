using System;
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
            private byte[] oldCallBytes;

            internal IOHelper(Client client) { this.client = client; }

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

            #region Socket.Send wrappers
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

            public bool WriteSocketSendCode()
            {
                byte[] OpCodes = new byte[]{
                //push	0						;_flag
        		0x6A, 0x00,
                //push	dword ptr [ebx]			;_length
                0xFF, 0x33,
                //add	ebx, 4
                0x83, 0xC3, 0x04,
                //push	ebx						;_buffer
                0x53,
                //mov	eax, ds:SocketStruct	;_socketstruct
                0xA1, 0xFF, 0xFF, 0xFF, 0xFF,
                //push	dword ptr [eax+4]		;_socket
                0xFF, 0x70, 0x04,
                //call	dword ptr ds:Send		;call send
                0xFF, 0x15, 0xFF, 0xFF, 0xFF, 0xFF,
                //retn
                0xC3
	        };

                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.SocketStruct), 0, OpCodes, 9, 4);
                Array.Copy(BitConverter.GetBytes(client.Addresses.Client.SendPointer), 0, OpCodes, 18, 4);

                if (pSendToServer == IntPtr.Zero)
                {
                    pSendToServer = Tibia.Util.WinApi.VirtualAllocEx(
                        client.ProcessHandle,
                        IntPtr.Zero,
                        (uint)OpCodes.Length,
                        WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                        WinApi.MemoryProtection.ExecuteReadWrite);
                }

                if (pSendToServer != IntPtr.Zero)
                {

                    if (client.Memory.WriteBytes(pSendToServer.ToInt64(), OpCodes, (uint)OpCodes.Length))
                    {
                        sendToServerCodeWritten = true;
                        return true;
                    }
                    WinApi.VirtualFreeEx(
                        client.ProcessHandle,
                        pSendToServer,
                        0,
                        WinApi.AllocationType.Release);
                    pSendToServer = IntPtr.Zero;
                }
                sendToServerCodeWritten = false;
                return false;

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

            public bool WriteOnGetNextPacketCode()
            {
                oldCallBytes = client.Memory.ReadBytes(
                            client.Addresses.Client.GetNextPacketCall,
                            5);
                #region opcodes
                byte[] opCodes = new byte[]{
                //fSendingToClient @ flagAddress
                0x00, 0x00, 0x00, 0x00, 
                                  
                //mov eax,dword ptr ds:[flagAddress]
                0xA1, 0x00, 0x00, 0x00, 0x00,

                //cmp eax,1
                0x83, 0xF8, 0x01,

                //JNZ SHORT~ -> mov eax,origAddress
                0x75, 0x26,

                //mov eax,dword ptr ds:[ADDR_RECV_STREAM+4]                         //dwSize
                0xA1, 0x00, 0x00, 0x00, 0x00, 

                //mov ebx,dword ptr ds:[ADDR_RECV_STREAM+8]                          //dwPos
                0x8B, 0x1D, 0x00, 0x00, 0x00, 0x00,

                //cmp ebx,eax
                0x39, 0xC3,

                //JGE SHORT~ -> //mov eax,-1
                0x7D, 0x11,

                //add ebx,dword ptr ds:[ADDR_RECV_STREAM]
                0x03, 0x1D, 0x00, 0x00, 0x00, 0x00,

                //mov al,byte ptr ds:[ebx]
                0x8A, 0x03,

                //mov ebx,ADDR_RECV_STREAM+8
                0xBB, 0x00, 0x00, 0x00, 0x00,

                //add dword ptr ds:[ebx],1
                0x83, 0x03, 0x01,

                //retn  
                0xC3,

                //mov eax,-1
                0xB8, 0xFF, 0xFF, 0xFF, 0xFF,

                //retn,
                0xC3,

                //mov eax,oldAddress
                0xB8, 0x00, 0x00, 0x00, 0x00,

                //call eax
                0xFF, 0xD0,

                //retn
                0xC3
                };
                #endregion
                #region fixing opcodes
                Array.Copy(
                    BitConverter.GetBytes(client.Addresses.Client.RecvStream + 4),
                    0,
                    opCodes,
                    15,
                    4);//mov eax,dword ptr ds:[ADDR_RECV_STREAM+4]  

                Array.Copy(
                    BitConverter.GetBytes(client.Addresses.Client.RecvStream + 8),
                    0,
                    opCodes,
                    21,
                    4); //mov ebx,dword ptr ds:[ADDR_RECV_STREAM+8]

                Array.Copy(
                    BitConverter.GetBytes(client.Addresses.Client.RecvStream),
                    0,
                    opCodes,
                    31,
                    4);//add ebx,dword ptr ds:[ADDR_RECV_STREAM]

                Array.Copy(
                    BitConverter.GetBytes(client.Addresses.Client.RecvStream + 8),
                    0,
                    opCodes,
                    38,
                    4);//mov ebx,ADDR_RECV_STREAM+8
                #endregion

                pSendToClient = WinApi.VirtualAllocEx(
                    client.ProcessHandle,
                    IntPtr.Zero,
                    (uint)opCodes.Length,
                    WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                    WinApi.MemoryProtection.ExecuteReadWrite);

                if (pSendToClient != IntPtr.Zero)
                {
                    Array.Copy(BitConverter.GetBytes(pSendToClient.ToInt32()), 0, opCodes, 5, 4);

                    //Begin HookCall
                    WinApi.MemoryProtection oldProtect = WinApi.MemoryProtection.NoAccess;
                    WinApi.MemoryProtection newProtect = WinApi.MemoryProtection.NoAccess;
                    uint newCall, oldCall;
                    byte[] call = new byte[] { 0xE8, 0x00, 0x00, 0x00, 0x00 };

                    newCall = (uint)(pSendToClient.ToInt32() + 4) - client.Addresses.Client.GetNextPacketCall - 5;
                    Array.Copy(BitConverter.GetBytes(newCall), 0, call, 1, 4);

                    if (WinApi.VirtualProtectEx(client.ProcessHandle,
                        new IntPtr(client.Addresses.Client.GetNextPacketCall),
                        new IntPtr(5),
                        WinApi.MemoryProtection.ReadWrite,
                        ref oldProtect))
                    {

                        oldCall = BitConverter.ToUInt32(
                            client.Memory.ReadBytes(client.Addresses.Client.GetNextPacketCall + 1, 4),
                            0);

                        int oldAddress = (int)(client.Addresses.Client.GetNextPacketCall + oldCall + 5);

                        Array.Copy(
                            BitConverter.GetBytes(oldAddress),
                            0,
                            opCodes,
                            53,
                            4);//mov eax,oldAddress

                        if (client.Memory.WriteBytes(pSendToClient.ToInt64(), opCodes, (uint)opCodes.Length))
                        {
                            if (client.Memory.WriteBytes(client.Addresses.Client.GetNextPacketCall, call, 5))
                            {
                                WinApi.VirtualProtectEx(client.ProcessHandle,
                                    new IntPtr(client.Addresses.Client.GetNextPacketCall),
                                    new IntPtr(5),
                                    oldProtect,
                                    ref newProtect);
                                sendToClientCodeWritten = true;
                                return true;
                            }
                        }
                        WinApi.VirtualProtectEx(client.ProcessHandle,
                            new IntPtr(client.Addresses.Client.GetNextPacketCall),
                            new IntPtr(5),
                            oldProtect,
                            ref newProtect);
                    }
                }
                if (pSendToClient == IntPtr.Zero)
                {
                    WinApi.VirtualFreeEx(
                        client.ProcessHandle,
                        pSendToClient,
                        (uint)opCodes.Length,
                        WinApi.AllocationType.Release);
                }
                sendToClientCodeWritten = false;
                return false;
            }

            public bool RestoreOldGetNextPacketCall()
            {
                if (oldCallBytes != null && sendToClientCodeWritten)
                {
                    if (client.Memory.WriteBytes(client.Addresses.Client.GetNextPacketCall, oldCallBytes, 5) &&
                        WinApi.VirtualFreeEx(
                            client.ProcessHandle,
                            pSendToClient,
                            60/*(uint)opCodes.Length*/,
                            WinApi.AllocationType.Release))
                    {
                        pSendToClient = IntPtr.Zero;
                        sendToClientCodeWritten = false;
                        return true;
                    }

                }
                return false;

            }
            #endregion
        }
    }
}

