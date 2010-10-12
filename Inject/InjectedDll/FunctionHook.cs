using System;
using System.Runtime.InteropServices;
using Tibia.Util;

namespace Inject
{
    public class FunctionHook<TDel> : FunctionHookContainer<TDel>.FunctionHook<TDel>, IHook where TDel : class
    {
        public FunctionHook(IntPtr hookAddress, TDel newFunction) : base(hookAddress, newFunction) { }
    }

    public abstract class FunctionHookContainer<Temp> where Temp : class
    {
        public abstract class FunctionHook<TDelegate> where TDelegate : class, Temp
        {
            IntPtr hookAddress;
            IntPtr newFunction;
            IntPtr oldFunction;

            TDelegate original;

            public TDelegate Original { get { return original; } }

            public FunctionHook(IntPtr hookAddress, TDelegate newFunction)
            {
                WinApi.MemoryProtection oldProtect, newProtect;

                this.hookAddress = hookAddress;
                this.newFunction = Marshal.GetFunctionPointerForDelegate(newFunction as Delegate);

                WinApi.VirtualProtect(hookAddress, 4, WinApi.MemoryProtection.ReadWrite, out oldProtect);

                oldFunction = Marshal.ReadIntPtr(hookAddress);

                WinApi.VirtualProtect(hookAddress, 4, oldProtect, out newProtect);

                original = Marshal.GetDelegateForFunctionPointer(oldFunction, typeof(TDelegate)) as TDelegate;
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
                WinApi.MemoryProtection oldProtect, newProtect;
                WinApi.VirtualProtect(hookAddress, 4, WinApi.MemoryProtection.ReadWrite, out oldProtect);

                Marshal.WriteIntPtr(hookAddress, newCall);

                WinApi.VirtualProtect(hookAddress, 4, oldProtect, out newProtect);
            }
        }
    }
}
