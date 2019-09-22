using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Contains the offsets that are used to map Microsoft intermediate
    /// language (MSIL) code to native code.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_DEBUG_IL_TO_NATIVE_MAP
    {
        /// <summary>The offset of the MSIL code.</summary>
        public uint ilOffset;

        /// <summary>The offset of the start of the native code.</summary>
        public uint nativeStartOffset;

        /// <summary>The offset of the end of the native code.</summary>
        public uint nativeEndOffset;
    }
}
