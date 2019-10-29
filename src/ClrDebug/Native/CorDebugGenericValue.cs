using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// A subclass of <c>ICorDebugValue</c> that applies to all values.
    /// This interface provides Get and Set methods for the value.
    /// </summary>
    /// <remarks>
    /// <c>ICorDebugGenericValue</c> is a sub-interface because it is non-remotable.
    ///
    /// For reference types, the value is the reference rather than the contents
    /// of the reference.
    ///
    /// This interface does not support being called remotely, either cross-machine
    /// or cross-process.
    ///
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugGenericValue : CorDebugValue
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Copies the value of this generic into the specified buffer.
        /// </summary>
        /// <param name="to">
        /// A value that is represented by this <c>ICorDebugGenericValue</c> object.
        /// The value may be a simple type or a reference type (that is, a pointer).
        /// </param>
        public int GetValue(void* to)
        {
            return Calli(_this, This[0]->GetValue, to);
        }

        /// <summary>
        /// Copies a new value from the specified buffer.
        /// </summary>
        /// <param name="from">
        /// A pointer to the buffer from which to copy the value.
        /// </param>
        /// <remarks>
        /// For reference types, the value is the reference, not the content.
        /// </remarks>
        public int SetValue(void* from)
        {
            return Calli(_this, This[0]->SetValue, from);
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public CorDebugValue.Vtable ICorDebugValue;

            public void* GetValue;

            public void* SetValue;
        }
    }
}
