namespace ClrDebug.Native
{
    /// <summary>
    /// Indicates the native location type of a variable.
    /// </summary>
    /// <remarks>
    /// A member of the <c>VariableLocationType</c> enumeration is returned
    /// by the <c>ICorDebugVariableHome::GetLocationType</c> method.
    /// </remarks>
    public enum VariableLocationType : int
    {
        /// <summary>The variable is in a register.</summary>
        VLT_REGISTER = 0,

        /// <summary>The variable is in a register-relative memory location.</summary>
        VLT_REGISTER_RELATIVE = 1,

        /// <summary>The variable is not stored in a register or a register-relative memory location.</summary>
        VLT_INVALID = 2,
    }
}
