using System;
using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CorDebugManagedCallback
    {
        public ICorDebugManagedCallbackVtable* Vtable;

        public static CorDebugManagedCallback* Alloc()
        {
            ICorDebugManagedCallbackVtable* pVtable =
                (ICorDebugManagedCallbackVtable*)Marshal.AllocHGlobal(sizeof(ICorDebugManagedCallbackVtable));

            CorDebugManagedCallback* pCallback =
                (CorDebugManagedCallback*)Marshal.AllocHGlobal(sizeof(CorDebugManagedCallback));

            pCallback->Vtable = pVtable;

            return pCallback;
        }

        public static void Free(CorDebugManagedCallback* pCallback)
        {
            if (pCallback == default)
            {
                return;
            }

            ICorDebugManagedCallbackVtable* vtable = pCallback->Vtable;
            if (vtable != default)
            {
                Marshal.FreeHGlobal(new IntPtr(vtable));
            }

            Marshal.FreeHGlobal(new IntPtr(pCallback));
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ICorDebugManagedCallbackVtable
    {
            public void* QueryInterface;

            public void* AddRef;

            public void* Release;

            public void* Breakpoint;

            public void* StepComplete;

            public void* Break;

            public void* Exception;

            public void* EvalComplete;

            public void* EvalException;

            public void* CreateProcess;

            public void* ExitProcess;

            public void* CreateThread;

            public void* ExitThread;

            public void* LoadModule;

            public void* UnloadModule;

            public void* LoadClass;

            public void* UnloadClass;

            public void* DebuggerError;

            public void* LogMessage;

            public void* LogSwitch;

            public void* CreateAppDomain;

            public void* ExitAppDomain;

            public void* LoadAssembly;

            public void* UnloadAssembly;

            public void* ControlCTrap;

            public void* NameChange;

            public void* UpdateModuleSymbols;

            public void* EditAndContinueRemap;

            public void* BreakpointSetError;
    }
}
