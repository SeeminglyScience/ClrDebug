using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides methods to work with arrays, pointers, function pointers,
    /// and reference types. This interface is an extension of the
    /// <c>ICorDebugAppDomain</c> interface.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either
    /// cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugAppDomain2 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets an array of the specified type, or a pointer or reference
        /// to the specified type.
        /// </summary>
        /// <param name="elementType">
        /// A value of the <c>CorElementType</c> enumeration that specifies the
        /// underlying native type (an array, pointer, or reference) to be created.
        /// </param>
        /// <param name="nRank">
        /// The rank (that is, number of dimensions) of the array. This value must
        /// be <c>0</c> if <c>elementType</c> specifies a pointer or reference type.
        /// </param>
        /// <param name="typeArg">
        /// A pointer to an <c>ICorDebugType</c> object that represents the type of array,
        /// pointer, or reference to be created.
        /// </param>
        /// <param name="type">
        /// A pointer to the address of an <c>ICorDebugType</c> object that represents
        /// the constructed array, pointer type, or reference type.
        /// </param>
        /// <remarks>
        /// The value of elementType must be one of the following:
        ///
        /// - <c>ELEMENT_TYPE_PTR</c>
        /// - <c>ELEMENT_TYPE_BYREF</c>
        /// - <c>ELEMENT_TYPE_ARRAY</c> or <c>ELEMENT_TYPE_SZARRAY</c>
        ///
        /// If the value of elementType is <c>ELEMENT_TYPE_PTR</c> or <c>ELEMENT_TYPE_BYREF</c>,
        /// <c>nRank</c> must be zero.
        /// </remarks>
        public int GetArrayOrPointerType(
            CorElementType elementType,
            int nRank,
            CorDebugType typeArg,
            out CorDebugType type)
        {
            void** pType = default;
            using var pTypeArg = typeArg?.AcquirePointer();
            int hResult = Calli(
                _this,
                This[0]->GetArrayOrPointerType,
                (uint)elementType,
                nRank,
                pTypeArg,
                &pType);

            ComFactory.Create(pType, hResult, out type);
            return hResult;
        }

        /// <summary>
        /// Gets a pointer to a function that has a given signature.
        /// </summary>
        /// <param name="nTypeArgs">
        /// The number of type arguments for the function.
        /// </param>
        /// <param name="typeArgs">
        /// An array of pointers, each of which points to an <c>ICorDebugType</c>
        /// object that represents a type argument of the function. The
        /// first element is the return type; each of the other elements is
        /// a parameter type.
        /// </param>
        /// <param name="type">
        /// A pointer to the address of an <c>ICorDebugType</c> object that
        /// represents the pointer to the function.
        /// </param>
        public int GetFunctionPointerType(ReadOnlySpan<CorDebugType> typeArgs, out CorDebugType type)
        {
            void** pType = default;
            using var pTypeArgs = NativeArray.AllocCom(typeArgs);
            int hResult = Calli(_this, This[0]->GetFunctionPointerType, typeArgs.Length, pTypeArgs, &pType);
            ComFactory.Create(pType, hResult, out type);
            return hResult;
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetArrayOrPointerType;

            public void* GetFunctionPointerType;
        }
    }
}
