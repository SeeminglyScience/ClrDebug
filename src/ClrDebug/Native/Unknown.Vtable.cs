using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    public unsafe partial class Unknown
    {
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Vtable
        {
            public void* QueryInterface;

            public void* AddRef;

            public void* Release;
        }
    }
}
