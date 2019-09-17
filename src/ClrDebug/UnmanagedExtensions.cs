using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ClrDebug
{
    internal static class UnmanagedExtensions
    {
        public static bool FromNativeBool(this int value) => value != 0;

        public static int ToNativeInt(this bool value) => value ? 1 : 0;

        public static void MaybeThrowHr(this int hr)
        {
            if (hr == 0 || (hr >> 31) == 0)
            {
                return;
            }

            throw new CorDebugException(hr);
        }
    }
}
