using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Tibia
{
    public class KeyboardHook
    {
        #region Interop

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        //This is the Import for the SetWindowsHookEx function.
        //Use this function to install a thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn,
        IntPtr hInstance, int threadId);

        //This is the Import for the UnhookWindowsHookEx function.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(IntPtr idHook);

        //This is the Import for the CallNextHookEx function.
        //Use this function to pass the hook information to the next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        // The type of method used as a handler (Filter)
        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        // The handle to the hook (used for installing/uninstalling it).
        private static IntPtr hHook = IntPtr.Zero;

        /// <summary>
        /// Check to see if either Control modifier is active.
        /// </summary>
        public static bool Control = false;
        /// <summary>
        /// Check to see if either Shift modifier is active.
        /// </summary>
        public static bool Shift = false;
        /// <summary>
        /// Check to see if either Alt modifier is active.
        /// </summary>
        public static bool Alt = false;
        /// <summary>
        /// Check to see if either Win modifier is active.
        /// </summary>
        public static bool Win = false;

        /// <summary>
        /// Delegate for handling a key down event.
        /// </summary>
        /// <param name="key">The key that was pressed. Check Control, Shift, Alt, and Win for modifiers.</param>
        /// <returns>True if you want the key to pass through
        /// (be recognized for the app), False if you want it
        /// to be trapped (app never sees it).</returns>
        public delegate bool HookHandler(Keys key);

        /// <summary>
        /// Add a HookHandler delegate to this to activate hotkeys.
        /// </summary>
        public static HookHandler KeyDown;

        /// <summary>
        /// Keep track of the hook state.
        /// </summary>
        public static bool Enabled;

        // Different types of hooks.
        private enum HookType : int
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

        /// <summary>
        /// Start the keyboard hook.
        /// </summary>
        /// <returns>True if no exceptions.</returns>
        public static bool Enable()
        {
            if (Enabled == false)
            {
                try
                {
                    using (Process curProcess = Process.GetCurrentProcess())
                    using (ProcessModule curModule = curProcess.MainModule)
                        hHook = SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL, Filter, GetModuleHandle(curModule.ModuleName), 0);
                    Enabled = true;
                    return true;
                }
                catch (Exception ex)
                {
                    Enabled = false;
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// Disable keyboard hooking.
        /// </summary>
        /// <returns>True if disabled correctly.</returns>
        public static bool Disable()
        {
            if (Enabled == true)
            {
                try
                {
                    UnhookWindowsHookEx(hHook);
                    Enabled = false;
                    return true;
                }
                catch (Exception ex)
                {
                    Enabled = true;
                    return false;
                }
            }
            else
                return false;
        }

        private static IntPtr Filter(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool result = true;
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_KEYDOWN 
                    || wParam == (IntPtr)WM_SYSKEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    if ((Keys)vkCode == Keys.LControlKey || 
                        (Keys)vkCode == Keys.RControlKey)
                        Control = true;
                    else if ((Keys)vkCode == Keys.LShiftKey ||
                        (Keys)vkCode == Keys.RShiftKey)
                        Shift = true;
                    else if ((Keys)vkCode == Keys.RMenu ||
                        (Keys)vkCode == Keys.LMenu)
                        Alt = true;
                    else if ((Keys)vkCode == Keys.RWin || 
                        (Keys)vkCode == Keys.LWin)
                        Win = true;
                    else
                        result = OnKeyDown((Keys)vkCode);
                }
                else if (wParam == (IntPtr)WM_KEYUP 
                    || wParam == (IntPtr)WM_SYSKEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    if ((Keys)vkCode == Keys.LControlKey ||
                        (Keys)vkCode == Keys.RControlKey)
                        Control = false;
                    else if ((Keys)vkCode == Keys.LShiftKey ||
                        (Keys)vkCode == Keys.RShiftKey)
                        Shift = false;
                    else if ((Keys)vkCode == Keys.RMenu ||
                        (Keys)vkCode == Keys.LMenu)
                        Alt = false;
                    else if ((Keys)vkCode == Keys.RWin ||
                        (Keys)vkCode == Keys.LWin)
                        Win = false;
                }
            }
            return result ? CallNextHookEx(hHook, nCode, wParam, lParam) : new IntPtr(1);
        }

        private static bool OnKeyDown(Keys key)
        {
            if (KeyDown != null)
                return KeyDown(key);
            else
                return true;
        }

        /// <summary>
        /// Return a string representation of the given key based on current modifiers.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string KeyToString(Keys key)
        {
            return (KeyboardHook.Control ? "Ctrl + " : "") +
                            (KeyboardHook.Alt ? "Alt + " : "") +
                            (KeyboardHook.Shift ? "Shift + " : "") +
                            (KeyboardHook.Win ? "Win + " : "") +
                            key.ToString();
        }
    }
}
