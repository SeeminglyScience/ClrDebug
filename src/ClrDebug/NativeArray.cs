using System;
using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    internal static class NativeArray
    {
        public static NativeComArray<TUnknown> AllocCom<TUnknown>(ReadOnlySpan<TUnknown> managedArray)
            where TUnknown : Unknown
        {
            return NativeComArray<TUnknown>.Alloc(managedArray);
        }
    }

    internal readonly unsafe ref struct NativeComArray<TUnknown> where TUnknown : Unknown
    {
        public readonly void*** Pointer;

        private readonly ReadOnlySpan<TUnknown> _managed;

        private NativeComArray(void*** array, ReadOnlySpan<TUnknown> managed)
        {
            Pointer = array;
            _managed = managed;
        }

        public static implicit operator void*(NativeComArray<TUnknown> nativeArray) => nativeArray.Pointer;

        public static explicit operator void**(NativeComArray<TUnknown> nativeArray) => (void**)nativeArray.Pointer;

        public static implicit operator void***(NativeComArray<TUnknown> nativeArray) => nativeArray.Pointer;

        public void Dispose()
        {
            if (Pointer == null)
            {
                return;
            }

            Marshal.FreeHGlobal(new IntPtr(Pointer));
            for (int i = 0; i < _managed.Length; i++)
            {
                _managed[i].Release();
            }
        }

        public static NativeComArray<TUnknown> Alloc(ReadOnlySpan<TUnknown> managedArray)
        {
            if (managedArray.IsEmpty)
            {
                return default;
            }

            var handle = Marshal.AllocHGlobal(sizeof(IntPtr) * managedArray.Length);
            try
            {
                var ptr = (void***)handle.ToPointer();
                for (int i = 0; i < managedArray.Length; i++)
                {
                    var managedObj = managedArray[i];
                    managedObj.AddRef();
                    ptr[i] = managedArray[i].DangerousGetPointer();
                }

                return new NativeComArray<TUnknown>(ptr, managedArray);
            }
            catch
            {
                Marshal.FreeHGlobal(handle);
                throw;
            }
        }
    }
}
