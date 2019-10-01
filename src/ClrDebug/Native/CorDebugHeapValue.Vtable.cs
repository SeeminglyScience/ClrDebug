using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    public unsafe partial class CorDebugHeapValue
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Vtable
        {
            public CorDebugValue.Vtable ICorDebugValue;

            public void* IsValid;

            public void* CreateRelocBreakpoint;
        }
    }
}
