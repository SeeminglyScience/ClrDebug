using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_DEBUG_IL_TO_NATIVE_MAP
    {
        public uint ilOffset;

        public uint nativeStartOffset;

        public uint nativeEndOffset;
    }
}
