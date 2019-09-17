using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;
using System;

namespace ClrDebug.Native
{
    public unsafe class CorDebugFrame : Unknown
    {
        private ICorDebugFrameVtable** This => (ICorDebugFrameVtable**)DangerousGetPointer();

        public int GetChain(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetChain, out chain);

        public int GetCode(out CorDebugCode code)
            => InvokeGetObject(_this, This[0]->GetCode, out code);

        public int GetFunction(out CorDebugFunction function)
            => InvokeGetObject(_this, This[0]->GetFunction, out function);

        public int GetStackRange(out ulong start, out ulong end)
        {
            ulong ref0 = default;
            ulong ref1 = default;

            int result = Calli(_this, This[0]->GetStackRange, &ref0, &ref1);
            start = ref0;
            end = ref1;
            return result;
        }

        public int GetCaller(out CorDebugFrame frame)
            => InvokeGetObject(_this, This[0]->GetCaller, out frame);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugFrameVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetChain;

            public void* GetCode;

            public void* GetFunction;

            public void* GetStackRange;

            public void* GetCaller;
        }
    }
}
