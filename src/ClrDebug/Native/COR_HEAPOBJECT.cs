using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides information about an object on the managed heap.
    /// </summary>
    /// <remarks>
    /// <c>COR_HEAPOBJECT</c> instances can be retrieved by enumerating
    /// an <c>ICorDebugHeapEnum</c> interface object that is populated by
    /// calling the <c>ICorDebugProcess5::EnumerateHeap</c> method.
    ///
    /// A <c>COR_HEAPOBJECT</c> instance provides information either about
    /// a live object on the managed heap, or about an object that is not
    /// rooted by any object but has not yet been collected by the garbage
    /// collector.
    ///
    /// For better performance, the <c>COR_HEAPOBJECT.address</c> field is a
    /// <c>CORDB_ADDRESS</c> value rather than the <c>ICorDebugValue</c> interface
    /// value used in much of the debugging API. To obtain an <c>ICorDebugValue</c>
    /// object for a given object address, you can pass the <c>CORDB_ADDRESS</c> value
    /// to the <c>ICorDebugProcess5::GetObject</c> method.
    ///
    /// For better performance, the <c>COR_HEAPOBJECT.type</c> field is a <c>COR_TYPEID</c>
    /// value rather than the <c>ICorDebugType</c> interface value used in much
    /// of the debugging API. To obtain an <c>ICorDebugType</c> object for a given
    /// type ID, you can pass the <c>COR_TYPEID</c> value to the
    /// <c>ICorDebugProcess5::GetTypeForTypeID</c> method.
    ///
    /// The <c>COR_HEAPOBJECT</c> structure includes a reference-counted COM interface.
    /// If you retrieve a COR_HEAPOBJECT instance from the enumerator by calling
    /// the <c>ICorDebugHeapEnum::Next</c> method, you must subsequently release
    /// the reference.
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_HEAPOBJECT
    {
        /// <summary>
        /// The address of the object in memory.
        /// </summary>
        public ulong address;

        /// <summary>
        /// The total size of the object, in bytes.
        /// </summary>
        public ulong size;

        /// <summary>
        /// The type of the object.
        /// </summary>
        public COR_TYPEID type;
    }
}
