namespace ClrDebug.Native
{
    /// <summary>
    /// Describes the amount of cached data that must be discarded
    /// based on changes to the process.
    /// </summary>
    /// <remarks>
    /// A member of the <c>CorDebugStateChange</c> enumeration is provided
    /// as an argument when the debugger calls the <c>ProcessStateChanged</c>
    /// method either with <c>ICorDebugProcess4::ProcessStateChanged</c> or
    /// <c>ICorDebugProcess6::ProcessStateChanged</c>.
    /// </remarks>
    public enum CorDebugStateChange : int
    {
        /// <summary>
        /// The process reached a new memory state via forward execution.
        /// </summary>
        PROCESS_RUNNING = 1,

        /// <summary>
        /// The process' memory may be arbitrarily different than it was previously.
        /// </summary>
        FLUSH_ALL = 2,
    }
}
