using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Contains the security descriptor for an object and
    /// specifies whether the handle retrieved by specifying
    /// this structure is inheritable. This structure provides
    /// security settings for objects created by various
    /// functions, such as <c>CreateFile</c>, <c>CreatePipe</c>,
    /// <c>CreateProcess</c>, <c>RegCreateKeyEx</c>,
    /// or <c>RegSaveKeyEx</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SECURITY_ATTRIBUTES
    {
        /// <summary>
        /// The size, in bytes, of this structure. Set this value
        /// to the size of the <see cref="SECURITY_ATTRIBUTES" />
        /// structure.
        /// </summary>
        public uint nLength;

        /// <summary>
        /// A pointer to a <c>SECURITY_DESCRIPTOR</c> structure that
        /// controls access to the object. If the value of this member
        /// is <see langword="null" />, the object is assigned the
        /// default security descriptor associated with the access
        /// token of the calling process. This is not the same as
        /// granting access to everyone by assigning a <see langword="null" />
        /// discretionary access control list (DACL). By default,
        /// the default DACL in the access token of a process allows
        /// access only to the user represented by the access token.
        /// </summary>
        public void* lpSecurityDescriptor;

        /// <summary>
        /// Indicates whether the returned handle is inherited when a
        /// new process is created. If this member is <see langword="true" />,
        /// the new process inherits the handle.
        /// </summary>
        public int bInheritHandle;
    }
}
