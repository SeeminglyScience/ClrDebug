using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// An extension of <c>ICorDebugHeapValue</c> that provides support for
    /// common language runtime (CLR) handles.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugHeapValue2 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Creates a handle of the specified type for the heap value represented
        /// by this <c>ICorDebugHeapValue2</c> object.
        /// </summary>
        /// <param name="type">
        /// A value of the <c>CorDebugHandleType</c> enumeration that specifies the
        /// type of handle to be created.
        /// </param>
        /// <param name="handle">
        /// A pointer to the address of an <c>ICorDebugHandleValue</c> object that
        /// represents the new handle for this heap value.
        /// </param>
        /// <remarks>
        /// The handle will be created in the application domain that is associated
        /// with the heap value, and will become invalid if the application domain
        /// gets unloaded.
        ///
        /// Multiple calls to this function for the same heap value will create
        /// multiple handles. Because handles affect the performance of the garbage
        /// collector, the debugger should limit itself to a relatively small number
        /// of handles (about 256) that are active at a time.
        /// </remarks>
        public int CreateHandle(CorDebugHandleType type, out CorDebugHandleValue handle)
        {
            void** pHandle = default;
            int hResult = Calli(_this, This[0]->CreateHandle, (int)type, &pHandle);
            ComFactory.Create(pHandle, hResult, out handle);
            return hResult;
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* CreateHandle;
        }
    }
}
