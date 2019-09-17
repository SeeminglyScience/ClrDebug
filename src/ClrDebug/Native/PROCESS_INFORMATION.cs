using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct PROCESS_INFORMATION
    {
        public void* hProcess;

        public void* hThread;

        public ulong dwProcessId;

        public ulong dwThreadId;
    }
}
