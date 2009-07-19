using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Tibia
{
    public static class Hooks
    {
        #region Interop

        internal const int WM_SIZE = 0x5;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        internal const int WM_SYSKEYDOWN = 0x0104;
        internal const int WM_SYSKEYUP = 0x0105;

        //This is the Import for the SetWindowsHookEx function.
        //Use this function to install a thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn,
        IntPtr hInstance, int threadId);

        //This is the Import for the UnhookWindowsHookEx function.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        internal static extern bool UnhookWindowsHookEx(IntPtr idHook);

        //This is the Import for the CallNextHookEx function.
        //Use this function to pass the hook information to the next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        // The type of method used as a handler (Filter)
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        // Different types of hooks.
        internal enum HookType : int
        {
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        //Mouse stuff
        internal const int WM_MOUSEMOVE = 0x200;
        internal const int WM_MOUSEWHEEL = 0x020A;
        internal const int WM_LBUTTONDOWN = 0x201;
        internal const int WM_RBUTTONDOWN = 0x204;
        internal const int WM_MBUTTONDOWN = 0x207;
        internal const int WM_XBUTTONDOWN = 0x20B;
        internal const int WM_LBUTTONUP = 0x202;
        internal const int WM_RBUTTONUP = 0x205;
        internal const int WM_MBUTTONUP = 0x208;
        internal const int WM_XBUTTONUP = 0x20C;

        internal const int XBUTTON1 = 0x1;
        internal const int XBUTTON2 = 0x2;

        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseHookStruct
        {
            public Point Point;
            public int Data;
            public int Flags;
            public int Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Point
        {
            public int X;
            public int Y;
        }
    }
}