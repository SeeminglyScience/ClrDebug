using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Extends the <see cref="CorDebugBreakpoint" /> to support breakpoints within functions.
    /// </summary>
    public unsafe class CorDebugFunctionBreakpoint : CorDebugBreakpoint
    {
        internal CorDebugFunctionBreakpoint()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the function in which the breakpoint is set.
        /// </summary>
        public int GetFunction(out CorDebugFunction function) => InvokeGetObject(_this, This[0]->GetFunction, out function);

        /// <summary>
        /// Gets the offset of the breakpoint within the function.
        /// </summary>
        public int GetOffset(out uint nOffset) => InvokeGet(_this, This[0]->GetOffset, out nOffset);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public CorDebugBreakpoint.Vtable ICorDebugBreakpoint;

            public void* GetFunction;

            public void* GetOffset;
        }
    }
}
