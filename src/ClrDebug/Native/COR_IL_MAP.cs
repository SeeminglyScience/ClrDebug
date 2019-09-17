using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_IL_MAP
    {
        public uint oldOffset;

        public uint newOffset;

        public bool fAccurate;
    }
}
