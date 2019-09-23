using System;
using System.Diagnostics;
using ClrDebug.Native;

namespace ClrDebug
{
    public class DebugEngine : IDisposable
    {
        private bool _isDisposed;

        private ManagedDebuggerCallbacks _callbacks;

        private CorDebugProcess _process;

        private DebugEngine()
        {
        }

        public CorDebug CorDebug { get; private set; }

        public CorDebugProcess CorProcess => _process;

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
                CorDebug = ComFactory.Create<CorDebug>(ppCordb),
            };

            engine.CorDebug.Initialize().MaybeThrowHr();
            engine.CorDebug.SetManagedHandler(engine._callbacks.CallbacksPointer).MaybeThrowHr();
            engine.CorDebug.DebugActiveProcess(
                unchecked((uint)processId),
                win32Attach: false,
                out engine._process)
                .MaybeThrowHr();

            engine._callbacks.Control = engine.CorProcess;

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

            CorDebug?.Dispose();
            CorProcess?.Dispose();
            _callbacks?.Dispose();
            CorDebug = null;
            _process = null;
            _callbacks = null;

            _isDisposed = true;
        }
    }
}
