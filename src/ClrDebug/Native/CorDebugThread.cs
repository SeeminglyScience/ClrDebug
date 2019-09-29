using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a thread in a process. The lifetime of an <see cref="CorDebugThread" />
    /// instance is the same as the lifetime of the thread it represents.
    /// </summary>
    public unsafe class CorDebugThread : Unknown
    {
        private ICorDebugThreadVtable** This => (ICorDebugThreadVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the process of which this thread forms a part.
        /// </summary>
        public int GetProcess(out CorDebugProcess process) => InvokeGetObject(_this, This[0]->GetProcess, out process);

        /// <summary>
        /// Gets the current operating system identifier of
        /// the active part of this thread.
        /// </summary>
        /// <remarks>
        /// The operating system identifier can potentially
        /// change during execution of a process, and can be
        /// a different value for different parts of the thread.
        /// </remarks>
        public int GetID(out uint dwThreadId) => InvokeGet(_this, This[0]->GetID, out dwThreadId);

        /// <summary>
        /// Gets the current handle for the active part of this thread.
        /// </summary>
        /// <remarks>
        /// The handle may change as the process executes, and may
        /// be different for different parts of the thread.
        ///
        /// This handle is owned by the debugging API. The debugger
        /// should duplicate it before using it.
        /// </remarks>
        public int GetHandle(out IntPtr hThreadHandle) => InvokeGet(_this, This[0]->GetHandle, out hThreadHandle);

        /// <summary>
        /// Gets the application domain in which this thread is currently executing.
        /// </summary>
        public int GetAppDomain(out CorDebugAppDomain appDomain) => InvokeGetObject(_this, This[0]->GetAppDomain, out appDomain);

        /// <summary>
        /// Sets flags that describe the debugging state of this thread.
        /// </summary>
        /// <param name="state">
        /// The new debugging state for this thread.
        /// </param>
        /// <remarks>
        /// This method sets the current debug state of the thread. (The
        /// "current debug state" represents the debug state if the process
        /// were to be continued, not the actual current state.) The normal
        /// value for this is <see cref="CorDebugThreadState.THREAD_RUN" />.
        /// Only the debugger can affect the debug state of a thread. Debug
        /// states do last across continues, so if you want to keep a thread
        /// in <see cref="CorDebugThreadState.THREAD_SUSPEND" /> over multiple
        /// continues, you can set it once and thereafter not have to worry
        /// about it. Suspending threads and resuming the process can cause
        /// deadlocks, though it's usually unlikely. This is an intrinsic
        /// quality of threads and processes and is by-design. A debugger can
        /// asynchronously break and resume the threads to break the deadlock.
        /// If the thread's user state includes <see cref="CorDebugUserState.USER_UNSAFE_POINT" />,
        /// then the thread may block a garbage collection (GC). This means
        /// the suspended thread has a much higher chance of causing a
        /// deadlock. This may not affect debug events already queued.
        /// Thus a debugger should drain the entire event queue (by calling
        /// <see cref="CorDebugController.HasQueuedCallbacks(CorDebugThread, out bool)" /> before
        /// suspending or resuming threads. Else it may get events on a
        /// thread that it believes it has already suspended.
        /// </remarks>
        public int SetDebugState(CorDebugThreadState state) => Calli(_this, This[0]->SetDebugState, (int)state);

        /// <summary>
        /// Gets the current debug state of this thread.
        /// </summary>
        /// <remarks>
        /// If the process is currently stopped, <see paramref="state" />
        /// represents the debug state that would exist for this thread
        /// if the process were to be continued, not the actual current
        /// state of this thread.
        /// </remarks>
        public int GetDebugState(out CorDebugThreadState state)
        {
            int pState = default;
            int result = Calli(_this, This[0]->GetDebugState, &pState);
            state = (CorDebugThreadState)pState;
            return result;
        }

        /// <summary>
        /// Gets the current user state of this thread.
        /// </summary>
        /// <remarks>
        /// The user state of the thread is the state of the thread
        /// when it is examined by the program that is being debugged.
        /// A thread may have multiple state bits set.
        /// </remarks>
        public int GetUserState(out CorDebugUserState state)
        {
            int pState = default;
            int result = Calli(_this, This[0]->GetUserState, &pState);
            state = (CorDebugUserState)pState;
            return result;
        }

        /// <summary>
        /// Gets the exception that is currently being thrown by
        /// managed code.
        /// </summary>
        /// <remarks>
        /// The exception object will exist from the time the exception
        /// is thrown until the end of the catch block. A function
        /// evaluation, which is performed by the <see cref="CorDebugEval" />
        /// methods, will clear out the exception object on setup and
        /// restore it on completion.
        ///
        /// Exceptions can be nested (for example, if an exception is
        /// thrown in a filter or in a function evaluation), so there
        /// may be multiple outstanding exceptions on a single thread.
        /// This method returns the most current exception.
        ///
        /// The exception object and type may change throughout the
        /// life of the exception. For example, after an exception of
        /// type x is thrown, the common language runtime (CLR) may
        /// run out of memory and promote it to an out-of-memory
        /// exception.
        /// </remarks>
        public int GetCurrentException(out CorDebugValue exceptionObject)
            => InvokeGetObject(_this, This[0]->GetCurrentException, out exceptionObject);

        [Obsolete("This method is not implemented. Do not use it.", error: true)]
        public int ClearCurrentException() => Calli(_this, This[0]->ClearCurrentException);

        /// <summary>
        /// Creates an stepper that allows stepping through the active
        /// frame of this thread.
        /// </summary>
        /// <remarks>
        /// The active frame may be unmanaged code.
        ///
        /// The stepper must be used to perform the actual stepping.
        /// </remarks>
        public int CreateStepper(out CorDebugStepper stepper) => InvokeGetObject(_this, This[0]->CreateStepper, out stepper);

        /// <summary>
        /// Gets all of the stack chains in this thread.
        /// </summary>
        /// <param name="chains">
        /// An enumerable containing all of the stack chains in this
        /// thread, starting at the active (most recent) chain.
        /// </param>
        /// <returns></returns>
        /// <remarks>
        /// The stack chain represents the physical call stack for
        /// the thread. The following circumstances create a stack
        /// chain boundary:
        ///
        /// - A managed-to-unmanaged or unmanaged-to-managed transition.
        /// - A context switch.
        /// - A debugger hijacking of a user thread.
        ///
        /// In the simple case for a thread that is running purely
        /// managed code in a single context, a one-to-one correspondence
        /// will exist between threads and stack chains.
        ///
        /// A debugger may want to rearrange the physical call stacks of
        /// all threads into logical call stacks. This would involve sorting
        /// all the threads' chains by their caller/callee relationships and
        /// regrouping them.
        /// </remarks>
        public int EnumerateChains(out CorDebugComEnum<CorDebugChain> chains)
            => InvokeGetObject(_this, This[0]->EnumerateChains, out chains);

        /// <summary>
        /// Gets the active (most recent) stack chain on this thread.
        /// </summary>
        /// <remarks>
        /// <see paramref="chain" /> is <see langword="null" /> if no stack
        /// chain is currently active.
        /// </remarks>
        public int GetActiveChain(out CorDebugChain chain) => InvokeGetObject(_this, This[0]->GetActiveChain, out chain);

        /// <summary>
        /// Gets the active (most recent) frame on this thread.
        /// </summary>
        /// <remarks>
        /// <see paramref="frame" /> is <see langword="null" /> if no frame
        /// is currently active.
        /// </remarks>
        public int GetActiveFrame(out CorDebugFrame frame) => InvokeGetObject(_this, This[0]->GetActiveFrame, out frame);

        /// <summary>
        /// Gets the register set that is associated with the
        /// active part of this thread.
        /// </summary>
        public int GetRegisterSet(out CorDebugRegisterSet registers) => InvokeGetObject(_this, This[0]->GetRegisterSet, out registers);

        /// <summary>
        /// Creates an eval that collects and exposes the functionality
        /// of this thread.
        /// </summary>
        /// <remarks>
        /// The evaluation object will push a new chain on the thread
        /// before doing its computation. This interrupts the computation
        /// currently being performed on the thread until the evaluation
        /// completes.
        /// </remarks>
        public int CreateEval(out CorDebugEval eval) => InvokeGetObject(_this, This[0]->CreateEval, out eval);

        /// <summary>
        /// Gets a <see cref="CorDebugValue" /> representing the common
        /// language runtime (CLR) thread.
        /// </summary>
        public int GetObject(out CorDebugValue @object) => InvokeGetObject(_this, This[0]->GetObject, out @object);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugThreadVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetProcess;

            public void* GetID;

            public void* GetHandle;

            public void* GetAppDomain;

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
