using System;

namespace ClrDebug.Native
{
    [Flags]
    public enum CorDebugUserState
    {
        USER_STOP_REQUESTED = 1,

        USER_SUSPEND_REQUESTED = 1 << 1,

        USER_BACKGROUND = 1 << 2,

        USER_UNSTARTED = 1 << 3,

        USER_STOPPED = 1 << 4,

        USER_WAIT_SLEEP_JOIN = 1 << 5,

        USER_SUSPENDED = 1 << 6,

        USER_UNSAFE_POINT = 1 << 7,

        USER_THREADPOOL = 1 << 8,
    }
}
