using System;
using System.Diagnostics;
using ClrDebug.Native;

namespace ClrDebug
{
    public class DebugEngine : IDisposable
    {
        private CorDebug _debug;

        private CorDebugProcess _process;

        private bool _isDisposed;

        private ManagedDebuggerCallbacks _callbacks;

        private DebugEngine()
        {
        }

        ~DebugEngine() => Dispose(false);

        public static unsafe DebugEngine Attach(int processId)
        {
            if (processId == Process.GetCurrentProcess().Id)
            {
                throw new ArgumentException(
                    "Cannot debug the current process.",
                    nameof(processId));
            }

            Process process = Process.GetProcessById(processId);
            IntPtr hModule = GetClrModule(process);
            void** ppCordb = default;

            MscordbiShim.CreateCordbObject(
                iDebuggerVersion: 5,
                unchecked((uint)processId),
                hModule.ToPointer(),
                &ppCordb);

            if (ppCordb == default)
            {
                throw new InvalidOperationException();
            }

            var engine = new DebugEngine
            {
                _callbacks = new LoggerManagedCallbacks(),
                _debug = ComFactory.Create<CorDebug>(ppCordb),
            };

            engine._debug.Initialize().MaybeThrowHr();
            engine._debug.SetManagedHandler(engine._callbacks.CallbacksPointer).MaybeThrowHr();
            engine._debug.DebugActiveProcess(
                unchecked((uint)processId),
                win32Attach: false,
                out engine._process)
                .MaybeThrowHr();

            engine._callbacks.Control = engine._process;

            return engine;
        }

        private static IntPtr GetClrModule(Process process)
        {
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.Equals(MscordbiShim.ClrModuleName, StringComparison.Ordinal))
                {
                    return module.BaseAddress;
                }
            }

            throw new ArgumentException(
                string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    @"CLR module (""{0}"") was not found in the specified process.",
                    MscordbiShim.ClrModuleName),
                nameof(process));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
            }

            _debug?.Dispose();
            _process?.Dispose();
            _callbacks?.Dispose();
            _debug = null;
            _process = null;
            _callbacks = null;

            _isDisposed = true;
        }
    }
}
