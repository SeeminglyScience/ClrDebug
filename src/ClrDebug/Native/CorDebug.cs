using System;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ClrDebug.Native
{
    public unsafe class CorDebug : Unknown
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CorDebug() : base()
        {
        }

        private ICorDebugVtable** This => (ICorDebugVtable**)DangerousGetPointer();

        public int Initialize() => Calli(_this, This[0]->Initialize);

        public int Terminate() => Calli(_this, This[0]->Terminate);

        public int SetManagedHandler(CorDebugManagedCallback* pCallback)
            => Calli(_this, This[0]->SetManagedHandler, pCallback);

        public int SetUnmanagedHandler(void* pCallback)
            => Calli(_this, This[0]->SetUnmanagedHandler, pCallback);

        public int CreateProcess(
            in ReadOnlySpan<char> lpApplicationName,
            in ReadOnlySpan<char> lpCommandLine,
            in SECURITY_ATTRIBUTES lpProcessAttributes,
            in SECURITY_ATTRIBUTES lpThreadAttributes,
            int bInheritHandles,
            ulong dwCreationFlags,
            void* lpEnvironment,
            in ReadOnlySpan<char> lpCurrentDirectory,
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

        public int DebugActiveProcess(uint id, bool win32Attach, out CorDebugProcess process)
        {
            void* pProcess = default;
            int result = Calli(_this, This[0]->DebugActiveProcess, id, win32Attach.ToNativeInt(), &pProcess);
            ComFactory.Create(pProcess, out process);
            return result;
        }

        public int EnumerateProcesses(out CorDebugEnum process)
            => InvokeGetObject(_this, This[0]->EnumerateProcesses, out process);

        public int GetProcess(uint dwProcessId, out CorDebugProcess process)
        {
            void* pProcess = default;
            int result = Calli(_this, This[0]->GetProcess, dwProcessId, &pProcess);
            ComFactory.Create(pProcess, out process);
            return result;
        }

        public int CanLaunchOrAttach(uint dwProcessId, int win32DebuggingEnabled)
            => Calli(_this, This[0]->CanLaunchOrAttach, dwProcessId, win32DebuggingEnabled);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugVtable
        {
            public IUnknownVtable IUnknown;

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
