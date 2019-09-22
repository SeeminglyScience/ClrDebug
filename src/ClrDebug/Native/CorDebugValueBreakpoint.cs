using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;
using System;

namespace ClrDebug.Native
{
    /// <summary>
    /// Extends the <see cref="CorDebugBreakpoint" /> to support breakpoints for object values.
    /// </summary>
    public unsafe class CorDebugValueBreakpoint : Unknown
    {
        private ICorDebugValueBreakpointVtable** This => (ICorDebugValueBreakpointVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the value of the object on which the breakpoint is set.
        /// </summary>
        public int GetValue(out CorDebugValue value) => InvokeGetObject(_this, This[0]->GetValue, out value);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugValueBreakpointVtable
        {
            public ICorDebugBreakpointVtable ICorDebugBreakpoint;

            public void* GetValue;
        }
    }
}
