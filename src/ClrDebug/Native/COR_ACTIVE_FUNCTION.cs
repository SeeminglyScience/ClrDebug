using System.Runtime.InteropServices;

namespace ClrDebug.Native
{

    /// <summary>
    /// Contains information about the functions that are currently active
    /// in a thread's frames. This structure is used by the
    /// <see cref="CorDebugThread2.GetActiveFunctions(uint, out uint, COR_ACTIVE_FUNCTION)" />
    /// method.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COR_ACTIVE_FUNCTION
    {
        private readonly unsafe void** _appDomain;

        private readonly unsafe void** _module;

        private readonly unsafe void** _function;

        /// <summary>
        /// The Microsoft intermediate language (MSIL) offset of the frame.
        /// </summary>
        public uint ilOffset;

        /// <summary>
        /// Reserved for future extensibility.
        /// </summary>
        public uint flags;

        /// <summary>
        /// The application domain owner of the ilOffset field.
        /// </summary>
        public unsafe CorDebugAppDomain GetAppDomain() => ComFactory.Create<CorDebugAppDomain>(_appDomain);

        /// <summary>
        /// The module owner of the <see cref="ilOffset" /> field.
        /// </summary>
        public unsafe CorDebugModule GetModule() => ComFactory.Create<CorDebugModule>(_module);

        /// <summary>
        /// The function owner of the <see cref="ilOffset" /> field.
        /// </summary>
        public unsafe CorDebugFunction2 GetFunction() => ComFactory.Create<CorDebugFunction2>(_function);
    }
}
