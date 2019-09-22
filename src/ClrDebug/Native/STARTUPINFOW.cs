using System.Runtime.InteropServices;

namespace ClrDebug.Native
{
    /// <summary>
    /// Specifies the window station, desktop, standard handles,
    /// and appearance of the main window for a process at creation
    /// time.
    /// </summary>
    /// <remarks>
    /// For graphical user interface (GUI) processes, this information
    /// affects the first window created by the <c>CreateWindow</c>
    /// function and shown by the <c>ShowWindow</c> function. For console
    /// processes, this information affects the console window if a new
    /// console is created for the process. A process can use the <c>GetStartupInfo</c>
    /// function to retrieve the <see cref="STARTUPINFOW" /> structure
    /// specified when the process was created.
    ///
    /// If a GUI process is being started and neither <c>STARTF_FORCEONFEEDBACK</c>
    /// or <c>STARTF_FORCEOFFFEEDBACK</c> is specified, the process feedback
    /// cursor is used. A GUI process is one whose subsystem is specified
    /// as "windows."
    ///
    /// If a process is launched from the taskbar or jump list, the system
    /// sets <c>hStdOutput</c> to a handle to the monitor that contains the
    /// taskbar or jump list used to launch the process. To retrieve this handle,
    /// use <c>GetStartupInfo</c> to retrieve the <see cref="STARTUPINFOW" /> structure
    /// and check that <c>hStdOutput</c> is set. If so, use <c>GetMonitorInfo</c> to
    /// check whether <c>hStdOutput</c> is a valid monitor handle (<c>HMONITOR</c>).
    /// The process can then use the handle to position its windows.
    ///
    /// If the <c>STARTF_UNTRUSTEDSOURCE</c> flag is set in the in the <see cref="STARTUPINFOW" />
    /// structure returned by the <c>GetStartupInfo</c> function, then applications
    /// should be aware that the command line is untrusted. If this flag is set,
    /// applications should disable potentially dangerous features such as macros,
    /// downloaded content, and automatic printing. This flag is optional.
    /// Applications that call <c>CreateProcess</c> are encouraged to set this flag
    /// when launching a program with a untrusted command line so that the created
    /// process can apply appropriate policy.
    ///
    /// The <c>STARTF_UNTRUSTEDSOURCE</c> flag is supported starting in Windows Vista,
    /// but it is not defined in the SDK header files prior to the Windows 10 SDK. To
    /// use the flag in versions prior to Windows 10, you can define it manually in
    /// your program.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct STARTUPINFOW
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        public ulong cb;

        /// <summary>
        /// Reserved; must be <see langword="null" />.
        /// </summary>
        public char* lpReserved;

        /// <summary>
        /// The name of the desktop, or the name of both the desktop and window
        /// station for this process. A backslash in the string indicates that the
        /// string includes both the desktop and window station names.
        /// </summary>
        public char* lpDesktop;

