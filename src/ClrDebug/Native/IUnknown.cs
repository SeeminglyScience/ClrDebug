using System;

namespace ClrDebug.Native
{
    public interface IUnknown
    {
        unsafe int QueryInterface(Guid riid, void** ppvObject);

        int AddRef();

        int Release();
    }
}
