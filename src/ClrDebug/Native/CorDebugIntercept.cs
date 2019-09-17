using System;

namespace ClrDebug.Native
{
    [Flags]
    public enum CorDebugIntercept : int
    {
        INTERCEPT_NONE = 0,

        INTERCEPT_CLASS_INIT = 1 << 0,

        INTERCEPT_EXCEPTION_FILTER = 1 << 1,

        INTERCEPT_SECURITY = 1 << 2,

        INTERCEPT_CONTEXT_POLICY = 1 << 3,

        INTERCEPT_INTERCEPTION = 1 << 4,

        INTERCEPT_ALL = 0xFFFF
    }
}
