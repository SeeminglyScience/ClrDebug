using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugStepper : Unknown
    {
        private ICorDebugStepperVtable** This => (ICorDebugStepperVtable**)DangerousGetPointer();

        public int IsActive(out int bActive) => InvokeGet(_this, This[0]->IsActive, out bActive);

        public int Deactivate() => Calli(_this, This[0]->Deactivate);

        public int SetInterceptMask(CorDebugIntercept mask) => Calli(_this, This[0]->SetInterceptMask, (int)mask);

        public int SetUnmappedStopMask(CorDebugUnmappedStop mask) => Calli(_this, This[0]->SetUnmappedStopMask, (int)mask);

        public int Step(bool bStepIn) => Calli(_this, This[0]->Step, bStepIn.ToNativeInt());

        public int StepRange(bool bStepIn, ReadOnlySpan<COR_DEBUG_STEP_RANGE> ranges, uint cRangeCount)
        {
            fixed (void* pRanges = ranges)
            {
                return Calli(_this, This[0]->StepRange, bStepIn.ToNativeInt(), pRanges, cRangeCount);
            }
        }

        public int StepOut() => Calli(_this, This[0]->StepOut);

        public int SetRangeIL(int bIL) => Calli(_this, This[0]->SetRangeIL, bIL);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugStepperVtable
        {
            public IUnknownVtable IUnknown;

            public void* IsActive;

            public void* Deactivate;

            public void* SetInterceptMask;

            public void* SetUnmappedStopMask;

            public void* Step;

            public void* StepRange;

            public void* StepOut;

            public void* SetRangeIL;
        }
    }
}
