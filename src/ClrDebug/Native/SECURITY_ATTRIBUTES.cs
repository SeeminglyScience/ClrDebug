using System;
using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SECURITY_ATTRIBUTES
    {
        public uint nLength;

        public void* lpSecurityDescriptor;

        public int bInheritHandle;
    }
}
