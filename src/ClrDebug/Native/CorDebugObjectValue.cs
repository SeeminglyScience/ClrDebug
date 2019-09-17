using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugObjectValue : Unknown
    {
        private ICorDebugObjectValueVtable** This => (ICorDebugObjectValueVtable**)DangerousGetPointer();

        public int GetClass(out CorDebugClass @class) => InvokeGetObject(_this, This[0]->GetClass, out @class);

        public int GetFieldValue(CorDebugClass @class, uint fieldDef, out CorDebugValue value)
        {
            using var pClass = @class.AquirePointer();
            void** pValue = default;
            int result = Calli(_this, This[0]->GetFieldValue, pClass, fieldDef, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        public int GetVirtualMethod(uint memberRef, out CorDebugFunction function)
        {
            void** pFunction = default;
            int result = Calli(_this, This[0]->GetVirtualMethod, memberRef, &pFunction);
            ComFactory.Create(pFunction, out function);
            return result;
        }

        public int GetContext(out CorDebugObjectValue context) => InvokeGetObject(_this, This[0]->GetContext, out context);

        public int IsValueClass(out bool bIsValueClass) => InvokeGet(_this, This[0]->IsValueClass, out bIsValueClass);

        public int GetManagedCopy(out Unknown @object) => InvokeGetObject(_this, This[0]->GetManagedCopy, out @object);

        public int SetFromManagedCopy(Unknown @object)
        {
            using var pObject = @object.AquirePointer();
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
