using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a step in code execution that is performed
    /// by a debugger, serves as an identifier between the
    /// issuance and completion of a command, and provides a
    /// way to cancel a step.
    /// </summary>
    /// <remarks>
    /// This class serves the following purposes:
    ///
    /// - It acts as an identifier between a step command that
    ///   is issued and the completion of that command.
    ///
    /// - It provides a central interface to encapsulate all the
    ///   stepping that can be performed.
    ///
    /// - It provides a way to prematurely cancel a stepping operation.
    ///
    /// There can be more than one stepper per thread. For example,
    /// a breakpoint may be hit while stepping over a function, and
    /// the user may wish to start a new stepping operation inside
    /// that function. It is up to the debugger to determine how to
    /// handle this situation. The debugger may want to cancel the
    /// original stepping operation or nest the two operations. This
    /// class supports both choices.
    ///
    /// A stepper may migrate between threads if the common language
    /// runtime (CLR) makes a cross-threaded, marshaled call.
    /// </remarks>
    public unsafe class CorDebugStepper : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a value that indicates whether this instance is
        /// currently executing a step.
        /// </summary>
        /// <remarks>
        /// Any step action remains active until the debugger
        /// receives a <c>ICorDebugManagedCallback::StepComplete</c>
        /// call, which automatically deactivates the stepper. A
        /// stepper may also be deactivated prematurely by calling
        /// <c>ICorDebugStepper::Deactivate</c> before the callback
        /// condition is reached.
        /// </remarks>
        public int IsActive(out int bActive) => InvokeGet(_this, This[0]->IsActive, out bActive);

        /// <summary>
        /// Causes this instance to cancel the last step command
        /// that it received.
        /// </summary>
        /// <remarks>
        /// A new stepping command may be issued after the most
        /// recently received step command has been canceled.
        /// </remarks>
        public int Deactivate() => Calli(_this, This[0]->Deactivate);

        /// <summary>
        /// Sets a value that specifies the types of code that are
        /// stepped into.
        /// </summary>
        /// <remarks>
        /// If the bit for an interceptor is set, the stepper will
        /// complete when the given type of intercepting code is
        /// encountered. If the bit is cleared, the intercepting
        /// code will be skipped.
        ///
        /// This method may have unforeseen interactions with
        /// <see cref="SetUnmappedStopMask(CorDebugUnmappedStop)" />
        /// (from the user's point of view). For example, if
        /// the only visible (that is, non-internal) portion
        /// of class initialization code lacks mapping information
        /// and <see cref="CorDebugUnmappedStop.STOP_NO_MAPPING_INFO "/> isn't set,
        /// the stepper will step over the class initialization.
        /// By default, only the <see cref="CorDebugIntercept.INTERCEPT_NONE" /> value
        /// of the <see cref="CorDebugIntercept" /> enumeration will be used.
        /// </remarks>
        public int SetInterceptMask(CorDebugIntercept mask) => Calli(_this, This[0]->SetInterceptMask, (int)mask);

        /// <summary>
        /// Sets a value that specifies the type of unmapped code
        /// in which execution will halt.
        /// </summary>
        /// <remarks>
        /// The default value is <see cref="CorDebugUnmappedStop.STOP_OTHER_UNMAPPED" />.
        /// The value <see cref="CorDebugUnmappedStop.STOP_UNMANAGED" /> is only
        /// valid with interop debugging.
        ///
        /// When the debugger finds a just-in-time (JIT) compilation
        /// that has no corresponding mapping to Microsoft intermediate
        /// language (MSIL), it halts execution if the flag specifying
        /// that type of unmapped code has been set; otherwise, stepping
        /// transparently continues.
        ///
        /// If the debugger doesn't use a stepper to enter a method,
        /// then it won't necessarily step over unmapped code.
        /// </remarks>
        public int SetUnmappedStopMask(CorDebugUnmappedStop mask) => Calli(_this, This[0]->SetUnmappedStopMask, (int)mask);

        /// <summary>
        /// Causes the stepper to single-step through its containing
        /// thread, and optionally, to continue single-stepping through
        /// functions that are called within the thread.
        /// </summary>
        /// <param name="bStepIn">
        /// Indicates whether the stepper should step into a function
        /// that is called within the thread. Specifying <see langword="false" />
        /// will cause the stepper to step over instead.
        /// </param>
        /// <remarks>
        /// The step completes when the common language runtime performs
        /// the next managed instruction in this stepper's frame. If
        /// <see cref="Step(bool)" /> is called on a stepper, which is
        /// not in managed code, the step will complete when the next
        /// managed code instruction is executed by the thread.
        /// </remarks>
        public int Step(bool bStepIn) => Calli(_this, This[0]->Step, bStepIn.ToNativeInt());

        /// <summary>
        /// Causes the stepper to single-step through its containing
        /// thread, and to return when it reaches code beyond the last
        /// of the specified ranges.
        /// </summary>
        /// <param name="bStepIn">
        /// Indicates whether the stepper should step into a function
        /// that is called within the thread. Specifying <see langword="false" />
        /// will cause the stepper to step over instead.
        /// </param>
        /// <param name="ranges">
        /// Specifies the ranges that the stepper should step through.
        /// </param>
        public int StepRange(bool bStepIn, Span<COR_DEBUG_STEP_RANGE> ranges)
        {
            fixed (void* pRanges = ranges)
            {
                return Calli(_this, This[0]->StepRange, bStepIn.ToNativeInt(), pRanges, ranges.Length);
            }
        }

        /// <summary>
        /// Causes stepper to single-step through its containing thread,
        /// and to complete when the current frame returns control to
        /// the calling frame.
        /// </summary>
        /// <remarks>
        /// A <see cref="StepOut" /> operation will complete after
        /// returning normally from the current frame to the calling
        /// frame.
        ///
        /// If <see cref="StepOut" /> is called when in unmanaged code,
        /// the step will complete when the current frame returns to
        /// the managed code that called it.
        ///
        /// In the .NET Framework version 2.0, do not use <see cref="StepOut" />
        /// with the <see cref="CorDebugUnmappedStop.STOP_UNMANAGED" /> flag set
        /// because it will fail. (Use <see cref="SetUnmappedStopMask(CorDebugUnmappedStop)" /> to
        /// set flags for stepping.) Interop debuggers must step out
        /// to native code themselves.
        /// </remarks>
        public int StepOut() => Calli(_this, This[0]->StepOut);

        /// <summary>
        /// Sets a value that specifies whether calls to <see cref="StepRange(bool, ReadOnlySpan{COR_DEBUG_STEP_RANGE})" />
        /// pass argument values that are relative to the native code
        /// or relative to Microsoft intermediate language (MSIL)
        /// code of the method that is being stepped through.
        /// </summary>
        /// <param name="bIL">
        /// Indicates whether the ranges are relative to the MSIL code (<see langword="true" />)
        /// or native code (<see langword="false" />). The default
        /// is <see langword="true" />.
        /// </param>
        public int SetRangeIL(int bIL) => Calli(_this, This[0]->SetRangeIL, bIL);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* IsActive;

            public void* Deactivate;

            public void* SetInterceptMask;

            public void* SetUnmappedStopMask;

            public void* Step;

            public void* StepRange;

            public void* StepOut;

            public void* SetRangeIL;
        }
    }
}
