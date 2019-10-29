using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides methods that manage a value that is a reference to an
    /// object. (That is, this interface provides methods that manage
    /// a pointer.)
    /// </summary>
    /// <remarks>
    /// The common language runtime (CLR) may do a garbage collection
    /// on objects when the debugged process is continued. The garbage
    /// collection may move objects around in memory. An <see cref="CorDebugReferenceValue" />
    /// will either cooperate with the garbage collection so that its
    /// information is updated after the garbage collection, or it will
    /// be invalidated implicitly before the garbage collection.
    ///
    /// The <see cref="CorDebugReferenceValue" /> object may be implicitly
    /// invalidated after the debugged process has been continued. The
    /// derived <c>ICorDebugHandleValue</c> is not invalidated until it
    /// is explicitly released or exposed.
    /// </remarks>
    public unsafe class CorDebugReferenceValue : CorDebugValue
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a value that indicates whether this instance is a <c>null</c>
        /// value, in which case the this does not point to an object.
        /// </summary>
        public int IsNull(out bool bNull) => InvokeGet(_this, This[0]->IsNull, out bNull);

        /// <summary>
        /// Gets the current memory address of the referenced object.
        /// </summary>
        public int GetValue(out ulong pValue) => InvokeGet(_this, This[0]->GetValue, out pValue);

        /// <summary>
        /// Sets the specified memory address. That is, this method sets
        /// this instance to point to an object.
        /// </summary>
        public int SetValue(ulong value) => Calli(_this, This[0]->SetValue, value);

        /// <summary>
        /// Gets the object that is referenced.
        /// </summary>
        /// <remarks>
        /// The value object is valid only while its reference has not
        /// yet been disabled.
        /// </remarks>
        public int Dereference(out CorDebugValue pValue) => InvokeGetObject(_this, This[0]->Dereference, out pValue);

        [Obsolete("DereferenceStrong is not implemented. Do not call this method.", error: true)]
        public int DereferenceStrong(out CorDebugValue pValue) => InvokeGetObject(_this, This[0]->DereferenceStrong, out pValue);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public CorDebugValue.Vtable ICorDebugValue;

            public void* IsNull;

            public void* GetValue;

            public void* SetValue;

            public void* Dereference;

            public void* DereferenceStrong;
        }
    }
}
