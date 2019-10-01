using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a local variable or argument of a function.
    /// </summary>
    public unsafe class CorDebugVariableHome : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the index of a function argument.
        /// </summary>
        /// <param name="argumentIndex">
        /// A pointer to the argument index.
        /// </param>
        /// <returns>
        /// The method returns the following values.
        ///
        /// - <c>S_OK</c>: The method call returned a valid argument index.
        /// - <c>E_FAIL</c>: The current <c>ICorDebugVariableHome</c> instance
        ///   represents a local variable.
        /// </returns>
        /// <remarks>
        /// The argument index can be used to retrieve metadata for this argument.
        /// </remarks>
        public int GetArgumentIndex(out uint argumentIndex)
            => InvokeGet(_this, This[0]->GetArgumentIndex, out argumentIndex);

        /// <summary>
        /// Gets the <c>ICorDebugCode</c> instance that contains this
        /// <c>ICorDebugVariableHome</c> object.
        /// </summary>
        /// <param name="code">
        /// A pointer to the address of the <c>ICorDebugCode</c> instance that
        /// contains this <c>ICorDebugVariableHome</c> object.
        /// </param>
        public int GetCode(out CorDebugCode code)
            => InvokeGetObject(_this, This[0]->GetCode, out code);

        /// <summary>
        /// Gets the native range over which this variable is live.
        /// </summary>
        /// <param name="startOffset">
        /// The logical offset at which the variable is first live.
        /// </param>
        /// <param name="endOffset">
        /// The logical offset immediately after the point at which
        /// the variable is last live.
        /// </param>
        public int GetLiveRange(out uint startOffset, out uint endOffset)
        {
            fixed (void* pStartOffset = &startOffset)
            fixed (void* pEndOffset = &endOffset)
            {
                return Calli(_this, This[0]->GetLiveRange, pStartOffset, pEndOffset);
            }
        }

        /// <summary>
        /// Gets the type of the variable's native location.
        /// </summary>
        /// <param name="locationType">
        /// A pointer to the type of the variable's native location.
        /// </param>
        public int GetLocationType(out VariableLocationType locationType)
        {
            fixed (void* pLocationType = &locationType)
            {
                return Calli(_this, This[0]->GetLocationType, pLocationType);
            }
        }

        /// <summary>
        /// Gets the offset from the base register for a variable.
        /// </summary>
        /// <param name="offset">
        /// The offset from the base register.
        /// </param>
        /// <returns>
        /// The method returns the following values:
        ///
        /// - <c>S_OK</c>: The variable is in a register-relative memory
        ///   location.
        ///
        /// - <c>E_FAIL</c>: The variable is not in a register-relative
        ///   memory location.
        /// </returns>
        public int GetOffset(out int offset) => InvokeGet(_this, This[0]->GetOffset, out offset);

        /// <summary>
        /// Gets the register that contains a variable with a location
        /// type of <c>VLT_REGISTER</c>, and the base register for a
        /// variable with a location type of <c>VLT_REGISTER_RELATIVE</c>.
        /// </summary>
        /// <param name="register">
        /// A <c>CorDebugRegister</c> enumeration value that indicates the
        /// register for a variable with a location type of <c>VLT_REGISTER</c>,
        /// and the base register for a variable with a location type of
        /// <c>VLT_REGISTER_RELATIVE</c>.
        /// </param>
        /// <returns>
        /// The method returns the following values:
        ///
        /// - <c>S_OK</c>: The variable is in the register indicated by the
        ///   <c>register</c> argument.
        ///
        /// - <c>E_FAIL</c>: The variable is not in a register or a
        ///   register-relative location.
        /// </returns>
        public int GetRegister(out CorDebugRegister register)
        {
            fixed (void* pRegister = &register)
            {
                return Calli(_this, This[0]->GetRegister, pRegister);
            }
        }

        /// <summary>
        /// Gets the managed slot-index of a local variable.
        /// </summary>
        /// <param name="slotIndex">
        /// A pointer to the slot-index of a local variable.
        /// </param>
        /// <returns>
        /// The method returns the following values.
        ///
        /// - <c>S_OK</c>: The method call returned a slot-index value
        ///   in <c>slotIndex</c>.
        ///
        /// - <c>E_FAIL</c>: The current <c>ICorDebugVariableHome</c>
        ///   instance represents a function argument.
        /// </returns>
        /// <remarks>
        /// The slot-index can be used to retrieve the metadata for this
        /// local variable.
        /// </remarks>
        public int GetSlotIndex(out uint slotIndex) => InvokeGet(_this, This[0]->GetSlotIndex, out slotIndex);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetCode;

            public void* GetSlotIndex;

            public void* GetArgumentIndex;

            public void* GetLiveRange;

            public void* GetLocationType;

            public void* GetRegister;

            public void* GetOffset;
        }
    }
}
