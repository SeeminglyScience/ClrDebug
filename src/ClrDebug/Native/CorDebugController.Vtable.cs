using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    public unsafe partial class CorDebugController
    {
        [StructLayout(LayoutKind.Sequential)]
        internal new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* Stop;

            public void* Continue;

            public void* IsRunning;

            public void* HasQueuedCallbacks;

            public void* EnumerateThreads;

            public void* SetAllThreadsDebugState;

            public void* Detach;

            public void* ExitCode;

            public void* CanCommitChanges;

            public void* CommitChanges;
        }
    }
}
