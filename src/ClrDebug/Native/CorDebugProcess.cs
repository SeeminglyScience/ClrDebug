using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a process that is executing managed code.
    /// </summary>
    public unsafe class CorDebugProcess : CorDebugController
    {
        private ICorDebugProcessVtable** This => (ICorDebugProcessVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the operating system (OS) ID of the process.
        /// </summary>
        public int GetID(out uint dwProcessId) => InvokeGet(_this, This[0]->GetID, out dwProcessId);

        /// <summary>
        /// Gets a handle to the process.
        /// </summary>
        /// <remarks>
        /// The retrieved handle is owned by the debugging interface. The debugger
        /// should duplicate the handle before using it.
        /// </remarks>
        public int GetHandle(out IntPtr hProcessHandle) => InvokeGet(_this, This[0]->GetHandle, out hProcessHandle);

        /// <summary>
        /// Gets this process's thread that has the specified operating system (OS) thread ID.
        /// </summary>
        /// <param name="dwThreadId">The OS thread ID of the thread to be retrieved.</param>
        /// <param name="thread">The specified thread.</param>
        public int GetThread(uint dwThreadId, out CorDebugThread thread)
        {
            void** pThread = default;
            int result = Calli(_this, This[0]->GetThread, dwThreadId, &pThread);
            ComFactory.Create(pThread, result, out thread);
            return result;
        }

        [Obsolete("This method has not been implemented.")]
        public int EnumerateObjects(out CorDebugEnum objects)
            => InvokeGetObject(_this, This[0]->EnumerateObjects, out objects);

        /// <summary>
        /// Gets a value that indicates whether an address is inside a stub
        /// that will cause a transition to managed code.
        /// </summary>
        /// <param name="address">Specifies the address in question.</param>
        /// <param name="transitionStub">
        /// A value indicating whether the address is inside a stub.
        /// </param>
        /// <remarks>
        /// This method can be used by unmanaged stepping code to decide when
        /// to return stepping control to the managed stepper.
        ///
        /// You can also identity transition stubs by looking at information
        /// in the portable executable (PE) file.
        /// </remarks>
        public int IsTransitionStub(long address, out bool transitionStub)
        {
            int pTransitionStub = default;
            int result = Calli(_this, This[0]->IsTransitionStub, address, &pTransitionStub);
            transitionStub = pTransitionStub.FromNativeBool();
            return result;
        }

        /// <summary>
        /// Gets a value that indicates whether the specified thread has been
        /// suspended as a result of the debugger stopping this process.
        /// </summary>
        /// <param name="threadID">The ID of the thread in question.</param>
        /// <param name="suspended">A value indicating whether the thread is suspended.</param>
        /// <remarks>
        /// When the specified thread has been suspended as a result of the
        /// debugger stopping this process, the specified thread's Win32 suspend
        /// count is incremented by one. The debugger user interface (UI) may want
        /// to take this information into account if it displays the operating system
        /// (OS) suspend count of the thread to the user.
        ///
        /// This method makes sense only in the context of unmanaged debugging.
        /// During managed debugging, threads are cooperatively suspended rather
        /// than OS-suspended.
        /// </remarks>
        public int IsOSSuspended(uint threadID, out bool suspended)
        {
            int pSuspended = default;
            int result = Calli(_this, This[0]->IsOSSuspended, threadID, &pSuspended);
            suspended = pSuspended.FromNativeBool();
            return result;
        }

        /// <summary>
        /// Gets the context for the given thread in this process.
        /// </summary>
        /// <param name="threadID">The ID of the thread for which to retrieve the context.</param>
        /// <param name="context">The destination buffer.</param>
        /// <remarks>
        /// The debugger should call this method rather than the Win32 <c>GetThreadContext</c>
        /// method, because the thread may actually be in a "hijacked" state, in which
        /// its context has been temporarily changed. This method should be used only
        /// when a thread is in native code. Use <see cref="CorDebugRegisterSet" /> for threads in
        /// managed code.
        ///
        /// The data returned is a context structure for the current platform.
        /// Just as with the Win32 <c>GetThreadContext</c> method, the caller should
        /// initialize the context parameter before calling this method.
        /// </remarks>
        public int GetThreadContext(uint threadID, Span<byte> context)
        {
            fixed (void* pContext = context)
            {
                return Calli(_this, This[0]->GetThreadContext, context.Length, pContext);
            }
        }

        /// <summary>
        /// Sets the context for the given thread in this process.
        /// </summary>
        /// <param name="threadID">The ID of the thread for which to set the context.</param>
        /// <param name="context">
        /// An array of bytes that describe the thread's context.
        ///
        /// The context specifies the architecture of the processor on which the thread is executing.
        /// </param>
        /// <remarks>
        /// The debugger should call this method rather than the
        /// Win32 <c>SetThreadContext</c> function, because the
        /// thread may actually be in a "hijacked" state, in which
        /// its context has been temporarily changed. This method
        /// should be used only when a thread is in native code.
        /// Use <see cref="CorDebugRegisterSet" /> for threads in
        /// managed code. You should never need to modify the context
        /// of a thread during an out-of-band (OOB) debug event.
        ///
        /// The data passed must be a context structure for the current platform.
        ///
        /// This method can corrupt the runtime if used improperly.
        /// </remarks>
        public int SetThreadContext(uint threadID, Span<byte> context)
        {
            fixed (void* pContext = context)
            {
                return Calli(_this, This[0]->SetThreadContext, context.Length, pContext);
            }
        }

        /// <summary>
        /// Reads a specified area of memory for this process.
        /// </summary>
        /// <param name="address">
        /// Specifies the base address of the memory to be read.
        /// </param>
        /// <param name="buffer">
        /// The destination buffer.
        /// </param>
        /// <param name="amountRead">
        /// The amount of bytes written to <see paramref="buffer" />.
        /// </param>
        /// <remarks>
        /// The ReadMemory method is primarily intended to be
        /// used by interop debugging to inspect memory regions
        /// that are being used by the unmanaged portion of the
        /// debuggee. This method can also be used to read
        /// Microsoft intermediate language (MSIL) code and native
        /// JIT-compiled code.
        ///
        /// Any managed breakpoints will be removed from the data
        /// that is returned in the buffer parameter. No
        /// adjustments will be made for native breakpoints set
        /// by <c>ICorDebugProcess2::SetUnmanagedBreakpoint</c>.
        ///
        /// No caching of process memory is performed.
        /// </remarks>
        public int ReadMemory(ulong address, Span<byte> buffer, out UIntPtr amountRead)
        {
            UIntPtr pAmountRead = default;
            fixed (void* pBuffer = buffer)
            {
                int result = Calli(_this, This[0]->ReadMemory, address, buffer.Length, pBuffer, &pAmountRead);
                amountRead = pAmountRead;
                return result;
            }
        }

        /// <summary>
        /// Writes data to an area of memory in this process.
        /// </summary>
        /// <param name="address">
        /// The base address of the memory area to which data
        /// is written. Before data transfer occurs, the system
        /// verifies that the memory area of the specified size,
        /// beginning at the base address, is accessible for writing.
        /// If it is not accessible, the method fails.
        /// </param>
        /// <param name="buffer">The source buffer.</param>
        /// <param name="amountWritten">
        /// The amount of bytes written to the specified memory area.
        /// </param>
        /// <remarks>
        /// Data is automatically written behind any breakpoints.
        /// In the .NET Framework version 2.0, native debuggers should
        /// not use this method to inject breakpoints into the
        /// instruction stream. Use <c>ICorDebugProcess2::SetUnmanagedBreakpoint</c>
        /// instead.
        ///
        /// This method should be used only outside of managed code.
        ///
        /// This method can corrupt the runtime if used improperly.
        /// </remarks>
        public int WriteMemory(ulong address, uint size, Span<byte> buffer, out UIntPtr amountWritten)
        {
            UIntPtr pAmountWritten = default;
            fixed (void* pBuffer = buffer)
            {
                int result = Calli(_this, This[0]->ReadMemory, address, buffer.Length, pBuffer, &pAmountWritten);
                amountWritten = pAmountWritten;
                return result;
            }
        }

        /// <summary>
        /// Clears the current unmanaged exception on the given thread.
        /// </summary>
        /// <param name="threadID">
        /// The ID of the thread on which the current unmanaged exception
        /// will be cleared.
        /// </param>
        /// <remarks>
        /// Call this method before calling <see cref="CorDebugController.Continue(bool)" />
        /// when a thread has reported an unmanaged exception that should
        /// be ignored by the debuggee. This will clear both the outstanding
        /// in-band (IB) and out-of-band (OOB) events on the given thread.
        /// All OOB breakpoints and single-step exceptions are automatically
        /// cleared.
        ///
        /// Use <c>ICorDebugThread2::InterceptCurrentException</c> to intercept
        /// the current managed exception on a thread.
        /// </remarks>
        public int ClearCurrentException(uint threadID) => Calli(_this, This[0]->ClearCurrentException, threadID);

        /// <summary>
        /// Enables and disables the transmission of log messages to the debugger.
        /// </summary>
        /// <remarks>
        /// This method is valid only after the <c>ICorDebugManagedCallback::CreateProcess</c>
        /// callback occurs.
        /// </remarks>
        public int EnableLogMessages(bool onOff) => Calli(_this, This[0]->EnableLogMessages, onOff.ToNativeInt());

        /// <summary>
        /// Sets the severity level of the specified log switch.
        /// </summary>
        /// <param name="logSwitchName">The name of the log switch.</param>
        /// <param name="lLevel">The severity level to be set.</param>
        /// <remarks>
        /// This method is valid only after the <c>ICorDebugManagedCallback::CreateProcess</c>
        /// callback has occurred.
        /// </remarks>
        public int ModifyLogSwitch(Span<char> logSwitchName, int lLevel)
        {
            fixed (void* pLogSwitchName = logSwitchName)
            {
                return Calli(_this, This[0]->ModifyLogSwitch, pLogSwitchName, lLevel);
            }
        }

        /// <summary>
        /// Enumerates all the application domains in this process.
        /// </summary>
        /// <remarks>
        /// This method can be used before the <c>ICorDebugManagedCallback::CreateProcess</c>
        /// callback.
        /// </remarks>
        public int EnumerateAppDomains(out CorDebugEnum<CorDebugAppDomain> appDomains)
            => InvokeGetObject(_this, This[0]->EnumerateAppDomains, out appDomains);

        [Obsolete("This method has not been implemented.")]
        public int GetObject(out CorDebugValue @object)
            => InvokeGetObject(_this, This[0]->GetObject, out @object);

        [Obsolete("This method is not implemented.")]
        public int ThreadForFiberCookie(uint fiberCookie, out CorDebugThread thread)
        {
            void** pThread = default;
            int result = Calli(_this, This[0]->ThreadForFiberCookie, fiberCookie, &pThread);
            ComFactory.Create(pThread, result, out thread);
            return result;
        }

        /// <summary>
        /// Gets the operating system (OS) thread ID of the debugger's
        /// internal helper thread.
        /// </summary>
        /// <remarks>
        /// During managed and unmanaged debugging, it is the debugger's
        /// responsibility to ensure that the thread with the specified
        /// ID remains running if it hits a breakpoint placed by the
        /// debugger. A debugger may also wish to hide this thread from
        /// the user. If no helper thread exists in the process yet, this
        /// method returns zero in <see paramref="thread" />.
        ///
        /// You cannot cache the thread ID of the helper thread, because
        /// it may change over time. You must re-query the thread ID at
        /// every stopping event.
        ///
        /// The thread ID of the debugger's helper thread will be correct
        /// on every unmanaged <c>ICorDebugManagedCallback::CreateThread</c>
        /// event, thus allowing a debugger to determine the thread ID of
        /// its helper thread and hide it from the user. A thread that is
        /// identified as a helper thread during an unmanaged
        /// <c>ICorDebugManagedCallback::CreateThread</c> event will never
        /// run managed user code.
        /// </remarks>
        public int GetHelperThreadID(out uint threadID)
            => InvokeGet(_this, This[0]->GetHelperThreadID, out threadID);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugProcessVtable
        {
            public ICorDebugControllerVtable ICorDebugController;

            public void* GetID;

            public void* GetHandle;

            public void* GetThread;

            public void* EnumerateObjects;

            public void* IsTransitionStub;

            public void* IsOSSuspended;

            public void* GetThreadContext;

            public void* SetThreadContext;

            public void* ReadMemory;

            public void* WriteMemory;

            public void* ClearCurrentException;

            public void* EnableLogMessages;

            public void* ModifyLogSwitch;

            public void* EnumerateAppDomains;

            public void* GetObject;

            public void* ThreadForFiberCookie;

            public void* GetHelperThreadID;
        }
    }
}
