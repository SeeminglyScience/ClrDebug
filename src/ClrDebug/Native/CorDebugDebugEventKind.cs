namespace ClrDebug.Native
{
    /// <summary>
    /// Indicates the type of event whose information is
    /// decoded by the <c>DecodeEvent</c> method.
    /// </summary>
    /// <remarks>
    /// A member of the <c>CorDebugDebugEventKind</c> enumeration is
    /// returned by calling the <c>ICorDebugDebugEvent::GetEventKind</c> method.
    ///
    /// Note:
    ///
    /// This enumeration is intended for use in .NET Native debugging scenarios only.
    /// </remarks>
    public enum CorDebugDebugEventKind
    {
        /// <summary>A module load event.</summary>
        DEBUG_EVENT_KIND_MODULE_LOADED = 1,

        /// <summary>A module unload event.</summary>
        DEBUG_EVENT_KIND_MODULE_UNLOADED = 2,

        /// <summary>A first-chance exception.</summary>
        DEBUG_EVENT_KIND_MANAGED_EXCEPTION_FIRST_CHANCE = 3,

        /// <summary>A first-chance user exception.</summary>
        DEBUG_EVENT_KIND_MANAGED_EXCEPTION_USER_FIRST_CHANCE = 4,

        /// <summary>An exception for which a catch handler exists.</summary>
        DEBUG_EVENT_KIND_MANAGED_EXCEPTION_CATCH_HANDLER_FOUND = 5,

        /// <summary>An unhandled exception.</summary>
        DEBUG_EVENT_KIND_MANAGED_EXCEPTION_UNHANDLED = 6
    }
}
