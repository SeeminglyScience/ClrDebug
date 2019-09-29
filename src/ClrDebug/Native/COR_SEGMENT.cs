using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Contains information about a region of memory in the managed heap.
    /// </summary>
    /// <remarks>
    /// The <c>COR_SEGMENTS</c> structure represents a region of memory
    /// in the managed heap. <c>COR_SEGMENTS</c> objects are members of the
    /// <c>ICorDebugHeapRegionEnum</c> collection object, which is populated
    /// by calling the<c> ICorDebugProcess5::EnumerateHeapRegions</c> method.
    ///
    /// The heap field is the processor number, which corresponds to the heap
    /// being reported. For workstation garbage collectors, its value is always
    /// zero, because workstations have only one garbage collection heap.
    /// For server garbage collectors, its value corresponds to the processor
    /// the heap is attached to. Note that there may be more or fewer garbage
    /// collection heaps than there are actual processors due to the implementation
    /// details of the garbage collector.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_SEGMENT
    {
        /// <summary>The starting address of the memory region.</summary>
        public ulong start;

        /// <summary>The ending address of the memory region.</summary>
        public ulong end;

        /// <summary>
        /// A <c>CorDebugGenerationTypes</c> enumeration member that
        /// indicates the generation of the memory region.
        /// </summary>
        public CorDebugGenerationTypes gen;

        /// <summary>
        /// The heap number in which the memory region resides.
        /// </summary>
        public uint heap;

    }
}
