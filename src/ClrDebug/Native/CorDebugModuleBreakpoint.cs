using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides support for module breakpoints.
    /// </summary>
    public unsafe class CorDebugModuleBreakpoint : CorDebugBreakpoint
    {
        internal CorDebugModuleBreakpoint()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the module in which this breakpoint is set.
        /// </summary>
        public int GetModule(out CorDebugModule module) => InvokeGetObject(_this, This[0]->GetModule, out module);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public CorDebugBreakpoint.Vtable ICorDebugBreakpoint;

            public void* GetModule;
        }
    }
}
