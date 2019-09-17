using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugChain : Unknown
    {
        private ICorDebugChainVtable** This => (ICorDebugChainVtable**)DangerousGetPointer();

        public int GetThread(out CorDebugThread thread)
            => InvokeGetObject(_this, This[0]->GetThread, out thread);

        public int GetStackRange(out ulong start, out ulong end)
        {
            ulong ref0 = default;
            ulong ref1 = default;

            int result = Calli(_this, This[0]->GetStackRange, &ref0, &ref1);
            start = ref0;
            end = ref1;
            return result;
        }

        public int GetContext(out CorDebugObjectValue context)
            => InvokeGetObject(_this, This[0]->GetContext, out context);

        public int GetCaller(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetCaller, out chain);

        public int GetCallee(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetCallee, out chain);

        public int GetPrevious(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetPrevious, out chain);

        public int GetNext(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetNext, out chain);

        public int IsManaged(out bool bManaged) => InvokeGet(_this, This[0]->IsManaged, out bManaged);

        public int EnumerateFrames(out CorDebugEnum<CorDebugFrame> frames)
            => InvokeGetObject(_this, This[0]->EnumerateFrames, out frames);

        public int GetActiveFrame(out CorDebugFrame frame)
            => InvokeGetObject(_this, This[0]->GetActiveFrame, out frame);

        public int GetRegisterSet(out CorDebugRegisterSet registers)
            => InvokeGetObject(_this, This[0]->GetRegisterSet, out registers);

        public int GetReason(out CorDebugChainReason reason)
        {
            int @ref = default;
            int result = Calli(_this, This[0]->GetReason, &@ref);
            reason = (CorDebugChainReason)@ref;
            return result;

        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugChainVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetThread;

            public void* GetStackRange;

            public void* GetContext;

            public void* GetCaller;

            public void* GetCallee;

            public void* GetPrevious;

            public void* GetNext;

            public void* IsManaged;

            public void* EnumerateFrames;

            public void* GetActiveFrame;

            public void* GetRegisterSet;

            public void* GetReason;
        }
    }
}
