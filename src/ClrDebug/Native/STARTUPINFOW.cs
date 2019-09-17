using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct STARTUPINFOW
    {
        public ulong cb;

        public char* lpReserved;

        public char* lpDesktop;

        public char* lpTitle;

        public ulong dwX;

        public ulong dwY;

        public ulong dwXSize;

        public ulong dwYSize;

        public ulong dwXCountChars;

        public ulong dwYCountChars;

        public ulong dwFillAttribute;

        public ulong dwFlags;

        public ushort wShowWindow;

        public ushort cbReserved2;

        public void* lpReserved2;

        public void* hStdInput;

        public void* hStdOutput;

        public void* hStdError;
    }
}
