namespace ClrDebug.Native
{
    /// <summary>
    /// Identifies the source of an object to be garbage-collected.
    /// </summary>
    /// <remarks>
    /// The <c>CorGCReferenceType</c> enumeration is used as follows:
    ///
    /// As the value of the type field of the <c>COR_GC_REFERENCE</c>
    /// structure, it indicates the source of a reference or handle.
    /// As the types argument to the <c>ICorDebugProcess5::EnumerateHandles</c>
    /// method, it specifies the types of handles to include in the enumeration.
    /// </remarks>
    public enum CorGCReferenceType : uint
    {
        /// <summary>A handle to a strong reference from the object handle table.</summary>
        CorHandleStrong = 1 << 0,

        /// <summary>A handle to a pinned strong reference from the object handle table.</summary>
        CorHandleStrongPinning = 1 << 1,

        /// <summary>A handle to a weak reference from the object handle table.</summary>
        CorHandleWeakShort = 1 << 2,

        /// <summary>A handle to a weak reference-counted object from the object handle table.</summary>
        CorHandleWeakRefCount = 1 << 3,

        /// <summary>A handle to a reference-counted object from the object handle table.</summary>
        CorHandleStrongRefCount = 1 << 5,

        /// <summary>A handle to a dependent object from the object handle table.</summary>
        CorHandleStrongDependent = 1 << 6,

        /// <summary>An asynchronous pinned object from the object handle table.</summary>
        CorHandleStrongAsyncPinned = 1 << 7,

        /// <summary>A strong handle that keeps an approximate size of the collective closure of all objects and object roots at garbage collection time.</summary>
        CorHandleStrongSizedByref = 1 << 8,

        /// <summary>A reference from the managed stack.</summary>
        CorReferenceStack = 0x80000001,

        /// <summary>A reference from the finalizer queue.</summary>
        CorReferenceFinalizer = 0x80000002,

        /// <summary>Return only strong references from the handle table. This value is used by the ICorDebugProcess5::EnumerateHandles method only.</summary>
        CorHandleStrongOnly = 0x1E3,

        /// <summary>Return only weak references from the handle table. This value is used by the ICorDebugProcess5::EnumerateHandles method only.</summary>
        CorHandleWeakOnly = 0xC,

        /// <summary>Return all references from the handle table. This value is used by the ICorDebugProcess5::EnumerateHandles method only.</summary>
        CorHandleAll = 0x7FFFFFFF
    }
}
