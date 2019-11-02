using System;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a scope, either a process or an AppDomain, in which code execution
    /// context can be controlled.
    /// </summary>
    /// <remarks>
    /// If this instance is controlling a process, the scope includes all threads of
    /// the process. If this instance is controlling an application domain, the scope
    /// includes only the threads of that particular application domain.
    /// </remarks>
    public unsafe partial class CorDebugController : Unknown
    {
        internal CorDebugController()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Performs a cooperative stop on all threads that are running managed code in the process.
        /// </summary>
        /// <param name="dwTimeoutIgnored">Not used.</param>
        /// <remarks>
        /// Stop performs a cooperative stop on all threads running managed code in the process.
        /// During a managed-only debugging session, unmanaged threads may continue to run (but
        /// will be blocked when trying to call managed code). During an interop debugging session,
        /// unmanaged threads will also be stopped. The <see paramref="dwTimeoutIgnored" /> value is currently ignored
        /// and treated as <c>INFINITE</c> (-1). If the cooperative stop fails due to a deadlock, all
        /// threads are suspended and <c>E_TIMEOUT</c> is returned.
        /// </remarks>
        public int Stop(int dwTimeoutIgnored) => Calli(_this, This[0]->Stop, dwTimeoutIgnored);

        /// <summary>
        /// Resumes execution of managed threads after a call to <see cref="Stop(int)" />.
        /// </summary>
        /// <param name="fIsOutOfBand">
        /// Indicates whether continuing from an out-of-band event.
        /// </param>
        /// <remarks>
        /// <see cref="Continue(bool)" /> continues the process after a call to the
        /// <see cref="Stop(int)" /> method.
        ///
        /// When doing mixed-mode debugging, do not call <see cref="Continue(bool)" /> on
        /// the Win32 event thread unless you are continuing from an out-of-band event.
        ///
        /// An in-band event is either a managed event or a normal unmanaged event during
        /// which the debugger supports interaction with the managed state of the process.
        /// In this case, the debugger receives the ICorDebugUnmanagedCallback::DebugEvent
        /// callback with its <see paramref="fOutOfBand" /> parameter set to <see langword="false" />.
        ///
        /// An out-of-band event is an unmanaged event during which interaction with the
        /// managed state of the process is impossible while the process is stopped due to
        /// the event. In this case, the debugger receives the ICorDebugUnmanagedCallback::DebugEvent
        /// callback with its <see paramref="fOutOfBand" /> parameter set to <see langword="true" />.
        /// </remarks>
        public int Continue(bool fIsOutOfBand) => Calli(_this, This[0]->Continue, fIsOutOfBand.ToNativeInt());

        /// <summary>
        /// Resumes execution of managed threads after a call to <see cref="Stop(int)" />.
        /// </summary>
        public void Continue() => Calli(_this, This[0]->Continue, false.ToNativeInt()).MaybeThrowHr();

        /// <summary>
        /// Gets a value that indicates whether the threads in the process are currently running freely.
        /// </summary>
        public int IsRunning(out bool bRunning) => InvokeGet(_this, This[0]->IsRunning, out bRunning);

        /// <summary>
        /// Gets a value that indicates whether any managed callbacks are currently queued for
        /// the specified thread.
        /// </summary>
        /// <param name="thread">Specifies the thread to query.</param>
        /// <param name="bQueuedRef">
        /// A value indicating whether any managed callbacks are currently queued for the specified
        /// thread (or for any thread if <see paramref="thread" /> is <see langword="null" />).
        /// </param>
        /// <remarks>
        /// Callbacks will be dispatched one at a time, each time <see cref="Continue(bool)" /> is
        /// called. The debugger can check this flag if it wants to report multiple debugging
        /// events that occur simultaneously.
        ///
        /// When debugging events are queued, they have already occurred, so the debugger must
        /// drain the entire queue to be sure of the state of the debuggee. (Call
        /// <see cref="Continue(bool)" /> to drain the queue.) For example, if the queue contains
        /// two debugging events on thread X, and the debugger suspends thread X after the first
        /// debugging event and then calls <see cref="Continue(bool)" />, the second debugging
        /// event for thread X will be dispatched although the thread has been suspended.
        /// </remarks>
        public int HasQueuedCallbacks(CorDebugThread thread, out bool bQueuedRef)
        {
            using var pThread = thread?.AcquirePointer();
            int pbQueuedRef = default;
            int result = Calli(_this, This[0]->HasQueuedCallbacks, (void*)pThread, &pbQueuedRef);
            bQueuedRef = pbQueuedRef.FromNativeBool();
            return result;
        }

        /// <summary>
        /// Gets an enumerator for the active managed threads in the process.
        /// </summary>
        /// <remarks>
        /// A thread is considered active after the <c>ICorDebugManagedCallback::CreateThread</c>
        /// callback has been dispatched and before the <c>ICorDebugManagedCallback::ExitThread</c>
        /// callback has been dispatched. A managed thread may not necessarily have any managed
        /// frames on its stack. Threads can be enumerated even before the <c>ICorDebugManagedCallback::CreateProcess</c>
        /// callback. The enumeration will naturally be empty.
        /// </remarks>
        public int EnumerateThreads(out CorDebugComEnum<CorDebugThread> threads)
            => InvokeGetObject(_this, This[0]->EnumerateThreads, out threads);

        /// <summary>
        /// Sets the debug state of all managed threads in the process.
        /// </summary>
        /// <param name="state">
        /// A value of the that specifies the target state of the thread for debugging.
        /// </param>
        /// <param name="exceptThisThread">
        /// The thread that should be exempted from the debug state setting. If this value
        /// is <see langword="null" />, no thread is exempted.
        /// </param>
        /// <remarks>
        /// This method may affect threads that are not visible via <see cref="EnumerateThreads(out CorDebugEnum{CorDebugThread})" />,
        /// so threads that were suspended with this method will need to be resumed.
        /// </remarks>
        public int SetAllThreadsDebugState(CorDebugThreadState state, CorDebugThread exceptThisThread)
        {
            using var pExceptThisThread = exceptThisThread?.AcquirePointer();
            return Calli(_this, This[0]->SetAllThreadsDebugState, (int)state, pExceptThisThread);
        }

        /// <summary>
        /// Detaches the debugger from the process or application domain.
        /// </summary>
        /// <remarks>
        /// The process or application domain continues execution normally, but the <see cref="CorDebugProcess" />
        /// or <see cref="ICorDebugAppDomain" /> object is no longer valid and no further callbacks will occur.
        ///
        /// In the .NET Framework version 2.0, if unmanaged debugging is enabled, this method will
        /// fail due to operating system limitations.
        /// </remarks>
        public int Detach() => Calli(_this, This[0]->Detach);

        [Obsolete("This method is obsolete.", error: true)]
        public int CanCommitChanges(
            uint cSnapshots,
            in ReadOnlySpan<CorDebugEditAndContinueSnapshot> snapshots,
            out CorDebugComEnum<CorDebugEditAndContinueErrorInfo> error)
        {
            void* pError = default;
            using var pSnapshots = NativeArray.AllocCom(snapshots);
            int result = Calli(_this, This[0]->CanCommitChanges, cSnapshots, pSnapshots, &pError);
            error = ComFactory.Create<CorDebugComEnum<CorDebugEditAndContinueErrorInfo>>(pError);
            return result;
        }

        [Obsolete("This method is obsolete.", error: true)]
        public int CommitChanges(
            uint cSnapshots,
            in ReadOnlySpan<CorDebugEditAndContinueSnapshot> snapshots,
            out CorDebugComEnum<CorDebugEditAndContinueErrorInfo> error)
        {
            void* pError = default;
            using var pSnapshots = NativeArray.AllocCom(snapshots);
            int result = Calli(_this, This[0]->CommitChanges, cSnapshots, pSnapshots, &pError);
            error = ComFactory.Create<CorDebugComEnum<CorDebugEditAndContinueErrorInfo>>((void**)pError);
            return result;
        }
    }
}
