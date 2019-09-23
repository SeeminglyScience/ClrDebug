using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a breakpoint in a function, or a watch point on a value.
    /// </summary>
    public unsafe class CorDebugBreakpoint : Unknown
    {
        private ICorDebugBreakpointVtable** This => (ICorDebugBreakpointVtable**)DangerousGetPointer();

        /// <summary>
        /// Sets the active state of this breakpoint.
        /// </summary>
        /// <param name="bActive">
        /// Indicates whether to set the state of this breakpoint to active.
        /// </param>
        public int Activate(bool bActive) => Calli(_this, This[0]->Activate, bActive.ToNativeInt());

        /// <summary>
        /// Gets a value that indicates whether this breakpoint is active.
        /// </summary>
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
