using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a single chunk of code in memory.
    /// </summary>
    /// <remarks>
    /// The single chunk of code is a region of native code
    /// that is part of a code object such as a function.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct CodeChunkInfo
    {
        /// <summary>
        /// A CORDB_ADDRESS value that specifies the starting
        /// address of the chunk.
        /// </summary>
        public ulong startAddr;

        /// <summary>The size, in bytes, of the chunk.</summary>
        public uint length;
    }
}
