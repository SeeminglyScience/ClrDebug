using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class Unknown : IUnknown, IComReference, IDisposable
    {
        protected void** _this;

        private bool _isDisposed;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Unknown()
        {
        }

        ~Unknown() => Dispose(false);

        public bool IsDefault => _this == default;

        private unsafe IUnknownVtable** This => (IUnknownVtable**)_this;

        public unsafe void** DangerousGetPointer()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(null);
            }

            if (_this == default)
            {
                throw new ArgumentNullException(nameof(_this));
            }

            return _this;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public int QueryInterface(Guid* riid, void** ppvObject)
            => Calli(_this, This[0]->QueryInterface, riid, ppvObject);

        public int AddRef() => Calli(_this, This[0]->AddRef);

        public int Release() => Calli(_this, This[0]->Release);

        public bool TryGetInterface<T>(in Guid riid, out T comObject) where T : IComReference, new()
        {
            void* ppvObject;
            fixed (Guid* pRiid = &riid)
            {
                int result = QueryInterface(pRiid, &ppvObject);
                if (result == HResult.E_NOINTERFACE)
                {
                    comObject = default;
                    return false;
                }
            }

            comObject = ComFactory.Create<T>(ppvObject);
            return true;
        }

        void IComReference.SetPointer(void** ptr)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(null);
            }

            if (_this != null)
            {
                throw new InvalidOperationException();
            }

            _this = ptr;
            Calli(_this, This[0]->AddRef);
        }

        internal ComPointer AcquirePointer()
        {
            return new ComPointer(this);
        }

        protected void** AssertNotDisposed(void** ptr)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(null);
            }

            if (ptr == default)
            {
                throw new ArgumentNullException(nameof(_this));
            }

            return _this;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
            }

            IUnknownVtable** vtablePtr = This;
            _this = default;
            if (vtablePtr != default)
            {
                Calli(vtablePtr, vtablePtr[0]->Release);
            }

            _isDisposed = true;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct IUnknownVtable
    {
        public void* QueryInterface;

        public void* AddRef;

        public void* Release;
    }
}
