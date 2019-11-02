using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides a method that enables a debugger to enumerate the local
    /// variables and arguments in a function.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugCode4 : Unknown
    {
        internal CorDebugCode4()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets an enumerator to the local variables and arguments in
        /// a function.
        /// </summary>
        /// <param name="enum">
        /// A pointer to the address of an <c>ICorDebugVariableHomeEnum</c>
        /// interface object that is an enumerator for the local variables
        /// and arguments in a function.
        /// </param>
        /// <remarks>
        /// The <c>ICorDebugVariableHomeEnum</c> interface object is a standard
        /// enumerator derived from the <c>ICorDebugEnum</c> interface that allows
        /// you to enumerate <c>ICorDebugVariableHome</c> objects. The collection
        /// may include multiple <c>ICorDebugVariableHome</c> objects for the same
        /// slot or argument index if they have different homes at different points
        /// in the function.
        /// </remarks>
        public int EnumerateVariableHomes(out CorDebugComEnum<CorDebugVariableHome> @enum)
            => InvokeGetObject(_this, This[0]->EnumerateVariableHomes, out @enum);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* EnumerateVariableHomes;
        }
    }
}
