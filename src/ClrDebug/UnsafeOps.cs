using System;
using System.Runtime.CompilerServices;

using ClrDebug.Native;
using static ClrDebug.Native.CalliInstructions;

namespace ClrDebug
{
    internal static class UnsafeOps
    {
        public static unsafe ReadOnlySpan<char> WCharToSpan(char* ptr)
        {
            if (ptr == default)
            {
                return default;
            }

            int i;
            for (i = 0; ptr[i] != '\0'; i++);
            if (i == 0)
            {
                return default;
            }

            return new ReadOnlySpan<char>(ptr, i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int InvokeGetObject<TUnknown>(void* @this, void* slot, out TUnknown value)
            where TUnknown : Unknown
        {
            void** pValue = default;
            int result = Calli(@this, slot, &pValue);
            value = ComFactory.Create<TUnknown>(pValue, result);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int InvokeGet(void* @this, void* slot, out int value)
        {
            int pValue = default;
            int result = Calli(@this, slot, &pValue);
            value = pValue;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int InvokeGet(void* @this, void* slot, out bool value)
        {
            int pValue = default;
            int result = Calli(@this, slot, &pValue);
            value = pValue.FromNativeBool();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int InvokeGet(void* @this, void* slot, out uint value)
        {
            uint pValue = default;
            int result = Calli(@this, slot, &pValue);
            value = pValue;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int InvokeGet(void* @this, void* slot, out IntPtr value)
        {
            IntPtr pValue = default;
            int result = Calli(@this, slot, &pValue);
            value = pValue;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int InvokeGet(void* @this, void* slot, out ulong value)
        {
            ulong pValue = default;
            int result = Calli(@this, slot, &pValue);
            value = pValue;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int InvokeGet(void* @this, void* slot, out long value)
        {
            long pValue = default;
            int result = Calli(@this, slot, &pValue);
            value = pValue;
            return result;
        }
    }
}
