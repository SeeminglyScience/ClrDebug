using System;

namespace ClrDebug.Native
{
    [Flags]
    public enum CorDebugChainReason : int
    {
        /// <summary>No call chain has been initiated.</summary>
        CHAIN_NONE = 0,

        /// <summary>The chain was initiated by a constructor.</summary>
        CHAIN_CLASS_INIT = 1 << 0,

        /// <summary>The chain was initiated by an exception filter.</summary>
        CHAIN_EXCEPTION_FILTER = 1 << 1,

        /// <summary>The chain was initiated by code that enforces security.</summary>
        CHAIN_SECURITY = 1 << 2,

        /// <summary>The chain was initiated by a context policy.</summary>
        CHAIN_CONTEXT_POLICY = 1 << 3,

        /// <summary>Not used.</summary>
        CHAIN_INTERCEPTION = 1 << 4,

        /// <summary>Not used.</summary>
        CHAIN_PROCESS_START = 1 << 5,

        /// <summary>The chain was initiated by the start of a thread execution.</summary>
        CHAIN_THREAD_START = 1 << 6,

        /// <summary>The chain was initiated by entry into managed code.</summary>
        CHAIN_ENTER_MANAGED = 1 << 7,

        /// <summary>The chain was initiated by entry into unmanaged code.</summary>
        CHAIN_ENTER_UNMANAGED = 1 << 8,

        /// <summary>Not used.</summary>
        CHAIN_DEBUGGER_EVAL = 1 << 9,

        /// <summary>Not used.</summary>
        CHAIN_CONTEXT_SWITCH = 1 << 10,

        /// <summary>The chain was initiated by a function evaluation.</summary>
        CHAIN_FUNC_EVAL = 1 << 11,
    }
}
