using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a frame on the current stack.
    /// </summary>
    public unsafe class CorDebugFrame : Unknown
    {
        private ICorDebugFrameVtable** This => (ICorDebugFrameVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the chain this frame is a part of.
        /// </summary>
        public int GetChain(out CorDebugChain chain)
            => InvokeGetObject(_this, This[0]->GetChain, out chain);

        /// <summary>
        /// Gets the code associated with this stack frame.
        /// </summary>
        public int GetCode(out CorDebugCode code)
            => InvokeGetObject(_this, This[0]->GetCode, out code);

        /// <summary>
        /// Gets the function that contains the code associated with this stack frame.
        /// </summary>
        /// <remarks>
        /// This method may fail if the frame is not associated with any particular function.
        /// </remarks>
        public int GetFunction(out CorDebugFunction function)
            => InvokeGetObject(_this, This[0]->GetFunction, out function);

        /// <summary>
        /// Gets the metadata token for the function that contains the code associated
        /// with this stack frame.
        /// </summary>
        public int GetFunctionToken(out int token)
            => InvokeGet(_this, This[0]->GetFunctionToken, out token);

        /// <summary>
        /// Gets the absolute address range of this stack frame.
        /// </summary>
        /// <param name="start">The starting address of the stack segment.</param>
        /// <param name="end">The ending address of the stack segment.</param>
        /// <remarks>
        /// The address range of the stack is useful for piecing together interleaved stack
        /// traces gathered from multiple debugging engines. The numeric range provides no
        /// information about the contents of the stack frame. It is meaningful only for
        /// comparison of stack frame locations.
        /// </remarks>
        public int GetStackRange(out ulong start, out ulong end)
        {
            ulong ref0 = default;
            ulong ref1 = default;

            int result = Calli(_this, This[0]->GetStackRange, &ref0, &ref1);
            start = ref0;
            end = ref1;
            return result;
        }

        /// <summary>
        /// Gets the frame that called this frame in the current chain.
        /// </summary>
        /// <remarks>If this frame is the outermost in the chain, <see paramref="frame" /> will
        /// be <see langword="null" />.
        public int GetCaller(out CorDebugFrame frame)
            => InvokeGetObject(_this, This[0]->GetCaller, out frame);

        /// <summary>
        /// Gets the frame this frame called is frame in the current chain.
        /// </summary>
        /// <remarks>If this frame is the innermost in the chain, <see paramref="frame" /> will
        /// be <see langword="null" />.
        public int GetCallee(out CorDebugFrame frame)
            => InvokeGetObject(_this, This[0]->GetCallee, out frame);

        /// <summary>
        /// Gets a stepper that allows the debugger to perform stepping operations relative
        /// to this frame.
        /// </summary>
        /// <remarks>
        /// If the frame is not active, the stepper object will typically have to return
        /// to the frame before the step is completed.
        /// </remarks>
        public int CreateStepper(out CorDebugStepper stepper)
            => InvokeGetObject(_this, This[0]->CreateStepper, out stepper);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugFrameVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetChain;

            public void* GetCode;

            public void* GetFunction;

            public void* GetFunctionToken;

            public void* GetStackRange;

            public void* GetCaller;

            public void* GetCallee;

            public void* CreateStepper;
        }
    }
}
