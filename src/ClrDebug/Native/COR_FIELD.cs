using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides information about a field in an object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_FIELD
    {
        /// <summary>An <c>mdFieldDef</c> token that can be used to get field information.</summary>
        public int token;

        /// <summary>The offset, in bytes, to the field data in the object.</summary>
        public uint offset;

        /// <summary>A <c>COR_TYPEID</c> value that identifies the type of this field.</summary>
        public COR_TYPEID id;

        /// <summary>A <c>CorElementType</c> enumeration value that indicates the type of the field.</summary>
        public CorElementType fieldType;
    }
}
