using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugAppDomain : Unknown
    {
        private ICorDebugAppDomainVtable** This => (ICorDebugAppDomainVtable**)DangerousGetPointer();

        public int GetProcess(out CorDebugProcess process)
            => InvokeGetObject(_this, This[0]->GetProcess, out process);

        public int EnumerateAssemblies(out CorDebugEnum<CorDebugAssembly> assemblies)
            => InvokeGetObject(_this, This[0]->EnumerateAssemblies, out assemblies);

        public int GetModuleFromMetaDataInterface(in Unknown metadata, out CorDebugModule module)
        {
            void* pModule = default;
            int result = Calli(_this, This[0]->GetModuleFromMetaDataInterface, metadata, &pModule);
            module = ComFactory.Create<CorDebugModule>((void**)pModule);
            return result;
        }

        public int EnumerateBreakpoints(out CorDebugEnum<CorDebugBreakpoint> breakpoints)
            => InvokeGetObject(_this, This[0]->EnumerateBreakpoints, out breakpoints);

        public int EnumerateSteppers(out CorDebugEnum<CorDebugStepper> steppers)
            => InvokeGetObject(_this, This[0]->EnumerateSteppers, out steppers);

        public int IsAttached(out bool bAttached)
            => InvokeGet(_this, This[0]->IsAttached, out bAttached);

        public int GetName(ref Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetName, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        public int GetObject(out CorDebugValue @object) => InvokeGetObject(_this, This[0]->GetObject, out @object);

        public int Attach() => Calli(_this, This[0]->Attach);

        public int GetID(out uint id) => InvokeGet(_this, This[0]->GetID, out id);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugAppDomainVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetProcess;

            public void* EnumerateAssemblies;

            public void* GetModuleFromMetaDataInterface;

            public void* EnumerateBreakpoints;

            public void* EnumerateSteppers;

            public void* IsAttached;

            public void* GetName;

            public void* GetObject;

            public void* Attach;

            public void* GetID;
        }
    }
}
