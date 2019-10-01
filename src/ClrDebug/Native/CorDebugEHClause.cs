using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents an exception handling (EH) clause for a given piece of
    /// intermediate language (IL) code.
    ///
    /// [Supported in the .NET Framework 4.5.2 and later versions]
    /// </summary>
    /// <remarks>
    /// An array of <c>CoreDebugEHClause</c> values is returned by the
    /// <c>GetEHClauses</c> method.
    ///
    /// The EH clause information is defined by the CLI specification. For
    /// more information, see Standard ECMA-355: Common Language Infrastructure (CLI), 6th Edition.
    ///
    /// The <c>flags</c> field can contain the following flags. Note that they
    /// are not defined in CorDebug.idl or CorDebug.h.
    ///
    /// - COR_ILEXCEPTION_CLAUSE_EXCEPTION (0x00000000A)
    ///   typed exception clause.
    ///
    /// - COR_ILEXCEPTION_CLAUSE_FILTER (0x00000001)
    ///   An exception filter and handler clause.
    ///
    /// - COR_ILEXCEPTION_CLAUSE_FINALLY (0x00000002)
    ///   A finally clause.
    ///
    /// - COR_ILEXCEPTION_CLAUSE_FAULT (0x00000004)
    ///   A fault clause (a finally clause that is called only when
    ///   an exception is thrown).
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct CorDebugEHClause
    {
        /// <summary>
        /// A bit field that describes the exception information in the
        /// EH clause.
        /// </summary>
        public uint Flags;

        /// <summary>
        /// The offset, in bytes, of the try block from the start of
        /// the method body.
        /// </summary>
        public uint TryOffset;

        /// <summary>The length, in bytes, of the try block.</summary>
        public uint TryLength;

        /// <summary>The location of the handler for this try block.</summary>
        public uint HandlerOffset;

        /// <summary>The size of the handler code in bytes.</summary>
        public uint HandlerLength;

        /// <summary>The metadata token for a type-based exception handler.</summary>
        public uint ClassToken;

        /// <summary>
        /// The offset, in bytes, from the start of the method body for a
        /// filter-based exception handler.
        /// </summary>
        public uint FilterOffset;
    }
}
