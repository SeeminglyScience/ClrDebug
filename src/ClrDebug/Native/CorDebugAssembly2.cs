using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents an assembly. This interface is an extension of
    /// the <c>ICorDebugAssembly</c> interface.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugAssembly2 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a value that indicates whether the assembly has been granted
        /// full trust by the runtime security system.
        /// </summary>
        /// <param name="bFullyTrusted">
        /// <c>true</c> if the assembly has been granted full trust by the runtime
        /// security system; otherwise, <c>false</c>.
        /// </param>
        /// <remarks>
        /// This method returns an HRESULT of <c>CORDBG_E_NOTREADY</c> if the security
        /// policy for the assembly has not yet been resolved, that is, if no code in
        /// the assembly has been run yet.
        /// </remarks>
        public int IsFullyTrusted(out bool bFullyTrusted)
        {
            fixed (void* pbFullyTrusted = &bFullyTrusted)
            {
                return Calli(_this, This[0]->IsFullyTrusted, pbFullyTrusted);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* IsFullyTrusted;
        }
    }
}
