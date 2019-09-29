using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Contains information about an object that is to be garbage-collected.
    /// </summary>
    /// <remarks>
    /// The type field is a <c>CorGCReferenceType</c> enumeration value that
    /// indicates where the reference came from. A particular <c>COR_GC_REFERENCE</c>
    /// value can reflect any of the following kinds of managed objects:
    ///
    /// - Objects from all managed stacks (<c>CorGCReferenceType.CorReferenceStack</c>).
    ///   This includes live references in managed code, as well as objects
    ///   created by the common language runtime.
    ///
    /// - Objects from the handle table (<c>CorGCReferenceType.CorHandle*</c>).
    ///   This includes strong references (<c>HNDTYPE_STRONG</c> and <c>HNDTYPE_REFCOUNT</c>)
    ///   and static variables in a module.
    ///
    /// - Objects from the finalizer queue (<c>CorGCReferenceType.CorReferenceFinalizer</c>).
    ///   The finalizer queue roots objects until the finalizer has run.
    ///
    /// The extraData field contains extra data depending on the source (or type)
    /// of the reference. Possible values are:
    ///
    /// - <c>DependentSource</c>. If the type is <c>CorGCREferenceType.CorHandleStrongDependent</c>,
    ///   this field is the object that, if alive, roots the object to be garbage-collected at
    ///   <c>COR_GC_REFERENCE.Location</c>.
    ///
    /// - <c>RefCount</c>. If the type is <c>CorGCREferenceType.CorHandleStrongRefCount</c>,
    ///   this field is the reference count of the handle.
    ///
    /// - <c>Size</c>. If the type is <c>CorGCREferenceType.CorHandleStrongSizedByref</c>,
    ///   this field is the last size of the object tree for which the garbage collector
    ///   calculated the object roots. Note that this calculation is not necessarily up to date.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_GC_REFERENCE
    {
        private unsafe void** domain;

        private unsafe void** location;

        public CorGCReferenceType type;

        public ulong extraData;

        public unsafe CorDebugAppDomain GetDomain() => ComFactory.Create<CorDebugAppDomain>(domain);

        public unsafe CorDebugValue GetLocation() => ComFactory.Create<CorDebugValue>(location);
    }
}
