using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    public unsafe class CorDebugFunction : Unknown
    {
        private ICorDebugFunctionVtable** This => (ICorDebugFunctionVtable**)DangerousGetPointer();

        public int GetModule(out CorDebugModule module) => InvokeGetObject(_this, This[0]->GetModule, out module);

        public int GetClass(out CorDebugClass @class) => InvokeGetObject(_this, This[0]->GetClass, out @class);

        public int GetToken(out uint methodDef) => InvokeGet(_this, This[0]->GetToken, out methodDef);

        public int GetILCode(out CorDebugCode code) => InvokeGetObject(_this, This[0]->GetILCode, out code);

        public int GetNativeCode(out CorDebugCode code) => InvokeGetObject(_this, This[0]->GetNativeCode, out code);

        public int CreateBreakpoint(out CorDebugFunctionBreakpoint breakpoint)
            => InvokeGetObject(_this, This[0]->CreateBreakpoint, out breakpoint);

        public int GetLocalVarSigToken(out uint mdSig) => InvokeGet(_this, This[0]->GetLocalVarSigToken, out mdSig);

        public int GetCurrentVersionNumber(out uint nCurrentVersion)
            => InvokeGet(_this, This[0]->GetCurrentVersionNumber, out nCurrentVersion);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugFunctionVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetModule;

            public void* GetClass;

            public void* GetToken;

            public void* GetILCode;

            public void* GetNativeCode;

            public void* CreateBreakpoint;

            public void* GetLocalVarSigToken;

            public void* GetCurrentVersionNumber;
        }
    }
}
