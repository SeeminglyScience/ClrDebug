using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugThread : Unknown
    {
        private ICorDebugThreadVtable** This => (ICorDebugThreadVtable**)DangerousGetPointer();

        public int GetProcess(out CorDebugProcess process) => InvokeGetObject(_this, This[0]->GetProcess, out process);

        public int GetID(out uint dwThreadId) => InvokeGet(_this, This[0]->GetID, out dwThreadId);

        public int GetHandle(out IntPtr hThreadHandle) => InvokeGet(_this, This[0]->GetHandle, out hThreadHandle);

        public int SetDebugState(CorDebugThreadState state) => Calli(_this, This[0]->SetDebugState, (int)state);

        public int GetDebugState(out CorDebugThreadState state)
        {
            int pState = default;
            int result = Calli(_this, This[0]->GetDebugState, &pState);
            state = (CorDebugThreadState)pState;
            return result;
        }

        public int GetUserState(out CorDebugUserState state)
        {
            int pState = default;
            int result = Calli(_this, This[0]->GetUserState, &pState);
            state = (CorDebugUserState)pState;
            return result;
        }

        public int GetCurrentException(out CorDebugValue exceptionObject)
            => InvokeGetObject(_this, This[0]->GetCurrentException, out exceptionObject);

        public int ClearCurrentException() => Calli(_this, This[0]->ClearCurrentException);

        public int CreateStepper(out CorDebugStepper stepper) => InvokeGetObject(_this, This[0]->CreateStepper, out stepper);

        public int EnumerateChains(out CorDebugEnum<CorDebugChain> chains) => InvokeGetObject(_this, This[0]->EnumerateChains, out chains);

        public int GetActiveChain(out CorDebugChain chain) => InvokeGetObject(_this, This[0]->GetActiveChain, out chain);

        public int GetActiveFrame(out CorDebugFrame frame) => InvokeGetObject(_this, This[0]->GetActiveFrame, out frame);

        public int GetRegisterSet(out CorDebugRegisterSet registers) => InvokeGetObject(_this, This[0]->GetRegisterSet, out registers);

        public int CreateEval(out CorDebugEval eval) => InvokeGetObject(_this, This[0]->CreateEval, out eval);

        public int GetObject(out CorDebugValue @object) => InvokeGetObject(_this, This[0]->GetObject, out @object);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugThreadVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetProcess;

            public void* GetID;

            public void* GetHandle;

            public void* SetDebugState;

            public void* GetDebugState;

            public void* GetUserState;

            public void* GetCurrentException;

            public void* ClearCurrentException;

            public void* CreateStepper;

            public void* EnumerateChains;

            public void* GetActiveChain;

            public void* GetActiveFrame;

            public void* GetRegisterSet;

            public void* CreateEval;

            public void* GetObject;
        }
    }
}
