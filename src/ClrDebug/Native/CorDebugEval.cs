using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugEval : Unknown
    {
        private ICorDebugEvalVtable** This => (ICorDebugEvalVtable**)DangerousGetPointer();

        public int CallFunction(CorDebugFunction function, ReadOnlySpan<CorDebugValue> args)
        {
            using var pArgs = NativeArray.AllocCom(args);
            return Calli(_this, This[0]->CallFunction, function, (void**)pArgs);
        }

        public int NewObject(CorDebugFunction constructor, ReadOnlySpan<CorDebugValue> args)
        {
            using var pArgs = NativeArray.AllocCom(args);
            return Calli(_this, This[0]->NewObject, constructor, (void**)pArgs);
        }

        public int NewObjectNoConstructor(CorDebugClass @class)
        {
            using var pClass = @class.AcquirePointer();
            return Calli(_this, This[0]->NewObjectNoConstructor, pClass);
        }

        public int NewString(Span<char> @string)
        {
            fixed (void* pString = @string)
            {
                return Calli(_this, This[0]->NewString, @pString);
            }
        }

        public int NewArray(
            CorElementType elementType,
            CorDebugClass elementClass,
            uint rank,
            Span<uint> dims,
            Span<uint> lowBounds)
        {
            fixed (void* pDims = dims)
            fixed (void* pLowBounds = lowBounds)
            {
                using var pElementClass = elementClass.AcquirePointer();
                return Calli(_this, This[0]->NewArray, (int)elementType, pElementClass, rank, pDims, pLowBounds);
            }
        }

        public int IsActive(out bool bActive) => InvokeGet(_this, This[0]->IsActive, out bActive);

        public int Abort() => Calli(_this, This[0]->Abort);

        public int GetResult(out CorDebugValue result) => InvokeGetObject(_this, This[0]->GetResult, out result);

        public int GetThread(out CorDebugThread thread) => InvokeGetObject(_this, This[0]->GetThread, out thread);

        public int CreateValue(CorElementType elementType, CorDebugClass elementClass, out CorDebugValue value)
        {
            using var pElementClass = elementClass.AcquirePointer();
            void* pValue = default;
            int result = Calli(_this, This[0]->CreateValue, (int)elementType, pElementClass, &pValue);
            ComFactory.Create((void**)pValue, out value);
            return result;
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugEvalVtable
        {
            public IUnknownVtable IUnknown;

            public void* CallFunction;

            public void* NewObject;

            public void* NewObjectNoConstructor;

            public void* NewString;

            public void* NewArray;

            public void* IsActive;

            public void* Abort;

            public void* GetResult;

            public void* GetThread;

            public void* CreateValue;
        }
    }
}
