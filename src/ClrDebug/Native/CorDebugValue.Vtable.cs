using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    public unsafe partial class CorDebugValue
    {
        [StructLayout(LayoutKind.Sequential)]
        internal new unsafe struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public new void* GetType;

            public void* GetSize;

            public void* GetAddress;

            public void* CreateBreakpoint;
        }
    }
}
