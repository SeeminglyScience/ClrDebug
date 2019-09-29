using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Contains a type identifier.
    /// </summary>
    /// <remarks>
    /// This structure is returned by a number of debugging methods
    /// that provide information about objects to be garbage-collected.
    /// It can then be passed as an argument to other debugging methods
    /// that provide additional information about that item. For example,
    /// by enumerating an <c>ICorDebugHeapEnum</c> object, you can retrieve
    /// individual <see cref="COR_HEAPOBJECT" /> objects that represent
    /// individual objects on the managed heap. You can then pass the
    /// <see cref="COR_TYPEID" /> value from the <see cref="COR_HEAPOBJECT.type" />
    /// field to the <c>ICorDebugProcess5::GetTypeForTypeID</c> method to
    /// retrieve an <see cref="CorDebugType" /> object that provides type
    /// information about the object.
    ///
    /// This object is intended to be opaque. Its individual fields should
    /// not be accessed or manipulated. Its sole use is as an identifier
    /// that is provided as an out parameter in a method call and that can,
    /// in turn, be passed to other methods to provide additional information.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_TYPEID
    {
        /// <summary>
        /// The first token.
        /// </summary>
        public ulong token1;

        /// <summary>
        /// The second token.
        /// </summary>
        public ulong token2;
    }
}
