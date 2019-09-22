namespace ClrDebug.Native
{
    /// <summary>
    /// Specifies the state of a thread for debugging.
    /// </summary>
    /// <remarks>
    /// The debugger uses this enumeration to control a thread's
    /// execution. The state of a thread can be set by using the
    /// <see cref="CorDebugThread.SetDebugState(CorDebugThreadState)" /> or
    /// <see cref="CorDebugController.SetAllThreadsDebugState(CorDebugThreadState, CorDebugThread)" />
    /// method.
    ///
    /// A callback provided to the hosting API enables message
    /// pumping, so an interrupted state is not needed.
    /// </remarks>
    public enum CorDebugThreadState : int
    {
        /// <summary>The thread runs freely, unless a debug event occurs.</summary>
        THREAD_RUN = 0,

        /// <summary>The thread cannot run.</summary>
        THREAD_SUSPEND = 1,
    }
}
