using System;
using System.Runtime.InteropServices;
using System.Threading;
using ClrDebug.Native;

namespace ClrDebug
{
    public abstract class ManagedDebuggerCallbacks : IDisposable
    {
        private unsafe delegate int QueryInterfaceDelegate(void* @this, Guid riid, void** ppvObject);

        private unsafe delegate int AddRefDelegate(void* @this);

        private unsafe delegate int ReleaseDelegate(void* @this);

        private unsafe delegate int PointerDelegate1(void* @this, void* arg0);

        private unsafe delegate int PointerDelegate2(void* @this, void* arg0, void* arg1);

        private unsafe delegate int PointerDelegate3(void* @this, void* arg0, void* arg1, void* arg2);

        private unsafe delegate int OnStepCompleteDelegate(void* @this, void* arg0, void* arg1, void* arg2, int arg3);

        private unsafe delegate int OnExceptionDelegate(void* @this, void* pAppDomain, void* pThread, bool unhandled);

        private unsafe delegate int OnDebuggerErrorDelegate(void* @this, void* pProcess, int errorHR, uint errorCode);

        private unsafe delegate int OnLogSwitchDelegate(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int lLevel,
            uint ulReason,
            char* pLogSwitchName,
            char* pParentName);

        private unsafe delegate int OnLogMessageDelegate(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int lLevel,
            char* pLogSwitchName,
            char* pMessage);

        private unsafe delegate int OnEditAndContinueRemapDelegate(void* @this, void* pAppDomain, void* pThread, void* pFunction, bool fAccurate);

        private unsafe delegate int OnBreakpointSetErrorDelegate(void* @this, void* pAppDomain, void* pThread, void* pBreakpoint, uint dwError);

        private unsafe delegate int OnFunctionRemapOpportunityDelegate(void* @this, void* pAppDomain, void* pThread, void* pOldFunction, void* pNewFunction, uint oldILOffset);

        private unsafe delegate int OnCreateConnectionDelegate(void* @this, void* pProcess, uint dwConnectionId, char* pConnName);

        private unsafe delegate int OnChangeConnectionDelegate(void* @this, void* pProcess, uint dwConnectionId);

        private unsafe delegate int OnDestroyConnectionDelegate(void* @this, void* pProcess, uint dwConnectionId);

        private unsafe delegate int OnException2Delegate(void* @this, void* pAppDomain, void* pThread, void* pFrame, uint nOffset, int dwEventType, uint dwFlags);

        private unsafe delegate int OnExceptionUnwindDelegate(void* @this, void* pAppDomain, void* pThread, int dwEventType, uint dwFlags);

        private bool _isDisposed = false;

        private unsafe CorDebugManagedCallback* _callbacks;

        private unsafe CorDebugManagedCallback2* _callbacks2;

        private readonly Delegate _queryInterface;

        private readonly Delegate _addRef;

        private readonly Delegate _release;

        private readonly Delegate _breakpoint;

        private readonly Delegate _stepComplete;

        private readonly Delegate _break;

        private readonly Delegate _exception;

        private readonly Delegate _evalComplete;

        private readonly Delegate _evalException;

        private readonly Delegate _createProcess;

        private readonly Delegate _exitProcess;

        private readonly Delegate _createThread;

        private readonly Delegate _exitThread;

        private readonly Delegate _loadModule;

        private readonly Delegate _unloadModule;

        private readonly Delegate _loadClass;

        private readonly Delegate _unloadClass;

        private readonly Delegate _debuggerError;

        private readonly Delegate _logMessage;

        private readonly Delegate _logSwitch;

        private readonly Delegate _createAppDomain;

        private readonly Delegate _exitAppDomain;

        private readonly Delegate _loadAssembly;

        private readonly Delegate _unloadAssembly;

        private readonly Delegate _controlCTrap;

        private readonly Delegate _nameChange;

        private readonly Delegate _updateModuleSymbols;

        private readonly Delegate _editAndContinueRemap;

        private readonly Delegate _breakpointSetError;

        private readonly Delegate _functionRemapOpportunity;

        private readonly Delegate _createConnection;

        private readonly Delegate _changeConnection;

        private readonly Delegate _destroyConnection;

        private readonly Delegate _exception2;

        private readonly Delegate _exceptionUnwind;

        private readonly Delegate _functionRemapComplete;

        private readonly Delegate _mDANotification;

        private int _refCount;

