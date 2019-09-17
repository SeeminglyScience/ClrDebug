using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugEditAndContinueErrorInfo : Unknown
    {
        private ICorDebugEditAndContinueErrorInfoVtable** This => (ICorDebugEditAndContinueErrorInfoVtable**)DangerousGetPointer();

        public int GetModule(out CorDebugModule module) => InvokeGetObject(_this, This[0]->GetModule, out module);

        public int GetToken(out uint token) => InvokeGet(_this, This[0]->GetToken, out token);

        public int GetErrorCode(out long hr) => InvokeGet(_this, This[0]->GetErrorCode, out hr);

        public int GetString(ref Span<char> szString, out uint charsUsed)
        {
            fixed (void* pCharsUsed = &charsUsed)
            fixed (void* pSzString = szString)
            {
                return Calli(_this, This[0]->GetString, (uint)szString.Length, pCharsUsed, pSzString);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugEditAndContinueErrorInfoVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetModule;

            public void* GetToken;

            public void* GetErrorCode;

            public void* GetString;
        }
    }
}
