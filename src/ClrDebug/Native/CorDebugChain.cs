using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a segment of a physical or logical call stack.
    /// </summary>
    public unsafe class CorDebugChain : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the physical thread this call chain is part of.
        /// </summary>
        public int GetThread(out CorDebugThread thread)
            => InvokeGetObject(_this, This[0]->GetThread, out thread);

        /// <summary>
        /// Gets the address range of the stack segment for this chain.
        /// </summary>
        /// <param name="start">The starting address of the stack segment.</param>
        /// <param name="end">The ending address of the stack segment.</param>
        public int GetStackRange(out ulong start, out ulong end)
        {
            ulong ref0 = default;
            ulong ref1 = default;

            int result = Calli(_this, This[0]->GetStackRange, &ref0, &ref1);
            start = ref0;
            end = ref1;
            return result;
        }

        [Obsolete("This method is not implemented in the current version of the .NET Framework.")]
        public int GetContext(out CorDebugObjectValue context)
            => InvokeGetObject(_this, This[0]->GetContext, out context);

        /// <summary>Gets the chain that called this chain.</summary>
        /// <param name="chain" />
        /// The caller chain. If this chain was spontaneously called (as would be the case if
        /// this chain or the debugger initialized the call stack), then <see paramref="chain" />
        /// will be <see langword="null" />.
        /// </param>
        /// <remarks>
        /// The calling chain may be on a different thread, if the call was marshaled
        /// across threads.
        /// </remarks>
        public int GetCaller(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetCaller, out chain);

        /// <summary>Gets the chain that was called by this chain.</summary>
        /// <param name="chain">
        /// The callee chain if this chain is not currently executing (that is, if this
        /// chain is waiting for a called chain to return), otherwise <see langword="null" />.
        /// </param>
        /// <remarks>
        /// This chain will wait for the called chain to return before it resumes
        /// execution. The called chain may be on another thread in the case of cross-thread
        /// marshaled calls.
        /// </remarks>
        public int GetCallee(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetCallee, out chain);

        /// <summary>
        /// Gets the previous chain of frames for the thread.
        /// </summary>
        /// <param name="chain">
        /// The previous chain if this chain is not the first, otherwise <see langword="null" />.
        /// </param>
        public int GetPrevious(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetPrevious, out chain);

        /// <summary>
        /// Gets the next chain of frames for the thread.
        /// </summary>
        /// <param name="chain">
        /// The next chain if this chain is not the last, otherwise <see langword="null" />.
        /// </param>
        public int GetNext(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetNext, out chain);

        /// <summary>
        /// Gets a value that indicates whether this chain is running managed code.
        /// </summary>
        public int IsManaged(out bool bManaged) => InvokeGet(_this, This[0]->IsManaged, out bManaged);

        /// <summary>
        /// Gets an enumerator that contains all the managed stack frames in the chain,
        /// starting with the most recent frame.
        /// </summary>
        /// <remarks>
        /// The chain represents the physical call stack for the thread.
        ///
        /// This method should be called only for managed chains. The debugging API does not
        /// provide methods for obtaining frames contained in unmanaged chains. The debugger
        /// must use other means to obtain this information.
        /// </remarks>
        public int EnumerateFrames(out CorDebugComEnum<CorDebugFrame> frames)
            => InvokeGetObject(_this, This[0]->EnumerateFrames, out frames);

        /// <summary>
        /// Gets the active (that is, most recent) frame on the chain.
        /// </summary>
        /// <remarks>
        /// If no managed stack frame is available, <see paramref="frame" /> is
        /// set to <see langword="null" />.
        ///
        /// If the active frame is not available, the call will succeed and
        /// <see paramref="frame" /> will be <see langword="null" />. Active frames will
        /// not be available for chains initiated due to <see cref="CorDebugChainReason.CHAIN_ENTER_UNMANAGED" />,
        /// and for some chains initiated due to <see cref="CorDebugChainReason.CHAIN_CLASS_INIT" />.
        /// </remarks>
        public int GetActiveFrame(out CorDebugFrame frame)
            => InvokeGetObject(_this, This[0]->GetActiveFrame, out frame);

        /// <summary>
        /// Gets the register set for the active part of this chain.
        /// </summary>
        public int GetRegisterSet(out CorDebugRegisterSet registers)
            => InvokeGetObject(_this, This[0]->GetRegisterSet, out registers);

        /// <summary>
        /// Gets the reason for the genesis of this calling chain.
        /// </summary>
        public int GetReason(out CorDebugChainReason reason)
        {
            int @ref = default;
            int result = Calli(_this, This[0]->GetReason, &@ref);
            reason = (CorDebugChainReason)@ref;
            return result;

        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

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
