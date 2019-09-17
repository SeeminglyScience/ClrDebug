using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugValue : Unknown
    {
        private ICorDebugValueVtable** This => (ICorDebugValueVtable**)DangerousGetPointer();

        public int GetType(out CorElementType type)
        {
            int pType = 0;
            int result = Calli(_this, This[0]->GetType, &pType);
            type = (CorElementType)pType;
            return result;
        }

        public int GetSize(out uint size)
            => InvokeGet(_this, This[0]->GetSize, out size);

        public int GetAddress(out ulong address)
            => InvokeGet(_this, This[0]->GetAddress, out address);

        public int CreateBreakpoint(out CorDebugValueBreakpoint breakpoint)
            => InvokeGetObject(_this, This[0]->CreateBreakpoint, out breakpoint);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ICorDebugValueVtable
    {
        public IUnknownVtable IUnknown;

        public new void* GetType;

        public void* GetSize;

        public void* GetAddress;

        public void* CreateBreakpoint;
    }
}
