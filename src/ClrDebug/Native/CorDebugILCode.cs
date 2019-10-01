using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a segment of intermediate language (IL) code.
    ///
    /// [Supported in the .NET Framework 4.5.2 and later versions]
    /// </summary>
    public unsafe class CorDebugILCode : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Returns a pointer to a list of exception handling (EH) clauses
        /// that are defined for this intermediate language (IL).
        ///
        /// [Supported in the .NET Framework 4.5.2 and later versions]
        /// </summary>
        /// <param name="amountWritten">
        /// The number of clauses about which information is written to the
        /// <c>clauses</c> array.
        /// </param>
        /// <param name="clauses">
        /// An array of <c>CorDebugEHClause</c> objects that contain information
        /// on exception handling clauses defined for this IL.
        /// </param>
        /// <remarks>
        /// If <c>clauses</c> is empty and <c>amountWritten</c> is non-null,
        /// <c>amountWritten</c> is set to the number of available exception
        /// handling clauses.  When the method returns, <c>clauses</c> contains
        /// a maximum of <c>clauses.Length</c> items, and <c>pcClauses</c> is
        /// set to the number of clauses actually written to the <c>clauses</c>
        /// array.
        /// </remarks>
        public int GetEHClauses(Span<CorDebugEHClause> clauses, out int amountWritten)
        {
            fixed (void* pClauses = clauses)
            fixed (void* pAmountWritten = &amountWritten)
            {
                return Calli(_this, This[0]->GetEHClauses, clauses.Length, pClauses, pAmountWritten);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetEHClauses;
        }
    }
}
