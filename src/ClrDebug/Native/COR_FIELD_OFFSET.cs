using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Stores the offset, within a class, of the specified field.
    /// </summary>
    /// <remarks>
    /// <c>IMetaDataImport::GetClassLayout</c> and <c>IMetaDataEmit::SetClassLayout</c>
    /// methods take a parameter of type <c>COR_FIELD_OFFSET</c>.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_FIELD_OFFSET
    {
        /// <summary>An <c>mdFieldDef</c> metadata token that represents the field.</summary>
        public int ridOfField;

        /// <summary>The field's offset within its class.</summary>
        public int ulOffset;
    }
}
