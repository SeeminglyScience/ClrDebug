namespace ClrDebug.Native
{
    /// <summary>
    /// Specifies the type of unmapped code that can trigger
    /// a halt in code execution by the stepper.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="CorDebugStepper.SetUnmappedStopMask(CorDebugUnmappedStop)" />
    /// method to set the flags that specify the unmapped code
    /// in which the stepper will stop.
    /// </remarks>
    public enum CorDebugUnmappedStop : int
    {
        /// <summary>Do not stop in any type of unmapped code.</summary>
        STOP_NONE = 0,

        /// <summary>Stop in prolog code.</summary>
        STOP_PROLOG = 1 << 0,

        /// <summary>Stop in epilog code.</summary>
        STOP_EPILOG = 1 << 1,

        /// <summary>Stop in code that has no mapping information.</summary>
        STOP_NO_MAPPING_INFO = 1 << 2,

        /// <summary>
        /// Stop in unmapped code that does not fit into the prolog,
        /// epilog, no-mapping-information, or unmanaged category.
        /// </summary>
        STOP_OTHER_UNMAPPED = 1 << 3,

        /// <summary>
        /// Stop in unmanaged code. This value is valid only with
        /// interop debugging.
        /// </summary>
        STOP_UNMANAGED = 1 << 4,

        /// <summary>Stop in all types of unmapped code.</summary>
        STOP_ALL = 0xFFFF
    }
}
