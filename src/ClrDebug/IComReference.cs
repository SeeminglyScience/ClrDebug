namespace ClrDebug
{
    public interface IComReference
    {
        unsafe void** DangerousGetPointer();

        bool IsDefault { get; }

        unsafe void SetPointer(void** ptr);
    }
}
