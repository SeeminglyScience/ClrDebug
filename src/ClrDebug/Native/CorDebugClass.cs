using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugClass : Unknown
    {
        private ICorDebugClassVtable** This => (ICorDebugClassVtable**)DangerousGetPointer();

        public int GetModule(out CorDebugModule module)
            => InvokeGetObject(_this, This[0]->GetModule, out module);

        public int GetToken(out uint typeDef) => InvokeGet(_this, This[0]->GetToken, out typeDef);

        public int GetStaticFieldValue(uint fieldDef, CorDebugFrame frame, out CorDebugValue value)
        {
            using var pFrame = frame.AquirePointer();
            void* pValue = default;
            int result = Calli(_this, This[0]->GetStaticFieldValue, fieldDef, pFrame, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugClassVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetModule;

            public void* GetToken;

            public void* GetStaticFieldValue;
        }
    }
}