        /// <summary>
        /// For console processes, this is the title displayed in the title bar if
        /// a new console window is created. If NULL, the name of the executable
        /// file is used as the window title instead. This parameter must be <see langword="null" />
        /// for GUI or console processes that do not create a new console window.
        /// </summary>
        public char* lpTitle;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USEPOSITION</c>, this member is the
        /// x offset of the upper left corner of a window if a new window is created,
        /// in pixels. Otherwise, this member is ignored.
        ///
        /// The offset is from the upper left corner of the screen. For GUI processes,
        /// the specified position is used the first time the new process calls <c>CreateWindow</c>
        /// to create an overlapped window if the <c>x</c> parameter of <c>CreateWindow</c>
        /// is <c>CW_USEDEFAULT</c>.
        /// </summary>
        public ulong dwX;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USEPOSITION</c>, this member is the
        /// y offset of the upper left corner of a window if a new window is created,
        /// in pixels. Otherwise, this member is ignored.
        ///
        /// The offset is from the upper left corner of the screen. For GUI processes,
        /// the specified position is used the first time the new process calls <c>CreateWindow</c>
        /// to create an overlapped window if the <c>y</c> parameter of <c>CreateWindow</c>
        /// is <c>CW_USEDEFAULT</c>.
        /// </summary>
        public ulong dwY;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USESIZE</c>, this member is the width
        /// of the window if a new window is created, in pixels. Otherwise, this member
        /// is ignored.
        ///
        /// For GUI processes, this is used only the first time the new process calls
        /// <c>CreateWindow</c> to create an overlapped window if the <c>nWidth</c> parameter
        /// of <c>CreateWindow</c> is <c>CW_USEDEFAULT</c>.
        /// </summary>
        public ulong dwXSize;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USESIZE</c>, this member is the height
        /// of the window if a new window is created, in pixels. Otherwise, this member
        /// is ignored.
        ///
        /// For GUI processes, this is used only the first time the new process calls
        /// <c>CreateWindow</c> to create an overlapped window if the <c>nHeight</c> parameter
        /// of <c>CreateWindow</c> is <c>CW_USEDEFAULT</c>.
        /// </summary>
        public ulong dwYSize;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USECOUNTCHARS</c>, if a new console
        /// window is created in a console process, this member specifies the screen buffer
        /// width, in character columns. Otherwise, this member is ignored.
        /// </summary>
        public ulong dwXCountChars;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USECOUNTCHARS</c>, if a new console
        /// window is created in a console process, this member specifies the screen buffer
        /// height, in character rows. Otherwise, this member is ignored.
        /// </summary>
        public ulong dwYCountChars;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USEFILLATTRIBUTE</c>, this member is the
        /// initial text and background colors if a new console window is created in a
        /// console application. Otherwise, this member is ignored.
        ///
        /// This value can be any combination of the following values:
        ///
        /// <c>FOREGROUND_BLUE</c>, <c>FOREGROUND_GREEN</c>, <c>FOREGROUND_RED</c>,
        /// <c>FOREGROUND_INTENSITY</c>, <c>BACKGROUND_BLUE</c>, <c>BACKGROUND_GREEN</c>,
        /// <c>BACKGROUND_RED</c>, and <c>BACKGROUND_INTENSITY</c>.
        ///
        /// For example, the following combination of values produces red text on a
        /// white background:
        ///
        /// <c>FOREGROUND_RED | BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_BLUE</c>
        /// </summary>
        public ulong dwFillAttribute;

