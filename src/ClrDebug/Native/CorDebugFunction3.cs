using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Logically extends the ICorDebugFunction interface to provide
    /// access to code from a ReJIT request.
    ///
    /// [Supported in the .NET Framework 4.5.2 and later versions]
    /// </summary>
    public unsafe class CorDebugFunction3 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets an interface pointer to an <c>ICorDebugILCode</c> that contains
        /// the IL from an active ReJIT request.
        ///
        /// [Supported in the .NET Framework 4.5.2 and later versions]
        /// </summary>
        /// <param name="reJitedILCode">
        /// A pointer to the IL from an active ReJIT request.
        /// </param>
        /// <remarks>
        /// If the method represented by this <c>ICorDebugFunction3</c> object
        /// has an active ReJIT request, <c>reJitedILCode</c> returns a
        /// pointer to its IL. If there is no active request, which is a common
        /// case, then <c>reJitedILCode</c> is null.
        ///
        /// A ReJIT request becomes active just after execution returns from
        /// the <c>ICorProfilerCallback4::GetReJITParameters</c> method call.
        /// It may not yet be JIT-compiled, and threads may still be executing
        /// in the original version of the code. A ReJIT request becomes inactive
        /// during the profiler's call to the <c>ICorProfilerInfo4::RequestRevert</c>
        /// method. Even after the IL is reverted, a thread can still be executing
        /// in the JIT-recompiled (ReJIT) code.
        /// </remarks>
        public int GetActiveReJitRequestILCode(out CorDebugILCode reJitedILCode)
            => InvokeGetObject(_this, This[0]->GetActiveReJitRequestILCode, out reJitedILCode);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetActiveReJitRequestILCode;
        }
    }
}
