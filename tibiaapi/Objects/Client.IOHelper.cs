using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Packets;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class IOHelper
        {
            private Client client;
            private Proxy proxy;
            private bool usingProxy = false;
            private bool sendCodeWritten = false;
            private IntPtr pSender;

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
                        return client.Memory.ReadBytes(Tibia.Addresses.Client.XTeaKey, 16).ToUInt32Array();
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
            /// Start the proxy associated with this client for official servers.
            /// </summary>
            /// <returns>True if the proxy initialized correctly.</returns>
            public bool StartProxy()
            {
                return StartProxy(false);
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
            /// Get the base address of our send function
            /// </summary>
            public IntPtr SenderAddress
            {
                get { return pSender; }
            }

            /// <summary>
            /// Checks if the code to call send functions has already been written to memory
            /// </summary>
            public bool IsSendCodeWritten
            {
                get
                {
                    return sendCodeWritten;
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

                Array.Copy(BitConverter.GetBytes(Tibia.Addresses.Client.SocketStruct), 0, OpCodes, 9, 4);
                Array.Copy(BitConverter.GetBytes(Tibia.Addresses.Client.SendPointer), 0, OpCodes, 18, 4);

                if (pSender == IntPtr.Zero)
                {
                    pSender = Tibia.Util.WinApi.VirtualAllocEx(
                        client.ProcessHandle, IntPtr.Zero, (uint)OpCodes.Length,
                        Tibia.Util.WinApi.MEM_COMMIT | Tibia.Util.WinApi.MEM_RESERVE,
                        Tibia.Util.WinApi.PAGE_EXECUTE_READWRITE);
                }

                if (pSender != IntPtr.Zero)
                {

                    if (client.Memory.WriteBytes(pSender.ToInt64(), OpCodes, (uint)OpCodes.Length))
                    {
                        sendCodeWritten = true;
                        return true;
                    }
                    Tibia.Util.WinApi.VirtualFreeEx(client.ProcessHandle, pSender, 0, Tibia.Util.WinApi.MEM_RELEASE);
                    pSender = IntPtr.Zero;
                }
                sendCodeWritten = false;
                return false;

            }
            #endregion
        }
    }
}
