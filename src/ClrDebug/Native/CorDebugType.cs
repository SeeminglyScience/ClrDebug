using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugType : Unknown
    {
        private ICorDebugTypeVtable** This => (ICorDebugTypeVtable**)DangerousGetPointer();

        public int GetType(out CorElementType ty)
        {
            int pTy = default;
            int result = Calli(_this, This[0]->GetType, &pTy);
            ty = (CorElementType)pTy;
            return result;
        }

        public int GetClass(out CorDebugClass @class)
            => InvokeGetObject(_this, This[0]->GetClass, out @class);

        public int EnumerateTypeParameters(out CorDebugEnum<CorDebugType> tyParEnum)
            => InvokeGetObject(_this, This[0]->EnumerateTypeParameters, out tyParEnum);

        public int GetFirstTypeParameter(out CorDebugType value)
            => InvokeGetObject(_this, This[0]->GetFirstTypeParameter, out value);

        public int GetBase(out CorDebugType @base) => InvokeGetObject(_this, This[0]->GetBase, out @base);

        public int GetStaticFieldValue(uint fieldDef, CorDebugFrame frame, out CorDebugValue value)
        {
            using var pFrame = frame.AcquirePointer();
            void** pValue = default;
            int result = Calli(_this, This[0]->GetStaticFieldValue, fieldDef, pFrame, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        public int GetRank(out uint nRank) => InvokeGet(_this, This[0]->GetRank, out nRank);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugTypeVtable
        {
            public IUnknownVtable IUnknown;

            public new void* GetType;

            public void* GetClass;

            public void* EnumerateTypeParameters;

            public void* GetFirstTypeParameter;

            public void* GetBase;

            public void* GetStaticFieldValue;

            public void* GetRank;
        }
    }
}
