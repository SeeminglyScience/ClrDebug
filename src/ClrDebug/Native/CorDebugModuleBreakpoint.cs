using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides support for module breakpoints.
    /// </summary>
    public unsafe class CorDebugModuleBreakpoint : Unknown
    {
        private ICorDebugModuleBreakpointVtable** This => (ICorDebugModuleBreakpointVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the module in which this breakpoint is set.
        /// </summary>
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
