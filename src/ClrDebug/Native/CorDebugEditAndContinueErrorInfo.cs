using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    [Obsolete("This class is obsolete.", error: true)]
    public unsafe class CorDebugEditAndContinueErrorInfo : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        [Obsolete("This method is obsolete.", error: true)]
        public int GetModule(out CorDebugModule module) => InvokeGetObject(_this, This[0]->GetModule, out module);

        [Obsolete("This method is obsolete.", error: true)]
        public int GetToken(out uint token) => InvokeGet(_this, This[0]->GetToken, out token);

        [Obsolete("This method is obsolete.", error: true)]
        public int GetErrorCode(out long hr) => InvokeGet(_this, This[0]->GetErrorCode, out hr);

        [Obsolete("This method is obsolete.", error: true)]
        public int GetString(Span<char> szString, out uint charsUsed)
        {
            fixed (void* pCharsUsed = &charsUsed)
            fixed (void* pSzString = szString)
            {
                return Calli(_this, This[0]->GetString, (uint)szString.Length, pCharsUsed, pSzString);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetModule;

            public void* GetToken;

            public void* GetErrorCode;

            public void* GetString;
        }
    }
}
