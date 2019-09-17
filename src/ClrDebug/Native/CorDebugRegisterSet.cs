using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugRegisterSet : Unknown
    {
        private ICorDebugRegisterSetVtable** This => (ICorDebugRegisterSetVtable**)DangerousGetPointer();

        public int GetRegistersAvailable(out ulong available)
            => InvokeGet(_this, This[0]->GetRegistersAvailable, out available);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugRegisterSetVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetRegistersAvailable;
        }
    }
}
