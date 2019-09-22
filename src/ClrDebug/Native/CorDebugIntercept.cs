using System;

namespace ClrDebug.Native
{
    [Flags]
    /// <summary>
    /// Describes the various other methods that may be called when stepping
    /// into a method. These are refererred to as interceptors. They are all
    /// invoked with frames of various types.
    /// </summary>
    public enum CorDebugIntercept : int
    {
        /// <summary>No interceptor was called.</summary>
        INTERCEPT_NONE = 0,

        /// <summary>Indicates that class initialization code is being run.</summary>
        INTERCEPT_CLASS_INIT = 1 << 0,

        /// <summary>Indicates that an exception was thrown and filter code is being run.</summary>
        INTERCEPT_EXCEPTION_FILTER = 1 << 1,

        /// <summary>Indicates "security code" is being run.</summary>
        INTERCEPT_SECURITY = 1 << 2,

        /// <summary>Indicates that a context policy is being run.</summary>
        INTERCEPT_CONTEXT_POLICY = 1 << 3,

        /// <summary>Indicates that some other interceptor was called.</summary>
        INTERCEPT_INTERCEPTION = 1 << 4,

        /// <summary>Represents all interceptor types.</summary>
        INTERCEPT_ALL = 0xFFFF
    }
}
