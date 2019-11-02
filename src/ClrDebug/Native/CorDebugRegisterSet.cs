using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents the set of registers available on the computer
    /// that is currently executing code.
    /// </summary>
    /// <remarks>
    /// This class supports only 32-bit registers. Use the <c>ICorDebugRegisterSet2</c>
    /// interface on platforms such as IA-64 that require additional
    /// registers.
    /// </remarks>
    public unsafe class CorDebugRegisterSet : Unknown
    {
        internal CorDebugRegisterSet()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a bit mask indicating which registers in are
        /// currently available.
        /// </summary>
        /// <remarks>
        /// A register may be unavailable if its value cannot be
        /// determined for the given situation.
        ///
        /// The returned mask contains a bit for each register
        /// (1 << the register index). The bit value is 1 if the
        /// register is available, or 0 if it is not available.
        /// </remarks>
        public int GetRegistersAvailable(out ulong available)
            => InvokeGet(_this, This[0]->GetRegistersAvailable, out available);

        /// <summary>
        /// Gets the value of each register (on the computer that
        /// is currently executing code) that is specified by
        /// the bit mask.
        /// </summary>
        /// <param name="mask">
        /// A bit mask that specifies which register values are to
        /// be retrieved. Each bit corresponds to a register. If a
        /// bit is set to one, the register's value is retrieved;
        /// otherwise, the register's value is not retrieved.
        /// </param>
        /// <param name="regBuffer">The destination buffer.</param>
        /// <remarks>
        /// The size of the array should be equal to the number of
        /// bits set to one in the bit mask. The size of <see paramref="regBuffer" />
        /// specifies the number of elements in the buffer that will
        /// receive the register values. If the size is too small for
        /// the number of registers indicated by the mask, the higher
        /// numbered registers will be truncated from the set. If the
        /// size value is too large, the unused elements will be unmodified.
        ///
        /// If the bit mask specifies a register that is unavailable, this method
        /// returns an indeterminate value for that register.
        /// </remarks>
        public int GetRegisters(ulong mask, Span<ulong> regBuffer)
        {
            fixed (void* pRegBuffer = regBuffer)
            {
                return Calli(_this, This[0]->GetRegisters, mask, regBuffer.Length, pRegBuffer);
            }
        }

        [Obsolete(
            "SetRegisters is not implemented in the .NET Framework version 2.0. Do not call "
            + "this method. Use the higher-level operations such as ICorDebugILFrame::SetIP or "
            + "ICorDebugNativeFrame::SetIP.",
            error: true)]
        public int SetRegisters(ulong mask, Span<ulong> regBuffer)
        {
            fixed (void* pRegBuffer = regBuffer)
            {
                return Calli(_this, This[0]->SetRegisters, mask, regBuffer.Length, pRegBuffer);
            }
        }

        /// <summary>
        /// Gets the context of the current thread.
        /// </summary>
        /// <param name="context">The destination buffer.</param>
        /// <remarks>
        /// The debugger should call this function instead
        /// of the Win32 <c>GetThreadContext</c> function,
        /// because the thread may be in a "hijacked" state
        /// where its context has been temporarily changed.
        /// The data returned is a Win32 <c>CONTEXT</c> structure
        /// for the current platform.
        ///
        /// For non-leaf frames, clients should check which
        /// registers are valid by using <see cref="GetRegistersAvailable(out ulong)" />.
        /// </remarks>
        public int GetThreadContext(Span<byte> context)
        {
            fixed (void* pContext = context)
            {
                return Calli(_this, This[0]->GetThreadContext, context.Length, pContext);
            }
        }

        [Obsolete(
            "SetThreadContext is not implemented in the .NET Framework version 2.0. "
            + "Do not call this method. Use the higher-level operation "
            + "ICorDebugNativeFrame::SetIP to set the context of a thread.",
            error: true)]
        public int SetThreadContext(Span<byte> context)
        {
            fixed (void* pContext = context)
            {
                return Calli(_this, This[0]->SetThreadContext, context.Length, pContext);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetRegistersAvailable;

            public void* GetRegisters;

            public void* SetRegisters;

            public void* GetThreadContext;

            public void* SetThreadContext;
        }
    }
}
