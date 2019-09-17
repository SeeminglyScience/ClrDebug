namespace ClrDebug.Native
{
    public enum CorDebugUnmappedStop : int
    {
        STOP_NONE = 0,

        STOP_PROLOG = 1 << 0,

        STOP_EPILOG = 1 << 1,

        STOP_NO_MAPPING_INFO = 1 << 2,

        STOP_OTHER_UNMAPPED = 1 << 3,

        STOP_UNMANAGED = 1 << 4,

        STOP_ALL = 0xFFFF
    }
}
