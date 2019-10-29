using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides methods to enable the debugger to execute code within the context of the
    /// code being debugged.
    /// </summary>
    /// <remarks>
    /// An <see cref="CorDebugEval" /> object is created in the context of a specific thread
    /// that is used to perform the evaluations. All objects and types used in a given evaluation
    /// must reside within the same application domain. That application domain need not be the
    /// same as the current application domain of the thread. Evaluations can be nested.
    ///
    /// The evaluation's operations do not complete until the debugger calls <c>ICorDebugController::Continue</c>,
    /// and then receives an <c>ICorDebugManagedCallback::EvalComplete</c> callback. If you need
    /// to use the evaluation functionality without allowing other threads to run, suspend the
    /// threads by using either <c>ICorDebugController::SetAllThreadsDebugState</c> or
    /// <c>ICorDebugController::Stop</c> before calling <c>ICorDebugController::Continue</c>.
    ///
    /// Because user code is running when the evaluation is in progress, any debug events can
    /// occur, including class loads and breakpoints. The debugger will receive callbacks, as
    /// normal, for these events. The state of the evaluation will be seen as part of the
    /// inspection of the normal program state. The stack chain will be a <see cref="CorDebugStepReason.CHAIN_FUNC_EVAL" />
    /// chain. The full debugger API will continue to operate as normal.
    ///
    /// If a deadlocked or infinite looping situation arises, the user code may never complete.
    /// In such a case, you must call <c>ICorDebugEval::Abort</c> before resuming the program.
    /// </remarks>
    public unsafe class CorDebugEval : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        [Obsolete("Use ICorDebugEval2::CallParameterizedFunction instead.")]
        public int CallFunction(CorDebugFunction function, ReadOnlySpan<CorDebugValue> args)
        {
            using var pArgs = NativeArray.AllocCom(args);
            using var pFunction = function?.AcquirePointer();
            return Calli(_this, This[0]->CallFunction, pFunction, (void**)pArgs);
        }

        [Obsolete("Use ICorDebugEval2::NewParameterizedObject instead.")]
        public int NewObject(CorDebugFunction constructor, ReadOnlySpan<CorDebugValue> args)
        {
            using var pArgs = NativeArray.AllocCom(args);
            using var pConstructor = constructor?.AcquirePointer();
            return Calli(_this, This[0]->NewObject, pConstructor, (void**)pArgs);
        }

        [Obsolete("Use ICorDebugEval2::NewParameterizedObjectNoConstructor instead.")]
        public int NewObjectNoConstructor(CorDebugClass @class)
        {
            using var pClass = @class.AcquirePointer();
            return Calli(_this, This[0]->NewObjectNoConstructor, pClass);
        }

        /// <summary>
        /// Allocates a new string instance with the specified contents.
        /// </summary>
        /// <param name="@string">The contents for the string.</param>
        /// <remarks>
        /// The string is always created in the application domain in which the thread is
        /// currently executing.
        /// </remarks>
        public int NewString(Span<char> @string)
        {
            fixed (void* pString = @string)
            {
                return Calli(_this, This[0]->NewString, @pString);
            }
        }

        [Obsolete("Use ICorDebugEval2::NewParameterizedArray instead.")]
        public int NewArray(
            CorElementType elementType,
            CorDebugClass elementClass,
            uint rank,
            Span<uint> dims,
            Span<uint> lowBounds)
        {
            fixed (void* pDims = dims)
            fixed (void* pLowBounds = lowBounds)
            {
                using var pElementClass = elementClass.AcquirePointer();
                return Calli(_this, This[0]->NewArray, (int)elementType, pElementClass, rank, pDims, pLowBounds);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether this ICorDebugEval object is currently executing.
        /// </summary>
        public int IsActive(out bool bActive) => InvokeGet(_this, This[0]->IsActive, out bActive);

        /// <summary>
        /// Aborts the computation this ICorDebugEval object is currently performing.
        /// </summary>
        /// <remarks>
        /// If the evaluation is nested and it is not the most recent one, <see cref="Abort" />
        /// may fail.
        /// </remarks>
        public int Abort() => Calli(_this, This[0]->Abort);

        /// <summary>
        /// Gets the results of this evaluation.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method is valid only after the evaluation is completed.
        ///
        /// If the evaluation completes normally, <see paramref="result" /> specifies the
        /// results. If it terminates with an exception, the result is the exception thrown.
        /// If the evaluation was for a new object, the result is the reference to the new object.
        /// </remarks>
        public int GetResult(out CorDebugValue result) => InvokeGetObject(_this, This[0]->GetResult, out result);

        /// <summary>
        /// Gets the thread in which this evaluation is executing or will execute.
        /// </summary>
        public int GetThread(out CorDebugThread thread) => InvokeGetObject(_this, This[0]->GetThread, out thread);

        [Obsolete("Use ICorDebugEval2::CreateValueForType instead.")]
        public int CreateValue(CorElementType elementType, CorDebugClass elementClass, out CorDebugValue value)
        {
            using var pElementClass = elementClass.AcquirePointer();
            void* pValue = default;
            int result = Calli(_this, This[0]->CreateValue, (int)elementType, pElementClass, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* CallFunction;

            public void* NewObject;

            public void* NewObjectNoConstructor;

            public void* NewString;

            public void* NewArray;

            public void* IsActive;

            public void* Abort;

            public void* GetResult;

            public void* GetThread;

            public void* CreateValue;
        }
    }
}
