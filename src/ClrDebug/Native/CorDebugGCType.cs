namespace ClrDebug.Native
{
    /// <summary>
    /// Indicates whether the garbage collector is running on
    /// a workstation or a server.
    /// </summary>
    public enum CorDebugGCType : int
    {
        /// <summary>The garbage collector is running on a workstation.</summary>
        CorDebugWorkstationGC = 0,

        /// <summary>The garbage collector is running on a server.</summary>
        CorDebugServerGC = 1,
    }
}
