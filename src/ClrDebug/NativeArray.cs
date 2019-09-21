using System;
using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    internal static class NativeArray
    {
        public static NativeComArray<T> AllocCom<T>(ReadOnlySpan<T> managedArray) where T : IComReference, IUnknown
        {
            return NativeComArray<T>.Alloc(managedArray);
        }
    }

    internal readonly unsafe ref struct NativeComArray<T> where T : IComReference, IUnknown
    {
        public readonly void*** Pointer;

        private readonly ReadOnlySpan<T> _managed;

        private NativeComArray(void*** array, ReadOnlySpan<T> managed)
        {
            Pointer = array;
            _managed = managed;
        }

        public static implicit operator void*(NativeComArray<T> nativeArray) => nativeArray.Pointer;

        public static explicit operator void**(NativeComArray<T> nativeArray) => (void**)nativeArray.Pointer;

        public static implicit operator void***(NativeComArray<T> nativeArray) => nativeArray.Pointer;

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

        public static NativeComArray<T> Alloc(ReadOnlySpan<T> managedArray)
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

                return new NativeComArray<T>(ptr, managedArray);
            }
            catch
            {
                Marshal.FreeHGlobal(handle);
                throw;
            }
        }
    }
}
