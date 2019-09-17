using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugEnum : Unknown
    {
        private ICorDebugEnumVtable** This => (ICorDebugEnumVtable**)DangerousGetPointer();

        public int Skip(ulong celt) => Calli(_this, This[0]->Skip, celt);

        public int Reset() => Calli(_this, This[0]->Reset);

        public int Clone(out CorDebugEnum ppEnum)
            => InvokeGetObject(_this, This[0]->Clone, out ppEnum);

        public int GetCount(out ulong celt)
        {
            fixed (ulong* count = &celt) return Calli(_this, This[0]->GetCount, count);
        }

        public int Next(ulong celt, out void** next, out ulong celtFetched)
        {
            fixed (void*** pNext = &next)
            fixed (ulong* pCeltFetched = &celtFetched)
            {
                return Calli(_this, This[0]->Next, celt, (void**)pNext, pCeltFetched);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugEnumVtable
        {
            public IUnknownVtable IUnknown;

            public void* Skip;

            public void* Reset;

            public void* Clone;

            public void* GetCount;

            public void* Next;
        }
    }

    public unsafe class CorDebugEnum<T> : CorDebugEnum
        where T : IComReference, new()
    {
        public int Next(ulong celt, out T next, out ulong celtFetched)
        {
            int result = Next(celt, out void** pNext, out celtFetched);
            next = ComFactory.Create<T>(pNext);
            return result;
        }
    }
}
