using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugModule : Unknown
    {
        private ICorDebugModuleVtable** This => (ICorDebugModuleVtable**)DangerousGetPointer();

        public int GetProcess(out CorDebugProcess process) => InvokeGetObject(_this, This[0]->GetProcess, out process);

        public int GetBaseAddress(out ulong address) => InvokeGet(_this, This[0]->GetBaseAddress, out address);

        public int GetAssembly(out CorDebugAssembly assembly) => InvokeGetObject(_this, This[0]->GetAssembly, out assembly);

        public int GetName(ref Span<char> szName, out uint charsUsed)
        {
            fixed (void* pCharsUsed = &charsUsed)
            fixed (void* pSzName = szName)
            {
                return Calli(_this, This[0]->GetName, (uint)szName.Length, pCharsUsed, pSzName);
            }
        }

        public int EnableJITDebugging(bool bTrackJITInfo, bool bAllowJitOpts)
        {
            return Calli(_this, This[0]->EnableJITDebugging, bTrackJITInfo.ToNativeInt(), bAllowJitOpts.ToNativeInt());
        }

        public int EnableClassLoadCallbacks(bool bClassLoadCallbacks)
        {
            return Calli(_this, This[0]->EnableClassLoadCallbacks, bClassLoadCallbacks.ToNativeInt());
        }

        public int GetFunctionFromToken(uint methodDef, out CorDebugFunction function)
        {
            void** pFunction = default;
            int result = Calli(_this, This[0]->GetFunctionFromToken, methodDef, &pFunction);
            ComFactory.Create(pFunction, out function);
            return result;
        }

        public int GetFunctionFromRVA(ulong rva, out CorDebugFunction function)
        {
            void** pFunction = default;
            int result = Calli(_this, This[0]->GetFunctionFromRVA, rva, &pFunction);
            ComFactory.Create(pFunction, out function);
            return result;
        }

        public int GetClassFromToken(uint methodDef, out CorDebugClass @class)
        {
            void** pClass = default;
            int result = Calli(_this, This[0]->GetClassFromToken, methodDef, &pClass);
            ComFactory.Create(pClass, out @class);
            return result;
        }

        public int CreateBreakpoint(out CorDebugModuleBreakpoint breakpoint)
            => InvokeGetObject(_this, This[0]->CreateBreakpoint, out breakpoint);

        public int GetEditAndContinueSnapshot(out CorDebugEditAndContinueSnapshot editAndContinueSnapshot)
            => InvokeGetObject(_this, This[0]->GetEditAndContinueSnapshot, out editAndContinueSnapshot);

        public int GetMetaDataInterface(Guid riid, out Unknown obj)
        {
            void** pObj = default;
            int result = Calli(_this, This[0]->GetMetaDataInterface, &riid, &pObj);
            ComFactory.Create(pObj, out obj);
            return result;
        }

        public int GetToken(out uint token) => InvokeGet(_this, This[0]->GetToken, out token);

        public int IsDynamic(out bool dynamic) => InvokeGet(_this, This[0]->IsDynamic, out dynamic);

        public int GetGlobalVariable(uint fieldDef, out CorDebugValue value)
        {
            void** pValue = default;
            int result = Calli(_this, This[0]->GetGlobalVariable, fieldDef, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        public int GetSize(out uint cBytes) => InvokeGet(_this, This[0]->GetSize, out cBytes);

        public int IsInMemory(out bool inMemory) => InvokeGet(_this, This[0]->IsInMemory, out inMemory);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugModuleVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetProcess;

            public void* GetBaseAddress;

            public void* GetAssembly;

            public void* GetName;

            public void* EnableJITDebugging;

            public void* EnableClassLoadCallbacks;

            public void* GetFunctionFromToken;

            public void* GetFunctionFromRVA;

            public void* GetClassFromToken;

            public void* CreateBreakpoint;

            public void* GetEditAndContinueSnapshot;

            public void* GetMetaDataInterface;

            public void* GetToken;

            public void* IsDynamic;

            public void* GetGlobalVariable;

            public void* GetSize;

            public void* IsInMemory;
        }
    }
}
