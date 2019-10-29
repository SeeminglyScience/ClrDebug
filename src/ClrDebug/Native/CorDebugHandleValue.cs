using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// A subclass of ICorDebugReferenceValue that represents a reference
    /// value to which the debugger has created a handle for garbage collection.
    /// </summary>
    /// <remarks>
    /// An <c>ICorDebugReferenceValue</c> object is invalidated by a break in the
    /// execution of debugged code. An <c>ICorDebugHandleValue</c> maintains its
    /// reference through breaks and continuations, until it is explicitly released.
    ///
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugHandleValue : CorDebugReferenceValue
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Releases the handle referenced by this <c>ICorDebugHandleValue</c> object
        /// without explicitly releasing the interface pointer.
        ///
        /// This method is defined as <c>Dispose</c> in cordebug.h, but has been changed
        /// as it conflicts with <see cref="IDisposable.Dispose" />.
        /// </summary>
        public int CorDispose() => Calli(_this, This[0]->Dispose);

        /// <summary>
        /// Gets a value that indicates the kind of handle referenced by this
        /// <c>ICorDebugHandleValue</c> object.
        /// </summary>
        /// <param name="type">
        /// A pointer to a value of the <c>CorDebugHandleType</c> enumeration
        /// that indicates the type of this handle.
        /// </param>
        public int GetHandleType(out CorDebugHandleType type)
        {
            fixed (void* pType = &type)
            {
                return Calli(_this, This[0]->GetHandleType, pType);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public CorDebugReferenceValue.Vtable ICorDebugReferenceValue;

            public void* Dispose;

            public void* GetHandleType;
        }
    }
}
