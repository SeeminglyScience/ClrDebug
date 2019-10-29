using System;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// A subclass of <c>ICorDebugValue</c> that represents an object that
    /// has been collected by the common language runtime (CLR) garbage
    /// collector.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe partial class CorDebugHeapValue : CorDebugValue
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// This method is not implemented in the current version of the .NET Framework.
        /// </summary>
        public int CreateRelocBreakpoint(out CorDebugValueBreakpoint breakpoint)
            => InvokeGetObject(_this, This[0]->CreateRelocBreakpoint, out breakpoint);

        /// <summary>
        /// Gets a value that indicates whether the object represented by
        /// this <c>ICorDebugHeapValue</c> is valid.
        ///
        /// This method has been deprecated in the .NET Framework version 2.0.
        /// </summary>
        /// <param name="isValid">
        /// A pointer to a Boolean value that indicates whether this value
        /// on the heap is valid.
        /// </param>
        /// <remarks>
        /// The value is invalid if it has been reclaimed by the garbage collector.
        ///
        /// This method has been deprecated. In the .NET Framework 2.0, all values
        /// are valid until <c>ICorDebugController::Continue</c> is called, at which
        /// time the values are invalidated.
        /// </remarks>
        public int IsValid(out bool isValid) => InvokeGet(_this, This[0]->IsValid, out isValid);
    }
}
