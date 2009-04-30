using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Objects
{
    public partial class Client
    {
        public class WindowHelper
        {
            private Client client;
            private int defBarY, defRectX, defRectY, defRectW, defRectH;
            private bool isVisible;

            internal WindowHelper(Client client) { this.client = client; }

            /// <summary>
            /// This will set the FPSLimit with the value you give
            /// NOTE: The official value is 1000/fpsmax
            /// </summary>
            /// <param name="value"></param>
            public void SetFPSLimit(double value)
            {
                int frameRateBegin = client.Memory.ReadInt32(Addresses.Client.FrameRatePointer);
                client.Memory.WriteDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset, value);
            }

            /// <summary>
            /// Get the current FPS of the client.
            /// </summary>
            public double FPSCurrent
            {
                get
                {
                    int frameRateBegin = client.Memory.ReadInt32(Addresses.Client.FrameRatePointer);
                    return client.Memory.ReadDouble(frameRateBegin + Addresses.Client.FrameRateCurrentOffset);
                }
            }

            /// <summary>
            /// Get or set the FPS limit for the client.
            /// </summary>
            /// <returns></returns>
            public double FPSLimit
            {
                get
                {
                    int frameRateBegin = client.Memory.ReadInt32(Addresses.Client.FrameRatePointer);
                    double value = 1000 / client.Memory.ReadDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset);

                    double valueL = Math.Round(value); // using Math.Round
                }
                set
                {
                    if (value <= 0) value = 1;
                    int frameRateBegin = client.Memory.ReadInt32(Addresses.Client.FrameRatePointer);
                    client.Memory.WriteDouble(frameRateBegin + Addresses.Client.FrameRateLimitOffset, Calculate.ConvertFPS(value));
                }
            }

            /// <summary>
            /// Flashes the client's window and taskbar.
            /// </summary>
            public void Flash()
            {
                Util.WinApi.FlashWindow(Handle, false);
            }

            /// <summary>
            /// Get if this client is the active window, or bring it to the foreground
            /// </summary>
            public bool IsActive
            {
                get
                { return Handle == Util.WinApi.GetForegroundWindow(); }
                set
                {
                    if (value)
                        Util.WinApi.SetForegroundWindow(Handle);
                }
            }

            /// <summary>
            /// Gets the client process' main window handle.
            /// </summary>
            public IntPtr Handle
            {
                get
                {
                    if (client.Process.MainWindowHandle == IntPtr.Zero)
                        client.Process.Refresh();

                    return client.Process.MainWindowHandle;
                }
            }

            /// <summary>
            /// Check if the client is minimized
            /// </summary>
            /// <returns></returns>
            public bool IsMinimized
            {
                get { return Util.WinApi.IsIconic(Handle); }
            }

            /// <summary>
            /// Check if the client is maximized
            /// </summary>
            /// <returns></returns>
            public bool IsMaximized
            {
                get { return Util.WinApi.IsZoomed(Handle); }
            }

            public bool IsVisible
            {
                set
                {
                    Util.WinApi.ShowWindow(Handle, value ? Util.WinApi.SW_SHOW : Util.WinApi.SW_HIDE);
                    isVisible = value;
                }
                get { return isVisible; }
            }

            /// <summary>
            /// Get or set the title of the client.
            /// </summary>
            public string Title
            {
                get { return client.Process.MainWindowTitle; }
                set { Util.WinApi.SetWindowText(Handle, value); }
            }

            /// <summary>
            /// Sets the Tibia client as the topmost application or not.
            /// </summary>
            public bool IsTopMost
            {
                set
                {
                    Util.WinApi.SetWindowPos(Handle, (value) ? Util.WinApi.HWND_TOPMOST :
                        Util.WinApi.HWND_NOTOPMOST, 0, 0, 0, 0, Util.WinApi.SWP_NOMOVE | Util.WinApi.SWP_NOSIZE);
                }
            }

            /// <summary>
            /// Gets the position of the client, and its outer boundaries
            /// </summary>
            public Rect Size
            {
                get
                {
                    Util.WinApi.RECT r = new Tibia.Util.WinApi.RECT();
                    Util.WinApi.GetWindowRect(Handle, ref r);
                    return new Rect(r);
                }
            }

            /// <summary>
            /// Gets or sets world only view.
            /// </summary>
            /// <returns></returns>
            public bool WorldOnlyView
            {
                get
                {
                    int screenBar;
                    screenBar = client.Memory.ReadInt32(Addresses.Client.GameWindowBar);
                    return client.Memory.ReadInt32(screenBar + 0x70) == Size.Height;
                }
                set
                {
                    int screenRect, screenBar;
                    screenRect = client.Memory.ReadInt32(Addresses.Client.GameWindowRectPointer);
                    screenRect = client.Memory.ReadInt32(screenRect + 0x18 + 0x04);
                    screenBar = client.Memory.ReadInt32(Addresses.Client.GameWindowBar);

                    if (value && client.Memory.ReadInt32(screenBar + 0x70) != Size.Height)
                    {
                        defBarY = client.Memory.ReadInt32(screenBar + 0x70);
                        defRectX = client.Memory.ReadInt32(screenRect + 0x14);
                        defRectY = client.Memory.ReadInt32(screenRect + 0x18);
                        defRectW = client.Memory.ReadInt32(screenRect + 0x1C);
                        defRectH = client.Memory.ReadInt32(screenRect + 0x20);
                        client.Memory.WriteInt32(screenBar + 0x70, Size.Height);
                        client.Memory.WriteInt32(screenRect + 0x14, 0);
                        client.Memory.WriteInt32(screenRect + 0x18, 0);
                        client.Memory.WriteInt32(screenRect + 0x1C, Size.Width);
                        client.Memory.WriteInt32(screenRect + 0x20, Size.Height);
                    }
                    else if (!value && defBarY != 0 && defRectX != 0 &&
                        defRectY != 0 && defRectW != 0 && defRectH != 0)
                    {
                        client.Memory.WriteInt32(screenBar + 0x70, defBarY);
                        client.Memory.WriteInt32(screenRect + 0x14, defRectX);
                        client.Memory.WriteInt32(screenRect + 0x18, defRectY);
                        client.Memory.WriteInt32(screenRect + 0x1C, defRectW);
                        client.Memory.WriteInt32(screenRect + 0x20, defRectH);
                    }
                }
            }

            /// <sumary>
            /// Gets or sets wide screen view
            /// </sumary>
            public bool WideScreenView
            {
                get
                {
                    int screenRect, screenBar;
                    screenRect = client.Memory.ReadInt32(Addresses.Client.GameWindowRectPointer);
                    screenRect = client.Memory.ReadInt32(screenRect + 0x18 + 0x04);
                    screenBar = client.Memory.ReadInt32(Addresses.Client.GameWindowBar);
                    return client.Memory.ReadInt32(screenRect + 0x14) == 5;
                }
                set
                {
                    int screenRect, screenBar;
                    screenRect = client.Memory.ReadInt32(Tibia.Addresses.Client.GameWindowRectPointer);
                    screenRect = client.Memory.ReadInt32(screenRect + 0x18 + 0x4);
                    screenBar = client.Memory.ReadInt32(Tibia.Addresses.Client.GameWindowBar);

                    if (value && !WideScreenView)
                    {
                        defRectX = client.Memory.ReadInt32(screenRect + 0x14);
                        defRectW = client.Memory.ReadInt32(screenRect + 0x1C);
                        client.Memory.WriteInt32(screenRect + 0x14, 5);
                        client.Memory.WriteInt32(screenRect + 0x1C, client.Memory.ReadInt32(screenBar + 0x74) - 10);
                    }
                    else if (!value && defRectX != 0 && defRectW != 0)
                    {
                        client.Memory.WriteInt32(screenRect + 0x14, defRectX);
                        client.Memory.WriteInt32(screenRect + 0x1C, defRectW);
                    }
                }

            }
        }
    }
}
