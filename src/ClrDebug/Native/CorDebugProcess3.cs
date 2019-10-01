using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Controls custom debugger notifications.
    /// </summary>
    public unsafe class CorDebugProcess3 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Enables and disables custom debugger notifications of
        /// the specified type.
        /// </summary>
        /// <param name="class">
        /// The type that specifies custom debugger notifications.
        /// </param>
        /// <param name="enabled">
        /// Indicates whether custom debugger notifications are enabled.
        /// </param>
        /// <remarks>
        /// When <see paramref="enabled" /> is set to <see langword="true" />, calls
        /// to the <c>Debugger.NotifyOfCrossThreadDependency</c> method trigger an
        /// <c>ICorDebugManagedCallback3::CustomNotification</c> callback. Notifications
        /// are disabled by default; therefore, the debugger must specify any notification
        /// types it knows about and wants to handle. Because the class is scoped by
        /// application domain, the debugger must call this method for every application
        /// domain in the process if it wants to receive the notification across the
        /// entire process.
        ///
        /// Starting with the .NET Framework 4, the only supported notification is a
        /// cross-thread dependency notification.
        /// </remarks>
        public int SetEnableCustomNotification(CorDebugClass @class, bool enable)
        {
            using var pClass = @class.AcquirePointer();
            return Calli(_this, This[0]->SetEnableCustomNotification, pClass, enable.ToNativeInt());
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* SetEnableCustomNotification;
        }
    }
}
