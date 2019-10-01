using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a type, which can be either basic or complex (that is, user-defined).
    /// If the type is generic, this represents the uninstantiated generic type.
    /// </summary>
    /// <remarks>
    /// This class represents an uninstantiated generic type. The <see cref="CorDebugType" /> class
    /// represents an instantiated generic type. For example, <see cref="System.Collections.Generic.Dictionary{TKey, TValue}" />
    /// would be represented by <see cref="CorDebugClass" />, whereas <see cref="System.Collections.Generic.Dictionary{int, string}" />
    /// would be represented by <see cref="CorDebugType" />.
    ///
    /// Non-generic types are represented by both <see cref="CorDebugClass" /> and <see cref="ICorDebugType" />.
    /// The latter interface was introduced in the .NET Framework version 2.0 to deal with type instantiation.
    /// </remarks>
    public unsafe class CorDebugClass : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the module that defines this class.
        /// </summary>
        public int GetModule(out CorDebugModule module)
            => InvokeGetObject(_this, This[0]->GetModule, out module);

        /// <summary>
        /// Gets the TypeDef metadata token that references the definition of this class.
        /// </summary>
        public int GetToken(out uint typeDef) => InvokeGet(_this, This[0]->GetToken, out typeDef);

        /// <summary>
        /// Gets the value of the specified static field.
        /// </summary>
        /// <param name="fieldDef">
        /// A FieldDef token that references the field to be retrieved.
        /// </param>
        /// <param name="frame">
        /// The frame to be used to disambiguate among thread, context, or application
        /// domain statics.
        ///
        /// If the static field is relative to a thread, a context, or an application domain,
        /// the frame will determine the proper value.
        /// </param>
        /// <param name="value">The value of the static field.</param>
        /// <remarks>
        /// For parameterized types, the value of a static field is relative to the particular
        /// instantiation. Therefore, if the class constructor takes parameters of type
        /// <see cref="Type" />, call <see cref="CorDebugType.GetStaticFieldValue" /> instead.
        /// </remarks>
        public int GetStaticFieldValue(uint fieldDef, CorDebugFrame frame, out CorDebugValue value)
        {
            using var pFrame = frame.AcquirePointer();
            void* pValue = default;
            int result = Calli(_this, This[0]->GetStaticFieldValue, fieldDef, pFrame, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetModule;

            public void* GetToken;

            public void* GetStaticFieldValue;
        }
    }
}
