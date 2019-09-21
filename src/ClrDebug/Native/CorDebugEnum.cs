using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugEnum : Unknown
    {
        private protected ICorDebugEnumVtable** This => (ICorDebugEnumVtable**)DangerousGetPointer();

        public int Skip(uint celt) => Calli(_this, This[0]->Skip, celt);

        public int Reset() => Calli(_this, This[0]->Reset);

        public int Clone(out CorDebugEnum ppEnum)
            => InvokeGetObject(_this, This[0]->Clone, out ppEnum);

        public int GetCount(out uint celt)
        {
            fixed (uint* count = &celt) return Calli(_this, This[0]->GetCount, count);
        }

        public int Next(uint celt, void*** next, out uint celtFetched)
        {
            fixed (uint* pCeltFetched = &celtFetched)
            {
                return Calli(_this, This[0]->Next, celt, next, pCeltFetched);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private protected unsafe struct ICorDebugEnumVtable
        {
            public IUnknownVtable IUnknown;

            public void* Skip;

            public void* Reset;

            public void* Clone;

            public void* GetCount;

            public void* Next;
        }
    }

    public class CorDebugEnum<T> : CorDebugEnum, IEnumerable<T>
        where T : IComReference, new()
    {
        public unsafe int Clone(out CorDebugEnum<T> ppEnum) => InvokeGetObject(_this, This[0]->Clone, out ppEnum);

        public ReadOnlySpan<T> ToSpan()
        {
            GetCount(out uint uCount).MaybeThrowHr();
            int count = unchecked((int)uCount);
            var result = new T[count].AsSpan();
            Next(result, out count).MaybeThrowHr();
            return result.Slice(0, count);
        }

        public T[] ToArray()
        {
            GetCount(out uint uCount).MaybeThrowHr();
            int count = unchecked((int)uCount);
            var result = new T[count];
            Next(result, out count).MaybeThrowHr();
            if (result.Length != count)
            {
                Array.Resize(ref result, count);
            }

            return result;
        }

        public unsafe int Next(Span<T> next, out int amountWritten)
        {
            int count = next.Length;
            Span<IntPtr> buffer = count < 60 ? stackalloc IntPtr[count] : new IntPtr[count];

            fixed (IntPtr* pBuffer = buffer)
            {
                var b = (void***)pBuffer;
                int result = Next(unchecked((uint)count), b, out uint celtFetched);
                amountWritten = unchecked((int)celtFetched);
                for (int i = 0; i < amountWritten; i++)
                {
                    next[i] = ComFactory.Create<T>(b[i]);
                }

                return result;
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<T>
        {
            private readonly CorDebugEnum<T> _parent;

            private unsafe void** _ptr;

            private T _obj;

            internal unsafe Enumerator(CorDebugEnum<T> parent)
            {
                parent.Clone(out _parent).MaybeThrowHr();
                if (_parent == null)
                {
                    throw new ArgumentException(
                        "Unable to clone enumerator.",
                        nameof(parent));
                }

                _ptr = default;
                _obj = default;
            }

            private static unsafe T CreateComObject(void** ptr)
                => ptr == null
                    ? default
                    : ComFactory.Create<T>(ptr);

            public unsafe T Current => _obj ??= CreateComObject(_ptr);

            object IEnumerator.Current => Current;

            public unsafe bool MoveNext()
            {
                fixed (void*** ptr = &_ptr)
                {
                    _obj = default;
                    _parent.Next(1, ptr, out uint celtFetched);
                    return celtFetched == 1;
                }
            }

            public void Reset() => _parent.Reset().MaybeThrowHr();

            public void Dispose() => _parent.Dispose();
        }
    }
}
