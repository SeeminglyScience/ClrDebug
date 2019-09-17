using System;
using System.Runtime.InteropServices;

namespace ClrDebug
{
    internal static class MscordbiShim
    {
        private static readonly IPlatformSpecificOps s_platform;

        static MscordbiShim()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (RuntimeInformation.FrameworkDescription.Contains("Core"))
                {
                    s_platform = new WindowsCoreOps();
                    return;
                }

                s_platform = new WindowsDesktopOps();
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                s_platform = new MacOps();
                return;
            }

            s_platform = new UnixOps();
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

        private class UnixOps : IPlatformSpecificOps
        {
            public string ClrModuleName => "libcoreclr.so";

            public unsafe void CreateCordbObject(int iDebuggerVersion, uint pid, void* hmodTargetCLR, void*** ppCordb)
            {
                NonWindowsInterop.CoreCLRCreateCordbObject(iDebuggerVersion, pid, hmodTargetCLR, ppCordb)
                    .MaybeThrowHr();
            }
        }

        private class MacOps : IPlatformSpecificOps
        {
            public string ClrModuleName => "libcoreclr.dylib";

            public unsafe void CreateCordbObject(int iDebuggerVersion, uint pid, void* hmodTargetCLR, void*** ppCordb)
            {
                NonWindowsInterop.CoreCLRCreateCordbObject(iDebuggerVersion, pid, hmodTargetCLR, ppCordb)
                    .MaybeThrowHr();
            }
        }

        private class WindowsDesktopOps : IPlatformSpecificOps
        {
            public string ClrModuleName => "clr.dll";

            public unsafe void CreateCordbObject(int iDebuggerVersion, uint pid, void* hmodTargetCLR, void*** ppCordb)
            {
                WindowsDesktopInterop.CreateCordbObject(
                    Math.Min(iDebuggerVersion, 4),
                    ppCordb)
                    .MaybeThrowHr();
            }
        }

        private class WindowsCoreOps : IPlatformSpecificOps
        {
            public string ClrModuleName => "coreclr.dll";

            public unsafe void CreateCordbObject(int iDebuggerVersion, uint pid, void* hmodTargetCLR, void*** ppCordb)
            {
                WindowsCoreInterop.CoreCLRCreateCordbObject(iDebuggerVersion, pid, hmodTargetCLR, ppCordb)
                    .MaybeThrowHr();
            }
        }

        private static class WindowsDesktopInterop
        {
            [DllImport("mscordbi", SetLastError = true)]
            public static extern unsafe int CreateCordbObject(
                int iDebuggerVersion,
                void*** ppCordb);
        }

        private static class WindowsCoreInterop
        {
            [DllImport("mscordbi", SetLastError = true)]
            public static extern unsafe int CoreCLRCreateCordbObject(
                int iDebuggerVersion,
                uint pid,
                void* hmodTargetCLR,
                void*** ppCordb);
        }

        private static class NonWindowsInterop
        {
            [DllImport("libmscordbi", SetLastError = true)]
            public static extern unsafe int CoreCLRCreateCordbObject(
                int iDebuggerVersion,
                uint pid,
                void* hmodTargetCLR,
                void*** ppCordb);
        }
    }
}
