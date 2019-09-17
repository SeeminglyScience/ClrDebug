using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugController : Unknown
    {
        private ICorDebugControllerVtable** This => (ICorDebugControllerVtable**)DangerousGetPointer();

        public int Stop(ulong dwTimeoutIgnored) => Calli(_this, This[0]->Stop, dwTimeoutIgnored);

        public int Continue(bool fIsOutOfBand) => Calli(_this, This[0]->Continue, fIsOutOfBand.ToNativeInt());

        public int IsRunning(out bool bRunning) => InvokeGet(_this, This[0]->IsRunning, out bRunning);

        public int HasQueuedCallbacks(CorDebugThread thread, out bool bQueuedRef)
        {
            using var pThread = thread.AquirePointer();
            int pbQueuedRef = default;
            int result = Calli(_this, This[0]->HasQueuedCallbacks, (void*)pThread, &pbQueuedRef);
            bQueuedRef = pbQueuedRef.FromNativeBool();
            return result;
        }

        public int EnumerateThreads(out CorDebugEnum<CorDebugThread> threads)
            => InvokeGetObject(_this, This[0]->EnumerateThreads, out threads);

        public int SetAllThreadsDebugState(CorDebugThreadState state, CorDebugThread exceptThisThread)
        {
            using var pExceptThisThread = exceptThisThread?.AquirePointer();
            return Calli(_this, This[0]->SetAllThreadsDebugState, (int)state, pExceptThisThread);
        }

        public int Detach() => Calli(_this, This[0]->Detach);

        public int ExitCode(uint exitCode) => Calli(_this, This[0]->ExitCode, exitCode);

        public int CanCommitChanges(
            uint cSnapshots,
            in ReadOnlySpan<CorDebugEditAndContinueSnapshot> snapshots,
            out CorDebugEnum<CorDebugEditAndContinueErrorInfo> error)
        {
            void* pError = default;
            using var pSnapshots = NativeArray.Alloc(snapshots);
            int result = Calli(_this, This[0]->CanCommitChanges, cSnapshots, pSnapshots, &pError);
            error = ComFactory.Create<CorDebugEnum<CorDebugEditAndContinueErrorInfo>>((void**)pError);
            return result;
        }

        public int CommitChanges(
            uint cSnapshots,
            in ReadOnlySpan<CorDebugEditAndContinueSnapshot> snapshots,
            out CorDebugEnum<CorDebugEditAndContinueErrorInfo> error)
        {
            void* pError = default;
            using var pSnapshots = NativeArray.Alloc(snapshots);
            int result = Calli(_this, This[0]->CommitChanges, cSnapshots, pSnapshots, &pError);
            error = ComFactory.Create<CorDebugEnum<CorDebugEditAndContinueErrorInfo>>((void**)pError);
            return result;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ICorDebugControllerVtable
    {
        public IUnknownVtable IUnknown;

        public void* Stop;

        public void* Continue;

        public void* IsRunning;

        public void* HasQueuedCallbacks;

        public void* EnumerateThreads;

        public void* SetAllThreadsDebugState;

        public void* Detach;

        public void* ExitCode;

        public void* CanCommitChanges;

        public void* CommitChanges;
    }
}
