using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tibia.Constants;
using Tibia.Objects;
using Tibia.Packets;
using Tibia.Util;

namespace Inject
{
    unsafe class InjectedMain
    {
        List<IHook> hooks = new List<IHook>();

        public static int EntryPoint(string pwzArgument)
        {
            new InjectedMain().Run();
            return 0;
        }

        volatile bool sendingToClient = false;

        private void Run()
        {
            Tibia.Version.Set(FileVersionInfo.GetVersionInfo(Path.Combine(Environment.CurrentDirectory, Process.GetCurrentProcess().ProcessName + ".exe")).FileVersion);

            CreateHooks();

            EnableHooks();

            ConnectPipes();

            NetworkMessage message = new NetworkMessage(recvBuffer, recvBuffer.Length);
            while (true)
            {
                int bytesRead = pipeRecv.Read(recvBuffer, 0, recvBuffer.Length);
                if (bytesRead > 0)
                {
                    message.Position = 0;
                    message.Length = bytesRead;
                    ProcessCommand(message);
                }
                else
                {
                    break;
                }
            }

            Close();
        }

        byte[] recvBuffer = new byte[1024 * 1024];

        NamedPipeClientStream pipeSend;
        NamedPipeClientStream pipeRecv;

        private void ConnectPipes()
        {
            int processId = Process.GetCurrentProcess().Id;

            string name = "InjectedDllPipe_" + processId + "_";

            pipeRecv = new NamedPipeClientStream(name + "2");
            pipeRecv.Connect();

            pipeSend = new NamedPipeClientStream(name + "1");
            pipeSend.Connect();
        }

        private void ProcessCommand(NetworkMessage message)
        {
            PipePacketType type = (PipePacketType)message.GetByte();
            switch (type)
            {
                case PipePacketType.HookSendToClient:
                    SendToClient(message.GetBuffer(), 1, (uint)(message.Length - 1));
                    break;
                case PipePacketType.HookSendToServer:
                    SendToServer(message.GetBuffer(), 1, (uint)(message.Length - 1));
                    break;
            }
        }

        unsafe struct PacketStream
        {
            public byte* Buffer;
            public uint Size;
            public uint Pos;
        }

        public delegate void SendMessageDelegate(int type, string msg);
        SendMessageDelegate SendMessage = (SendMessageDelegate)Marshal.GetDelegateForFunctionPointer((IntPtr)0x00407520, typeof(SendMessageDelegate));

        public delegate void PrintTextDelegate(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, string lpText, int nAlign);
        PrintTextDelegate PrintText;

        public delegate void DrawItemDelegate(
            int surface,
            int x, int y,
            int size,
            uint itemId, int itemData1, int itemData2,
            int edgeR, int edgeG, int edgeB,
            int clipX, int clipY, int clipW, int clipH,
            ClientFont textFont, int textRed, int textGreen, int textBlue, int textAlign,
            int textForce);
        DrawItemDelegate DrawItemRaw;

        public delegate void DrawSkinDelegate(
            int surface,
            int x, int y,
            int width, int height,
            SkinType skinId,
            int dX, int dY);
        DrawSkinDelegate DrawSkin;

        unsafe delegate int GetNextPacketDelegate();

        unsafe PacketStream* packet;
        CallHook<GetNextPacketDelegate> GetNextPacketHook;
        CallHook<PrintTextDelegate> PrintFpsHook;

        delegate int WndProcDelegate(IntPtr hWnd, int msg, int wParam, int lParam);
        WindowHook<WndProcDelegate> WndProcHook;

        unsafe delegate int SendDelegate(SOCKET s, byte* buf, int len, int flags);
        FunctionHook<SendDelegate> SendHook;

        unsafe private void CreateHooks()
        {
            hooks.Add(GetNextPacketHook = new CallHook<GetNextPacketDelegate>(
                new IntPtr(Tibia.Addresses.Client.GetNextPacketCall),
                (GetNextPacketDelegate)GetNextPacket));

            hooks.Add(PrintFpsHook = new CallHook<PrintTextDelegate>(
                new IntPtr(Tibia.Addresses.TextDisplay.PrintFPS),
                (PrintTextDelegate)PrintFps));

            hooks.Add(SendHook = new FunctionHook<SendDelegate>(
                new IntPtr(Tibia.Addresses.Client.SendPointer),
                (SendDelegate)WinsockSend));

            hooks.Add(WndProcHook = new WindowHook<WndProcDelegate>(
                Process.GetCurrentProcess().MainWindowHandle,
                WinApi.GWLP_WNDPROC,
                (WndProcDelegate)WndProc));
        }

        private unsafe void EnableHooks()
        {
            packet = (PacketStream*)Tibia.Addresses.Client.RecvStream;

            Parser = (ParserDelegate)Marshal.GetDelegateForFunctionPointer(
                (IntPtr)Tibia.Addresses.Client.ParserFunc, typeof(ParserDelegate));

            PrintText = (PrintTextDelegate)Marshal.GetDelegateForFunctionPointer(
                (IntPtr)Tibia.Addresses.TextDisplay.PrintTextFunc, typeof(PrintTextDelegate));

            DrawItemRaw = (DrawItemDelegate)Marshal.GetDelegateForFunctionPointer(
                (IntPtr)Tibia.Addresses.DrawItem.DrawItemFunc, typeof(DrawItemDelegate));

            DrawSkin = (DrawSkinDelegate)Marshal.GetDelegateForFunctionPointer(
                (IntPtr)Tibia.Addresses.DrawSkin.DrawSkinFunc, typeof(DrawSkinDelegate));

            hooks.ForEach(h => h.Enable());
        }

