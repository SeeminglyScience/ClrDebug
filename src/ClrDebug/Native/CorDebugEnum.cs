using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Serves as the abstract base interface for the enumerators that are used by a
    /// debugging application.
    /// </summary>
    public abstract unsafe class CorDebugEnum<TItem> : Unknown
    {
        private protected CorDebugEnum()
        {
        }

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
        /// Gets the number of items in the enumeration.
        /// </summary>
        public int GetCount(out uint celt)
        {
            fixed (uint* count = &celt) return Calli(_this, This[0]->GetCount, count);
        }

        public int Next(uint count, void* next, uint* amountWritten)
        {
            return Calli(_this, This[0]->Next, count, next, amountWritten);
        }

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        public abstract int Clone(out CorDebugEnum<TItem> ppEnum);

        /// <summary>
        /// Gets the specified number of <see cref="T" /> objects from the enumeration, starting
        /// at the current position.
        /// </summary>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="amountWritten">The amount of objects written to <see paramref="next" />.</param>
        public abstract int Next(Span<TItem> buffer, out int amountWritten);

        /// <summary>
        /// Creates a span from the COM enumerator.
        /// </summary>
        public ReadOnlySpan<TItem> ToSpan()
        {
            GetCount(out uint uCount).MaybeThrowHr();
            int count = unchecked((int)uCount);
            var result = new TItem[count].AsSpan();
            Next(result, out count).MaybeThrowHr();
            return result.Slice(0, count);
        }

        /// <summary>
        /// Creates an array from the COM enumerator.
        /// </summary>
        public TItem[] ToArray()
        {
            GetCount(out uint uCount).MaybeThrowHr();
            int count = unchecked((int)uCount);
            var result = new TItem[count];
            Next(result, out count).MaybeThrowHr();
            if (result.Length != count)
            {
                Array.Resize(ref result, count);
            }

            return result;
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

    public class CorDebugStructEnum<T> : CorDebugEnum<T>, IEnumerable<T>
        where T : unmanaged
    {
        /// <inheritdoc />
        public override unsafe int Clone(out CorDebugEnum<T> ppEnum)
        {
            void** clonedEnum = default;
            int hResult = Calli(_this, This[0]->Clone, &clonedEnum);
            ppEnum = ComFactory.Create<CorDebugStructEnum<T>>(clonedEnum, hResult);
            return hResult;
        }

        /// <inheritdoc />
        public override unsafe int Next(Span<T> buffer, out int amountWritten)
        {
            fixed (void* pBuffer = buffer)
            fixed (int* pAmountWritten = &amountWritten)
            {
                return Next((uint)buffer.Length, pBuffer, (uint*)pAmountWritten);
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public struct Enumerator : IEnumerator<T>
        {
            private readonly CorDebugEnum<T> _parent;

            private readonly bool _ownsParent;

            private T _obj;

            internal Enumerator(CorDebugStructEnum<T> parent)
            {
                int hResult = parent.Clone(out _parent);
                _ownsParent = hResult != HResult.E_NOTIMPL;

                if (_ownsParent)
                {
                    hResult.MaybeThrowHr();
                }
                else
                {
                    _parent = parent;
                }

                _obj = default;
            }

            public unsafe T Current => _obj;

            object IEnumerator.Current => Current;

            public unsafe bool MoveNext()
            {
                fixed (T* ptr = &_obj)
                {
                    _obj = default;
                    int hResult = _parent.Next(1, ptr, default);
                    if (hResult == HResult.E_FAIL)
                    {
                        return false;
                    }

                    hResult.MaybeThrowHr();
                    return true;
                }
            }

            public void Reset() => _parent.Reset().MaybeThrowHr();

            public void Dispose()
            {
                if (_ownsParent)
                {
                    _parent.Dispose();
                }
            }
        }
    }

    /// <summary>
    /// Represents any non-abstract interface that inherits ICorDebugEnum.
    /// </summary>
    public class CorDebugComEnum<T> : CorDebugEnum<T>, IEnumerable<T>
        where T : IComReference, new()
    {
        /// <inheritdoc />
        public override unsafe int Clone(out CorDebugEnum<T> ppEnum)
        {
            void** clonedEnum = default;
            int hResult = Calli(_this, This[0]->Clone, &clonedEnum);
            ppEnum = ComFactory.Create<CorDebugComEnum<T>>(clonedEnum, hResult);
            return hResult;
        }

        /// <inheritdoc />
        public override unsafe int Next(Span<T> buffer, out int amountWritten)
        {
            int count = buffer.Length;
            Span<IntPtr> nativeBuffer = count < 60 ? stackalloc IntPtr[count] : new IntPtr[count];
            fixed (IntPtr* pNativeBuffer = nativeBuffer)
            {
                var b = (void***)pNativeBuffer;
                uint celtFetched = default;
                int result = Next(unchecked((uint)count), b, &celtFetched);
                amountWritten = unchecked((int)celtFetched);
                for (int i = 0; i < amountWritten; i++)
                {
                    buffer[i] = ComFactory.Create<T>(b[i]);
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

            private readonly bool _ownsParent;

            private unsafe void** _ptr;

            internal unsafe Enumerator(CorDebugComEnum<T> parent)
            {
                int hResult = parent.Clone(out _parent);
                _ownsParent = hResult != HResult.E_NOTIMPL;
                if (_ownsParent)
                {
                    hResult.MaybeThrowHr();
                }
                else
                {
                    _parent = parent;
                }

                _ptr = default;
            }

            public unsafe T Current => ComFactory.Create<T>(_ptr);

            object IEnumerator.Current => Current;

            public unsafe bool MoveNext()
            {
                fixed (void*** ptr = &_ptr)
                {
                    int hResult = _parent.Next(1, ptr, default);
                    if (hResult == HResult.E_FAIL)
                    {
                        return false;
                    }

                    hResult.MaybeThrowHr();
                    return true;
                }
            }

            public void Reset() => _parent.Reset().MaybeThrowHr();

            public void Dispose()
            {
                if (_ownsParent)
                {
                    _parent.Dispose();
                }
            }
        }
    }
}
