using System;

namespace ClrDebug.Native
{
    [Flags]
    public enum CorDebugChainReason : int
    {
        CHAIN_NONE = 0,

        CHAIN_CLASS_INIT = 1 << 0,

        CHAIN_EXCEPTION_FILTER = 1 << 1,

        CHAIN_SECURITY = 1 << 2,

        CHAIN_CONTEXT_POLICY = 1 << 3,

        CHAIN_INTERCEPTION = 1 << 4,

        CHAIN_PROCESS_START = 1 << 5,

        CHAIN_THREAD_START = 1 << 6,

        CHAIN_ENTER_MANAGED = 1 << 7,

        CHAIN_ENTER_UNMANAGED = 1 << 8,

        CHAIN_DEBUGGER_EVAL = 1 << 9,

        CHAIN_CONTEXT_SWITCH = 1 << 10,

        CHAIN_FUNC_EVAL = 1 << 11,
    }
}
