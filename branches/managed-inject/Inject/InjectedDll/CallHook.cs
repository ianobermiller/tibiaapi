using System;
using System.Runtime.InteropServices;
using Tibia.Util;

namespace Inject
{
    public class CallHook<TDel> : CallHookContainer<TDel>.CallHook<TDel>, IHook where TDel : class
    {
        public CallHook(IntPtr hookAddress, TDel newFunction) : base(hookAddress, newFunction) { }
    }

    public abstract class CallHookContainer<Temp> where Temp : class
    {
        public abstract class CallHook<TDelegate> where TDelegate : class, Temp
        {
            IntPtr hookAddress;
            IntPtr newFunction;
            IntPtr oldFunction;

            TDelegate original;

            public TDelegate Original { get { return original; } }

            public CallHook(IntPtr hookAddress, TDelegate newFunction)
            {
                WinApi.MemoryProtection oldProtect, newProtect;

                this.hookAddress = hookAddress;
                this.newFunction = Marshal.GetFunctionPointerForDelegate(newFunction as Delegate);

                WinApi.VirtualProtect(hookAddress, 5, WinApi.MemoryProtection.ReadWrite, out oldProtect);

                oldFunction = Marshal.ReadIntPtr(new IntPtr(hookAddress.ToInt32() + 1));

                WinApi.VirtualProtect(hookAddress, 5, oldProtect, out newProtect);

                IntPtr originalFunction = new IntPtr(hookAddress.ToInt32() + oldFunction.ToInt32() + 5);
                original = Marshal.GetDelegateForFunctionPointer(originalFunction, typeof(TDelegate)) as TDelegate;
            }

            public void Enable()
            {
                SetCall(new IntPtr(newFunction.ToInt32() - hookAddress.ToInt32() - 5));
            }

            public void Disable()
            {
                SetCall(oldFunction);
            }

            private void SetCall(IntPtr newCall)
            {
                WinApi.MemoryProtection oldProtect, newProtect;
                WinApi.VirtualProtect(hookAddress, 5, WinApi.MemoryProtection.ReadWrite, out oldProtect);

                Marshal.WriteIntPtr(hookAddress, 1, newCall);

                WinApi.VirtualProtect(hookAddress, 5, oldProtect, out newProtect);
            }
        }
    }
}
