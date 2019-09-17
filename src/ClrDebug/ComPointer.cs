using System;
using ClrDebug.Native;

namespace ClrDebug
{
    internal readonly struct ComPointer : IDisposable
    {
        private readonly unsafe void** _ptr;

        private readonly Unknown _managedObject;

        public unsafe ComPointer(Unknown managedObject)
        {
            _managedObject = managedObject;
            _ptr = managedObject.DangerousGetPointer();
            managedObject.AddRef();
        }

        public static unsafe implicit operator void*(ComPointer? comPointer)
            => comPointer == null ? default : comPointer.Value._ptr;

        public static unsafe implicit operator void**(ComPointer comPointer) => comPointer._ptr;

        public static unsafe implicit operator void*(ComPointer comPointer) => comPointer._ptr;

        public void Dispose()
        {
            _managedObject.Release();
        }
    }
}
