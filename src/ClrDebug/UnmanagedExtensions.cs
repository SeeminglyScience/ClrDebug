using System.Runtime.CompilerServices;
using ClrDebug.Native;

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

        public static ref Primitive128 ToCalliArg(ref this COR_TYPEID type)
        {
            return ref Unsafe.As<COR_TYPEID, Primitive128>(ref type);
        }
    }
}
