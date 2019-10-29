using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides methods that extend the capabilities of <c>ICorDebugCode</c>.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugCode2 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the chunks of code that this code object is composed of.
        /// </summary>
        /// <param name="cbufSize">
        /// Size of the <c>chunks</c> array.
        /// </param>
        /// <param name="pcnumChunks">
        /// The number of chunks returned in the <c>chunks</c> array.
        /// </param>
        /// <param name="chunks">
        /// An array of "CodeChunkInfo" structures, each of which represents a
        /// single chunk of code. If the value of <c>cbufSize</c> is 0, this
        /// parameter can be null.
        /// </param>
        /// <remarks>
        /// The code chunks will never overlap, and they will follow the order
        /// in which they would have been concatenated by <c>ICorDebugCode::GetCode</c>.
        /// A Microsoft intermediate language (MSIL) code object in the .NET Framework
        /// version 2.0 will comprise a single code chunk.
        /// </remarks>
        public int GetCodeChunks(Span<CodeChunkInfo> buffer, out int amountWritten)
        {
            fixed (void* pBuffer = buffer)
            fixed (void* pAmountWritten = &amountWritten)
            {
                return Calli(
                    _this,
                    This[0]->GetCodeChunks,
                    buffer.Length,
                    &pAmountWritten,
                    pBuffer);
            }
        }

        /// <summary>
        /// Gets the flags that specify the conditions under which this
        /// code object was either just-in-time (JIT) compiled or generated
        /// using the native image generator (Ngen.exe).
        /// </summary>
        /// <param name="pdwFlags">
        /// A pointer to a value of the <c>CorDebugJITCompilerFlags</c> enumeration
        /// that specifies the behavior of the JIT compiler or the native image generator.
        /// </param>
        public int GetCompilerFlags(out int dwFlags)
            => InvokeGet(_this, This[0]->GetCompilerFlags, out dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetCodeChunks;

            public void* GetCompilerFlags;
        }
    }
}
