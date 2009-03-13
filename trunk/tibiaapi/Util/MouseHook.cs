using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

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
        /// <param name="button">The mouse button that was pressed.</param>
        /// <returns>True if you want the key to pass through
        /// (be recognized for the app), False if you want it
        /// to be trapped (app never sees it).</returns>
        public delegate bool MouseButtonHandler(MouseButtons button);

        public delegate bool MouseMoveHandler(Point point);

        public delegate bool MouseScrollHandler(int delta);

        public static MouseButtonHandler ButtonDown;
        public static MouseButtonHandler ButtonUp;
        public static MouseMoveHandler Moved;
        public static MouseScrollHandler Scrolled;

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
                Hooks.MouseHookStruct info = (Hooks.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Hooks.MouseHookStruct));
                switch ((int)wParam)
                {
                    case Hooks.WM_LBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Left);
                        break;
                    case Hooks.WM_LBUTTONUP:
                        result = OnMouseUp(MouseButtons.Left);
                        break;
                    case Hooks.WM_RBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Right);
                        break;
                    case Hooks.WM_RBUTTONUP:
                        result = OnMouseUp(MouseButtons.Right);
                        break;
                    case Hooks.WM_MBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Middle);
                        break;
                    case Hooks.WM_MBUTTONUP:
                        result = OnMouseUp(MouseButtons.Middle);
                        break;
                    case Hooks.WM_XBUTTONDOWN:
                        if (info.Data >> 16 == Hooks.XBUTTON1)
                            result = OnMouseDown(MouseButtons.XButton1);
                        else if (info.Data >> 16 == Hooks.XBUTTON2)
                            result = OnMouseDown(MouseButtons.XButton2);
                        break;
                    case Hooks.WM_XBUTTONUP:
                        if (info.Data >> 16 == Hooks.XBUTTON1)
                            result = OnMouseUp(MouseButtons.XButton1);
                        else if (info.Data >> 16 == Hooks.XBUTTON2)
                            result = OnMouseUp(MouseButtons.XButton2);
                        break;
                    case Hooks.WM_MOUSEMOVE:
                        result = OnMouseMove(new Point(info.Point.X, info.Point.Y));
                        break;
                    case Hooks.WM_MOUSEWHEEL:
                        result = OnMouseWheel((info.Data >> 16) & 0xffff);
                        break;
                }
            }

            return result ? Hooks.CallNextHookEx(hHook, nCode, wParam, lParam) : new IntPtr(1);
        }

        private static bool OnMouseDown(MouseButtons button)
        {
            if (ButtonDown != null)
                return ButtonDown(button);
            else
                return true;
        }

        private static bool OnMouseUp(MouseButtons button)
        {
            if (ButtonUp != null)
                return ButtonUp(button);
            else
                return true;
        }

        private static bool OnMouseMove(Point point)
        {
            if (Moved != null)
                return Moved(point);
            else
                return true;
        }

        private static bool OnMouseWheel(int delta)
        {
            if (Scrolled != null)
                return Scrolled(delta);
            else
                return true;
        }
    }
}