        internal unsafe ManagedDebuggerCallbacks()
        {
            _queryInterface = new QueryInterfaceDelegate(QueryInterface);
            _callbacks = CorDebugManagedCallback.Alloc();
            ICorDebugManagedCallbackVtable* vtable = _callbacks->Vtable;
            vtable->QueryInterface = (void*)Marshal.GetFunctionPointerForDelegate(_queryInterface);

            _addRef = new AddRefDelegate(AddRef);
            vtable->AddRef = (void*)Marshal.GetFunctionPointerForDelegate(_addRef);

            _release = new ReleaseDelegate(Release);
            vtable->Release = (void*)Marshal.GetFunctionPointerForDelegate(_release);

            _breakpoint = new PointerDelegate3(OnBreakpoint);
            vtable->Breakpoint = (void*)Marshal.GetFunctionPointerForDelegate(_breakpoint);

            _stepComplete = new OnStepCompleteDelegate(OnStepComplete);
            vtable->StepComplete = (void*)Marshal.GetFunctionPointerForDelegate(_stepComplete);

            _break = new PointerDelegate2(OnBreak);
            vtable->Break = (void*)Marshal.GetFunctionPointerForDelegate(_break);

            _exception = new OnExceptionDelegate(OnException);
            vtable->Exception = (void*)Marshal.GetFunctionPointerForDelegate(_exception);

            _evalComplete = new PointerDelegate3(OnEvalComplete);
            vtable->EvalComplete = (void*)Marshal.GetFunctionPointerForDelegate(_evalComplete);

            _evalException = new PointerDelegate3(OnEvalException);
            vtable->EvalException = (void*)Marshal.GetFunctionPointerForDelegate(_evalException);

            _createProcess = new PointerDelegate1(OnCreateProcess);
            vtable->CreateProcess = (void*)Marshal.GetFunctionPointerForDelegate(_createProcess);

            _exitProcess = new PointerDelegate1(OnExitProcess);
            vtable->ExitProcess = (void*)Marshal.GetFunctionPointerForDelegate(_exitProcess);

            _createThread = new PointerDelegate2(OnCreateThread);
            vtable->CreateThread = (void*)Marshal.GetFunctionPointerForDelegate(_createThread);

            _exitThread = new PointerDelegate2(OnExitThread);
            vtable->ExitThread = (void*)Marshal.GetFunctionPointerForDelegate(_exitThread);

            _loadModule = new PointerDelegate2(OnLoadModule);
            vtable->LoadModule = (void*)Marshal.GetFunctionPointerForDelegate(_loadModule);

            _unloadModule = new PointerDelegate2(OnUnloadModule);
            vtable->UnloadModule = (void*)Marshal.GetFunctionPointerForDelegate(_unloadModule);

            _loadClass = new PointerDelegate2(OnLoadClass);
            vtable->LoadClass = (void*)Marshal.GetFunctionPointerForDelegate(_loadClass);

            _unloadClass = new PointerDelegate2(OnUnloadClass);
            vtable->UnloadClass = (void*)Marshal.GetFunctionPointerForDelegate(_unloadClass);

            _debuggerError = new OnDebuggerErrorDelegate(OnDebuggerError);
            vtable->DebuggerError = (void*)Marshal.GetFunctionPointerForDelegate(_debuggerError);

            _logMessage = new OnLogMessageDelegate(OnLogMessage);
            vtable->LogMessage = (void*)Marshal.GetFunctionPointerForDelegate(_logMessage);

            _logSwitch = new OnLogSwitchDelegate(OnLogSwitch);
            vtable->LogSwitch = (void*)Marshal.GetFunctionPointerForDelegate(_logSwitch);

            _createAppDomain = new PointerDelegate2(OnCreateAppDomain);
            vtable->CreateAppDomain = (void*)Marshal.GetFunctionPointerForDelegate(_createAppDomain);

            _exitAppDomain = new PointerDelegate2(OnExitAppDomain);
            vtable->ExitAppDomain = (void*)Marshal.GetFunctionPointerForDelegate(_exitAppDomain);

            _loadAssembly = new PointerDelegate2(OnLoadAssembly);
            vtable->LoadAssembly = (void*)Marshal.GetFunctionPointerForDelegate(_loadAssembly);

            _unloadAssembly = new PointerDelegate2(OnUnloadAssembly);
            vtable->UnloadAssembly = (void*)Marshal.GetFunctionPointerForDelegate(_unloadAssembly);

            _controlCTrap = new PointerDelegate1(OnControlCTrap);
            vtable->ControlCTrap = (void*)Marshal.GetFunctionPointerForDelegate(_controlCTrap);

            _nameChange = new PointerDelegate2(OnNameChange);
            vtable->NameChange = (void*)Marshal.GetFunctionPointerForDelegate(_nameChange);

            _updateModuleSymbols = new PointerDelegate3(OnUpdateModuleSymbols);
            vtable->UpdateModuleSymbols = (void*)Marshal.GetFunctionPointerForDelegate(_updateModuleSymbols);

            _editAndContinueRemap = new OnEditAndContinueRemapDelegate(OnEditAndContinueRemap);
            vtable->EditAndContinueRemap = (void*)Marshal.GetFunctionPointerForDelegate(_editAndContinueRemap);

            _breakpointSetError = new OnBreakpointSetErrorDelegate(OnBreakpointSetError);
            vtable->BreakpointSetError = (void*)Marshal.GetFunctionPointerForDelegate(_breakpointSetError);

            _callbacks2 = CorDebugManagedCallback2.Alloc();
            ICorDebugManagedCallback2Vtable* vtable2 = _callbacks2->Vtable;
            vtable2->QueryInterface = vtable->QueryInterface;
            vtable2->AddRef = vtable->AddRef;
            vtable2->Release = vtable->Release;

            _functionRemapOpportunity = new OnFunctionRemapOpportunityDelegate(OnFunctionRemapOpportunity);
            vtable2->FunctionRemapOpportunity = (void*)Marshal.GetFunctionPointerForDelegate(_functionRemapOpportunity);

            _createConnection = new OnCreateConnectionDelegate(OnCreateConnection);
            vtable2->CreateConnection = (void*)Marshal.GetFunctionPointerForDelegate(_createConnection);

            _changeConnection = new OnChangeConnectionDelegate(OnChangeConnection);
            vtable2->ChangeConnection = (void*)Marshal.GetFunctionPointerForDelegate(_changeConnection);

            _destroyConnection = new OnDestroyConnectionDelegate(OnDestroyConnection);
            vtable2->DestroyConnection = (void*)Marshal.GetFunctionPointerForDelegate(_destroyConnection);

            _exception2 = new OnException2Delegate(OnException2);
            vtable2->Exception = (void*)Marshal.GetFunctionPointerForDelegate(_exception2);

            _exceptionUnwind = new OnExceptionUnwindDelegate(OnExceptionUnwind);
            vtable2->ExceptionUnwind = (void*)Marshal.GetFunctionPointerForDelegate(_exceptionUnwind);

            _functionRemapComplete = new PointerDelegate3(OnFunctionRemapComplete);
            vtable2->FunctionRemapComplete = (void*)Marshal.GetFunctionPointerForDelegate(_functionRemapComplete);

            _mDANotification = new PointerDelegate3(OnMDANotification);
            vtable2->MDANotification = (void*)Marshal.GetFunctionPointerForDelegate(_mDANotification);
        }

