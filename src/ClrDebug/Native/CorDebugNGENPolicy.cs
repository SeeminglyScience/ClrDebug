namespace ClrDebug.Native
{
    /// <summary>
    /// Provides a value that determines whether a debugger loads
    /// native (NGen) images from the native image cache.
    /// </summary>
    /// <remarks>
    /// The <c>CorDebugNGENPolicy</c> enumeration is used by the
    /// <c>ICorDebugProcess5::EnableNGENPolicy</c> method. Disabling
    /// the use of images from the local native image cache provides
    /// for a consistent debugging experience by ensuring that the
    /// debugger loads debuggable JIT-compiled images instead of
    /// optimized native images.
    /// </remarks>
    public enum CorDebugNGENPolicy : int
    {
        /// <summary>
        /// In a Windows 8.x Store app, the use of images from the
        /// local native image cache is disabled. In a desktop app,
        /// this setting has no effect.
        /// </summary>
        DISABLE_LOCAL_NIC = 1,
    }
}
