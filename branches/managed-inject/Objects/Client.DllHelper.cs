using System;
using System.IO;
using System.Threading;
using Tibia.Packets;
using Tibia.Util;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class DllHelper
        {
            private Client client;
            private AutoResetEvent pipeIsReady;
            private Pipe pipe = null;
            public event EventHandler PipeInitialized;

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
                if (!File.Exists(filename))
                {
                    throw new FileNotFoundException("Dll to inject does not exist: " + filename);
                }

                // Get a block of memory to store the filename in the client
                IntPtr remoteAddress = WinApi.VirtualAllocEx(
                    client.ProcessHandle,
                    IntPtr.Zero,
                    (uint)filename.Length,
                    WinApi.AllocationType.Commit | WinApi.AllocationType.Reserve,
                    WinApi.MemoryProtection.ExecuteReadWrite);

                // Write the filename to the client's memory
                client.Memory.WriteStringNoEncoding(remoteAddress.ToInt32(), filename);

                // Start the remote thread, first loading our library
                IntPtr thread = WinApi.CreateRemoteThread(
                    client.ProcessHandle, IntPtr.Zero, 0,
                    WinApi.GetProcAddress(WinApi.GetModuleHandle("Kernel32"), "LoadLibraryA"),
                    remoteAddress, 0, IntPtr.Zero);

                WinApi.WaitForSingleObject(thread, 0xFFFFFFFF); // Infinite

                // Free the memory used for the filename
                WinApi.VirtualFreeEx(
                    client.ProcessHandle,
                    remoteAddress,
                    (uint)filename.Length,
                    WinApi.AllocationType.Release);

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

                pipe = new Pipe(client);
                pipe.OnConnected += new Pipe.PipeNotification(OnPipeConnect);
                client.ContextMenu.AddInternalEvents();
                client.Icon.AddInternalEvents();

                if (!Inject(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), "InjectBooter.dll")))
                    throw new Tibia.Exceptions.InjectDLLNotFoundException();
            }

            private void OnPipeConnect()
            {
                //Hook Display functions
                //Packets.Pipes.HooksEnableDisablePacket.Send(client, true);
                pipeIsReady.Set();

                if (PipeInitialized != null)
                    PipeInitialized.BeginInvoke(client, new EventArgs(), null, null);
            }

            public static void Extract(byte[] resourceBytes, string newFileName)
            {
                bool doExtract = false;

                if (File.Exists(newFileName))
                {
                    byte[] embeddedBytes = resourceBytes;
                    byte[] existingBytes = File.ReadAllBytes(newFileName);

                    if (embeddedBytes.Length == existingBytes.Length)
                    {
                        uint embeddedChecksum = AdlerChecksum.Generate(
                            ref embeddedBytes, 0, embeddedBytes.Length);
                        uint existingChecksum = AdlerChecksum.Generate(
                            ref existingBytes, 0, existingBytes.Length);

                        if (embeddedChecksum != existingChecksum)
                        {
                            doExtract = true;
                        }
                    }
                    else
                    {
                        doExtract = true;
                    }
                }
                else
                {
                    doExtract = true;
                }

                if (doExtract)
                {
                    FileStream fileStream = new FileStream(newFileName, FileMode.Create);
                    fileStream.Write(resourceBytes, 0, (int)resourceBytes.Length);
                    fileStream.Close();
                }
            }
        }
    }
}
