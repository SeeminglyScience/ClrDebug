namespace ClrDebug.Native
{
    /// <summary>
    /// Describes the format of the data in a byte array that contains
    /// information about a native exception debug event.
    /// </summary>
    /// <remarks>
    /// A member of the <c>CorDebugRecordFormat</c> enumeration is passed
    /// to the <c>DecodeEvent</c> method to indicate the format of the byte
    /// array in its <c>pRecord</c> argument.
    public enum CorDebugRecordFormat : int
    {
        /// <summary>The data is a 32-bit Windows exception record.</summary>
        FORMAT_WINDOWS_EXCEPTIONRECORD32 = 1,

        /// <summary>The data is a 64-bit Windows exception record.</summary>
        FORMAT_WINDOWS_EXCEPTIONRECORD64 = 2,
    }
}
