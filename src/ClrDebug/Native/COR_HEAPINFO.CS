using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides general information about the garbage collection heap,
    /// including whether it is enumerable.
    /// </summary>
    /// <remarks>
    /// An instance of the <see cref="COR_HEAPINFO" /> structure is returned
    /// by calling the <see cref="CorDebugProcess5.GetGCHeapInformation(out COR_HEAPINFO)" />
    /// method. Before enumerating objects on the garbage collection heap,
    /// you must always check the <see cref="areGCStructuresValid" /> field to
    /// ensure that the heap is in an enumerable state.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_HEAPINFO
    {
        /// <summary>
        /// <c>true</c> if garbage collection structures are valid and the
        /// heap can be enumerated; otherwise, <c>false</c>.
        /// </summary>
        public bool areGCStructuresValid;

        /// <summary>
        /// The size, in bytes, of pointers on the target architecture.
        /// </summary>
        public uint pointerSize;

        /// <summary>
        /// The number of logical garbage collection heaps in the process.
        /// </summary>
        public uint numHeaps;

        /// <summary>
        /// <c>true</c> if concurrent (background) garbage collection is
        /// enabled; otherwise, <c>false</c>.
        /// </summary>
        public bool concurrent;

        /// <summary>
        /// Indicates whether the garbage collector is running on a
        /// workstation or a server.
        /// </summary>
        public CorDebugGCType gcType;
    }
}
