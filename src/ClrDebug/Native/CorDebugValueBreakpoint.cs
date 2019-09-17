using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;
using System;

namespace ClrDebug.Native
{
    public unsafe class CorDebugValueBreakpoint : Unknown
    {
        private ICorDebugValueBreakpointVtable** This => (ICorDebugValueBreakpointVtable**)DangerousGetPointer();

        public int GetValue(out CorDebugValue value) => InvokeGetObject(_this, This[0]->GetValue, out value);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugValueBreakpointVtable
        {
            public ICorDebugBreakpointVtable ICorDebugBreakpoint;

            public void* GetValue;
        }
    }
}
