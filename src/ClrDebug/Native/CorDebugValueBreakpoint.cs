using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Extends the <see cref="CorDebugBreakpoint" /> to support breakpoints for object values.
    /// </summary>
    public unsafe class CorDebugValueBreakpoint : CorDebugBreakpoint
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the value of the object on which the breakpoint is set.
        /// </summary>
        public int GetValue(out CorDebugValue value) => InvokeGetObject(_this, This[0]->GetValue, out value);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public CorDebugBreakpoint.Vtable ICorDebugBreakpoint;

            public void* GetValue;
        }
    }
}
