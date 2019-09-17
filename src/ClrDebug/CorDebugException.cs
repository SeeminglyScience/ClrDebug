using System;
using ClrDebug.Native;

namespace ClrDebug
{
    public sealed class CorDebugException : Exception
    {
        private (string Id, string Message) _resource;

        internal CorDebugException(int hresult)
        {
            HResult = hresult;
        }

        public string ErrorId => GetResourceInfo().Id;

        public override string Message => GetResourceInfo().Message;

        public static Exception TryCreateForHR(int hr)
        {
            if (hr == 0 || (hr >> 31) == 0)
            {
                return null;
            }

            return new CorDebugException(hr);
        }

        private (string Id, string Message) GetResourceInfo()
        {
            if (_resource.Id != null)
            {
                return _resource;
            }

            if (CorError.s_errorMap.TryGetValue(HResult, out _resource))
            {
                return _resource;
            }

            return _resource = (CorError.NoResourceIdMessage, CorError.NoResourceStringMessage);
        }
    }
}