        /// <summary>
        /// A bitfield that determines whether certain members are used when the process
        /// creates a window. This member can be one or more of the following values.
        ///
        /// STARTF_FORCEONFEEDBACK (0x00000040)
        /// Indicates that the cursor is in feedback mode for two seconds after <c>CreateProcess</c>
        /// is called. The Working in Background cursor is displayed (see the Pointers
        /// tab in the Mouse control panel utility).
        ///
        /// If during those two seconds the process makes the first GUI call, the system
        /// gives five more seconds to the process. If during those five seconds the process
        /// shows a window, the system gives five more seconds to the process to finish drawing
        /// the window.
        ///
        /// The system turns the feedback cursor off after the first call to <c>GetMessage</c>,
        /// regardless of whether the process is drawing.
        ///
        /// STARTF_FORCEOFFFEEDBACK (0x00000080)
        /// Indicates that the feedback cursor is forced off while the process is starting.
        /// The Normal Select cursor is displayed.
        ///
        /// STARTF_PREVENTPINNING (0x00002000)
        /// Indicates that any windows created by the process cannot be pinned on the taskbar.
        ///
        /// This flag must be combined with STARTF_TITLEISAPPID.
        ///
        /// STARTF_RUNFULLSCREEN (0x00000020)
        /// Indicates that the process should be run in full-screen mode, rather than
        /// in windowed mode.
        ///
        /// This flag is only valid for console applications running on an x86 computer.
        ///
        /// STARTF_TITLEISAPPID (0x00001000)
        /// The lpTitle member contains an AppUserModelID. This identifier controls how the
        /// taskbar and Start menu present the application, and enables it to be associated
        /// with the correct shortcuts and Jump Lists. Generally, applications will use the
        /// <c>SetCurrentProcessExplicitAppUserModelID</c> and <c>GetCurrentProcessExplicitAppUserModelID</c>
        /// functions instead of setting this flag. For more information, see Application
        /// User Model IDs.
        ///
        /// If <c>STARTF_PREVENTPINNING</c> is used, application windows cannot be pinned on
        /// the taskbar. The use of any AppUserModelID-related window properties by the application
        /// overrides this setting for that window only.
        ///
        /// This flag cannot be used with <c>STARTF_TITLEISLINKNAME</c>.
        ///
        /// STARTF_TITLEISLINKNAME (0x00000800)
        /// The <c>lpTitle</c> member contains the path of the shortcut file (.lnk) that the
        /// user invoked to start this process. This is typically set by the shell when a .lnk
        /// file pointing to the launched application is invoked. Most applications will not
        /// need to set this value.
        ///
        /// This flag cannot be used with <c>STARTF_TITLEISAPPID</c>.
        ///
        /// STARTF_UNTRUSTEDSOURCE (0x00008000)
        /// The command line came from an untrusted source.
        ///
        /// STARTF_USECOUNTCHARS (0x00000008)
        /// The <c>dwXCountChars</c> and <c>dwYCountChars</c> members contain additional
        /// information.
        ///
        /// STARTF_USEFILLATTRIBUTE (0x00000010)
        /// The <c>dwFillAttribute</c> member contains additional information.
        ///
        /// STARTF_USEHOTKEY (0x00000200)
        /// The <c>hStdInput</c> member contains additional information.
        ///
        /// This flag cannot be used with <c>STARTF_USESTDHANDLES</c>.
        ///
        /// STARTF_USEPOSITION (0x00000004)
        /// The <c>dwX</c> and <c>dwY</c> members contain additional information.
        ///
        /// STARTF_USESHOWWINDOW (0x00000001)
        /// The <c>wShowWindow</c> member contains additional information.
        ///
        /// STARTF_USESIZE (0x00000002)
        /// The <c>dwXSize</c> and <c>dwYSize</c> members contain additional information.
        ///
        /// STARTF_USESTDHANDLES (0x00000100)
        /// The <c>hStdInput</c>, <c>hStdOutput</c>, and <c>hStdError</c> members contain
        /// additional information.
        ///
        /// If this flag is specified when calling one of the process creation functions,
        /// the handles must be inheritable and the function's bInheritHandles parameter must
        /// be set to <see langword="true" />.
        ///
        /// If this flag is specified when calling the <c>GetStartupInfo</c> function, these
        /// members are either the handle value specified during process creation or
        /// <c>INVALID_HANDLE_VALUE</c>.
        ///
        /// Handles must be closed with <c>CloseHandle</c> when they are no longer needed.
        ///
        /// This flag cannot be used with <c>STARTF_USEHOTKEY</c>.
        /// </summary>
        public ulong dwFlags;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USESHOWWINDOW</c>, this member can be any
        /// of the values that can be specified in the <c>nCmdShow</c> parameter for the <c>ShowWindow</c>
        /// function, except for <c>SW_SHOWDEFAULT</c>. Otherwise, this member is ignored.
        ///
        /// For GUI processes, the first time <c>ShowWindow</c> is called, its <c>nCmdShow</c>
        /// parameter is ignored <c>wShowWindow</c> specifies the default value. In subsequent
        /// calls to <c>ShowWindow</c>, the <c>wShowWindow</c> member is used if the <c>nCmdShow</c>
        /// parameter of <c>ShowWindow</c> is set to <c>SW_SHOWDEFAULT</c>.
        /// </summary>
        public ushort wShowWindow;

        /// <summary>
        /// Reserved for use by the C Run-time; must be zero.
        /// </summary>
        public ushort cbReserved2;

        /// <summary>
        /// Reserved for use by the C Run-time; must be <see langword="null" />.
        /// </summary>
        public void* lpReserved2;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USESTDHANDLES</c>, this member is the
        /// standard input handle for the process. If <c>STARTF_USESTDHANDLES</c> is not
        /// specified, the default for standard input is the keyboard buffer.
        ///
        /// If <c>dwFlags</c> specifies <c>STARTF_USEHOTKEY</c>, this member specifies a
        /// hotkey value that is sent as the <c>wParam</c> parameter of a <c>WM_SETHOTKEY</c>
        /// message to the first eligible top-level window created by the application that
        /// owns the process. If the window is created with the <c>WS_POPUP</c> window style,
        /// it is not eligible unless the <c>WS_EX_APPWINDOW</c> extended window style is also
        /// set.
        ///
        /// Otherwise, this member is ignored.
        /// </summary>
        public void* hStdInput;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USESTDHANDLES</c>, this member is the
        /// standard output handle for the process. Otherwise, this member is ignored and
        /// the default for standard output is the console window's buffer.
        ///
        /// If a process is launched from the taskbar or jump list, the system sets <c>hStdOutput</c>
        /// to a handle to the monitor that contains the taskbar or jump list used to
        /// launch the process.
        /// </summary>
        public void* hStdOutput;

        /// <summary>
        /// If <c>dwFlags</c> specifies <c>STARTF_USESTDHANDLES</c>, this member is the
        /// standard error handle for the process. Otherwise, this member is ignored and
        /// the default for standard error is the console window's buffer.
        /// </summary>
        public void* hStdError;
    }
}
