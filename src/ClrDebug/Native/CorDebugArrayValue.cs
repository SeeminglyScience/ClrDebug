using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// A subclass of <c>ICorDebugHeapValue</c> that represents a
    /// single-dimensional or multi-dimensional array.
    /// </summary>
    /// <remarks>
    /// <c>ICorDebugArrayValue</c> supports both single-dimensional
    /// and multi-dimensional arrays.
    ///
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugArrayValue : CorDebugHeapValue
    {
        internal CorDebugArrayValue()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the base index of each dimension in the array.
        /// </summary>
        /// <param name="indicies">
        /// An array of integers, each of which is the base index (that is,
        /// the starting index) of a dimension of this <c>ICorDebugArrayValue</c>
        /// object.
        /// </param>
        public int GetBaseIndicies(Span<uint> indicies)
        {
            fixed (void* pIndicies = indicies)
            {
                return Calli(_this, This[0]->GetBaseIndicies, indicies.Length, pIndicies);
            }
        }

        /// <summary>
        /// Gets the total number of elements in the array.
        /// </summary>
        /// <param name="count">
        /// A pointer to the total number of elements in the array.
        /// </param>
        public int GetCount(out int count) => InvokeGet(_this, This[0]->GetCount, out count);

        /// <summary>
        /// Gets the number of elements in each dimension of this array.
        /// </summary>
        /// <param name="dims">
        /// An array of integers, each of which specifies the number of
        /// elements in a dimension in this <c>ICorDebugArrayValue</c>
        /// object.
        /// </param>
        public int GetDimensions(Span<int> dims)
        {
            fixed (void* pDims = dims)
            {
                return Calli(_this, This[0]->GetDimensions, dims.Length, pDims);
            }
        }

        /// <summary>
        /// Gets the value of the given array element.
        /// </summary>
        /// <param name="indices">
        /// An array of index values, each of which specifies a position within a dimension
        /// of the <c>ICorDebugArrayValue</c> object.
        ///
        /// This value must not be null.
        /// </param>
        /// <param name="value">
        /// A pointer to the address of an <c>ICorDebugValue</c> object that represents
        /// the value of the specified element.
        /// </param>
        public int GetElement(Span<uint> indices, out CorDebugValue value)
        {
            void** pValue = default;
            fixed (void* pIndices = indices)
            {
                int hResult = Calli(_this, This[0]->GetElement, indices.Length, pIndices, &pValue);
                ComFactory.Create(pValue, hResult, out value);
                return hResult;
            }
        }

        /// <summary>
        /// Gets the element at the given position, treating the array as
        /// a zero-based, single-dimensional array.
        /// </summary>
        /// <param name="position">
        /// The position of the element to be retrieved.
        /// </param>
        /// <param name="value">
        /// A pointer to the address of an <c>ICorDebugValue</c> object that
        /// represents the value of the element.
        /// </param>
        /// <remarks>
        /// The layout of a multi-dimension array follows the C++ style of
        /// array layout.
        /// </remarks>
        public int GetElementAtPosition(uint position, out CorDebugValue value)
        {
            void** pValue = default;
            int hResult = Calli(_this, This[0]->GetElementAtPosition, position, &pValue);
            ComFactory.Create(pValue, hResult, out value);
            return hResult;
        }

        /// <summary>
        /// Gets a value that indicates the simple type of the elements
        /// in the array.
        /// </summary>
        /// <param name="type">
        /// A pointer to a value of the <c>CorElementType</c> enumeration
        /// that indicates the type.
        /// </param>
        public int GetElementType(out CorElementType type)
        {
            fixed (void* pType = &type)
            {
                return Calli(_this, This[0]->GetElementType, pType);
            }
        }

        /// <summary>
        /// Gets the number of dimensions in the array.
        /// </summary>
        /// <param name="rank">
        /// A pointer to the number of dimensions in this
        /// <c>ICorDebugArrayValue</c> object.
        /// </param>
        public int GetRank(out uint rank) => InvokeGet(_this, This[0]->GetRank, out rank);

        /// <summary>
        /// Gets a value that indicates whether any dimensions of this array
        /// have a base index of non-zero.
        /// </summary>
        /// <param name="hasBaseIndicies">
        /// A pointer to a Boolean value that is <c>true</c> if one or more
        /// dimensions of this <c>ICorDebugArrayValue</c> object have a base
        /// index of non-zero; otherwise, the Boolean value is <c>false</c>.
        /// </param>
        public int HasBaseIndicies(out bool hasBaseIndicies)
            => InvokeGet(_this, This[0]->HasBaseIndicies, out hasBaseIndicies);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public CorDebugHeapValue.Vtable ICorDebugHeapValue;

            public void* GetElementType;

            public void* GetRank;

            public void* GetCount;

            public void* GetDimensions;

            public void* HasBaseIndicies;

            public void* GetBaseIndicies;

            public void* GetElement;

            public void* GetElementAtPosition;
        }
    }
}
