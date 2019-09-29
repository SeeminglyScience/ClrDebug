using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides information about the layout of an object in memory.
    /// </summary>
    /// <remarks>
    /// If <c>numFields</c> is greater than zero, you can call the
    /// <c>ICorDebugProcess5::GetTypeFields</c> method to obtain
    /// information about the fields in this type. If type is <c>ELEMENT_TYPE_STRING</c>,
    /// <c>ELEMENT_TYPE_ARRAY</c>, or <c>ELEMENT_TYPE_SZARRAY</c>, the
    /// size of objects of this type is variable, and you can pass the
    /// <c>COR_TYPEID</c> structure to the <c>ICorDebugProcess5::GetArrayLayout</c>
    /// method.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_TYPE_LAYOUT
    {
        /// <summary>
        /// The identifier of the parent type to this type. This will
        /// be the <c>NULL</c> type id (token1= 0, token2 = 0) if the
        /// type id corresponds to <c>System.Object</c>.
        /// </summary>
        public COR_TYPEID parentID;

        /// <summary>
        /// The base size of an object of this type. This is the total
        /// size for non-variable sized objects.
        /// </summary>
        public uint objectSize;

        /// <summary>
        /// The number of fields included in objects of this type.
        /// </summary>
        public uint numFields;

        /// <summary>
        /// If this type is boxed, the beginning offset of an object's
        /// fields. This field is valid only for value types such as
        /// primitives and structures.
        /// </summary>
        public uint boxOffset;

        /// <summary>
        /// The <c>CorElementType</c> to which this type belongs.
        /// </summary>
        public CorElementType type;
    }
}
