using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Logically extends the <c>ICorDebugProcess</c> interface to enable or
    /// disable certain types of <c>ICorDebugManagedCallback2</c> exception callbacks.
    ///
    /// [Supported in the .NET Framework 4.6 and later versions]
    /// </summary>
    public unsafe class CorDebugProcess8 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Enables or disables certain types of ICorDebugManagedCallback2 exception callbacks.
        ///
        /// [Supported in the .NET Framework 4.6 and later versions]
        /// </summary>
        /// <param name="enableExceptionsOutsideOfJMC">
        /// Indicates whether exceptions outside of JMC shoudl trigger callback events.
        /// </param>
        /// <remarks>
        /// If the value of <c>enableExceptionsOutsideOfJMC</c> is <c>false</c>:
        ///
        /// - A <c>DEBUG_EXCEPTION_FIRST_CHANCE</c> exception will not result
        ///   in a callback to the debugger.
        ///
        /// - A <c>DEBUG_EXCEPTION_CATCH_HANDLER_FOUND</c> exception will not
        ///   result in a callback to the debugger if the exception never escapes
        ///   into user code (that is, the path from an exception origin to an exception
        ///   handler has no methods marked as JustMyCode, or JMC).
        ///
        /// The default value of <c>enableExceptionsOutsideOfJMC</c> is <c>true</c>.
        /// </remarks>
        public int EnableExceptionCallbacksOutsideOfMyCode(bool enableExceptionsOutsideOfJMC)
        {
            return Calli(
                _this,
                This[0]->EnableExceptionCallbacksOutsideOfMyCode,
                enableExceptionsOutsideOfJMC.ToNativeInt());
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* EnableExceptionCallbacksOutsideOfMyCode;
        }
    }
}
