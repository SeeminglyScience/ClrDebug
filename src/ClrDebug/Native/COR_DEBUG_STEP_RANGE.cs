using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Contains the offset information for a range of code.
    ///
    /// This structure is used by the <see cref="CorDebugStepper.StepRange(bool, System.Span{COR_DEBUG_STEP_RANGE})" /> method.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_DEBUG_STEP_RANGE
    {
        /// <summary>The offset of the beginning of the range.</summary>
        public uint startOffset;

        /// <summary>The offset of the end of the range.</summary>
        public uint endOffset;
    }
}
