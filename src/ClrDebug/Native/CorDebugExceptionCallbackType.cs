namespace ClrDebug.Native
{
    /// <summary>
    /// Indicates the type of callback that is made from an <c>ICorDebugManagedCallback2::Exception</c> event.
    /// </summary>
    public enum CorDebugExceptionCallbackType : int
    {
        /// <summary>An exception was thrown.</summary>
        DEBUG_EXCEPTION_FIRST_CHANCE = 1,

        /// <summary>The exception windup process entered user code.</summary>
        DEBUG_EXCEPTION_USER_FIRST_CHANCE = 2,

        /// <summary>The exception windup process found a catch block in user code.</summary>
        DEBUG_EXCEPTION_CATCH_HANDLER_FOUND = 3,

        /// <summary>The exception was not handled.</summary>
        DEBUG_EXCEPTION_UNHANDLED = 4,
    }
}
