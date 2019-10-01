using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a stack frame of Microsoft intermediate language (MSIL) code.
    /// This interface is a subclass of the <c>ICorDebugFrame</c> interface.
    /// </summary>
    /// <remarks>
    /// The <c>ICorDebugILFrame</c> interface is a specialized <c>ICorDebugFrame</c>
    /// interface. It is used either for MSIL code frames or for just-in-time (JIT)
    /// compiled frames. The JIT-compiled frames implement both the <c>ICorDebugILFrame</c>
    /// interface and the <c>ICorDebugNativeFrame</c> interface.
    ///
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugILFrame : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets an HRESULT that indicates whether it is safe to set the
        /// instruction pointer to the specified offset location in Microsoft
        /// Intermediate Language (MSIL) code.
        /// </summary>
        /// <param name="nOffset">
        /// The desired setting for the instruction pointer.
        /// </param>
        /// <remarks>
        /// Use the <c>CanSetIP</c> method before calling the <c>ICorDebugILFrame::SetIP</c>
        /// method. If <c>CanSetIP</c> returns any HRESULT other than <c>S_OK</c>,
        /// you can still invoke <c>ICorDebugILFrame::SetIP</c>, but there is no
        /// guarantee that the debugger will continue the safe and correct execution
        /// of the code being debugged.
        /// </remarks>
        public int CanSetIP(uint nOffset)
        {
            return Calli(_this, This[0]->CanSetIP, nOffset);
        }

        /// <summary>
        /// Gets an enumerator for the arguments in this frame.
        /// </summary>
        /// <param name="valueEnum">
        /// A pointer to the address of an <c>ICorDebugValueEnum</c> object that
        /// is the enumerator for the arguments in this frame.
        /// </param>
        /// <remarks>
        /// <c>EnumerateArguments</c> gets an enumerator that can list the
        /// arguments available in the call frame that is represented by this
        /// <c>ICorDebugILFrame</c> object. The list will include arguments
        /// that are vararg (that is, a variable number of arguments) as well
        /// as arguments that are not <c>vararg</c>.
        /// </remarks>
        public int EnumerateArguments(out CorDebugComEnum<CorDebugValue> valueEnum)
            => InvokeGetObject(_this, This[0]->EnumerateArguments, out valueEnum);

        /// <summary>
        /// Gets an enumerator for the local variables in this frame.
        /// </summary>
        /// <param name="valueEnum">
        /// A pointer to the address of an <c>ICorDebugValueEnum</c> object that
        /// is the enumerator for the local variables in this frame.
        /// </param>
        /// <remarks>
        /// <c>EnumerateLocalVariables</c> gets an enumerator that can list the
        /// local variables available in the call frame that is represented by
        /// this <c>ICorDebugILFrame</c> object. The list may not include all
        /// of the local variables in the running function, because some of
        /// them may not be active.
        /// </remarks>
        public int EnumerateLocalVariables(out CorDebugComEnum<CorDebugValue> valueEnum)
            => InvokeGetObject(_this, This[0]->EnumerateLocalVariables, out valueEnum);

        /// <summary>
        /// Gets the value of the specified argument in this Microsoft intermediate
        /// language (MSIL) stack frame.
        /// </summary>
        /// <param name="dwIndex">
        /// The index of the argument in this MSIL stack frame.
        /// </param>
        /// <param name="value">
        /// A pointer to the address of an <c>ICorDebugValue</c> object that
        /// represents the retrieved value.
        /// </param>
        /// <remarks>
        /// The <c>GetArgument</c> method can be used either in an MSIL stack frame
        /// or in a just-in-time (JIT) compiled frame.
        /// </remarks>
        public int GetArgument(uint dwIndex, out CorDebugValue value)
        {
            void** pValue = default;
            int hResult = Calli(_this, This[0]->GetArgument, dwIndex, &pValue);
            ComFactory.Create(pValue, hResult, out value);
            return hResult;
        }

        /// <summary>
        /// Gets the value of the instruction pointer and a bitwise combination
        /// value that describes how the value of the instruction pointer was
        /// obtained.
        /// </summary>
        /// <param name="nOffset">
        /// The value of the instruction pointer.
        /// </param>
        /// <param name="mappingResult">
        /// A pointer to a bitwise combination of the <c>CorDebugMappingResult</c>
        /// enumeration values that describe how the value of the instruction pointer
        /// was obtained.
        /// </param>
        /// <remarks>
        /// The value of the instruction pointer is the stack frame's offset
        /// into the function's Microsoft intermediate language (MSIL) code.
        /// If the stack frame is active, this address is the next instruction
        /// to execute. If the stack frame is not active, this address is the
        /// next instruction to execute when the stack frame is reactivated.
        ///
        /// If this frame is a just-in-time (JIT) compiled frame, the value of
        /// the instruction pointer will be determined by mapping backwards
        /// from the actual native instruction pointer, so the value may be
        /// only approximate.
        /// </remarks>
        public int GetIP(out uint nOffset, out CorDebugMappingResult mappingResult)
        {
            fixed (void* pnOffset = &nOffset)
            fixed (void* pMappingResult = &mappingResult)
            {
                return Calli(_this, This[0]->GetIP, pnOffset, pMappingResult);
            }
        }

        /// <summary>
        /// Gets the value of the specified local variable in this Microsoft
        /// intermediate language (MSIL) stack frame.
        /// </summary>
        /// <param name="dwIndex">
        /// The index of the local variable in this MSIL stack frame.
        /// </param>
        /// <param name="value">
        /// A pointer to the address of an <c>ICorDebugValue</c> object that
        /// represents the retrieved value.
        /// </param>
        /// <remarks>
        /// The <c>GetLocalVariable</c> method can be used either in an MSIL
        /// stack frame or in a just-in-time (JIT) compiled frame.
        /// </remarks>
        public int GetLocalVariable(uint dwIndex, out CorDebugValue value)
        {
            void** pValue = default;
            int hResult = Calli(_this, This[0]->GetLocalVariable, dwIndex, &pValue);
            ComFactory.Create(pValue, hResult, out value);
            return hResult;
        }

        /// <summary>
        /// This method has not been implemented.
        /// </summary>
        public int GetStackDepth(out uint depth)
            => InvokeGet(_this, This[0]->GetStackDepth, out depth);

        /// <summary>
        /// This method has not been implemented.
        /// </summary>
        public int GetStackValue(uint dwIndex, out CorDebugValue value)
        {
            void** pValue = default;
            int hResult = Calli(_this, This[0]->GetStackValue, dwIndex, &pValue);
            ComFactory.Create(pValue, hResult, out value);
            return hResult;
        }

        /// <summary>
        /// Sets the instruction pointer to the specified offset location in
        /// the Microsoft intermediate language (MSIL) code.
        /// </summary>
        /// <param name="nOffset">
        /// The offset location in the MSIL code.
        /// </param>
        /// <remarks>
        /// Calls to <c>SetIP</c> immediately invalidate all frames and chains
        /// for the current thread. If the debugger needs frame information after
        /// a call to <c>SetIP</c>, it must perform a new stack trace.
        ///
        /// <c>ICorDebug</c> will attempt to keep the stack frame in a valid state.
        /// However, even if the frame is in a valid state, there still may be problems
        /// such as uninitialized local variables. The caller is responsible for ensuring
        /// the coherency of the running program.
        ///
        /// On 64-bit platforms, the instruction pointer cannot be moved out of a
        /// <c>catch</c> or <c>finally</c> block. If <c>SetIP</c> is called to make such
        /// a move on a 64-bit platform, it will return an HRESULT indicating failure.
        /// </remarks>
        public int SetIP(uint nOffset)
        {
            return Calli(_this, This[0]->SetIP, nOffset);
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public CorDebugFrame.Vtable ICorDebugFrame;

            public void* GetIP;

            public void* SetIP;

            public void* EnumerateLocalVariables;

            public void* GetLocalVariable;

            public void* EnumerateArguments;

            public void* GetArgument;

            public void* GetStackDepth;

            public void* GetStackValue;

            public void* CanSetIP;
        }
    }
}
