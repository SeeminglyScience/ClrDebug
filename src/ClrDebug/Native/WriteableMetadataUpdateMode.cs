namespace ClrDebug.Native
{
    /// <summary>
    /// Provides values that specify whether in-memory updates to
    /// metadata are visible to a debugger.
    ///
    /// [Supported in the .NET Framework 4.5.2 and later versions]
    /// </summary>
    /// <remarks>
    /// A member of the <c>WriteableMetadataUpdateMode</c> enumeration
    /// can be passed to the <c>SetWriteableMetadataUpdateMode</c> method
    /// to control whether in-memory updates to metadata in the target
    /// process are visible to the debugger.
    ///
    /// The <c>LegacyCompatPolicy</c> option enforces the same behavior as
    /// in versions of the .NET Framework before 4.5.2. This often means that
    /// metadata from updates is not visible. However, calls to a number of
    /// debugging methods implicitly coerce the debugger to make updates visible.
    /// For example, if the debugger passes <c>ICorDebugILFrame::GetLocalVariable</c>
    /// the index of a variable not found in the method's original metadata, all
    /// metadata for the module is updated to a snapshot matching the current state
    /// of the process. In other words, with the <c>LegacyCompatPolicy</c> option,
    /// the debugger might see none, some, or all of the available metadata updates,
    /// depending on how it uses other parts of the unmanaged debugging API.
    /// </remarks>
    public enum WriteableMetadataUpdateMode
    {
        /// <summary>
        /// Maintain compatibility with previous versions of the
        /// .NET Framework when making in-memory updates to metadata
        /// visible.
        /// </summary>
        LegacyCompatPolicy = 0,

        /// <summary>
        /// Make in-memory updates to metadata visible to the debugger.
        /// </summary>
        AlwaysShowUpdates = 1,
    }
}
