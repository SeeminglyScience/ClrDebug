using System;
using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Maps a Windows Runtime GUID to its corresponding <c>ICorDebugType</c> object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CorDebugGuidToTypeMapping
    {
        /// <summary>
        /// The GUID of the cached Windows Runtime type.
        /// </summary>
        public Guid iid;

        private unsafe void** pType;

        /// <summary>
        /// A pointer to an <c>ICorDebugType</c> object that provides
        /// information about the cached type.
        /// </summary>
        public unsafe CorDebugType GetCorType() => ComFactory.Create<CorDebugType>(pType);
    }
}
