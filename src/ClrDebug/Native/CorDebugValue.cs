using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a value in the process being debugged.
    /// The value can be a read or a write value.
    /// </summary>
    /// <remarks>
    /// In general, ownership of a value object is passed
    /// when it is returned. The recipient is responsible for
    /// removing a reference from the object when it is finished
    /// with the object.
    ///
    /// Depending on where the value was retrieved from, the value
    /// may not remain valid after the process is resumed. So, in
    /// general, the value shouldn't be held across a call of the
    /// <see cref="CorDebugController.Continue(bool)" /> method.
    /// </remarks>
    public unsafe partial class CorDebugValue : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the primitive type of this object.
        /// </summary>
        /// <remarks>
        /// If the object is a complex run-time type, that type may
        /// be examined through the appropriate subclasses of the
        /// <see cref="CorDebugValue" /> class. For example,
        /// <see cref="CorDebugObjectValue" />, which inherits from
        /// <see cref="ICorDebugValue" />, represents a complex type.
        ///
        /// The <see cref="GetType(out CorElementType)" /> and <see cref="CorDebugObjectValue.GetClass(out CorDebugClass)" />
        /// methods each return information about the type of a value.
        /// They are both superseded by the generics-aware <c>ICorDebugValue2::GetExactType</c>
        /// method.
        /// </remarks>
        public int GetType(out CorElementType type)
        {
            int pType = 0;
            int result = Calli(_this, This[0]->GetType, &pType);
            type = (CorElementType)pType;
            return result;
        }

        /// <summary>
        /// Gets the size, in bytes, of this object.
        /// </summary>
        /// <remarks>
        /// If the value's type is a reference type, this method returns
        /// the size of the pointer rather than the size of the object.
        ///
        /// This method returns <c>COR_E_OVERFLOW</c> for objects that are
        /// larger than 4 GB on 64-bit platforms. Use the <c>ICorDebugValue3::GetSize64</c>
        /// method instead for objects that are larger than 4 GB.
        /// </remarks>
        public int GetSize(out uint size)
            => InvokeGet(_this, This[0]->GetSize, out size);

        /// <summary>
        /// Gets the address of object, which is in the process of
        /// being debugged.
        /// </summary>
        /// <remarks>
        /// If the value is unavailable, 0 (zero) is returned. This
        /// could happen if the value is at least partly in registers
        /// or stored in a garbage collector handle (<see cref="GCHandle" />).
        /// </remarks>
        public int GetAddress(out ulong address)
            => InvokeGet(_this, This[0]->GetAddress, out address);

        [Obsolete("The CreateBreakpoint method is currently not implemented.")]
        public int CreateBreakpoint(out CorDebugValueBreakpoint breakpoint)
            => InvokeGetObject(_this, This[0]->CreateBreakpoint, out breakpoint);
    }
}
