using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Tibia
{
    /// <summary>
    /// The global mouse hook. This can be used to globally capture mouse input.
    /// </summary>
    public static class MouseHook
    {
        // The handle to the hook (used for installing/uninstalling it).
        private static IntPtr hHook = IntPtr.Zero;

        //Delegate that points to the filter function
        private static Hooks.HookProc hookproc = new Hooks.HookProc(Filter);

        /// <summary>
        /// Delegate for handling mouse input.
        /// </summary>
        /// <param name="key">The key that was pressed. Check Control, Shift, Alt, and Win for modifiers.</param>
        /// <returns>True if you want the key to pass through
        /// (be recognized for the app), False if you want it
        /// to be trapped (app never sees it).</returns>
        public delegate bool MouseHookHandler(MouseButtons button);

        /// <summary>
        /// Add a MouseHook delegate here.
        /// </summary>
        public static MouseHookHandler MouseDown;

        /// <summary>
        /// Keep track of the hook state.
        /// </summary>
        private static bool Enabled;

        /// <summary>
        /// Start the mouse hook.
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
                        hHook = Hooks.SetWindowsHookEx((int)Hooks.HookType.WH_MOUSE_LL, hookproc, Hooks.GetModuleHandle(curModule.ModuleName), 0);
                    Enabled = true;
                    return true;
                }
                catch
                {
                    Enabled = false;
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// Disable mouse hooking.
        /// </summary>
        /// <returns>True if disabled correctly.</returns>
        public static bool Disable()
        {
            if (Enabled == true)
            {
                try
                {
                    Hooks.UnhookWindowsHookEx(hHook);
                    Enabled = false;
                    return true;
                }
                catch
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
                if (wParam == (IntPtr)Hooks.WM_LBUTTONDOWN)
                {
                    OnMouseDown(MouseButtons.Left);
                }
                else if (wParam == (IntPtr)Hooks.WM_RBUTTONDOWN)
                {
                    OnMouseDown(MouseButtons.Right);
                }
                else if (wParam == (IntPtr)Hooks.WM_MBUTTONDOWN)
                {
                    OnMouseDown(MouseButtons.Middle);
                }
                else if (wParam == (IntPtr)Hooks.WM_XBUTTONDOWN)
                {
                    Hooks.MouseHookStruct stru = (Hooks.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Hooks.MouseHookStruct));

                    if (stru.Data >> 16 == Hooks.XBUTTON1)
                        OnMouseDown(MouseButtons.XButton1);
                    else if (stru.Data >> 16 == Hooks.XBUTTON2)
                        OnMouseDown(MouseButtons.XButton2);
                }
            }

            return result ? Hooks.CallNextHookEx(hHook, nCode, wParam, lParam) : new IntPtr(1);
        }

        private static bool OnMouseDown(MouseButtons button)
        {
            if (MouseDown != null)
                return MouseDown(button);
            else
                return true;
        }
    }
}