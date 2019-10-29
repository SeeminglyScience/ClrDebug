using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EnumHandle
    {
        private unsafe void* _handle;

        public static EnumHandle Default => default;

        public unsafe bool IsNil => _handle == null;

        internal unsafe void* ToPointer() => _handle;
    }
}
