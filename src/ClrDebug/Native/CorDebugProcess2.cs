using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// A logical extension of <see cref="CorDebugProcess" />, which represents
    /// a process running managed code.
    /// </summary>
    public unsafe class CorDebugProcess2 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the thread on which the task with the specified identifier
        /// is executing.
        /// </summary>
        /// <param name="taskid">The identifier for the task.</param>
        /// <param name="thread">The requested thread.</param>
        /// <remarks>
        /// The host can set the task identifier by using the <c>ICLRTask::SetTaskIdentifier</c>
        /// method.
        /// </remarks>
        public unsafe int GetThreadForTaskID(ulong taskid, out CorDebugThread2 thread)
        {
            void** pThread = default;
            int hResult = Calli(_this, This[0]->GetThreadForTaskID, taskid, &pThread);
            ComFactory.Create(pThread, hResult, out thread);
            return hResult;
        }

        /// <summary>
        /// Gets the version number of the common language runtime (CLR) that
        /// is running in this process.
        /// </summary>
        /// <remarks>
        /// This method returns an error code if no runtime has been loaded
        /// in the process.
        /// </remarks>
        public unsafe int GetVersion(out COR_VERSION version)
        {
            fixed (void* pVersion = &version)
            {
                return Calli(_this, This[0]->GetVersion, pVersion);
            }
        }

        /// <summary>
        /// Sets an unmanaged breakpoint at the specified native image offset.
        /// </summary>
        /// <param name="address">
        /// The address that specifies the native image offset.
        /// </param>
        /// <param name="buffer">
        /// The destination buffer.
        /// </param>
        /// <param name="amountWritten">
        /// The amount of bytes written to <see paramref="buffer" />.
        /// </param>
        /// <remarks>
        /// If the native image offset is within the common language runtime (CLR),
        /// the breakpoint will be ignored. This allows the CLR to avoid dispatching
        /// an out-of-band breakpoint, when the breakpoint is set by the debugger.
        /// </remarks>
        public unsafe int SetUnmanagedBreakpoint(ulong address, Span<byte> buffer, out int amountWritten)
        {
            fixed (void* pBuffer = buffer)
            fixed (void* pAmountWritten = &amountWritten)
            {
                return Calli(_this, This[0]->SetUnmanagedBreakpoint, buffer.Length, pBuffer, pAmountWritten);
            }
        }

        /// <summary>
        /// Removes a previously set breakpoint at the given address.
        /// </summary>
        /// <param name="address">The address that specifies the native image offset.</param>
        /// <remarks>
        /// The specified breakpoint would have been previously set by an earlier
        /// call to <see cref="SetUnmanagedBreakpoint(ulong, Span{byte}, out int)" />.
        ///
        /// This method can be called while the process being debugged is running.
        ///
        /// This method returns a failure code if the debugger is attached in managed-only
        /// mode or if no breakpoint exists at the specified address.
        /// </remarks>
        public unsafe int ClearUnmanagedBreakpoint(ulong address)
            => Calli(_this, This[0]->ClearUnmanagedBreakpoint, address);

        /// <summary>
        /// Sets the flags that must be embedded in a precompiled image in order for
        /// the runtime to load that image into the current process.
        /// </summary>
        /// <param name="pdwFlags"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method specifies the flags that must be embedded in a precompiled
        /// image so that the runtime will load that image into this process. The
        /// flags set by this method are used only to select the correct precompiled
        /// image. If no such image exists, the runtime will load the Microsoft
        /// intermediate language (MSIL) image and the just-in-time (JIT) compiler
        /// instead. In that case, the debugger must still use the
        /// <see cref="CorDebugModule2.SetJITCompilerFlags" /> method to set the flags
        /// as desired for the JIT compilation.
        ///
        /// If an image is loaded, but some JIT compiling must take place for that image
        /// (which will be the case if the image contains generics), the compiler flags
        /// specified by this method will apply to the extra JIT compilation.
        ///
        /// This method must be called during the <c>ICorDebugManagedCallback::CreateProcess</c>
        /// callback. Attempts to call the it afterwards will fail. Also, attempts to set
        /// flags that are either not defined in the <see cref="CorDebugJITCompilerFlags" />
        /// enumeration or are not legal for the given process will fail.
        /// </remarks>
        public unsafe int SetDesiredNGENCompilerFlags(CorDebugJITCompilerFlags pdwFlags)
            => Calli(_this, This[0]->SetDesiredNGENCompilerFlags, (uint)pdwFlags);

        /// <summary>
        /// Gets the current compiler flag settings that the common language runtime
        /// (CLR) uses to select the correct precompiled (that is, native) image to
        /// be loaded into this process.
        /// </summary>
        /// <remarks>
        /// Use the <see cref="SetDesiredNGENCompilerFlags(CorDebugJITCompilerFlags)" />
        /// method to set the flags that the CLR will use to select the correct
        /// pre-compiled image to load.
        /// </remarks>
        public unsafe int GetDesiredNGENCompilerFlags(out CorDebugJITCompilerFlags pdwFlags)
        {
            uint flags = default;
            int hResult = Calli(_this, This[0]->GetDesiredNGENCompilerFlags, &flags);
            pdwFlags = (CorDebugJITCompilerFlags)flags;
            return hResult;
        }

        /// <summary>
        /// Gets a reference pointer to the specified managed object that has a
        /// garbage collection handle.
        /// </summary>
        /// <param name="handle">
        /// A pointer to a managed object that has a garbage collection handle.
        /// This value is a <see cref="IntPtr" /> object and can be retrieved
        /// from the <see cref="GCHandle" /> for the managed object.
        /// </param>
        /// <param name="outValue">
        /// The reference to the specified managed object.
        /// </param>
        /// <remarks>
        /// Do not confuse the returned reference value with a garbage collection
        /// reference value.
        ///
        /// The returned reference behaves like a normal reference. It is disabled
        /// when code execution continues after a breakpoint. The lifetime of the
        /// target object is not affected by the lifetime of the reference value.
        ///
        /// This method does not validate the handle. Therefore, this method can
        /// potentially corrupt both the debugger and the code being debugged if
        /// an invalid handle is passed.
        /// </remarks>
        public unsafe int GetReferenceValueFromGCHandle(UIntPtr handle, out CorDebugReferenceValue outValue)
        {
            void** pOutValue = default;
            int hResult = Calli(_this, This[0]->GetReferenceValueFromGCHandle, handle, &pOutValue);
            ComFactory.Create(pOutValue, hResult, out outValue);
            return hResult;
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetThreadForTaskID;

            public void* GetVersion;

            public void* SetUnmanagedBreakpoint;

            public void* ClearUnmanagedBreakpoint;

            public void* SetDesiredNGENCompilerFlags;

            public void* GetDesiredNGENCompilerFlags;

            public void* GetReferenceValueFromGCHandle;
        }
    }
}
