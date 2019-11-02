using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a segment of either Microsoft intermediate language (MSIL) code or native code.
    /// </summary>
    /// <remarks>
    /// <see cref="CorDebugCode" /> can represent either MSIL or native code. An <see cref="CorDebugFunction" />
    /// object that represents MSIL code can have either zero or one <see cref="CorDebugCode" /> objects
    /// associated with it. An <see cref="CorDebugFunction" /> object that represents native code can
    /// have any number of <see cref="CorDebugCode" /> objects associated with it.
    /// </remarks>
    public unsafe class CorDebugCode : Unknown
    {
        internal CorDebugCode()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a value that indicates whether this represents code that was compiled in
        /// Microsoft intermediate language (MSIL).
        /// </summary>
        public int IsIL(out bool bIL) => InvokeGet(_this, This[0]->IsIL, out bIL);

        /// <summary>
        /// Gets the function associated with this instance.
        /// </summary>
        /// <remarks>
        /// <see cref="CorDebugCode" /> and <see cref="CorDebugFunction" /> maintain a one-to-one
        /// relationship.
        /// </remarks>
        public int GetFunction(out CorDebugFunction function) => InvokeGetObject(_this, This[0]->GetFunction, out function);

        /// <summary>
        /// Gets the relative virtual address (RVA) of the code segment that this instance represents.
        /// </summary>
        public int GetAddress(out ulong start) => InvokeGet(_this, This[0]->GetAddress, out start);

        /// <summary>Gets the size, in bytes, of the binary code.</summary>
        public int GetSize(out uint cBytes) => InvokeGet(_this, This[0]->GetSize, out cBytes);

        /// <summary>
        /// Creates a breakpoint in this code segment at the specified offset.
        /// </summary>
        /// <param name="offset">The offset at which to create the breakpoint.</param>
        /// <param name="breakpoint">The created breakpoint.</param>
        /// <remarks>
        /// Before the breakpoint is active, it must be added to the process object.
        ///
        /// If this code is Microsoft intermediate language (MSIL) code, and there is
        /// a just-in-time (JIT)-compiled, native version of the code, the breakpoint
        /// will be applied in the JIT-compiled code as well. (The same is true if the
        /// code is JIT-compiled later.)
        /// </remarks>
        public int CreateBreakpoint(uint offset, out CorDebugFunctionBreakpoint breakpoint)
        {
            void** pBreakpoint = default;
            int result = Calli(_this, This[0]->CreateBreakpoint, offset, &pBreakpoint);
            ComFactory.Create(pBreakpoint, out breakpoint);
            return result;
        }

        [Obsolete("Use ICorDebugCode2::GetCodeChunks instead.")]
        /// <summary>
        /// Gets all the code for the specified function, formatted for disassembly.
        /// </summary>
        /// <param name="startOffset">The offset of the beginning of the function.</param>
        /// <param name="endOffset">The offset of the end of the function.</param>
        /// <param name="buffer">The array into which the code will be returned.</param>
        /// <param name="bytesUsed">
        /// The number of bytes written to <see paramref="buffer" />.
        /// </param>
        /// <remarks>
        /// If the function's code has been divided into multiple chunks, they are
        /// concatenated in order of increasing native offset. Instruction boundaries
        /// are not checked.
        /// </remarks>
        public int GetCode(uint startOffset, uint endOffset, Span<byte> buffer, out uint bytesUsed)
        {
            fixed (void* pBuffer = buffer)
            {
                uint pcBufferSize = default;
                int result = Calli(_this, This[0]->GetCode, startOffset, endOffset, buffer.Length, pBuffer, &pcBufferSize);
                bytesUsed = pcBufferSize;
                return result;
            }
        }

        /// <summary>
        /// Gets the one-based number that identifies the version of the code that this represents.
        /// </summary>
        /// <remarks>
        /// The version number is incremented each time an edit-and-continue (EnC) operation
        /// is performed on the code.
        /// </remarks>
        public int GetVersionNumber(out uint nVersion) => InvokeGet(_this, This[0]->GetVersionNumber, out nVersion);

        /// <summary>
        /// Gets an array of <see cref="COR_DEBUG_IL_TO_NATIVE_MAP" /> instances that represent
        /// mappings from Microsoft intermediate language (MSIL) offsets to native offsets.
        /// </summary>
        /// <param name="map">
        /// An array of <see cref="COR_DEBUG_IL_TO_NATIVE_MAP" /> structures, each of which
        /// represents a mapping from an MSIL offset to a native offset.
        ///
        /// There is no ordering to the array of elements returned.
        /// </param>
        /// <param name="mapsUsed">
        /// The number of maps written to <see paramref="map" />.
        /// </param>
        /// <remarks>
        /// This method returns meaningful results only if this instance represents
        /// native code that was just-in-time (JIT) compiled from MSIL code.
        /// </remarks>
        public int GetILToNativeMapping(Span<COR_DEBUG_IL_TO_NATIVE_MAP> map, out int mapsUsed)
        {
            fixed (void* pMap = map)
            {
                int pMapsUsed = default;
                int result = Calli(_this, This[0]->GetILToNativeMapping, map.Length, &pMapsUsed, pMap);
                mapsUsed = pMapsUsed;
                return result;
            }
        }

        /// <summary>
        /// Gets an array of <see cref="COR_DEBUG_IL_TO_NATIVE_MAP" /> instances that represent
        /// mappings from Microsoft intermediate language (MSIL) offsets to native offsets.
        /// </summary>
        /// <remarks>
        /// This method returns meaningful results only if this instance represents
        /// native code that was just-in-time (JIT) compiled from MSIL code.
        /// </remarks>
        public COR_DEBUG_IL_TO_NATIVE_MAP[] GetILToNativeMapping()
        {
            int length = 0;
            Calli(_this, This[0]->GetILToNativeMapping, 0, null, &length).MaybeThrowHr();
            if (length == 0)
            {
                return Array.Empty<COR_DEBUG_IL_TO_NATIVE_MAP>();
            }

            var result = new COR_DEBUG_IL_TO_NATIVE_MAP[length];
            fixed (void* r = result)
            {
                Calli(_this, This[0]->GetILToNativeMapping, result.Length, &length, r).MaybeThrowHr();
            }

            if (result.Length != length)
            {
                Array.Resize(ref result, length);
            }

            return result;
        }

        [Obsolete("This method is not implemented in the current version of the .NET Framework.")]
        public int GetEnCRemapSequencePoints(Span<uint> offsets, out uint offsetsUsed)
        {
            fixed (void* pOffsets = offsets)
            {
                uint pOffsetsUsed = default;
                int result = Calli(_this, This[0]->GetEnCRemapSequencePoints, offsets.Length, &pOffsetsUsed, pOffsets);
                offsetsUsed = pOffsetsUsed;
                return result;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* IsIL;

            public void* GetFunction;

            public void* GetAddress;

            public void* GetSize;

            public void* CreateBreakpoint;

            public void* GetCode;

            public void* GetVersionNumber;

            public void* GetILToNativeMapping;

            public void* GetEnCRemapSequencePoints;
        }
    }
}
