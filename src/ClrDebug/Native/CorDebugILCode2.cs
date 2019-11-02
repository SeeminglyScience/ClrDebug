using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Logically extends the ICorDebugILCode interface to provide methods that
    /// return the token for a function's local variable signature, and that map
    /// a profiler's instrumented intermediate language (IL) offsets to original
    /// method IL offsets.
    ///
    /// [Supported in the .NET Framework 4.5.2 and later versions]
    /// </summary>
    public unsafe class CorDebugILCode2 : Unknown
    {
        internal CorDebugILCode2()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Returns a map from profiler-instrumented intermediate language (IL)
        /// offsets to original method IL offsets for this instance.
        ///
        /// [Supported in the .NET Framework 4.5.2 and later versions]
        /// </summary>
        /// <param name="map">
        /// An array of <c>COR_IL_MAP</c> values that provide information on mappings
        /// from profiler-instrumented IL to the IL of the original method.
        /// </param>
        /// <param name="amountWritten">
        /// The number of <c>COR_IL_MAP</c> values written to the map array.
        /// </param>
        /// <remarks>
        /// If the profiler sets the mapping by calling the <c>ICorProfilerInfo::SetILInstrumentedCodeMap</c>
        /// method, the debugger can call this method to retrieve the mapping and to
        /// use the mapping internally when calculating IL offsets for stack traces and
        /// variable lifetimes.
        ///
        /// If <see paramref="map" /> is empty and <see paramref="pcMap" /> is non-null,
        /// <see paramref="pcMap" /> is set to the number of available <c>COR_IL_MAP</c> values.
        /// When the method returns, <see paramref="pcMap" /> is set to the number of
        /// <c>COR_IL_MAP</c> values actually written to the <see paramref="map" /> array.
        ///
        /// If the IL hasn't been instrumented or the mapping wasn't provided by a
        /// profiler, this method returns <c>S_OK</c> and sets <see paramref="pcMap" /> to 0.
        /// </remarks>
        public int GetInstrumentedILMap(Span<COR_IL_MAP> map, out int amountWritten)
        {
            fixed (void* pMap = map)
            fixed (void* pAmountWritten = &amountWritten)
            {
                return Calli(_this, This[0]->GetInstrumentedILMap, map.Length, pAmountWritten, pMap);
            }
        }

        /// <summary>
        /// Gets the metadata token for the local variable signature for the
        /// function that is represented by this instance.
        ///
        /// [Supported in the .NET Framework 4.5.2 and later versions]
        /// </summary>
        /// <param name="mdSig">
        /// A pointer to the <c>mdSignature</c> token for the local variable
        /// signature for this function, or <c>mdSignatureNil</c> if there is
        /// no signature (that is, if the function doesn't have any local variables).
        /// </param>
        public int GetLocalVarSigToken(out int mdSig)
            => InvokeGet(_this, This[0]->GetLocalVarSigToken, out mdSig);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetLocalVarSigToken;

            public void* GetInstrumentedILMap;
        }
    }
}
