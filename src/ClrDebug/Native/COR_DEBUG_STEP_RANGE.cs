using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_DEBUG_STEP_RANGE
    {
        public uint startOffset;

        public uint endOffset;
    }
}
