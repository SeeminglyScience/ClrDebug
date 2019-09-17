using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugBreakpoint : Unknown
    {
        private ICorDebugBreakpointVtable** This => (ICorDebugBreakpointVtable**)DangerousGetPointer();

        public int Activate(bool bActive) => Calli(_this, This[0]->Activate, bActive.ToNativeInt());

        public int IsActivate(out bool bActive) => InvokeGet(_this, This[0]->IsActivate, out bActive);
    }

    [StructLayout(LayoutKind.Sequential)]

    internal unsafe struct ICorDebugBreakpointVtable
    {
        public IUnknownVtable IUnknown;

        public void* Activate;

        public void* IsActivate;
    }
}
