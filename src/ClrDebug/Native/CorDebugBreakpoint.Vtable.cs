using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    public unsafe partial class CorDebugBreakpoint
    {
        [StructLayout(LayoutKind.Sequential)]

        internal new unsafe struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* Activate;

            public void* IsActivate;
        }
    }
}
