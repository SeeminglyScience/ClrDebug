using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a generic class or a class with a method parameter
    /// of type <c>System.Type</c>. This interface extends <c>ICorDebugClass</c>.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugClass2 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the type declaration for this class.
        /// </summary>
        /// <param name="elementType">
        /// A value of the CorElementType enumeration that specifies the element
        /// type for this class: Set this value to <c>ELEMENT_TYPE_VALUETYPE</c>
        /// if this <c>ICorDebugClass2</c> represents a value type. Set this value
        /// to <c>ELEMENT_TYPE_CLASS</c> if this <c>ICorDebugClass2</c> represents
        /// a complex type.
        /// </param>
        /// <param name="nTypeArgs">
        /// The number of type parameters, if the type is generic. The number of type
        /// parameters (if any) must match the number required by the class.
        /// </param>
        /// <param name="typeArgs">
        /// An array of pointers, each of which points to an <c>ICorDebugType</c> object
        /// that represents a type parameter. If the class is non-generic, this value is null.
        /// </param>
        /// <param name="type">
        /// A pointer to the address of an <c>ICorDebugType</c> object that represents the
        /// type declaration. This object is equivalent to a <c>System.Type</c> object in
        /// managed code.
        /// </param>
        /// <remarks>
        /// If the class is non-generic, that is, if it has no type parameters,
        /// <c>GetParameterizedType</c> simply gets the runtime type object corresponding
        /// to the class. The <c>elementType</c> parameter should be set to the correct
        /// element type for the class: <c>ELEMENT_TYPE_VALUETYPE</c> if the class is a
        /// value type; otherwise, <c>ELEMENT_TYPE_CLASS</c>.
        ///
        /// If the class accepts type parameters (for example, <c>ArrayList&lt;T&gt;</c>),
        /// you can use <c>GetParameterizedType</c> to construct a type object for an
        /// instantiated type such as <c>ArrayList&lt;int&gt;</c>.
        /// </remarks>
        public int GetParameterizedType(
            CorElementType elementType,
            ReadOnlySpan<CorDebugType> typeArgs,
            out CorDebugType type)
        {
            void** pType = default;
            using var pTypeArgs = NativeArray.AllocCom(typeArgs);
            int hResult = Calli(
                _this,
                This[0]->GetParameterizedType,
                (uint)elementType,
                typeArgs.Length,
                pTypeArgs,
                &pType);

            ComFactory.Create(pType, hResult, out type);
            return hResult;
        }

        /// <summary>
        /// For each method of the class, sets a value that indicates whether
        /// the method is user-defined code.
        /// </summary>
        /// <param name="bIsJustMyCode">
        /// Set to <c>true</c> to indicate that the method is user-defined code;
        /// otherwise, set to <c>false</c>.
        /// </param>
        /// <remarks>
        /// A just-my-code (JMC) stepper will skip non-user-defined code.
        /// User-defined code must be a subset of debuggable code.
        ///
        /// <c>SetJMCStatus</c> returns an HRESULT value of <c>S_FALSE</c> if it fails
        /// to set the value for any method, even if it successfully sets the value
        /// for all other methods.
        /// </remarks>
        public int SetJMCStatus(bool bIsJustMyCode)
        {
            int hResult = Calli(_this, This[0]->SetJMCStatus, bIsJustMyCode.ToNativeInt());
            return hResult;
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetParameterizedType;

            public void* SetJMCStatus;
        }
    }
}
