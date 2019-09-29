namespace ClrDebug.Native
{
    /// <summary>
    /// Specifies the generation of a region of memory on the managed heap.
    /// </summary>
    public enum CorDebugGenerationTypes : int
    {
        /// <summary>Generation 0.</summary>
        CorDebug_Gen0 = 0,

        /// <summary>Generation 1.</summary>
        CorDebug_Gen1 = 1,

        /// <summary>Generation 2.</summary>
        CorDebug_Gen2 = 2,

        /// <summary>The large object heap.</summary>
        CorDebug_LOH = 3,
    }
}