        ~ManagedDebuggerCallbacks() => Dispose(false);

        protected internal CorDebugController Control { get; internal set; }

        internal unsafe CorDebugManagedCallback* CallbacksPointer => _callbacks;

        internal unsafe CorDebugManagedCallback2* Callbacks2Pointer => _callbacks2;

        public static ManagedDebuggerCallbacks CreateDefault() => new DefaultManagedCallbacks();

        public static ManagedDebuggerCallbacks CreateEventBased() => new EventManagedCallbacks();

        public virtual unsafe int QueryInterface(void* @this, Guid riid, void** ppvObject)
        {
            if (ppvObject == default)
            {
                return CorError.E_POINTER;
            }

            if (riid == CorGuids.IUnknown)
            {
                *ppvObject = _callbacks;
                return 0;
            }

            if (riid == CorGuids.ICorDebugManagedCallback)
            {
                *ppvObject = _callbacks;
                return 0;
            }

            if (riid == CorGuids.ICorDebugManagedCallback2)
            {
                *ppvObject = _callbacks2;
                return 0;
            }

            return CorError.E_NOINTERFACE;
        }

        public virtual unsafe int AddRef(void* @this) => Interlocked.Increment(ref _refCount);

        public virtual unsafe int Release(void* @this) => Interlocked.Decrement(ref _refCount);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual unsafe void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
            }