        SOCKET sendSocket;

        int WinsockSend(SOCKET s, byte* buf, int len, int flags)
        {
            sendSocket = s;
            int bytesSent = SendHook.Original(s, buf, len, flags);

            if (bytesSent > 0)
            {
                byte[] b = new byte[bytesSent];
                for (int i = 0; i < bytesSent; i++)
                {
                    b[i] = buf[i];
                }

                Send(PipePacketType.HookSentPacket, b);
            }

            return bytesSent;
        }

        private void DisableHooks()
        {
            hooks.ForEach(h => h.Disable());
        }

        private int WndProc(IntPtr hWnd, int msg, int wParam, int lParam)
        {
            switch ((uint)msg)
            {
                case WinApi.WM_RBUTTONDOWN:
                    MessageBox.Show("Clicked right button");
                    break;

                default:
                    break;
            }

            return WndProcHook.Original(hWnd, msg, wParam, lParam);
        }

        private void Close()
        {
            DisableHooks();

            ClosePipes();
        }

        private void ClosePipes()
        {
            if (pipeSend != null)
            {
                pipeSend.Close();
            }

            if (pipeRecv != null)
            {
                pipeSend.Close();
            }
        }

        public delegate void ParserDelegate();
        ParserDelegate Parser;

        void SendToClient(byte[] dataBuffer, int offset, uint dataSize)
        {
            sendingToClient = true;
            fixed (byte* fixedPtr = &dataBuffer[offset])
            {
                PacketStream streamBackup = *packet;

                packet->Buffer = fixedPtr;
                packet->Size = dataSize;
                packet->Pos = 0;

                Parser();
                *packet = streamBackup;
            }
            sendingToClient = false;
        }

        void SendToServer(byte[] dataBuffer, int offset, uint dataSize)
        {
            fixed (byte* fixedPtr = &dataBuffer[offset])
            {
                SendHook.Original(sendSocket, fixedPtr, (int)dataSize, 0);
            }
        }

        void PrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, string lpText, int nAlign)
        {
            PrintFpsHook.Original(nSurface, nX, nY, nFont, nRed, nGreen, nBlue, lpText, nAlign);

            //PrintFpsHook.Original(nSurface, nX, nY + 14, nFont, 0xFF, 0x7F, 0x00, "Hello world", nAlign);

            //DrawingItem item = new DrawingItem();
            //item.Id = Tibia.Constants.Items.Valuable.CrystalCoin.Id;
            //item.Count = 50;
            //item.Position.X = item.ClippingRectangle.X = 200;
            //item.Position.Y = item.ClippingRectangle.Y = 100;
            //DrawItem(item);
        }

        void DrawItem(DrawingItem item)
        {
            DrawItemRaw(
                item.Surface,
                item.Position.X, item.Position.Y,
                item.Size,
                item.Id, item.Count, item.ItemSubType,
                item.EdgeColor.R, item.EdgeColor.G, item.EdgeColor.B,
                item.ClippingRectangle.X, item.ClippingRectangle.Y, item.ClippingRectangle.Width, item.ClippingRectangle.Height,
                item.TextFormat.Font,
                item.TextFormat.Color.R, item.TextFormat.Color.G, item.TextFormat.Color.B,
                item.TextFormat.Alignment, item.TextFormat.Force);
        }

        unsafe int GetNextPacket()
        {
            if (sendingToClient)
            {
                if (packet->Pos < packet->Size)
                {
                    //read the first byte of command to return from the function
                    int nextCmd = *(packet->Buffer + packet->Pos);

                    //increase stream pointer since we've read the first byte
                    packet->Pos++;
                    return nextCmd;
                }
                else return -1;
            }

            int packetType = GetNextPacketHook.Original();

            if (packetType != -1)
            {
                if ((packet->Pos - 1) == 8)
                {
                    byte* wholePacket = packet->Buffer + packet->Pos - 1;
                    uint packetSize = packet->Size - packet->Pos + 1;
                    if (packetSize > 0)
                    {
                        byte[] b = new byte[packetSize];

                        for (int i = 0; i < packetSize; i++)
                        {
                            b[i] = wholePacket[i];
                        }

                        Send(PipePacketType.HookReceivedPacket, b);
                    }
                }
            }

            return packetType;
        }

        private void Send(PipePacketType type, byte[] buffer)
        {
            try
            {
                if (pipeSend.CanWrite)
                {
                    byte[] b = new byte[buffer.Length + 1];
                    b[0] = (byte)type;

                    Array.Copy(buffer, 0, b, 1, buffer.Length);

                    pipeSend.BeginWrite(b, 0, b.Length, BeginWrite, null);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void BeginWrite(IAsyncResult ar)
        {
            try
            {
                pipeSend.EndWrite(ar);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public unsafe struct SOCKET
        {
            private void* handle;
            private SOCKET(int _handle)
            {
                handle = (void*)_handle;
            }
            public static bool operator ==(SOCKET s, int i)
            {
                return ((int)s.handle == i);
            }
            public static bool operator !=(SOCKET s, int i)
            {
                return ((int)s.handle != i);
            }
            public static implicit operator SOCKET(int i)
            {
                return new SOCKET(i);
            }
            public static implicit operator uint(SOCKET s)
            {
                return (uint)s.handle;
            }
            public override bool Equals(object obj)
            {
                return (obj is SOCKET) ? (((SOCKET)obj).handle == this.handle) : base.Equals(obj);
            }
            public override int GetHashCode()
            {
                return (int)handle;
            }
        }
    }
}
