using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugAssembly : Unknown
    {
        private ICorDebugAssemblyVtable** This => (ICorDebugAssemblyVtable**)DangerousGetPointer();

        public int GetProcess(out CorDebugProcess process)
            => InvokeGetObject(_this, This[0]->GetProcess, out process);

        public int GetAppDomain(out CorDebugAppDomain appDomain)
            => InvokeGetObject(_this, This[0]->GetAppDomain, out appDomain);

        public int EnumerateModules(out CorDebugEnum<CorDebugModule> modules)
            => InvokeGetObject(_this, This[0]->EnumerateModules, out modules);

        public int GetCodeBase(ref Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetCodeBase, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        public int GetName(ref Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetName, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugAssemblyVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetProcess;

            public void* GetAppDomain;

            public void* EnumerateModules;

            public void* GetCodeBase;

            public void* GetName;
        }
    }
}
