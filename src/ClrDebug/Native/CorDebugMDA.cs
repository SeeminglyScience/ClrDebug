using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a managed debugging assistant (MDA) message.
    /// </summary>
    public unsafe class CorDebugMDA : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets a string containing the name of the managed debugging assistant (MDA).
        /// </summary>
        /// <param name="szName">The destination buffer.</param>
        /// <param name="amountWritten">
        /// The amount of characters written to <see paramref="szName" />.
        /// </param>
        /// <remarks>
        /// MDA names are unique values. This method is a convenient performance alternative to
        /// getting the XML stream and extracting the name from the stream based on the schema.
        /// </remarks>
        public int GetName(Span<char> szName, out uint amountWritten)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &amountWritten)
            {
                return Calli(_this, This[0]->GetName, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        /// <summary>
        /// Gets a string containing the description of the managed debugging assistant (MDA).
        /// </summary>
        /// <param name="szName">The destination buffer.</param>
        /// <param name="amountWritten">
        /// The amount of characters written to <see paramref="szName" />.
        /// </param>
        /// <remarks>The string can be zero in length.</remarks>
        public int GetDescription(Span<char> szName, out uint amountWritten)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &amountWritten)
            {
                return Calli(_this, This[0]->GetDescription, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        /// <summary>
        /// Gets the full XML stream associated with the managed debugging assistant (MDA).
        /// </summary>
        /// <param name="szName">The destination buffer.</param>
        /// <param name="amountWritten">
        /// The amount of characters written to <see paramref="szName" />.
        /// </param>
        /// <remarks>
        /// This method can potentially affect performance, depending on the size of the
        /// associated XML stream.
        /// </remarks>
        public int GetXML(Span<char> szName, out uint amountWritten)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &amountWritten)
            {
                return Calli(_this, This[0]->GetXML, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        /// <summary>
        /// Gets the flags associated with the managed debugging assistant (MDA).
        /// </summary>
        public int GetFlags(out CorDebugMDAFlags flags)
        {
            int pFlags = default;
            int result = Calli(_this, This[0]->GetFlags, &pFlags);
            flags = (CorDebugMDAFlags)pFlags;
            return result;
        }

        /// <summary>
        /// Gets the operating system (OS) thread identifier upon which the managed debugging
        /// assistant (MDA) is executing.
        /// </summary>
        /// <remarks>
        /// The OS thread is used instead of an <see cref="CorDebugThread" /> to allow for
        /// situations in which an MDA is fired either on a native thread or on a managed
        /// thread that has not yet entered managed code.
        /// </remarks>
        public int GetOSThreadId(out uint osTid) => InvokeGet(_this, This[0]->GetOSThreadId, out osTid);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetName;

            public void* GetDescription;

            public void* GetXML;

            public void* GetFlags;

            public void* GetOSThreadId;
        }
    }
}
