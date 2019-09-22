using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace ClrDebug
{
    internal static class MscordbiShim
    {
        private unsafe delegate int CoreCLRCreateCordbObjectInvoker(
                int iDebuggerVersion,
                uint pid,
                void* hmodTargetCLR,
                void*** ppCordb);

        private unsafe delegate int CreateCordbObjectInvoker(int iDebuggerVersion, void*** ppCordb);

        private const string CoreCLRCreateCordbObjectName = "CoreCLRCreateCordbObject";

        private const string CreateCordbObjectName = "CreateCordbObject";

        private static readonly IPlatformSpecificOps s_platform;

        static MscordbiShim()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (RuntimeInformation.FrameworkDescription.Contains("Core"))
                {
                    s_platform = new WindowsCorePlatform();
                    return;
                }

                s_platform = new WindowsDesktopPlatform();
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                s_platform = new MacPlatform();
                return;
            }

            s_platform = new UnixPlatform();
        }

        public static string ClrModuleName => s_platform.ClrModuleName;

        public static unsafe void CreateCordbObject(
            int iDebuggerVersion,
            uint pid,
            void* hmodTargetCLR,
            void*** ppCordb)
        {
            if (ppCordb == null)
            {
                throw new ArgumentNullException(nameof(ppCordb));
            }

            s_platform.CreateCordbObject(iDebuggerVersion, pid, hmodTargetCLR, ppCordb);
        }

        private interface IPlatformSpecificOps
        {
            string ClrModuleName { get; }

            unsafe void CreateCordbObject(
                int iDebuggerVersion,
                uint pid,
                void* hmodTargetCLR,
                void*** ppCordb);
        }

        private abstract class PlatformBase<TInvoker> : IPlatformSpecificOps where TInvoker : Delegate
        {
            protected readonly TInvoker Invoker;

            public PlatformBase()
            {
                string libPath = Path.Combine(
                    Path.GetDirectoryName(typeof(int).Assembly.Location),
                    DllName);

                if (!File.Exists(libPath))
                {
                    throw new InvalidOperationException("Could not find mscordbi library.");
                }

                IntPtr module = GetLibrary(libPath);
                if (module == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                IntPtr functionPtr = GetFunctionPointer(
                    module,
                    FunctionName);

                if (functionPtr == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                Invoker = Marshal.GetDelegateForFunctionPointer<TInvoker>(functionPtr);
            }

            public abstract string ClrModuleName { get; }

            public abstract string FunctionName { get; }

            public abstract string DllName { get; }

            public abstract unsafe void CreateCordbObject(
                int iDebuggerVersion,
                uint pid,
                void* hmodTargetCLR,
                void*** ppCordb);

            public abstract IntPtr GetLibrary(string libraryPath);

            public abstract IntPtr GetFunctionPointer(IntPtr module, string functionName);
        }

        private abstract class WindowsPlatform<TInvoker> : PlatformBase<TInvoker>
            where TInvoker : Delegate
        {
            public override string DllName => "mscordbi.dll";

            public override IntPtr GetFunctionPointer(IntPtr module, string functionName)
            {
                return WindowsInterop.GetProcAddress(module, functionName);
            }

            public override IntPtr GetLibrary(string libraryPath)
            {
                return WindowsInterop.LoadLibraryW(libraryPath);
            }
        }

        private class WindowsDesktopPlatform : WindowsPlatform<CreateCordbObjectInvoker>
        {
            public override string ClrModuleName => "clr.dll";

            public override string FunctionName => CreateCordbObjectName;

            public override unsafe void CreateCordbObject(int iDebuggerVersion, uint pid, void* hmodTargetCLR, void*** ppCordb)
            {
                Invoker(Math.Min(iDebuggerVersion, 4), ppCordb).MaybeThrowHr();
            }
        }

        private class WindowsCorePlatform : WindowsPlatform<CoreCLRCreateCordbObjectInvoker>
        {
            public override string ClrModuleName => "coreclr.dll";

            public override string FunctionName => CoreCLRCreateCordbObjectName;

            public override unsafe void CreateCordbObject(
                int iDebuggerVersion,
                uint pid,
                void* hmodTargetCLR,
                void*** ppCordb)
            {
                Invoker(iDebuggerVersion, pid, hmodTargetCLR, ppCordb).MaybeThrowHr();
            }
        }

        private abstract class NonWindowsPlatform : PlatformBase<CoreCLRCreateCordbObjectInvoker>
        {
            public override string FunctionName => CoreCLRCreateCordbObjectName;

            public override unsafe void CreateCordbObject(
                int iDebuggerVersion,
                uint pid,
                void* hmodTargetCLR,
                void*** ppCordb)
            {
                Invoker(iDebuggerVersion, pid, hmodTargetCLR, ppCordb).MaybeThrowHr();
            }

            public override IntPtr GetFunctionPointer(IntPtr module, string functionName)
            {
                return NonWindowsInterop.dlsym(module, functionName);
            }

            public override IntPtr GetLibrary(string libraryPath)
            {
                // Immediate function call binding.
                const int RTLD_NOW = 0x00002;
                return NonWindowsInterop.dlopen(libraryPath, RTLD_NOW);
            }
        }

        private class MacPlatform : NonWindowsPlatform
        {
            public override string ClrModuleName => "libcoreclr.dylib";

            public override string DllName => "libmscordbi.dylib";
        }

        private class UnixPlatform : NonWindowsPlatform
        {
            public override string ClrModuleName => "libcoreclr.so";

            public override string DllName => "libmscordbi.so";
        }

        private static class WindowsInterop
        {
            [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadLibraryW(string lpLibFileName);

            [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
        }

        private static class NonWindowsInterop
        {
            [DllImport("libc", SetLastError = true, CharSet = CharSet.Ansi)]
            public static extern IntPtr dlopen(string file, int mode);

            [DllImport("libc", SetLastError = true, CharSet = CharSet.Ansi)]
            public static extern IntPtr dlsym(IntPtr handle, string name);
        }
    }
}
