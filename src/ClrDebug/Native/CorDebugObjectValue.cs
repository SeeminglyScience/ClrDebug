using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a value that contains an object.
    /// </summary>
    /// <remarks>
    /// An <see cref="CorDebugObjectValue" /> remains valid until the
    /// process being debugged is continued.
    /// </remarks>
    public unsafe class CorDebugObjectValue : Unknown
    {
        private ICorDebugObjectValueVtable** This => (ICorDebugObjectValueVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the class of this object value.
        /// </summary>
        /// <remarks>
        /// This method and <see cref="CorDebugValue.GetType(out CorElementType)" />
        /// each return information about the type of a value; they are both
        /// superseded by the generics-aware <c>ICorDebugValue2::GetExactType</c>.
        /// </remarks>
        public int GetClass(out CorDebugClass @class) => InvokeGetObject(_this, This[0]->GetClass, out @class);

        /// <summary>
        /// Gets the value of the specified field of the specified class for this object value.
        /// </summary>
        /// <remarks>
        /// The class, specified in <see paramref="class" />, must be in
        /// the hierarchy of the object value's class, and the field must
        /// be a field of that class.
        ///
        /// This method will still succeed for generic objects and generic
        /// classes. For example, if <c>MyDictionary{V}</c> inherits from
        /// <c>Dictionary{string,V}</c>, and the object value is of type
        /// <c>MyDictionary{int32}</c>, passing the <see cref="CorDebugClass" />
        /// object for <c>Dictionary{K,V}</c> will successfully get a
        /// field of <c>Dictionary{string,int32}</c>.
        /// </remarks>
        public int GetFieldValue(CorDebugClass @class, uint fieldDef, out CorDebugValue value)
        {
            using var pClass = @class.AcquirePointer();
            void** pValue = default;
            int result = Calli(_this, This[0]->GetFieldValue, pClass, fieldDef, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        [Obsolete("GetVirtualMethod is not implemented in this version of the .NET Framework.")]
        public int GetVirtualMethod(uint memberRef, out CorDebugFunction function)
        {
            void** pFunction = default;
            int result = Calli(_this, This[0]->GetVirtualMethod, memberRef, &pFunction);
            ComFactory.Create(pFunction, out function);
            return result;
        }

        [Obsolete("GetContext is not implemented in this version of the .NET Framework.")]
        public int GetContext(out CorDebugObjectValue context) => InvokeGetObject(_this, This[0]->GetContext, out context);

        /// <summary>
        /// Gets a value that indicates whether this object value is a value type.
        /// </summary>
        public int IsValueClass(out bool bIsValueClass) => InvokeGet(_this, This[0]->IsValueClass, out bIsValueClass);

        [Obsolete("GetManagedCopy is obsolete. Do not call this method.", error: true)]
        public int GetManagedCopy(out Unknown @object) => InvokeGetObject(_this, This[0]->GetManagedCopy, out @object);

        [Obsolete("SetFromManagedCopy is obsolete. Do not call this method.", error: true)]
        public int SetFromManagedCopy(Unknown @object)
        {
            using var pObject = @object.AcquirePointer();
            return Calli(_this, This[0]->SetFromManagedCopy, pObject);
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugObjectValueVtable
        {
            public ICorDebugValueVtable ICorDebugValue;

            public void* GetClass;

            public void* GetFieldValue;

            public void* GetVirtualMethod;

            public void* GetContext;

            public void* IsValueClass;

            public void* GetManagedCopy;

            public void* SetFromManagedCopy;
        }
    }
}
