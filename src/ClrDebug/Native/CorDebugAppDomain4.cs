using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Logically extends the <c>ICorDebugAppDomain</c> interface to get a
    /// managed object from a COM callable wrapper.
    /// </summary>
    public unsafe class CorDebugAppDomain4 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a managed object from a COM callable wrapper (CCW) pointer.
        /// </summary>
        /// <param name="ccwPointer">
        /// A COM callable wrapper (CCW) pointer.
        /// </param>
        /// <param name="managedObject">
        /// A pointer to the address of an <c>ICorDebugValue</c> object that
        /// represents the managed object that corresponds to the given CCW pointer.
        /// </param>
        public int GetObjectForCCW(long ccwPointer, out CorDebugValue managedObject)
        {
            void** pManagedObject = default;
            int hResult = Calli(_this, This[0]->GetObjectForCCW, ccwPointer, &pManagedObject);
            ComFactory.Create(pManagedObject, hResult, out managedObject);
            return hResult;
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetObjectForCCW;
        }
    }
}
