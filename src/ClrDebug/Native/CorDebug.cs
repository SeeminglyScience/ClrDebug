using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides methods that allow developers to debug applications in the
    /// common language runtime (CLR) environment.
    /// </summary>
    /// <remarks>
    /// This class represents an event processing loop for a debugger process.
    /// The debugger must wait for the <c>ICorDebugManagedCallback::ExitProcess</c>
    /// callback from all processes being debugged before releasing this interface.
    ///
    /// The <see cref="CorDebug" /> object is the initial object to control all
    /// further managed debugging. In the .NET Framework versions 1.0 and 1.1,
    /// this object was a CoClass object created from COM. In the .NET Framework
    /// version 2.0, this object is no longer a CoClass object. It must be created
    /// by the <c>CreateDebuggingInterfaceFromVersion</c> function, which is more
    /// version-aware. This new creation function enables clients to get a specific
    /// implementation of <c>ICorDebug</c>, which also emulates a specific version
    /// of the debugging API.
    /// </remarks>
    public unsafe class CorDebug : Unknown
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CorDebug() : base()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// The debugger must call this method at creation time to initialize the debugging
        /// services. This method must be called before any other method on <see cref="ICorDebug" />
        /// is called.
        /// </summary>
        public int Initialize() => Calli(_this, This[0]->Initialize);

        /// <summary>
        /// Must be called when the <see cref="CorDebug" /> object is no longer needed.
        /// </summary>
        public int Terminate() => Calli(_this, This[0]->Terminate);

        /// <summary>
        /// Specifies the event handler object for managed events.
        /// </summary>
        /// <param name="pCallback">
        /// A pointer to an <see cref="CorDebugManagedCallback" /> object, which is the event
        /// handler object.
        /// </param>
        /// <remarks>
        /// Must be called at creation time.
        ///
        /// If the <see cref="CorDebugManagedCallback" /> implementation does not contain
        /// sufficient interfaces to handle debugging events for the application that is being
        /// debugged, <see cref="SetManagedHandler" /> returns <see cref="HResult.E_NOINTERFACE" />.
        /// </remarks>
        public int SetManagedHandler(CorDebugManagedCallback* pCallback)
            => Calli(_this, This[0]->SetManagedHandler, pCallback);

        /// <summary>
        /// Specifies the event handler object for unmanaged events.
        /// </summary>
        /// <param name="pCallback">
        /// A pointer to an <see cref="CorDebugUnmanagedCallback" /> object that represents the
        /// event handler for unmanaged events.
        /// </param>
        public int SetUnmanagedHandler(void* pCallback)
            => Calli(_this, This[0]->SetUnmanagedHandler, pCallback);

        /// <summary>
        /// Launches a process and its primary thread under the control of the debugger.
        /// </summary>
        /// <param name="lpApplicationName">
        /// Specifies the module to be executed by the launched process. The module is executed
        /// in the security context of the calling process.
        /// </param>
        /// <param name="lpCommandLine">
        /// Specifies the command line to be executed by the launched process. The application
        /// name (for example, "SomeApp.exe") must be the first argument.
        /// </param>
        /// <param name="lpProcessAttributes">
        /// Specifies the security descriptor for the process. If <see langword="null" />, the
        /// process gets a default security descriptor.
        /// </param>
        /// <param name="lpThreadAttributes">
        /// Specifies the security descriptor for the primary thread of the process. If
        /// <see langword="null" />, the thread gets a default security descriptor.
        /// </param>
        /// <param name="bInheritHandles">
        /// Indicates whether each inheritable handle in the calling process is inherited by the
        /// launched process, or false to indicate that the handles are not inherited. The
        /// inherited handles have the same value and access rights as the original handles.
        /// </param>
        /// <param name="dwCreationFlags">
        /// A bitwise combination of the Win32 Process Creation Flags that control the priority
        /// class and the behavior of the launched process.
        /// </param>
        /// <param name="lpEnvironment">
        /// Pointer to an environment block for the new process.
        /// </param>
        /// <param name="lpCurrentDirectory">
        /// Specifies the full path to the current directory for the process. If this parameter
        /// is <see langword="null" />, the new process will have the same current drive and
        /// directory as the calling process.
        /// </param>
        /// <param name="lpStartupInfo">
        /// Specifies the window station, desktop, standard handles, and appearance of the main
        /// window for the launched process.
        /// </param>
        /// <param name="lpProcessInformation">
        /// Specifies the identification information about the process to be launched.
        /// </param>
        /// <param name="debuggingFlags">
        /// Specifies the debugging options.
        /// </param>
        /// <param name="pProcess">
        /// The <see cref="CorDebugProcess" /> object that represents the created process.
        /// </param>
        /// <remarks>
        /// The parameters of this method are the same as those of the Win32 CreateProcess method.
        ///
        /// To enable unmanaged mixed-mode debugging, set <see paramref="dwCreationFlags" /> to
        /// <c>DEBUG_PROCESS | DEBUG_ONLY_THIS_PROCESS</c>. If you want to use only managed
        /// debugging, do not set these flags.
        ///
        /// If the debugger and the process to be debugged (the attached process) share a single
        /// console, and if interop debugging is used, it is possible for the attached process to
        /// hold console locks and stop at a debug event. The debugger will then block any attempt
        /// to use the console. To avoid this problem, set the <c>CREATE_NEW_CONSOLE</c> flag in
        /// the <see paramref="dwCreationFlags" /> parameter.
        ///
        /// Interop debugging is not supported on Win9x and non-x86 platforms such as IA-64-based
        /// and AMD64-based platforms.
        /// </remarks>
        public int CreateProcess(
            ReadOnlySpan<char> lpApplicationName,
            ReadOnlySpan<char> lpCommandLine,
            in SECURITY_ATTRIBUTES lpProcessAttributes,
            in SECURITY_ATTRIBUTES lpThreadAttributes,
            int bInheritHandles,
            uint dwCreationFlags,
            void* lpEnvironment,
            ReadOnlySpan<char> lpCurrentDirectory,
            in STARTUPINFOW lpStartupInfo,
            in PROCESS_INFORMATION lpProcessInformation,
            CorDebugCreateProcessFlags debuggingFlags,
            out CorDebugProcess pProcess)
        {
            void* ppProcess = default;
            SECURITY_ATTRIBUTES processAttributes = lpProcessAttributes;
            SECURITY_ATTRIBUTES threadAttributes = lpThreadAttributes;
            STARTUPINFOW startupInfo = lpStartupInfo;
            PROCESS_INFORMATION processInformation = lpProcessInformation;
            fixed (void* plpApplicationName = lpApplicationName)
            fixed (void* plpCommandLine = lpCommandLine)
            fixed (void* plpCurrentDirectory = lpCurrentDirectory)
            {
                int result = Calli(
                    _this,
                    This[0]->CreateProcess,
                    plpApplicationName,
                    plpCommandLine,
                    &processAttributes,
                    &threadAttributes,
                    bInheritHandles,
                    dwCreationFlags,
                    lpEnvironment,
                    plpCurrentDirectory,
                    &startupInfo,
                    &processInformation,
                    (int)debuggingFlags,
                    &ppProcess);

                ComFactory.Create(ppProcess, out pProcess);
                return result;
            }
        }

        /// <summary>
        /// Attaches the debugger to an existing process.
        /// </summary>
        /// <param name="id">
        /// The ID of the process to which the debugger is to be attached.
        /// </param>
        /// <param name="win32Attach">
        /// Indicates whether the debugger should behave as the Win32 debugger for the process
        /// and dispatch the unmanaged callbacks.
        /// </param>
        /// <param name="process">
        /// The process to which the debugger has been attached.
        /// </param>
        public int DebugActiveProcess(uint id, bool win32Attach, out CorDebugProcess process)
        {
            void* pProcess = default;
            int result = Calli(_this, This[0]->DebugActiveProcess, id, win32Attach.ToNativeInt(), &pProcess);
            ComFactory.Create(pProcess, out process);
            return result;
        }

        /// <summary>Gets the processes that are being debugged.</summary>
        public int EnumerateProcesses(out CorDebugComEnum<CorDebugProcess> process)
            => InvokeGetObject(_this, This[0]->EnumerateProcesses, out process);

        /// <summary>
        /// Gets the <see cref="CorDebugProcess" /> instance for the specified process.
        /// </summary>
        /// <param name="dwProcessId">The ID of the process.</param>
        /// <param name="process">The instance for the specified process.</param>
        public int GetProcess(uint dwProcessId, out CorDebugProcess process)
        {
            void* pProcess = default;
            int result = Calli(_this, This[0]->GetProcess, dwProcessId, &pProcess);
            ComFactory.Create(pProcess, out process);
            return result;
        }

        /// <summary>
        /// Returns an HRESULT that indicates whether launching a new process or attaching
        /// to the specified existing process is possible within the context of the current
        /// machine and runtime configuration.
        /// </summary>
        /// <param name="dwProcessId">The ID of an existing process.</param>
        /// <param name="win32DebuggingEnabled">
        /// Indicates if you plan to launch with Win32 debugging enabled, or to attach with
        /// Win32 debugging enabled.
        /// </param>
        /// <returns>
        /// <c>S_OK</c> if the debugging services determine that launching a new process or
        /// attaching to the given process is possible, given the information about the current
        /// machine and runtime configuration. Possible HRESULT values are:
        ///
        /// <c>S_OK</c>
        ///
        /// <c>CORDBG_E_DEBUGGING_NOT_POSSIBLE</c>
        ///
        /// <c>CORDBG_E_KERNEL_DEBUGGER_PRESENT</c>
        ///
        /// <c>CORDBG_E_KERNEL_DEBUGGER_ENABLED</c>
        /// </returns>
        /// <remarks>
        /// This method is purely informational. The interface will not stop you from launching
        /// or attaching to a process, regardless of the value returned by this method.
        ///
        /// If you plan to launch with Win32 debugging enabled or attach with Win32 debugging
        /// enabled, pass <see langword="true" /> for <see paramref="win32DebuggingEnabled" />. The
        /// HRESULT returned by this method might differ if you use this option.
        /// </remarks>
        public int CanLaunchOrAttach(uint dwProcessId, int win32DebuggingEnabled)
            => Calli(_this, This[0]->CanLaunchOrAttach, dwProcessId, win32DebuggingEnabled);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* Initialize;

            public void* Terminate;

            public void* SetManagedHandler;

            public void* SetUnmanagedHandler;

            public void* CreateProcess;

            public void* DebugActiveProcess;

            public void* EnumerateProcesses;

            public void* GetProcess;

            public void* CanLaunchOrAttach;
        }
    }
}
