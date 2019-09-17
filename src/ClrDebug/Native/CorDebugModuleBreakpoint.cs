using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugModuleBreakpoint : Unknown
    {
        private ICorDebugModuleBreakpointVtable** This => (ICorDebugModuleBreakpointVtable**)DangerousGetPointer();

        public int GetModule(out CorDebugModule module) => InvokeGetObject(_this, This[0]->GetModule, out module);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugModuleBreakpointVtable
        {
            public IUnknownVtable IUnknown;

            public ICorDebugBreakpointVtable ICorDebugBreakpoint;

            public void* GetModule;
        }
    }
}
