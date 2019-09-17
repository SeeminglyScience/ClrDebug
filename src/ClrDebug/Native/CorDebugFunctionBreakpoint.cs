using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugFunctionBreakpoint : CorDebugBreakpoint
    {
        private ICorDebugFunctionBreakpointVtable** This => (ICorDebugFunctionBreakpointVtable**)DangerousGetPointer();

        public int GetFunction(out CorDebugFunction function) => InvokeGetObject(_this, This[0]->GetFunction, out function);

        public int GetOffset(out uint nOffset) => InvokeGet(_this, This[0]->GetOffset, out nOffset);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugFunctionBreakpointVtable
        {
            public IUnknownVtable IUnknown;

            public ICorDebugBreakpointVtable ICorDebugBreakpoint;

            public void* GetFunction;

            public void* GetOffset;
        }
    }
}
