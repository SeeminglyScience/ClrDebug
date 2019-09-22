using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a type, either basic or complex (that is, user-defined).
    /// If the type is generic, <see cref="CorDebugType" /> represents the
    /// instantiated generic type.
    /// </summary>
    /// <remarks>
    /// If the type is generic, <see cref="CorDebugClass" /> represents the
    /// uninstantiated type. The <see cref="CorDebugType" /> class represents
    /// an instantiated generic type. For example, <c>Hashtable{K, V}</c> would
    /// be represented by <see cref="CorDebugClass" />, whereas <c>Hashtable{Int32, String}</c>
    /// would be represented by <see cref="CorDebugType" />.
    ///
    /// Non-generic types are represented by both <see cref="CorDebugClass" /> and
    /// <see cref="CorDebugType" />. The latter interface was introduced in
    /// the .NET Framework version 2.0 to deal with type instantiation.
    /// </remarks>
    public unsafe class CorDebugType : Unknown
    {
        private ICorDebugTypeVtable** This => (ICorDebugTypeVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a <see cref="CorElementType" /> value that describes the
        /// native type of the common language runtime (CLR) Type represented
        /// by this type.
        /// </summary>
        /// <remarks>
        /// If the value of <see paramref="ty" /> is either <see cref="CorElementType.ELEMENT_TYPE_CLASS" />
        /// or <see cref="CorElementType.ELEMENT_TYPE_VALUETYPE" />, the
        /// <see cref="GetClass(out CorDebugClass)" /> method may be called to
        /// get the uninstantiated type for a generic type; otherwise, do not
        /// call <see cref="GetClass(out CorDebugClass)" />.
        /// </remarks>
        public int GetType(out CorElementType ty)
        {
            int pTy = default;
            int result = Calli(_this, This[0]->GetType, &pTy);
            ty = (CorElementType)pTy;
            return result;
        }

        /// <summary>
        /// Gets the <see cref="CorDebugClass" /> that represents the
        /// uninstantiated generic type.
        /// </summary>
        /// <remarks>
        /// This method can be called only under certain conditions.
        /// Call <see cref="GetType(out CorElementType)" /> before calling
        /// this method. If <see cref="GetType(out CorElementType)" /> returns
        /// <see cref="CorElementType.ELEMENT_TYPE_CLASS" /> or <see cref="CorElementType.ELEMENT_TYPE_VALUETYPE" />,
        /// then this method can be called to get the uninstantiated type
        /// for a generic type.
        /// </remarks>
        public int GetClass(out CorDebugClass @class)
            => InvokeGetObject(_this, This[0]->GetClass, out @class);

        /// <summary>
        /// Gets the Type parameters of the class referenced by this type.
        /// </summary>
        /// <remarks>
        /// You can use this method if the <see cref="GetType(out CorElementType)" /> is
        /// any of the following:
        ///
        /// - <see cref="CorElementType.ELEMENT_TYPE_CLASS" />
        /// - <see cref="CorElementType.ELEMENT_TYPE_VALUETYPE" />
        /// - <see cref="CorElementType.ELEMENT_TYPE_ARRAY" />
        /// - <see cref="CorElementType.ELEMENT_TYPE_SZARRAY" />
        /// - <see cref="CorElementType.ELEMENT_TYPE_BYREF" />
        /// - <see cref="CorElementType.ELEMENT_TYPE_PTR" />
        /// - <see cref="CorElementType.ELEMENT_TYPE_FNPTR" />
        ///
        /// The number of parameters and their order depends on the type:
        ///
        /// - <see cref="CorElementType.ELEMENT_TYPE_CLASS" /> or <see cref="CorElementType.ELEMENT_TYPE_VALUETYPE" />:
        ///   The number of type parameters contained will depend on the number
        ///   of formal type parameters for the corresponding class. For example,
        ///   if the type is class Dict{String,int32}, then EnumerateTypeParameters
        ///   will return an enumerable that contains objects representing String and
        ///   int32 in sequence.
        ///
        /// - <see cref="CorElementType.ELEMENT_TYPE_FNPTR" />: The number of
        /// type parameters contained will be one greater than the number of arguments
        /// accepted by the function. The first type parameter contained in the enumerable
        /// is the return type for the function, and the subsequent type parameters are
        /// the function's parameters.
        ///
        /// - <see cref="CorElementType.ELEMENT_TYPE_ARRAY" />,
        ///   <see cref="CorElementType.ELEMENT_TYPE_SZARRAY" />,
        ///   <see cref="CorElementType.ELEMENT_TYPE_BYREFARRAY" />,
        ///   <see cref="CorElementType.ELEMENT_TYPE_PTR" />: One type parameter will be
        ///   returned. For example, if the type is an array type such as int32[],
        ///   then this method will return an enumerable that contains an object
        ///   representing int32.
        /// </remarks>
        public int EnumerateTypeParameters(out CorDebugEnum<CorDebugType> tyParEnum)
            => InvokeGetObject(_this, This[0]->EnumerateTypeParameters, out tyParEnum);

        /// <summary>
        /// Gets the first Type parameter of the type represented by
        /// this type.
        /// </summary>
        /// <remarks>
        /// This method can be called in cases where the additional
        /// information about the type involves, at most, one type parameter.
        /// In particular, it can be used if the type is an <see cref="CorElementType.ELEMENT_TYPE_ARRAY" />,
        /// <see cref="CorElementType.ELEMENT_TYPE_SZARRAY" />, <see cref="CorElementType.ELEMENT_TYPE_BYREF" />,
        /// or <see cref="CorElementType.ELEMENT_TYPE_PTR" />, as indicated by the
        /// <see cref="GetType(out CorElementType)" /> method.
        /// </remarks>
        public int GetFirstTypeParameter(out CorDebugType value)
            => InvokeGetObject(_this, This[0]->GetFirstTypeParameter, out value);

        /// <summary>
        /// Gets the base type, if one exists, of the type represented
        /// by this type.
        /// </summary>
        /// <remarks>
        /// Looking up the base type for a type is useful to implement common
        /// debugger functionality, such as printing out all the fields of an
        /// object or its parent classes.
        /// </remarks>
        public int GetBase(out CorDebugType @base) => InvokeGetObject(_this, This[0]->GetBase, out @base);

        /// <summary>
        /// Gets the value of the static field referenced by the specified
        /// field token in the specified stack frame.
        /// </summary>
        /// <param name="fieldDef">
        /// A <c>mdFieldDef</c> token that specifies the static field.
        /// </param>
        /// <param name="frame">
        /// The frame to use as context.
        /// </param>
        /// <param name="value">
        /// The value of the static field.
        /// </param>
        /// <remarks>
        /// This method may be used only if the type is <see cref="CorElementType.ELEMENT_TYPE_CLASS" />
        /// or <see cref="CorElementType.ELEMENT_TYPE_VALUETYPE" />, as indicated
        /// by the <see cref="GetType(out CorElementType)" /> method.
        ///
        /// For non-generic types, the operation performed by this method is
        /// identical to calling <see cref="CorDebugClass.GetStaticFieldValue(uint, CorDebugFrame, out CorDebugValue)" />
        /// on the <see cref="CorDebugClass" /> object that is returned by <see cref="GetClass(out CorDebugClass)" />.
        ///
        /// For generic types, a static field value will be relative to
        /// a particular instantiation. Also, if the static field could
        /// possibly be relative to a thread, a context, or an application
        /// domain, then the stack frame will help the debugger determine
        /// the proper value.
        /// </remarks>
        public int GetStaticFieldValue(uint fieldDef, CorDebugFrame frame, out CorDebugValue value)
        {
            using var pFrame = frame.AcquirePointer();
            void** pValue = default;
            int result = Calli(_this, This[0]->GetStaticFieldValue, fieldDef, pFrame, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        /// <summary>
        /// Gets the number of dimensions in an array type.
        /// </summary>
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
