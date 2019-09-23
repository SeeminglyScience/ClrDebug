using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Logically extends the <see cref="CorDebugFunction" /> to provide support
    /// for Just My Code step-through debugging, which skips non-user code.
    /// </summary>
    public unsafe class CorDebugFunction2 : Unknown
    {
        private ICorDebugFunction2Vtable** This => (ICorDebugFunction2Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a value that indicates whether the function that is represented
        /// by this object is marked as user code.
        /// </summary>
        /// <remarks>
        /// If the function represented by this object cannot be debugged,
        /// <see paramref="isJustMyCode" /> will always be <see langword="false" />.
        /// </remarks>
        public int SetJMCStatus(bool isJustMyCode) => Calli(_this, This[0]->SetJMCStatus, isJustMyCode.ToNativeInt());

        /// <summary>
        /// Gets a value that indicates whether the function that is represented
        /// by this object is marked as user code.
        /// </summary>
        /// <remarks>
        /// If the function represented by this object cannot be debugged,
        /// <see paramref="isJustMyCode" /> will always be <see langword="false" />.
        /// </remarks>
        public int GetJMCStatus(out bool isJustMyCode) => InvokeGet(_this, This[0]->GetJMCStatus, out isJustMyCode);

        [Obsolete("EnumerateNativeCode is not implemented in the current version of the .NET Framework.")]
        public int EnumerateNativeCode(out CorDebugEnum<CorDebugCode> pCodeEnum)
            => InvokeGetObject(_this, This[0]->EnumerateNativeCode, out pCodeEnum);

        /// <summary>
        /// Gets the Edit and Continue version of this function.
        /// </summary>
        /// <remarks>
        /// The runtime keeps track of the number of edits that have taken
        /// place to each module during a debug session. The version number
        /// of a function is one more than the number of the edit that
        /// introduced the function. The function's original version is
        /// version 1. The number is incremented for a module every time
        /// <see cref="CorDebugModule2.ApplyChanges" /> is called on that
        /// module. Thus, if a function's body was replaced in the first and
        /// third call to <see cref="CorDebugModule2.ApplyChanges" />,
        /// <see cref="GetVersionNumber(out uint)" /> may return version 1, 2,
        /// or 4 for that function, but not version 3. (That function would have
        /// no version 3.)
        ///
        /// The version number is tracked separately for each module. So, if you
        /// perform four edits on Module 1, and none on Module 2, your next edit on
        /// Module 1 will assign a version number of 6 to all the edited functions
        /// in Module 1. If the same edit touches Module 2, the functions in
        /// Module 2 will get a version number of 2.
        ///
        /// The version number obtained by this method may be lower than that obtained
        /// by <see cref="CorDebugFunction.GetCurrentVersionNumber(out uint)" />.
        ///
        /// The <see cref="CorDebugCode.GetVersionNumber(out uint)" /> method performs
        /// the same operation as <see cref="GetVersionNumber(out uint)" />.
        /// </remarks>
        public int GetVersionNumber(out uint nVersion) => InvokeGet(_this, This[0]->GetVersionNumber, out nVersion);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugFunction2Vtable
        {
            public IUnknownVtable IUnknown;

            public void* SetJMCStatus;

            public void* GetJMCStatus;

            public void* EnumerateNativeCode;

            public void* GetVersionNumber;
        }
    }
}
