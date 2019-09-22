using System;
using ClrDebug.Native;

namespace ClrDebug
{
    internal static class ComFactory
    {
        public unsafe static void Create<T>(void* ptr, int hResult, out T value) where T : IComReference, new()
        {
            value = Create<T>(ptr, hResult);
        }

        public unsafe static void Create<T>(void* ptr, out T value) where T : IComReference, new()
        {
            value = Create<T>(ptr);
        }

        public unsafe static T Create<T>(void* ptr, int hResult) where T : IComReference, new()
        {
            if (hResult != HResult.S_OK)
            {
                return default;
            }

            return Create<T>(ptr);
        }

        public unsafe static T Create<T>(void* ptr) where T : IComReference, new()
        {
            if (ptr == null)
            {
                return default;
            }

            var comObject = new T();
            comObject.SetPointer((void**)ptr);
            return comObject;
        }
    }
}
