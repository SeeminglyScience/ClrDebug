using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Specifies changes in the relative offset of a function.
    /// </summary>
    /// <remarks>
    /// The format of the map is as follows: The debugger will assume that
    /// <c>oldOffset</c> refers to an MSIL offset within the original,
    /// unmodified MSIL code. The <c>newOffset</c> parameter refers to the
    /// corresponding MSIL offset within the new, instrumented code.
    ///
    /// For stepping to work properly, the following requirements should
    /// be met:
    ///
    /// - The map should be sorted in ascending order.
    /// - Instrumented MSIL code should not be reordered.
    /// - Original MSIL code should not be removed.
    /// - The map should include entries to map all the sequence points from
    ///   the program database (PDB) file.
    ///
    /// The map does not interpolate missing entries. The following example
    /// shows a map and its results.
    ///
    /// Map:
    /// - 0 old offset, 0 new offset
    /// - 5 old offset, 10 new offset
    /// - 9 old offset, 20 new offset
    ///
    /// Results:
    /// - An old offset of 0, 1, 2, 3, or 4 will be mapped to a new offset of 0.
    /// - An old offset of 5, 6, 7, or 8 will be mapped to new offset 10.
    /// - An old offset of 9 or higher will be mapped to new offset 20.
    /// - A new offset of 0, 1, 2, 3, 4, 5, 6, 7, 8, or 9 will be mapped to old offset 0.
    /// - A new offset of 10, 11, 12, 13, 14, 15, 16, 17, 18, or 19 will be mapped to old offset 5.
    /// - A new offset of 20 or higher will be mapped to old offset 9.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_IL_MAP
    {
        /// <summary>
        /// The old Microsoft intermediate language (MSIL) offset relative
        /// to the beginning of the function.
        /// </summary>
        public uint oldOffset;

        /// <summary>
        /// The new MSIL offset relative to the beginning of the function.
        /// </summary>
        public uint newOffset;

        /// <summary>
        /// <see langword="true" /> if the mapping is known to be accurate; otherwise, <see langword="false" />.
        /// </summary>
        public bool fAccurate;
    }
}
