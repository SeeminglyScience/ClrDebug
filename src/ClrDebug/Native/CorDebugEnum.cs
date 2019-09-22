using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;
using System.ComponentModel;

namespace ClrDebug.Native
{
    /// <summary>
    /// Serves as the abstract base interface for the enumerators that are used by a
    /// debugging application.
    /// </summary>
    public unsafe class CorDebugEnum : Unknown
    {
        private protected ICorDebugEnumVtable** This => (ICorDebugEnumVtable**)DangerousGetPointer();

        /// <summary>
        /// Moves the cursor forward in the enumeration by the specified number of items.
        /// </summary>
        public int Skip(uint celt) => Calli(_this, This[0]->Skip, celt);

        /// <summary>
        /// Moves the cursor to the beginning of the enumeration.
        /// </summary>
        public int Reset() => Calli(_this, This[0]->Reset);

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        public int Clone(out CorDebugEnum ppEnum)
            => InvokeGetObject(_this, This[0]->Clone, out ppEnum);

        /// <summary>
        /// Gets the number of items in the enumeration.
        /// </summary>
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

    /// <summary>
    /// Represents any non-abstract interface that inherits ICorDebugEnum.
    /// </summary>
    public class CorDebugEnum<T> : CorDebugEnum, IEnumerable<T>
        where T : IComReference, new()
    {
        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        public unsafe int Clone(out CorDebugEnum<T> ppEnum) => InvokeGetObject(_this, This[0]->Clone, out ppEnum);

        /// <summary>
        /// Creates a span from the COM enumerator.
        /// </summary>
        public ReadOnlySpan<T> ToSpan()
        {
            GetCount(out uint uCount).MaybeThrowHr();
            int count = unchecked((int)uCount);
            var result = new T[count].AsSpan();
            Next(result, out count).MaybeThrowHr();
            return result.Slice(0, count);
        }

        /// <summary>
        /// Creates an array from the COM enumerator.
        /// </summary>
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

        /// <summary>
        /// Gets the specified number of <see cref="T" /> objects from the enumeration, starting
        /// at the current position.
        /// </summary>
        /// <param name="next">The destination buffer.</param>
        /// <param name="amountWritten">The amount of objects written to <see paramref="next" />.</param>
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

        [EditorBrowsable(EditorBrowsableState.Never)]
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
