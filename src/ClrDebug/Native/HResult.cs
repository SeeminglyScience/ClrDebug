namespace ClrDebug.Native
{
    public static class HResult
    {
        public const int S_OK = 0;

        public const int S_FALSE = 1;

        public const int E_NOINTERFACE = unchecked((int)0x80004002);

        public const int E_POINTER = unchecked((int)0x80004003);
    }
}
