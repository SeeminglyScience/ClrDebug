namespace ClrDebug.Native
{
    /// <summary>
    /// Specifies a common language runtime Type, a type modifier,
    /// or information about a type in a metadata type signature.
    /// </summary>
    /// <remarks>
    /// The type modifiers form the basis for representing more
    /// complex types. A <see cref="CorElementType" /> type modifier value is
    /// applied to the value that immediately follows it in the
    /// type signature. The value that follows the <see cref="CorElementType" /> type
    /// modifier value can be a <see cref="CorElementType" /> simple type
    /// value, a metadata token, or other value, as specified in the
    /// following table.
    ///
    /// ELEMENT_TYPE_PTR         ELEMENT_TYPE_PTR &lt;a CorElementType value&gt;
    /// ELEMENT_TYPE_BYREF       ELEMENT_TYPE_BYREF &lt;a CorElementType value&gt;
    /// ELEMENT_TYPE_VALUETYPE   ELEMENT_TYPE_VALUETYPE &lt;an mdTypeDef metadata token&gt;
    /// ELEMENT_TYPE_CLASS       ELEMENT_TYPE_CLASS &lt;an mdTypeDef metadata token&gt;
    /// ELEMENT_TYPE_VAR         ELEMENT_TYPE_VAR &lt;number&gt;
    /// ELEMENT_TYPE_ARRAY       ELEMENT_TYPE_ARRAY &lt;a CorElementType value &lt;rank&gt; &lt;count1&gt; &lt;bound1&gt; ... &lt;countN&gt; &lt;boundN&gt;
    /// ELEMENT_TYPE_GENERICINST ELEMENT_TYPE_GENERICINST &lt;an mdTypeDef metadata token &lt;argument Count&gt; &lt;arg1&gt; ... &lt;argN&gt;
    /// ELEMENT_TYPE_FNPTR       ELEMENT_TYPE_FNPTR &lt;complete signature for the function, including calling convention&gt;
    /// ELEMENT_TYPE_SZARRAY     ELEMENT_TYPE_SZARRAY &lt;a CorElementType value&gt;
    /// ELEMENT_TYPE_MVAR        ELEMENT_TYPE_MVAR &lt;number&gt;
    /// ELEMENT_TYPE_CMOD_REQD   ELEMENT_TYPE_&lt;a mdTypeRef or mdTypeDef metadata token&gt;
    /// ELEMENT_TYPE_CMOD_OPT    E_T_CMOD_OPT &lt;a mdTypeRef or mdTypeDef metadata token&gt;
    /// </remarks>
    public enum CorElementType : int
    {
        /// <summary>Used internally.</summary>
        ELEMENT_TYPE_END = 0,

        /// <summary>A void type.</summary>
        ELEMENT_TYPE_VOID = 1,

        /// <summary>A Boolean type</summary>
        ELEMENT_TYPE_BOOLEAN = 2,

        /// <summary>A character type.</summary>
        ELEMENT_TYPE_CHAR = 3,

        /// <summary>A signed 1-byte integer.</summary>
        ELEMENT_TYPE_I1 = 4,

        /// <summary>An unsigned 1-byte integer.</summary>
        ELEMENT_TYPE_U1 = 5,

        /// <summary>A signed 2-byte integer.</summary>
        ELEMENT_TYPE_I2 = 6,

        /// <summary>An unsigned 2-byte integer.</summary>
        ELEMENT_TYPE_U2 = 7,

        /// <summary>A signed 4-byte integer.</summary>
        ELEMENT_TYPE_I4 = 8,

        /// <summary>An unsigned 4-byte integer.</summary>
        ELEMENT_TYPE_U4 = 9,

        /// <summary>A signed 8-byte integer.</summary>
        ELEMENT_TYPE_I8 = 10,

        /// <summary>An unsigned 8-byte integer.</summary>
        ELEMENT_TYPE_U8 = 11,

        /// <summary>A 4-byte floating point.</summary>
        ELEMENT_TYPE_R4 = 12,

        /// <summary>An 8-byte floating point.</summary>
        ELEMENT_TYPE_R8 = 13,

        /// <summary>A System.String type.</summary>
        ELEMENT_TYPE_STRING = 14,

        /// <summary>A pointer type modifier.</summary>
        ELEMENT_TYPE_PTR = 15,

        /// <summary>A reference type modifier.</summary>
        ELEMENT_TYPE_BYREF = 16,

        /// <summary>A value type modifier.</summary>
        ELEMENT_TYPE_VALUETYPE = 17,

        /// <summary>A class type modifier.</summary>
        ELEMENT_TYPE_CLASS = 18,

        /// <summary>A class variable type modifier.</summary>
        ELEMENT_TYPE_VAR = 19,

        /// <summary>A multi-dimensional array type modifier.</summary>
        ELEMENT_TYPE_ARRAY = 20,

        /// <summary>A type modifier for generic types.</summary>
        ELEMENT_TYPE_GENERICINST = 21,

        /// <summary>A typed reference.</summary>
        ELEMENT_TYPE_TYPEDBYREF = 22,

        /// <summary>Size of a native integer.</summary>
        ELEMENT_TYPE_I = 24,

        /// <summary>Size of an unsigned native integer.</summary>
        ELEMENT_TYPE_U = 25,

        /// <summary>A pointer to a function.</summary>
        ELEMENT_TYPE_FNPTR = 27,

        /// <summary>A System.Object type.</summary>
        ELEMENT_TYPE_OBJECT = 28,

        /// <summary>A single-dimensional, zero lower-bound array type modifier.</summary>
        ELEMENT_TYPE_SZARRAY = 29,

        /// <summary>A method variable type modifier.</summary>
        ELEMENT_TYPE_MVAR = 30,

        /// <summary>A C language required modifier.</summary>
        ELEMENT_TYPE_CMOD_REQD = 31,

        /// <summary>A C language optional modifier.</summary>
        ELEMENT_TYPE_CMOD_OPT = 32,

        /// <summary>Used internally.</summary>
        ELEMENT_TYPE_INTERNAL = 33,

        /// <summary>An invalid type.</summary>
        ELEMENT_TYPE_MAX = 34,

        /// <summary>Used internally.</summary>
        ELEMENT_TYPE_MODIFIER = 64,

        /// <summary>A type modifier that is a sentinel for a list of a variable number of parameters.</summary>
        ELEMENT_TYPE_SENTINEL = 65,

        /// <summary>Used internally.</summary>
        ELEMENT_TYPE_PINNED = 69
    }
}
