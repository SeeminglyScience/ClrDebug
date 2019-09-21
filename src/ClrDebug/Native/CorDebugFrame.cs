using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

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

        public int GetFunctionToken(out int token)
            => InvokeGet(_this, This[0]->GetFunctionToken, out token);

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

        public int GetCallee(out CorDebugFrame frame)
            => InvokeGetObject(_this, This[0]->GetCallee, out frame);

        public int CreateStepper(out CorDebugStepper stepper)
            => InvokeGetObject(_this, This[0]->CreateStepper, out stepper);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugFrameVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetChain;

            public void* GetCode;

            public void* GetFunction;

            public void* GetFunctionToken;

            public void* GetStackRange;

            public void* GetCaller;

            public void* GetCallee;

            public void* CreateStepper;
        }
    }
}
