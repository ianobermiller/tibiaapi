using System;
using System.Runtime.InteropServices;
using Tibia.Util;

namespace Inject
{
    public class WindowHook<TDel> : WindowHookContainer<TDel>.WindowHook<TDel>, IHook where TDel : class
    {
        public WindowHook(IntPtr handle, int index, TDel newFunction) : base(handle, index, newFunction) { }
    }

    public abstract class WindowHookContainer<Temp> where Temp : class
    {
        public abstract class WindowHook<TDelegate> where TDelegate : class, Temp
        {
            IntPtr handle;
            int index;
            IntPtr newFunction;
            IntPtr oldFunction;

            TDelegate original;

            public TDelegate Original { get { return original; } }

            public WindowHook(IntPtr handle, int index, TDelegate newFunction)
            {
                this.handle = handle;
                this.index = index;
                this.newFunction = Marshal.GetFunctionPointerForDelegate(newFunction as Delegate);
                this.oldFunction = WinApi.GetWindowLong(handle, index);
                this.original = Marshal.GetDelegateForFunctionPointer(oldFunction, typeof(TDelegate)) as TDelegate;
            }

            public void Enable()
            {
                SetCall(newFunction);
            }

            public void Disable()
            {
                SetCall(oldFunction);
            }

            private void SetCall(IntPtr newCall)
            {
                WinApi.SetWindowLong(
                   handle,
                   index,
                   newCall);
            }
        }
    }
}
