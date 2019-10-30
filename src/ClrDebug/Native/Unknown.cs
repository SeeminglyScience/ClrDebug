using System;
using System.ComponentModel;

using static ClrDebug.Native.CalliInstructions;

namespace ClrDebug.Native
{
    public partial class Unknown : IUnknown, IDisposable
    {
        protected unsafe void** _this;

        private bool _isDisposed;

        private protected Unknown()
        {
        }

        private unsafe Vtable** This => (Vtable**)_this;

        ~Unknown() => Dispose(false);

        public unsafe bool IsDefault => _this == default;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public unsafe int QueryInterface(Guid* riid, void** ppvObject)
            => Calli(_this, This[0]->QueryInterface, riid, ppvObject);

        public unsafe int AddRef() => Calli(_this, This[0]->AddRef);

        public unsafe int Release() => Calli(_this, This[0]->Release);

        public unsafe bool TryGetInterface<T>(in Guid riid, out T comObject) where T : Unknown
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

        internal unsafe void SetPointer(void** ptr)
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

        internal unsafe void** DangerousGetPointer()
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

        protected unsafe void** AssertNotDisposed(void** ptr)
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

        protected virtual unsafe void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
            }

            Vtable** vtablePtr = This;
            _this = default;
            if (vtablePtr != default)
            {
                Calli(vtablePtr, vtablePtr[0]->Release);
            }

            _isDisposed = true;
        }
    }
}