            CorDebugManagedCallback.Free(_callbacks);
            _callbacks = default;
            CorDebugManagedCallback2.Free(_callbacks2);
            _callbacks2 = default;
            _isDisposed = true;
        }

        protected abstract unsafe int OnBreakpoint(void* @this, void* pAppDomain, void* pThread, void* pBreakpoint);

        protected abstract unsafe int OnStepComplete(void* @this, void* pAppDomain, void* pThread, void* pStepper, int reason);

        protected abstract unsafe int OnBreak(void* @this, void* pAppDomain, void* thread);

        protected abstract unsafe int OnException(void* @this, void* pAppDomain, void* pThread, bool unhandled);

        protected abstract unsafe int OnEvalComplete(void* @this, void* pAppDomain, void* pThread, void* pEval);

        protected abstract unsafe int OnEvalException(void* @this, void* pAppDomain, void* pThread, void* pEval);

        protected abstract unsafe int OnCreateProcess(void* @this, void* pProcess);

        protected abstract unsafe int OnExitProcess(void* @this, void* pProcess);

        protected abstract unsafe int OnCreateThread(void* @this, void* pAppDomain, void* thread);

        protected abstract unsafe int OnExitThread(void* @this, void* pAppDomain, void* thread);

        protected abstract unsafe int OnLoadModule(void* @this, void* pAppDomain, void* pModule);

        protected abstract unsafe int OnUnloadModule(void* @this, void* pAppDomain, void* pModule);

        protected abstract unsafe int OnLoadClass(void* @this, void* pAppDomain, void* c);

        protected abstract unsafe int OnUnloadClass(void* @this, void* pAppDomain, void* c);

        protected abstract unsafe int OnDebuggerError(void* @this, void* pProcess, int errorHR, uint errorCode);

        protected abstract unsafe int OnLogMessage(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int lLevel,
            char* pLogSwitchName,
            char* pMessage);

        protected abstract unsafe int OnLogSwitch(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int lLevel,
            uint ulReason,
            char* pLogSwitchName,
            char* pParentName);

        protected abstract unsafe int OnCreateAppDomain(void* @this, void* pProcess, void* pAppDomain);

        protected abstract unsafe int OnExitAppDomain(void* @this, void* pProcess, void* pAppDomain);

        protected abstract unsafe int OnLoadAssembly(void* @this, void* pAppDomain, void* pAssembly);

        protected abstract unsafe int OnUnloadAssembly(void* @this, void* pAppDomain, void* pAssembly);

        protected abstract unsafe int OnControlCTrap(void* @this, void* pProcess);

        protected abstract unsafe int OnNameChange(void* @this, void* pAppDomain, void* pThread);

        protected abstract unsafe int OnUpdateModuleSymbols(void* @this, void* pAppDomain, void* pModule, void* pSymbolStream);

        protected abstract unsafe int OnEditAndContinueRemap(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFunction,
            bool fAccurate);

        protected abstract unsafe int OnBreakpointSetError(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pBreakpoint,
            uint dwError);

        protected abstract unsafe int OnFunctionRemapOpportunity(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pOldFunction,
            void* pNewFunction,
            uint oldILOffset);

        protected abstract unsafe int OnCreateConnection(
            void* @this,
            void* pProcess,
            uint dwConnectionId,
            char* pConnName);

        protected abstract unsafe int OnChangeConnection(
            void* @this,
            void* pProcess,
            uint dwConnectionId);

        protected abstract unsafe int OnDestroyConnection(
            void* @this,
            void* pProcess,
            uint dwConnectionId);

        protected abstract unsafe int OnException2(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFrame,
            uint nOffset,
            int dwEventType,
            uint dwFlags);

        protected abstract unsafe int OnExceptionUnwind(
            void* @this,
            void* pAppDomain,
            void* pThread,
            int dwEventType,
            uint dwFlags);

        protected abstract unsafe int OnFunctionRemapComplete(
            void* @this,
            void* pAppDomain,
            void* pThread,
            void* pFunction);

        protected abstract unsafe int OnMDANotification(
            void* @this,
            void* pController,
            void* pThread,
            void* pMDA);

        protected unsafe int Continue()
        {
            return Control == null || Control.IsDefault
                ? 0
                : Control.Continue(fIsOutOfBand: false);
        }

        protected unsafe void AssertNotDisposed()
        {
            if (!_isDisposed)
            {
                return;
            }

            throw new ObjectDisposedException(null);
        }
    }
}
