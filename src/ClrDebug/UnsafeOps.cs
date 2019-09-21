using System;
using System.Runtime.CompilerServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug
{
    public static class UnsafeOps
    {
        internal static unsafe ReadOnlySpan<char> WCharToSpan(char* ptr)
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
        internal static unsafe int InvokeGetObject<T>(void* @this, void* slot, out T value) where T : IComReference, new()
        {
            void** pValue = default;
            int result = Calli(@this, slot, &pValue);
            value = default;
            if (result == 0 && pValue != null)
            {
                value = new T();
                value.SetPointer(pValue);
            }

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
