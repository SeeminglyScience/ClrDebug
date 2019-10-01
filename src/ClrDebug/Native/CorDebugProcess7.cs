using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides a method that configures the debugger to handle in-memory
    /// metadata updates in the target process.
    ///
    /// [Supported in the .NET Framework 4.5.2 and later versions]
    /// </summary>
    public unsafe class CorDebugProcess7 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Configures how the debugger handles in-memory updates to metadata
        /// within the target process.
        ///
        /// [Supported in the .NET Framework 4.5.2 and later versions]
        /// </summary>
        /// <param name="flags">
        /// A WriteableMetadataUpdateMode enumeration value that specifies whether
        /// in-memory updates to metadata in the target process are visible
        /// (<c>WriteableMetadataUpdateMode::AlwaysShowUpdates</c>) or not visible
        /// (<c>WriteableMetadataUpdateMode::LegacyCompatPolicy</c>) to the debugger.
        /// </param>
        /// <remarks>
        /// Updates to the metadata of the target process can come from Edit and Continue,
        /// a profiler, or <c>System.Reflection.Emit</c>.
        /// </remarks>
        public int SetWriteableMetadataUpdateMode(WriteableMetadataUpdateMode flags)
        {
            return Calli(_this, This[0]->SetWriteableMetadataUpdateMode, (int)flags);
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* SetWriteableMetadataUpdateMode;
        }
    }
}
