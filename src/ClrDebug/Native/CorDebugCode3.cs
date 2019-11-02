using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides a method that extends <c>ICorDebugCode</c> and <c>ICorDebugCode2</c>
    /// to provide information about a managed return value.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugCode3 : Unknown
    {
        internal CorDebugCode3()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// For a specified IL offset, gets the native offsets where a
        /// breakpoint should be placed so that the debugger can obtain
        /// the return value from a function.
        /// </summary>
        /// <param name="ilOffset">
        /// The IL offset. It must be a function call site or the function
        /// call will fail.
        /// </param>
        /// <param name="offsets">
        /// An array of native offsets. Typically, <c>pOffsets</c> contains
        /// a single offset, although a single IL instruction can map to
        /// multiple map to multiple <c>CALL</c> assembly instructions.
        /// </param>
        /// <param name="amountWritten">
        /// A pointer to the number of offsets actually returned. Usually,
        /// its value is 1, but a single IL instruction can map to multiple
        /// <c>CALL</c> assembly instructions.
        /// </param>
        /// <remarks>
        /// This method is used along with the <c>ICorDebugILFrame3::GetReturnValueForILOffset</c>
        /// method to get the return value of a method that returns a reference
        /// type. Passing an IL offset to a function call site to this method
        /// returns one or more native offsets. The debugger can then set
        /// breakpoints on these native offsets in the function. When the
        /// debugger hits one of the breakpoints, you can then pass the same
        /// IL offset that you passed to this method to the <c>ICorDebugILFrame3::GetReturnValueForILOffset</c>
        /// method to get the return value. The debugger should then clear
        /// all the breakpoints that it set.
        ///
        /// WARNING:
        ///
        /// The <c>ICorDebugCode3::GetReturnValueLiveOffset</c> and <c>ICorDebugILFrame3::GetReturnValueForILOffset</c>
        /// methods allow you to get return value information for reference
        /// types only. Retrieving return value information from value types
        /// (that is, all types that derive from <c>System.ValueType</c>) is
        /// not supported.
        ///
        /// The function returns the following <c>HRESULT</c> values.
        ///
        /// - <c>S_OK</c>: Success.
        ///
        /// - <c>CORDBG_E_INVALID_OPCODE</c>: The given IL offset site is not
        ///   a call instruction, or the function returns <c>void</c>.
        ///
        /// - <c>CORDBG_E_UNSUPPORTED</c>: The given IL offset is a proper call,
        ///   but the return type is unsupported for getting a return value.
        ///
        /// The <c>ICorDebugCode3::GetReturnValueLiveOffset</c> method is available
        /// only on x86-based and AMD64 systems.
        /// </remarks>
        public int GetReturnValueLiveOffset(uint ilOffset, Span<uint> offsets, out int amountWritten)
        {
            fixed (void* pOffsets = offsets)
            fixed (void* pAmountWritten = &amountWritten)
            {
                return Calli(
                    _this,
                    This[0]->GetReturnValueLiveOffset,
                    ilOffset,
                    offsets.Length,
                    pAmountWritten,
                    pOffsets);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetReturnValueLiveOffset;
        }
    }
}
