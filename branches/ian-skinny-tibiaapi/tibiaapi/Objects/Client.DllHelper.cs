using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Tibia.Packets;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class DllHelper
        {
            private Client client;
            private AutoResetEvent pipeIsReady;
            private Pipe pipe = null; //For Displaying Text
            public event EventHandler PipeInizialazed;

            internal DllHelper(Client client)
            {
                this.client = client;
                pipeIsReady = new AutoResetEvent(false);
            }

            /// <summary>
            /// Inject a DLL into the process
            /// </summary>
            /// <param name="filename"></param>
            /// <returns></returns>
            public bool Inject(string filename)
            {
                if (!File.Exists(filename)) return false;
                // Get a block of memory to store the filename in the client
                IntPtr remoteAddress = Util.WinApi.VirtualAllocEx(client.ProcessHandle, IntPtr.Zero, (uint)filename.Length, Util.WinApi.MEM_COMMIT | Util.WinApi.MEM_RESERVE, Util.WinApi.PAGE_READWRITE);
                client.Memory.WriteStringNoEncoding(remoteAddress.ToInt32(), filename);
                IntPtr thread = Util.WinApi.CreateRemoteThread(client.ProcessHandle, IntPtr.Zero, 0, Util.WinApi.GetProcAddress(Util.WinApi.GetModuleHandle("Kernel32"), "LoadLibraryA"), remoteAddress, 0, IntPtr.Zero);
                Util.WinApi.VirtualFreeEx(client.ProcessHandle, remoteAddress, (uint)filename.Length, Util.WinApi.MEM_RELEASE);
                return thread.ToInt32() > 0 && remoteAddress.ToInt32() > 0;
            }

            /// <summary>
            /// Gets a value indicating if a dialog is opened.
            /// </summary>
            public AutoResetEvent PipeIsReady
            {
                get { return pipeIsReady; }
            }

            /// <summary>
            /// Get the pipe that connects client client to it's injected dll
            /// </summary>
            public Pipe Pipe
            {
                get { return pipe; }
            }
            public void InitializePipe()
            {
                if (pipe != null)
                    return;

                pipe = new Pipe(client, "TibiaAPI" + client.Process.Id.ToString());
                pipe.OnConnected += new Pipe.PipeNotification(OnPipeConnect);
                client.ContextMenu.AddInternalEvents();

                if (!Inject(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), "TibiaAPI_Inject.dll")))
                    throw new Tibia.Exceptions.InjectDLLNotFoundException();
            }

            private void OnPipeConnect()
            {
                //Set constants for displaying
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.PrintName, Tibia.Addresses.TextDisplay.PrintName);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.PrintFPS, Tibia.Addresses.TextDisplay.PrintFPS);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.ShowFPS, Tibia.Addresses.TextDisplay.ShowFPS);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.PrintTextFunc, Tibia.Addresses.TextDisplay.PrintTextFunc);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.NopFPS, Tibia.Addresses.TextDisplay.NopFPS);

                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.AddContextMenuFunc, Tibia.Addresses.ContextMenus.AddContextMenuPtr);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.OnClickContextMenu, Tibia.Addresses.ContextMenus.OnClickContextMenuPtr);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.SetOutfitContextMenu, Tibia.Addresses.ContextMenus.AddSetOutfitContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.PartyActionContextMenu, Tibia.Addresses.ContextMenus.AddPartyActionContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.CopyNameContextMenu, Tibia.Addresses.ContextMenus.AddCopyNameContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.TradeWithContextMenu, Tibia.Addresses.ContextMenus.AddTradeWithContextMenu);
                Packets.Pipes.SetConstantPacket.Send(client, PipeConstantType.OnClickContextMenuVf, Tibia.Addresses.ContextMenus.OnClickContextMenuVf);

                //Hook Display functions
                Packets.Pipes.InjectDisplayPacket.Send(client, true);
                pipeIsReady.Set();

                if (PipeInizialazed != null)
                    PipeInizialazed.BeginInvoke(client, new EventArgs(), null, null);

            }
        }
    }
}
