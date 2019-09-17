using System;
using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CorDebugManagedCallback2
    {
        public ICorDebugManagedCallback2Vtable* Vtable;

        public static CorDebugManagedCallback2* Alloc()
        {
            ICorDebugManagedCallback2Vtable* pVtable =
                (ICorDebugManagedCallback2Vtable*)Marshal.AllocHGlobal(sizeof(ICorDebugManagedCallback2Vtable));

            CorDebugManagedCallback2* pCallback =
                (CorDebugManagedCallback2*)Marshal.AllocHGlobal(sizeof(CorDebugManagedCallback2));

            pCallback->Vtable = pVtable;

            return pCallback;
        }

        public static void Free(CorDebugManagedCallback2* pCallback)
        {
            if (pCallback == default)
            {
                return;
            }

            ICorDebugManagedCallback2Vtable* vtable = pCallback->Vtable;
            if (vtable != default)
            {
                Marshal.FreeHGlobal(new IntPtr(vtable));
            }

            Marshal.FreeHGlobal(new IntPtr(pCallback));
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ICorDebugManagedCallback2Vtable
    {
        public void* QueryInterface;

        public void* AddRef;

        public void* Release;

        public void* FunctionRemapOpportunity;

        public void* CreateConnection;

        public void* ChangeConnection;

        public void* DestroyConnection;

        public void* Exception;

        public void* ExceptionUnwind;

        public void* FunctionRemapComplete;

        public void* MDANotification;
    }
}
