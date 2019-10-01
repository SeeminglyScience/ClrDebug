namespace ClrDebug.Native
{
    /// <summary>
    /// Describes why an exported function calls managed code.
    /// </summary>
    /// <remarks>
    /// This enumeration is used by the <c>ICorDebugProcess6::GetExportStepInfo</c>
    /// method to provide information about stepping through managed code.
    ///
    /// Note:
    ///
    /// This enumeration is intended for use in .NET Native debugging scenarios only.
    /// </remarks>
    public enum CorDebugCodeInvokePurpose : int
    {
        /// <summary>None or unknown.</summary>
        CODE_INVOKE_PURPOSE_NONE = 0,

        /// <summary>
        /// The managed code will run any managed entry point, such
        /// as a reverse p-invoke. Any more detailed purpose is unknown
        /// by the runtime.
        /// </summary>
        CODE_INVOKE_PURPOSE_NATIVE_TO_MANAGED_TRANSITION = 1,

        /// <summary>The managed code will run a static constructor.</summary>
        CODE_INVOKE_PURPOSE_CLASS_INIT = 2,

        /// <summary>
        /// The managed code will run the implementation for some interface
        /// method that was called.
        /// </summary>
        CODE_INVOKE_PURPOSE_INTERFACE_DISPATCH = 3,
    }
}
