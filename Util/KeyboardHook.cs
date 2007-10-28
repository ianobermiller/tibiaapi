using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Tibia
{
    public static class KeyboardHook
    {
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        static int hHook = 0;

        public const int WH_KEYBOARD_LL = 13;

        static HookProc KeyboardProc;

        //This is the Import for the SetWindowsHookEx function.
        //Use this function to install a thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn,
        IntPtr hInstance, int threadId);

        //This is the Import for the UnhookWindowsHookEx function.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //This is the Import for the CallNextHookEx function.
        //Use this function to pass the hook information to the next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode,
        IntPtr wParam, IntPtr lParam);

        public static void SetKeyHookLL()
        {
            KeyboardProc = new HookProc(KeyboardHookProc);
            hHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardProc, (IntPtr)0, AppDomain.GetCurrentThreadId());
        }

        public static void Unhook()
        {
            UnhookWindowsHookEx(hHook);
        }

        public static int KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            MessageBox.Show("ok");

            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }
    }
}
